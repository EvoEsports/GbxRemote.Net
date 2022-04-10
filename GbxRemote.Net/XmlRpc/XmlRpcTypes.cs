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
    /// <summary>
    /// Handles conversions between XML elements, XML-RPC type values and native C# values.
    /// </summary>
    public static class XmlRpcTypes {
        /// <summary>
        /// Mappings for XML-RPC type names to XML type classes.
        /// </summary>
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
        /// <param name="element">The XML element to convert.</param>
        /// <returns>A XML type value.</returns>
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
        /// <param name="xmlValue">The XML value to convert to a native C# type.</param>
        /// <param name="instanceType">Type to return, if this is null (ignored), the template value is used instead.</param>
        /// <returns>A C# native value.</returns>
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
                return ToNativeArray<T>((XmlRpcArray)xmlValue, instanceType);
            } else if (t == typeof(XmlRpcStruct)) {
                return ToNativeStruct<T>((XmlRpcStruct)xmlValue, instanceType);
            }

            return null;
        }

        /// <summary>
        /// Convert a XML-RPC struct to a C# object.
        /// </summary>
        /// <typeparam name="T">Type to convert to. A DynamicObject returned is used if "object" is passed.</typeparam>
        /// <param name="xmlStruct">The XML type struct to convert.</param>
        /// <param name="instanceType">Type to convert to, template is used instead if null (ignored).</param>
        /// <returns>A DynamicObject or custom type if specified.</returns>
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

            var properties = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            // copy all the available fields to the instance
            foreach (var property in properties) {
                if (xmlStruct.Fields.ContainsKey(property.Name)) {
                    var fieldType = property.PropertyType.GetElementType() ?? property.PropertyType;
                    object objValue = ToNativeValue<object>(xmlStruct.Fields[property.Name], fieldType);
                    Type objType = objValue.GetType();

                    if (objType.IsArray) {
                        // array requires special conversion to work
                        int length = (int)objType.GetProperty("Length").GetValue(objValue);
                        Array fieldInstance = Array.CreateInstance(property.PropertyType.GetElementType(), length);
                        Array.Copy((Array)objValue, fieldInstance, length);
                        property.SetValue(nativeStruct, fieldInstance);
                    } else {
                        property.SetValue(nativeStruct, objValue);
                    }
                }
            }

            return nativeStruct;
        }

        /// <summary>
        /// Convert a XML-RPC array to a native array.
        /// </summary>
        /// <typeparam name="T">Type of the array.</typeparam>
        /// <param name="arr">XML type array to convert.</param>
        /// <param name="instanceType">Type to convert to, template is used if null or ignored.</param>
        /// <returns>A native C# array of type T/instanceType.</returns>
        public static T[] ToNativeArray<T>(XmlRpcArray arr, Type instanceType = null) {
            return arr.Values.Select((v, i) => (T)ToNativeValue<T>(v, instanceType)).ToArray();
        }

        /// <summary>
        /// Convert a XML-RPC array to a native 2d array.
        /// </summary>
        /// <typeparam name="T">Type of the 2D array to convert to.</typeparam>
        /// <param name="arr">An XML type array that also has a subarray as it's elements.</param>
        /// <returns>A native C# 2D array of type T.</returns>
        public static T[][] ToNative2DArray<T>(XmlRpcArray arr) {
            T[][] result = new T[arr.Values.Length][];
            for (int i = 0; i < arr.Values.Length; i++) {
                XmlRpcArray xmlArr = (XmlRpcArray)arr.Values[i];
                result[i] = ToNativeArray<T>(xmlArr);
            }
            
            return result;
        }

        /// <summary>
        /// Create a XML type object from a native C# type value.
        /// </summary>
        /// <param name="obj">The value to convert.</param>
        /// <returns>A XML type value.</returns>
        public static XmlRpcBaseType ToXmlRpcValue(object obj) {
            if (obj == null) 
                return null;

            Type t = obj.GetType();

            if (t == typeof(Base64)) { // base64
                return new XmlRpcBase64((Base64)obj);
            } else if (t == typeof(bool)) { // boolean
                return new XmlRpcBoolean((bool)obj);
            } else if (t == typeof(DateTime)) { // dateTime.iso8601
                return new XmlRpcDateTime((DateTime)obj);
            } else if (t == typeof(double)) { // double
                return new XmlRpcDouble((double)obj);
            } else if (t == typeof(float)) { // double
                return new XmlRpcDouble((double)obj);
            } else if (t == typeof(int)) { // int/i4
                return new XmlRpcInteger((int)obj);
            } else if (t == typeof(uint)) { // int/i4
                return new XmlRpcInteger((int)obj);
            } else if (t == typeof(string)) { // string
                return new XmlRpcString((string)obj);
            } else if (t == typeof(DynamicObject)) { // struct
                return new XmlRpcStruct(obj);
            } else if (t.IsArray) { // array
                return ToXmlRpcArray(obj);

            } else if (t.IsClass) { // struct
                return new XmlRpcStruct(obj);
            }

            return null;
        }

        /// <summary>
        /// Convert a generic array into an XML-RPC array.
        /// </summary>
        /// <param name="obj">This can be any type as long as it's an array.</param>
        /// <returns>A XML type array.</returns>
        public static XmlRpcArray ToXmlRpcArray(object obj) {
            IEnumerable arr = obj as IEnumerable;
            List<XmlRpcBaseType> items = new List<XmlRpcBaseType>();

            foreach (var item in arr)
                items.Add(ToXmlRpcValue(item));

            return new XmlRpcArray(items.ToArray());
        }
    }
}
