using GbxRemoteNet.XmlRpc.Packets;
using GbxRemoteNet.XmlRpc.Types;
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

        // events
        public delegate Task TaskAction();
        public delegate Task CallbackAction(MethodCall call);

        public event TaskAction OnConnected;
        public event CallbackAction OnCallback;

        public NadeoXmlRpcClient(string host, int port) {
            connectHost = host;
            connectPort = port;
        }

        private async void RecvLoop() {
            try {
                while (!recvCancel.IsCancellationRequested) {
                    ResponseMessage response = await ResponseMessage.FromIOAsync(xmlRpcIO);

                    Console.WriteLine("================== MESSAGE START ==================");
                    Console.WriteLine($"Message length: {response.Header.MessageLength}");
                    Console.WriteLine($"Handle: {response.Header.Handle}");
                    Console.WriteLine($"Is callback: {response.Header.IsCallback}");
                    Console.WriteLine(response.MessageXml);
                    Console.WriteLine("================== MESSAGE END ==================");

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
                Console.WriteLine($"Recieve Loop Exception: {e}");
            }
        }

        /// <summary>
        /// Connect to the remote XMLRPC server.
        /// </summary>
        /// <returns></returns>
        public async Task ConnectAsync() {
            var connectAddr = await Dns.GetHostAddressesAsync(connectHost);

            tcpClient = new TcpClient();
            await tcpClient.ConnectAsync(connectAddr[0], connectPort);
            xmlRpcIO = new XmlRpcIO(tcpClient);

            // check header
            ConnectHeader header = await ConnectHeader.FromIOAsync(xmlRpcIO);
            if (!header.IsValid) {
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
            recvCancel.Cancel();
            await taskRecvLoop;
            tcpClient.Close();
        }

        /// <summary>
        /// Get the next handler value.
        /// </summary>
        /// <returns></returns>
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
        /// <returns></returns>
        public async Task<ResponseMessage> CallAsync(string method, params XmlRpcBaseType[] args) {
            uint handle = await GetNextHandle();
            MethodCall call = new(method, handle, args);

            //Console.WriteLine(call.Call.MainDocument);

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
