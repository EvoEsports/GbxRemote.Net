using GbxRemoteNet.XmlRpc;
using GbxRemoteNet.XmlRpc.Packets;
using GbxRemoteNet.XmlRpc.Types;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GbxRemoteNet {
    /// <summary>
    /// GBXRemote client for connecting to and managing TrackMania servers through XML-RPC.
    /// </summary>
    public partial class GbxRemoteClient : NadeoXmlRpcClient {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        private readonly GbxRemoteClientOptions Options;

        /// <summary>
        /// This is the API version the client will be using.
        /// </summary>
        public const string ApiVersion = "2013-04-16";

        /// <summary>
        /// Create a new instance of the GBXRemote client.
        /// </summary>
        /// <param name="host">The address to the TrackMania server. Default: 127.0.0.1</param>
        /// <param name="port">The port the XML-RPC server is listening to on your TrackMania server. Default: 5000</param>
        public GbxRemoteClient(string host, int port) : base(host, port) {
            OnCallback += GbxRemoteClient_OnCallback;
            Options = new();
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
        private async Task<XmlRpcBaseType> CallOrFaultAsync(string method, params object[] args) {
            logger.Debug("Calling remote with method {method}", method);

            var msg = await CallAsync(method, MethodArgs(args));

            if (msg.IsFault) {
                logger.Error("Remote call failed with reason: {message}", (XmlRpcFault)msg.ResponseData);
                throw new XmlRpcFaultException((XmlRpcFault)msg.ResponseData);
            }

            return msg.ResponseData;
        }

        /// <summary>
        /// Convert C# type values to XML-RPC type values and create an argument list.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static XmlRpcBaseType[] MethodArgs(params object[] args) {
            logger.Debug("Converting C# types to XML-RPC");
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
            logger.Debug("Client connecting to GbxRemote");

            if (!await ConnectAsync(Options.ConnectionRetries, Options.ConnectionRetryTimeout))
                return false;

            await SetApiVersionAsync(ApiVersion);

            if (await AuthenticateAsync(login, password)) {
                logger.Debug("Client connected to GbxRemote");
                return true;
            }

            // disconnect if login failed
            await DisconnectAsync();
            logger.Error("Client failed to connect to GbxRemote");
            return false;
        }
    }
}
