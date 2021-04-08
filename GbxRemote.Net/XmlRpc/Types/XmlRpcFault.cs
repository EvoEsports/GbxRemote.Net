using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GbxRemoteNet.XmlRpc.Types {
    public class XmlRpcFault : XmlRpcBaseType, IEquatable<XmlRpcFault> {
        public int FaultCode;
        public string FaultString;

        public XmlRpcFault(int faultCode, string faultString) : base(null) {
            FaultCode = faultCode;
            FaultString = faultString;
        }

        public XmlRpcFault(XElement element) : base(element) {
            XmlRpcStruct faultStruct = new(element);
            FaultCode = ((XmlRpcInteger)faultStruct.Fields["faultCode"]).Value;
            FaultString = ((XmlRpcString)faultStruct.Fields["faultString"]).Value;
        }

        public override XElement GetXml() {
            throw new NotImplementedException();
        }

        public bool Equals(XmlRpcFault other) {
            return FaultCode.Equals(other.FaultCode) && FaultString.Equals(other.FaultString);
        }
    }
}
