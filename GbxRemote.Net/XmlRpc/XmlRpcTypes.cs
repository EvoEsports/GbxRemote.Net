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
        public static object ToNativeValue<T>(XmlRpcBaseType xmlValue, Type instanceType = null) {
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
                return ToNativeArray<T>((XmlRpcArray)xmlValue,instanceType);
            } else if (t == typeof(XmlRpcStruct)) {
                return ToNativeStruct<T>((XmlRpcStruct)xmlValue, instanceType);
            }

            return null;
        }

        /// <summary>
        /// Convert a XML-RPC struct to a C# object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlStruct"></param>
        /// <returns></returns>
        public static object ToNativeStruct<T>(XmlRpcStruct xmlStruct, Type instanceType = null) {
            // use correct type
            Type t = instanceType;
            if (instanceType == null)
                t = typeof(T);

            if (t == typeof(DynamicObject) || t == typeof(object)) {
                // just copy the value if we have a dynamic object
                DynamicObject obj = new();

                foreach (var kv in xmlStruct.Fields)
                    obj.Add(kv.Key, ToNativeValue<object>(kv.Value));

                return obj;
            }

            object nativeStruct = Activator.CreateInstance(t);

            var fields = t.GetFields(BindingFlags.Public | BindingFlags.Instance);

            // copy all the available fields to the instance
            foreach (var field in fields) {
                if (xmlStruct.Fields.ContainsKey(field.Name)) {
                    object objValue = ToNativeValue<object>(xmlStruct.Fields[field.Name], field.FieldType);
                    Type objType = objValue.GetType();

                    if (objType.IsArray) {
                        // array requires special conversion to work
                        int length = (int)objType.GetProperty("Length").GetValue(objValue);
                        Array fieldInstance = Array.CreateInstance(field.FieldType.GetElementType(), length);
                        Array.Copy((Array)objValue, fieldInstance, length);
                        field.SetValue(nativeStruct, fieldInstance);
                    } else {
                        field.SetValue(nativeStruct, objValue);
                    }
                }
            }

            return nativeStruct;
        }

        /// <summary>
        /// Convert a XML-RPC array to a native array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static T[] ToNativeArray<T>(XmlRpcArray arr, Type instanceType = null) {
            return arr.Values.Select((v, i) => (T)ToNativeValue<T>(v, instanceType)).ToArray();
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
