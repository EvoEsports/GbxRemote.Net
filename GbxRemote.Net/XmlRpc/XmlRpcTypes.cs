using GbxRemoteNet.Structs;
using GbxRemoteNet.XmlRpc.ExtraTypes;
using GbxRemoteNet.XmlRpc.Types;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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

        /// <summary>
        /// Get the value of a XML-RPC type instance.
        /// This simply extracts the value property of each type.
        /// </summary>
        /// <typeparam name="T">Array or struct type to cast to.</typeparam>
        /// <param name="xmlValue"></param>
        /// <returns>Null if value isnt found.</returns>
        public static object ToNativeValue<T>(XmlRpcBaseType xmlValue) {
            if (xmlValue == null)
                return null;

            Type t = xmlValue.GetType();

            if (t == typeof(XmlRpcBase64)) {
                return ((XmlRpcBase64)xmlValue).Value;
            } else if (t == typeof(XmlRpcBoolean)) {
                return ((XmlRpcBoolean)xmlValue).Value;
            } else if (t == typeof(XmlRpcDateTime)) {
                return ((XmlRpcDateTime)xmlValue).Value;
            } else if (t == typeof(XmlRpcDouble)) {
                return ((XmlRpcDouble)xmlValue).Value;
            } else if (t == typeof(XmlRpcInteger)) {
                return ((XmlRpcInteger)xmlValue).Value;
            } else if (t == typeof(XmlRpcString)) {
                return ((XmlRpcString)xmlValue).Value;
            } else if (t == typeof(XmlRpcArray)) {
                return ToNativeArray<T>((XmlRpcArray)xmlValue);
            } else if (t == typeof(XmlRpcStruct)) {
                return ToNativeStruct<T>((XmlRpcStruct)xmlValue);
            }

            return null;
        }

        /// <summary>
        /// Convert a XML-RPC struct to a C# object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlStruct"></param>
        /// <returns></returns>
        public static object ToNativeStruct<T>(XmlRpcStruct xmlStruct) {
            Type t = typeof(T);

            if (t == typeof(DynamicObject)) {
                DynamicObject obj = new();

                foreach (var kv in xmlStruct.Fields)
                    obj.Add(kv.Key, ToNativeValue<object>(kv.Value));

                return obj;
            }

            T nativeStruct = (T)Activator.CreateInstance(t);
            var fields = t.GetFields(BindingFlags.Public | BindingFlags.Instance);

            foreach (var field in fields) {
                field.SetValue(nativeStruct, ToNativeValue<object>(xmlStruct.Fields[field.Name]));
            }

            return nativeStruct;
        }

        /// <summary>
        /// Convert a XML-RPC array to a native array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static T[] ToNativeArray<T>(XmlRpcArray arr) {
            return arr.Values.Select((v, i) => (T)ToNativeValue<T>(v)).ToArray();
        }

        /// <summary>
        /// Convert a XML-RPC array to a native 2d array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static T[][] ToNative2DArray<T>(XmlRpcArray arr) {
            T[][] result = new T[arr.Values.Length][];
            for (int i = 0; i < arr.Values.Length; i++) {
                XmlRpcArray xmlArr = (XmlRpcArray)arr.Values[i];
                result[i] = ToNativeArray<T>(xmlArr);
            }

            return result;
        }

        /// <summary>
        /// Create 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static XmlRpcBaseType ToXmlRpcValue(object obj) {
            if (obj == null) 
                return null;

            Type t = obj.GetType();

            if (t == typeof(Base64)) {
                return new XmlRpcBase64((Base64)obj);

            } else if (t == typeof(bool)) {
                return new XmlRpcBoolean((bool)obj);

            } else if (t == typeof(DateTime)) {
                return new XmlRpcDateTime((DateTime)obj);

            } else if (t == typeof(double)) {
                return new XmlRpcDouble((double)obj);
            } else if (t == typeof(float)) {
                return new XmlRpcDouble((double)obj);

            } else if (t == typeof(int)) {
                return new XmlRpcInteger((int)obj);
            } else if (t == typeof(uint)) {
                return new XmlRpcInteger((int)obj);

            } else if (t == typeof(string)) {
                return new XmlRpcString((string)obj);

            } else if (t == typeof(DynamicObject)) {
                return new XmlRpcStruct(obj);

            } else if (t.IsArray) {
                return ToXmlRpcArray(obj);

            } else if (t.IsClass) { // struct
                return new XmlRpcStruct(obj);
            }

            return null;
        }

        /// <summary>
        /// Convert a generic array into an XML-RPC array.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static XmlRpcArray ToXmlRpcArray(object obj) {
            IEnumerable arr = obj as IEnumerable;
            List<XmlRpcBaseType> items = new List<XmlRpcBaseType>();

            foreach (var item in arr)
                items.Add(ToXmlRpcValue(item));

            return new XmlRpcArray(items.ToArray());
        }
    }
}
