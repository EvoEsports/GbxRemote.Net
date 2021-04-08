using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GbxRemoteNet.XmlRpc.Types {
    public class XmlRpcString : XmlRpcBaseType, IEquatable<XmlRpcString> {
        public string Value;

        public XmlRpcString(string value) : base(null) {
            Value = value;
        }

        public XmlRpcString(XElement element) : base(element) {
            Value = element.Value;
        }

        public bool Equals(XmlRpcString other) {
            return Value.Equals(other.Value);
        }

        public override bool Equals(object obj) {
            return Equals((XmlRpcString)obj);
        }

        public override int GetHashCode() {
            return GetHashCode();
        }

        public override XElement GetXml() {
            return new XElement(XmlRpcElementNames.String, Value);
        }
    }
}
