using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GbxRemoteNet.XmlRpc.Types {
    public class XmlRpcDateTime : XmlRpcBaseType {
        public DateTime Value;

        public XmlRpcDateTime(DateTime value) : base(null) {
            Value = value;
        }

        public XmlRpcDateTime(XElement element) : base(element) {
            Value = DateTime.Parse(element.Value);
        }

        public override XElement GetXml() {
            return new XElement(XmlRpcElementNames.DateTime, Value.ToString("o") /* ISO 8601 */);
        }
    }
}
