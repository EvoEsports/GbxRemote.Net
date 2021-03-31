using GbxRemoteNet.XmlRpc;
using GbxRemoteNet.XmlRpc.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GbxRemoteNet {
    public partial class GbxRemoteClient {
        #region System Methods
        /// <summary>
        /// Return an array of all available XML-RPC methods on this server.
        /// </summary>
        /// <returns></returns>
        public async Task<string[]> SystemListMethods() {
            var msg = await CallAsync("system.listMethods");
            var response = msg.GetResponseData();

            if (msg.IsFault)
                throw new XmlRpcFaultException((XmlRpcFault)response);

            var arr = (XmlRpcArray)msg.GetResponseData();
            return arr.Values.Select((method, i) => ((XmlRpcString)method).Value).ToArray();
        }
        #endregion

        #region Session Methods
        /// <summary>
        /// Allow user authentication by specifying a login and a password, to gain access to the set of functionalities corresponding to this authorization level.
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<bool> AuthenticateAsync(string login, string password) {
            var msg = await CallAsync("Authenticate",
                new XmlRpcString(login),
                new XmlRpcString(password)
            );
            var response = msg.GetResponseData();

            if (msg.IsFault)
                throw new XmlRpcFaultException((XmlRpcFault)response);

            return ((XmlRpcBoolean)msg.GetResponseData()).Value;
        }

        /// <summary>
        /// Change the password for the specified login/user. Only available to SuperAdmin.
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<bool> ChangeAuthPassword(string login, string password) {
            var msg = await CallAsync("ChangeAuthPassword",
                new XmlRpcString(login),
                new XmlRpcString(password)
            );
            var response = msg.GetResponseData();

            if (msg.IsFault)
                throw new XmlRpcFaultException((XmlRpcFault)response);

            return ((XmlRpcBoolean)msg.GetResponseData()).Value;
        }

        /// <summary>
        /// Allow the GameServer to call you back.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> EnableCallbacks() {
            var msg = await CallAsync("EnableCallbacks");
            var response = msg.GetResponseData();

            if (msg.IsFault)
                throw new XmlRpcFaultException((XmlRpcFault)response);

            return ((XmlRpcBoolean)msg.GetResponseData()).Value;
        }

        /// <summary>
        /// Define the wanted api.
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        public async Task<bool> SetApiVersion(string version) {
            var msg = await CallAsync("SetApiVersion", new XmlRpcString(version));
            var response = msg.GetResponseData();

            if (msg.IsFault)
                throw new XmlRpcFaultException((XmlRpcFault)response);

            return ((XmlRpcBoolean)msg.GetResponseData()).Value;
        }
        #endregion
    }
}
