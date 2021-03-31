using GbxRemoteNet.XmlRpc.ExtraTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GbxRemoteNet.XmlRpc.Types {
    public class XmlRpcStruct : XmlRpcBaseType {
        public Struct Fields;

        public XmlRpcStruct(Struct fields) : base(null) {
            Fields = fields;
        }

        public XmlRpcStruct(XElement element) : base(element) {
            Fields = new Struct();
            var members = element.Elements(XmlRpcElementNames.Member);

            foreach (XElement member in members) {
                string name = member.Elements(XmlRpcElementNames.Name).First().Value;
                XElement valueElement = member.Elements(XmlRpcElementNames.Value)
                                              .First()
                                              .Elements()
                                              .First();

                XmlRpcBaseType value = XmlRpcTypes.ElementToInstance(valueElement);

                Fields.Add(name, value);
            }
        }

        public override XElement GetXml() {
            XElement structElement = new(XmlRpcElementNames.Struct);

            foreach (var kv in Fields)
                structElement.Add(new XElement(XmlRpcElementNames.Member,
                    new XElement(XmlRpcElementNames.Name, kv.Key),
                    new XElement(XmlRpcElementNames.Value, kv.Value.GetXml())
                ));

            return structElement;
        }
    }
}
