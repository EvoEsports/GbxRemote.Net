using GbxRemoteNet.XmlRpc;
using GbxRemoteNet.XmlRpc.Packets;
using GbxRemoteNet.XmlRpc.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace GbxRemoteNet {
    /// <summary>
    /// GBXRemote client for connecting to and managing TrackMania servers through XML-RPC.
    /// </summary>
    public partial class GbxRemoteClient : NadeoXmlRpcClient {
        private readonly GbxRemoteClientOptions Options;
        private readonly ILogger _logger;

        /// <summary>
        /// This is the API version the client will be using.
        /// </summary>
        public const string ApiVersion = "2013-04-16";

        /// <summary>
        /// Create a new instance of the GBXRemote client.
        /// </summary>
        /// <param name="host">The address to the TrackMania server. Default: 127.0.0.1</param>
        /// <param name="port">The port the XML-RPC server is listening to on your TrackMania server. Default: 5000</param>
        /// <param name="logger"></param>
        public GbxRemoteClient(string host, int port, ILogger logger=null) : base(host, port, logger) {
            OnCallback += GbxRemoteClient_OnCallback;
            Options = new();

            _logger = logger;
        }

        /// <summary>
        /// Create a new instance of the GBXRemote client.
        /// </summary>
        /// <param name="host">The address to the TrackMania server. Default: 127.0.0.1</param>
        /// <param name="port">The port the XML-RPC server is listening to on your TrackMania server. Default: 5000</param>
        public GbxRemoteClient(string host, int port, GbxRemoteClientOptions options) : base(host, port) {
            OnCallback += GbxRemoteClient_OnCallback;
            Options = options;
        }

        /// <summary>
        /// Call a remote method and throw an exception if a fault occured.
        /// </summary>
        /// <param name="method"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public async Task<XmlRpcBaseType> CallOrFaultAsync(string method, params object[] args) {
            var msg = await CallAsync(method, MethodArgs(args));

            if (msg.IsFault) {
                _logger?.LogError("Remote call failed with reason: {Message}", (XmlRpcFault)msg.ResponseData);
                throw new XmlRpcFaultException((XmlRpcFault)msg.ResponseData);
            }

            return msg.ResponseData;
        }

        /// <summary>
        /// Call a remote method on the server and return the recieved message.
        /// </summary>
        /// <param name="method"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public async Task<ResponseMessage> CallMethodAsync(string method, params object[] args) {
            return await CallAsync(method, MethodArgs(args));
        }

        /// <summary>
        /// Convert C# type values to XML-RPC type values and create an argument list.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public XmlRpcBaseType[] MethodArgs(params object[] args) {
            _logger?.LogDebug("Converting C# types to XML-RPC");
            XmlRpcBaseType[] xmlRpcArgs = new XmlRpcBaseType[args.Length];
            
            for (int i = 0; i < args.Length; i++)
                xmlRpcArgs[i] = XmlRpcTypes.ToXmlRpcValue(args[i]);

            return xmlRpcArgs;
        }

        /// <summary>
        /// Connect and login to GBXRemote.
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<bool> LoginAsync(string login, string password) {
            _logger?.LogDebug("Client connecting to GbxRemote");

            if (!await ConnectAsync(Options.ConnectionRetries, Options.ConnectionRetryTimeout))
                return false;

            await SetApiVersionAsync(ApiVersion);

            if (await AuthenticateAsync(login, password)) {
                _logger?.LogDebug("Client connected to GbxRemote");
                return true;
            }

            // disconnect if login failed
            await DisconnectAsync();
            _logger?.LogError("Client failed to connect to GbxRemote");
            return false;
        }
    }
}
