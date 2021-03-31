using GbxRemoteNet.XmlRpc;
using GbxRemoteNet.XmlRpc.Packets;
using GbxRemoteNet.XmlRpc.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GbxRemoteNet {
    public partial class GbxRemoteClient : NadeoXmlRpcClient {
        public GbxRemoteClient(string host, int port) : base(host, port) {

        }

        /// <summary>
        /// Call a remote method and throw an exception if a fault occured.
        /// </summary>
        /// <param name="method"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private async Task<XmlRpcBaseType> CallOrFaultAsync(string method, params XmlRpcBaseType[] args) {
            var msg = await CallAsync(method, args);

            if (msg.IsFault)
                throw new XmlRpcFaultException((XmlRpcFault)msg.ResponseData);

            return msg.ResponseData;
        }

        /// <summary>
        /// Convert C# type values to XML-RPC type values and create an argument list.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private XmlRpcBaseType[] Arguments(params object[] args) {
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
            await ConnectAsync();

            if (await AuthenticateAsync(login, password))
                return true;

            // disconnect if login failed
            await DisconnectAsync();
            return false;
        }
    }
}
