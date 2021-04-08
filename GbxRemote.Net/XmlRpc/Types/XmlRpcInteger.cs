using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GbxRemoteNet.XmlRpc.Types {
    public class XmlRpcInteger : XmlRpcBaseType, IEquatable<XmlRpcInteger> {
        public int Value;

        public XmlRpcInteger(int value) : base(null) {
            Value = value;
        }

        public XmlRpcInteger(XElement element) : base(element) {
            Value = Convert.ToInt32(element.Value);
        }

        public bool Equals(XmlRpcInteger other) {
            return Value.Equals(other.Value);
        }

        public override XElement GetXml() {
            return new XElement(XmlRpcElementNames.Integer, Value);
        }
    }
}
