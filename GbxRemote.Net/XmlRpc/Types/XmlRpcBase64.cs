using GbxRemoteNet.XmlRpc.ExtraTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GbxRemoteNet.XmlRpc.Types {
    public class XmlRpcBase64 : XmlRpcBaseType {
        public Base64 Value;

        public XmlRpcBase64(Base64 value) : base(null) {
            Value = value;
        }

        public XmlRpcBase64(XElement element) : base(element) {
            Value =  Base64.FromBase64String(element.Value);
        }

        public override XElement GetXml() {
            return new XElement(XmlRpcElementNames.Base64, Value);
        }
    }
}
