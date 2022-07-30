using GbxRemoteNet.XmlRpc.Packets;
using GbxRemoteNet.XmlRpc.Types;
using NLog;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GbxRemoteNet.XmlRpc
{
    public class NadeoXmlRpcClient
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        uint handler = 0x80000000;
        object handlerLock = new();
        ConcurrentDictionary<uint, ManualResetEvent> responseHandles = new();
        ConcurrentDictionary<uint, ResponseMessage> responseMessages = new();

        // connection
        string connectHost;
        int connectPort;
        TcpClient tcpClient;
        XmlRpcIO xmlRpcIO;

        // recvieve
        Task taskRecvLoop;
        CancellationTokenSource recvCancel;

        /// <summary>
        /// Generic action for events.
        /// </summary>
        /// <returns></returns>
        public delegate Task TaskAction();
        /// <summary>
        /// Action for the OnCallback event.
        /// </summary>
        /// <param name="call">Information about the call.</param>
        /// <returns></returns>
        public delegate Task CallbackAction(MethodCall call);

        /// <summary>
        /// Invoked when the client is connected to the XML-RPC server.
        /// </summary>
        public event TaskAction OnConnected;
        /// <summary>
        /// Called when a callback occured from the XML-RPC server.
        /// </summary>
        public event CallbackAction OnCallback;
        /// <summary>
        /// Triggered when the client has been disconnected from the server.
        /// </summary>
        public event TaskAction OnDisconnected;

        public NadeoXmlRpcClient(string host, int port)
        {
            connectHost = host;
            connectPort = port;
        }

        /// <summary>
        /// Handles all responses from the XML-RPC server.
        /// </summary>
        private async void RecvLoop()
        {
            try
            {
                logger.Debug("Recieve loop initiated.");

                while (!recvCancel.IsCancellationRequested)
                {
                    ResponseMessage response = await ResponseMessage.FromIOAsync(xmlRpcIO);

                    logger.Trace("================== MESSAGE START ==================");
                    logger.Trace($"Message length: {response.Header.MessageLength}");
                    logger.Trace($"Handle: {response.Header.Handle}");
                    logger.Trace($"Is callback: {response.Header.IsCallback}");
                    logger.Trace(response.MessageXml);
                    logger.Trace("================== MESSAGE END ==================");

                    if (response.IsCallback)
                    {
                        // invoke listeners and
                        // run callback handler in a new thread to avoid blocking of new responses
                        _ = Task.Run(() => OnCallback?.Invoke(new MethodCall(response)));
                    }
                    else if (responseHandles.ContainsKey(response.Header.Handle))
                    {
                        // attempt to signal the call method
                        responseMessages[response.Header.Handle] = response;
                        responseHandles[response.Header.Handle].Set();
                    }
                }
            }
            catch (Exception e)
            {
                logger.Error(e, $"Receive loop raised an exception: {e.Message}");
                await DisconnectAsync();
            }
        }

        /// <summary>
        /// Connect to the remote XMLRPC server.
        /// </summary>
        /// <param name="retries">Number of times to re-try connection.</param>
        /// <param name="retryTimeout">Number of milliseconds to wait between each re-try.</param>
        /// <returns></returns>
        public async Task<bool> ConnectAsync(int retries = 0, int retryTimeout = 1000)
        {
            logger.Debug("Client connecting to the remote XML-RPC server.");
            var connectAddr = await Dns.GetHostAddressesAsync(connectHost);

            tcpClient = new TcpClient();

            // try to connect
            while (retries >= 0)
            {
                try
                {
                    await tcpClient.ConnectAsync(connectAddr[0], connectPort);

                    if (tcpClient.Connected)
                        break;
                }
                catch (Exception e)
                {
                    logger.Error(e, $"Exception occured when trying to connect to server: {e.Message}");
                }

                logger.Error("Failed to connect to server.");

                retries--;

                if (retries < 0)
                    break;

                await Task.Delay(retryTimeout);
            }

            if (retries < 0)
                return false; // connection failed

            xmlRpcIO = new XmlRpcIO(tcpClient);

            logger.Debug("Client connected to XML-RPC server with IP: {connectAddr}", connectAddr);

            // Cancellation token to cancel task if it takes longer than a second
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.CancelAfter(1000);
            
            // check header
            ConnectHeader header = null;
            try
            {
                header = await ConnectHeader.FromIOAsync(xmlRpcIO, cancellationTokenSource.Token);
            }
            catch (Exception e)
            {
                logger.Error(e, $"Exception occured when trying to get connect header: {e.Message}");
                logger.Error("Failed to get connect header.");
                return false;
            }
            
            if (!header.IsValid)
            {
                logger.Error("Client is using an invalid header protocol: {header.protocol}", header.Protocol);
                throw new Exception($"Invalid protocol: {header.Protocol}");
            }

            recvCancel = new CancellationTokenSource();
            taskRecvLoop = new Task(RecvLoop, recvCancel.Token);
            taskRecvLoop.Start();

            OnConnected?.Invoke();
            return true;
        }

        /// <summary>
        /// Stop the recieve loop and disconnect.
        /// </summary>
        /// <returns></returns>
        public async Task DisconnectAsync()
        {
            logger.Debug("Client is disconnecting from XML-RPC server.");
            try
            {
                recvCancel.Cancel();
                await taskRecvLoop;
                tcpClient.Close();
            }
            catch (Exception e)
            {
                logger.Warn(e, "An exception occured when trying to disconnect: {message}");
            }

            OnDisconnected?.Invoke();

            logger.Debug("Client disconnected from XML-RPC server.");
        }

        /// <summary>
        /// Get the next handler value.
        /// </summary>
        /// <returns>The next handle value.</returns>
        public async Task<uint> GetNextHandle()
        {
            // lock because we may access this in multiple threads
            lock (handlerLock)
            {
                if (handler + 1 == 0xffffffff)
                    handler = 0x80000000;

                logger.Trace("Next handler value: {handler}", handler);

                return handler++;
            }
        }

        /// <summary>
        /// Call a remote method.
        /// </summary>
        /// <param name="method">Method name</param>
        /// <param name="args">Arguments to the method if available.</param>
        /// <returns>Response returned by the call.</returns>
        public async Task<ResponseMessage> CallAsync(string method, params XmlRpcBaseType[] args)
        {
            uint handle = await GetNextHandle();
            MethodCall call = new(method, handle, args);

            logger.Trace("Calling remote method: {method}", method);
            logger.Trace("================== CALL START ==================");
            logger.Trace(call.Call.MainDocument);
            logger.Trace("================== CALL END ==================");

            responseHandles[handle] = new(false);

            byte[] data = await call.Serialize();
            await xmlRpcIO.WriteBytesAsync(data);


            // wait for response
            responseHandles[handle].WaitOne();
            ResponseMessage message = responseMessages[handle];
            return message;
        }
    }
}
