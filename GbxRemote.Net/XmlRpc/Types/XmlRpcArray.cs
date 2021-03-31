using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GbxRemoteNet.XmlRpc.Types {
    public class XmlRpcArray : XmlRpcBaseType {
        public XmlRpcBaseType[] Values;

        public XmlRpcArray(XmlRpcBaseType[] values) : base(null) {
            Values = values;
        }

        public XmlRpcArray(XElement element) : base(element) {
            var arrayValues = element.Elements(XmlRpcElementNames.Data)
                                            .First()
                                            .Elements(XmlRpcElementNames.Value);
            List<XmlRpcBaseType> values = new();

            foreach (XElement valueElement in arrayValues) {
                XmlRpcBaseType value = XmlRpcTypes.ElementToInstance(valueElement.Elements().First());
                values.Add(value);
            }

            Values = values.ToArray();
        }

        public override XElement GetXml() {
            XElement arrayElement = new(XmlRpcElementNames.Array);
            XElement dataElement = new(XmlRpcElementNames.Data);
            arrayElement.Add(dataElement);

            foreach (var value in Values)
                dataElement.Add(new XElement(XmlRpcElementNames.Value, 
                    value.GetXml()
                ));

            return arrayElement;
        }
    }
}
