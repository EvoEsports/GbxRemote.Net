using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GbxRemoteNet.XmlRpc
{
    /// <summary>
    /// Represents a XML-RPC request.
    /// </summary>
    public abstract class XmlRpcRequest
    {
        /// <summary>
        /// The XML document of the request.
        /// </summary>
        public XDocument MainDocument { get; private set; }

        /// <summary>
        /// Create a new XML-RPC request with a specific name for
        /// the root element.
        /// </summary>
        /// <param name="rootElement">Name of the root element in the XML tree.</param>
        public XmlRpcRequest(string rootElement)
        {
            MainDocument = new XDocument(
                new XDeclaration("1.0", "utf-8", null),
                new XElement(rootElement)
            );
        }

        /// <summary>
        /// Generate formatted XML from the request data.
        /// </summary>
        /// <returns></returns>
        public string GenerateXML()
        {
            StringWriter sw = new StringWriter();
            MainDocument.Save(sw, SaveOptions.DisableFormatting);
            return sw.ToString();
        }
    }
}
