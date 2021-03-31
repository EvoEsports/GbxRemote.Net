using GbxRemoteNet.XmlRpc;
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
