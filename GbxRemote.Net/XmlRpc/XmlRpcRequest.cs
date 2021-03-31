using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GbxRemoteNet.XmlRpc {
    public abstract class XmlRpcRequest {
        public XDocument MainDocument { get; private set; }

        public XmlRpcRequest(string rootElement) {
            MainDocument = new XDocument(
                new XDeclaration("1.0", null, null),
                new XElement(rootElement)
            );
        }

        /// <summary>
        /// Generate formatted XML from the request data.
        /// </summary>
        /// <returns></returns>
        public string GenerateXML() {
            StringWriter sw = new StringWriter();
            MainDocument.Save(sw);
            return sw.ToString();
        }
    }
}
