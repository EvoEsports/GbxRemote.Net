using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GbxRemoteNet.XmlRpc.Types {
    /// <summary>
    /// Represents an XML-RPC double.
    /// </summary>
    public class XmlRpcDouble : XmlRpcBaseType, IEquatable<XmlRpcDouble> {
        public double Value;

        public XmlRpcDouble(double value) : base(null) {
            Value = value;
        }

        public XmlRpcDouble(XElement element) : base(element) {
            Value = Convert.ToDouble(element.Value, CultureInfo.InvariantCulture);
        }

        public bool Equals(XmlRpcDouble other) {
            return Value.Equals(other.Value);
        }

        public override bool Equals(object obj) {
            return Equals((XmlRpcDouble)obj);
        }

        public override int GetHashCode() {
            return GetHashCode();
        }

        /// <summary>
        /// Generate the XML element for this value.
        /// </summary>
        /// <returns>Generated element</returns>
        public override XElement GetXml() {
            return new XElement(XmlRpcElementNames.Double, Value);
        }
    }
}
