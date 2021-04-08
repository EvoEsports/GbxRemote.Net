using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GbxRemoteNet.XmlRpc.Types {
    public class XmlRpcBoolean : XmlRpcBaseType, IEquatable<XmlRpcBoolean> {
        public bool Value;

        public XmlRpcBoolean(bool value) : base(null) {
            Value = value;
        }

        public XmlRpcBoolean(XElement element) : base(element) {
            string value = element.Value.Trim();

            if (value == "0" || value == "1")
                Value = Convert.ToBoolean(Convert.ToInt32(value));
            else
                Value = Convert.ToBoolean(value);
        }

        public bool Equals(XmlRpcBoolean other) {
            return Value.Equals(other.Value);
        }

        public override XElement GetXml() {
            return new XElement(XmlRpcElementNames.Boolean, Value ? '1' : '0');
        }
    }
}
