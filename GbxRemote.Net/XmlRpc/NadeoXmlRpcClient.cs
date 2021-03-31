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
        object handlerLock = new object();
        ConcurrentDictionary<uint, ManualResetEvent> responseHandles = new();
        ConcurrentDictionary<uint, Message> responseMessages = new();

        // connection
        IPAddress connectAddr;
        int connectPort;
        TcpClient tcpClient;
        XmlRpcIO xmlRpcIO;

        // recvieve
        Task taskRecvLoop;
        CancellationTokenSource recvCancel;

        // events
        public delegate Task TaskAction();
        public delegate Task CallbackAction(Message message);

        public event TaskAction OnConnected;
        public event CallbackAction OnCallback;

        public NadeoXmlRpcClient(string host, int port) {
            connectAddr = IPAddress.Parse(host);
            connectPort = port;
        }

        private async void RecvLoop() {
            try {
                while (!recvCancel.IsCancellationRequested) {

                    Message message = await Message.FromIOAsync(xmlRpcIO);

                    if (message.Header.IsCallback) {
                        // just invoke the callback event
                        OnCallback?.Invoke(message);
                    } else if (responseHandles.ContainsKey(message.Header.Handle)) {
                        // attempt to signal the call method
                        responseMessages[message.Header.Handle] = message;
                        responseHandles[message.Header.Handle].Set();
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
            tcpClient = new TcpClient();
            await tcpClient.ConnectAsync(connectAddr, connectPort);
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
        public async Task<Message> CallAsync(string method, params XmlRpcBaseType[] args) {
            uint handle = await GetNextHandle();
            MethodCall call = new(method, handle, args);

            responseHandles[handle] = new ManualResetEvent(false);

            byte[] data = await call.Serialize();
            await xmlRpcIO.WriteBytesAsync(data);

            // wait for response
            responseHandles[handle].WaitOne();
            Message message = responseMessages[handle];
            return message;
        }
    }
}
