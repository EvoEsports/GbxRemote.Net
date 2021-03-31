using GbxRemoteNet.XmlRpc.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GbxRemoteNet.XmlRpc {
    public class XmlRpcFaultException : Exception {
        public XmlRpcFault Fault { get; private set; }

        public XmlRpcFaultException(XmlRpcFault fault) {
            Fault = fault;
        }
    }
}
