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

namespace GbxRemoteNet.XmlRpc {
    public class NadeoXmlRpcClient {
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

        public NadeoXmlRpcClient(string host, int port) {
            connectHost = host;
            connectPort = port;
        }

        /// <summary>
        /// Handles all responses from the XML-RPC server.
        /// </summary>
        private async void RecvLoop() {
            try {
                logger.Debug("Recieve loop initiated.");

                while (!recvCancel.IsCancellationRequested) {
                    ResponseMessage response = await ResponseMessage.FromIOAsync(xmlRpcIO);

                    logger.Debug("================== MESSAGE START ==================");
                    logger.Debug($"Message length: {response.Header.MessageLength}");
                    logger.Debug($"Handle: {response.Header.Handle}");
                    logger.Debug($"Is callback: {response.Header.IsCallback}");
                    logger.Debug(response.MessageXml);
                    logger.Debug("================== MESSAGE END ==================");

                    if (response.IsCallback) {
                        // invoke listeners and
                        // run callback handler in a new thread to avoid blocking of new responses
                        _ = Task.Run(() => OnCallback?.Invoke(new MethodCall(response)));
                    } else if (responseHandles.ContainsKey(response.Header.Handle)) {
                        // attempt to signal the call method
                        responseMessages[response.Header.Handle] = response;
                        responseHandles[response.Header.Handle].Set();
                    }
                }
            } catch (Exception e) {
               logger.Error(e, "Receive loop raised an exception.");
            }
        }

        /// <summary>
        /// Connect to the remote XMLRPC server.
        /// </summary>
        /// <returns></returns>
        public async Task ConnectAsync() {
            logger.Debug("Client connecting to the remote XML-RPC server.");
            var connectAddr = await Dns.GetHostAddressesAsync(connectHost);

            tcpClient = new TcpClient();
            await tcpClient.ConnectAsync(connectAddr[0], connectPort);
            xmlRpcIO = new XmlRpcIO(tcpClient);

            logger.Debug("Client connected to XML-RPC server with IP: {connectAddr}", connectAddr);

            // check header
            ConnectHeader header = await ConnectHeader.FromIOAsync(xmlRpcIO);
            if (!header.IsValid) {
                logger.Error("Client is using an invalid header protocol: {header.protocol}", header.Protocol);
                throw new Exception($"Invalid protocol: {header.Protocol}");
            }

            recvCancel = new CancellationTokenSource();
            taskRecvLoop = new Task(RecvLoop, recvCancel.Token);
            taskRecvLoop.Start();

            OnConnected?.Invoke();
        }

        /// <summary>
        /// Stop the recieve loop and disconnect.
        /// </summary>
        /// <returns></returns>
        public async Task DisconnectAsync() {
            logger.Debug("Client is disconnecting from XML-RPC server.");
            recvCancel.Cancel();
            await taskRecvLoop;
            tcpClient.Close();
            logger.Debug("Client disconnected from XML-RPC server.");
        }

        /// <summary>
        /// Get the next handler value.
        /// </summary>
        /// <returns>The next handle value.</returns>
        public async Task<uint> GetNextHandle() {
            // lock because we may access this in multiple threads
            lock (handlerLock) {
                if (handler + 1 == 0xffffffff)
                    handler = 0x80000000;
                return handler++;
            }
        }

        /// <summary>
        /// Call a remote method.
        /// </summary>
        /// <param name="method">Method name</param>
        /// <param name="args">Arguments to the method if available.</param>
        /// <returns>Response returned by the call.</returns>
        public async Task<ResponseMessage> CallAsync(string method, params XmlRpcBaseType[] args) {
            uint handle = await GetNextHandle();
            MethodCall call = new(method, handle, args);

            logger.Debug("================== CALL START ==================");
            logger.Debug(call.Call.MainDocument);
            logger.Debug("================== CALL END ==================");

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
