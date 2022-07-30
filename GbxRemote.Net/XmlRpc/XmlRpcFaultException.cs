using GbxRemoteNet.XmlRpc.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GbxRemoteNet.XmlRpc {
    /// <summary>
    /// Exception thrown if a fault occured.
    /// </summary>
    public class XmlRpcFaultException : Exception {
        /// <summary>
        /// Information related to the fault.
        /// </summary>
        public XmlRpcFault Fault { get; private set; }

        /// <summary>
        /// Creates a new instance of the exception using the
        /// provided fault info.
        /// </summary>
        /// <param name="fault">Object containing info about the fault.</param>
        public XmlRpcFaultException(XmlRpcFault fault) {
            Fault = fault;
        }

        public override string Message => $"({Fault.FaultCode}) {Fault.FaultString}";
    }
}
