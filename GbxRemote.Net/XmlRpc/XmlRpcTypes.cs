using GbxRemoteNet.XmlRpc.ExtraTypes;
using GbxRemoteNet.XmlRpc.Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GbxRemoteNet.XmlRpc {
    public static class XmlRpcTypes {
        static Dictionary<string, Type> TypesMap = new() {
            // array
            { XmlRpcElementNames.Array.ToLower(), typeof(XmlRpcArray) },

            // struct
            { XmlRpcElementNames.Struct.ToLower(), typeof(XmlRpcStruct) },

            // base64
            { XmlRpcElementNames.Base64.ToLower(), typeof(XmlRpcBase64) },

            // boolean
            { XmlRpcElementNames.Boolean.ToLower(), typeof(XmlRpcBoolean) },

            // datetime
            { XmlRpcElementNames.DateTime.ToLower(), typeof(XmlRpcDateTime) },

            // double
            { XmlRpcElementNames.Double.ToLower(), typeof(XmlRpcDouble) },

            // integer
            { XmlRpcElementNames.Integer.ToLower(), typeof(XmlRpcInteger) },
            { XmlRpcElementNames.I4.ToLower(), typeof(XmlRpcInteger) },

            // string
            { XmlRpcElementNames.String.ToLower(), typeof(XmlRpcString) }
        };

        /// <summary>
        /// Create an instance of a XMLRPC type from a XElement.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static XmlRpcBaseType ElementToInstance(XElement element) {
            string elementName = element.Name.ToString();
            if (!TypesMap.ContainsKey(elementName.ToLower()))
                throw new InvalidDataException($"Element '{elementName}' does not exist in the XMLRPC spec!");

            Type xmlRpcType = TypesMap[elementName.ToLower()];
            XmlRpcBaseType value = (XmlRpcBaseType)Activator.CreateInstance(xmlRpcType, element);

            return value;
        }
    }
}
