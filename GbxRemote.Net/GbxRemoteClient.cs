using GbxRemoteNet.XmlRpc;
using GbxRemoteNet.XmlRpc.Packets;
using GbxRemoteNet.XmlRpc.Types;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GbxRemoteNet {
    public partial class GbxRemoteClient : NadeoXmlRpcClient {
        private readonly ILogger<GbxRemoteClient> logger;
        public const string ApiVersion = "2013-04-16";

        public GbxRemoteClient(ILogger<GbxRemoteClient> logger, string host, int port) : base(logger, host, port) {
            this.logger = logger;
            OnCallback += GbxRemoteClient_OnCallback;
            InvokeEventOnModeScriptMethodResponse = false;
        }

        /// <summary>
        /// Call a remote method and throw an exception if a fault occured.
        /// </summary>
        /// <param name="method"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private async Task<XmlRpcBaseType> CallOrFaultAsync(string method, params object[] args) {
            logger.LogDebug("Calling remote with method {method}.", method);
            var msg = await CallAsync(method, MethodArgs(args));

            if (msg.IsFault)
                logger.LogWarning("Remote call failed with reason: {message}", (XmlRpcFault)msg.ResponseData);
                throw new XmlRpcFaultException((XmlRpcFault)msg.ResponseData);

            return msg.ResponseData;
        }

        /// <summary>
        /// Convert C# type values to XML-RPC type values and create an argument list.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private XmlRpcBaseType[] MethodArgs(params object[] args) {
            logger.LogDebug("Converting C# types to XML-RPC");
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
            logger.LogInformation("Client connecting to GbxRemote.");
            await ConnectAsync();
            await SetApiVersionAsync(ApiVersion);

            if (await AuthenticateAsync(login, password))
                logger.LogInformation("Client connected to GbxRemote.");
                return true;

            // disconnect if login failed
            await DisconnectAsync();
            logger.LogWarning("Client failed to connect to GbxRemote.");
            return false;
        }
    }
}
