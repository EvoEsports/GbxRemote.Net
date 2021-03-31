using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GbxRemoteNet.XmlRpc.Types {
    public class XmlRpcDouble : XmlRpcBaseType {
        public double Value;

        public XmlRpcDouble(double value) : base(null) {
            Value = value;
        }

        public XmlRpcDouble(XElement element) : base(element) {
            Value = Convert.ToDouble(element.Value);
        }

        public override XElement GetXml() {
            return new XElement(XmlRpcElementNames.Double, Value);
        }
    }
}
