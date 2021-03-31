using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GbxRemoteNet.XmlRpc.Types {
    public class XmlRpcString : XmlRpcBaseType {
        public string Value;

        public XmlRpcString(string value) : base(null) {
            Value = value;
        }

        public XmlRpcString(XElement element) : base(element) {
            Value = element.Value;
        }

        public override XElement GetXml() {
            return new XElement(XmlRpcElementNames.String, Value);
        }
    }
}
