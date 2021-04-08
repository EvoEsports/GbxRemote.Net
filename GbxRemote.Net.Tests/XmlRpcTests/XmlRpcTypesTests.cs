using GbxRemoteNet.XmlRpc;
using GbxRemoteNet.XmlRpc.ExtraTypes;
using GbxRemoteNet.XmlRpc.Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xunit;

namespace GbxRemote.Net.Tests.XmlRpcTests {
    public class XmlRpcTypesTests {
        [Fact]
        public void ElementToInstance_Throws_On_Invalid_XMLRPC_Element() {
            XElement element = new("InvalidElement");

            Assert.Throws<InvalidDataException>(() => XmlRpcTypes.ElementToInstance(element));
        }

        public static IEnumerable<object[]> ElementToInstanceData => new List<object[]> {
            new object[]{ new XElement("i4", "21"), new XmlRpcInteger(21) },
            new object[]{ new XElement("int", "3465"), new XmlRpcInteger(3465) },
            new object[]{ new XElement("string", "Test String"), new XmlRpcString("Test String") },
            new object[]{ new XElement("boolean", "1"), new XmlRpcBoolean(true) },
            new object[]{ new XElement("boolean", "0"), new XmlRpcBoolean(false) },
            new object[]{ new XElement("double", "34534.1234"), new XmlRpcDouble(34534.1234) },
            new object[]{ new XElement("dateTime.iso8601", "2021-04-06T16:36:44.1557489+02:00"), new XmlRpcDateTime(DateTime.Parse("2021-04-06T16:36:44.1557489+02:00")) },
            new object[]{ new XElement("base64", "VGVzdCBTdHJpbmc="), new XmlRpcBase64(Base64.FromBase64String("VGVzdCBTdHJpbmc=")) },
            new object[]{ new XElement("array", new XElement("data",
                    new XElement("value", new XElement("i4", 1)),
                    new XElement("value", new XElement("int", 2)),
                    new XElement("value", new XElement("string", "3")),
                    new XElement("value", new XElement("double", 4))
                )), new XmlRpcArray(new XmlRpcBaseType[]{
                    new XmlRpcInteger(1),
                    new XmlRpcInteger(2),
                    new XmlRpcString("3"),
                    new XmlRpcDouble(4),
                })},
            new object[] { new XElement("struct",
                new XElement("member",
                        new XElement("name", "Key1"),
                        new XElement("value", new XElement("i4", 1))
                ),
                new XElement("member",
                        new XElement("name", "Key2"),
                        new XElement("value", new XElement("int", 2))
                ),
                new XElement("member",
                        new XElement("name", "Key3"),
                        new XElement("value", new XElement("string", "3"))
                ),
                new XElement("member",
                        new XElement("name", "Key4"),
                        new XElement("value", new XElement("double", 4))
                )
                ), new XmlRpcStruct(new Struct() {
                    { "Key1", new XmlRpcInteger(1) },
                    { "Key2", new XmlRpcInteger(2) },
                    { "Key3", new XmlRpcString("3") },
                    { "Key4", new XmlRpcDouble(4) }
                })}
        };

        [Theory]
        [MemberData(nameof(ElementToInstanceData))]
        public void ElementToInstance_Converts_Types_Correctly(XElement element, object expected) {
            XmlRpcBaseType result = XmlRpcTypes.ElementToInstance(element);

            Assert.Equal(result, expected);
        }

        [Fact]
        public static void ToNativeValue_Returns_Null_If_Null() {
            var result = XmlRpcTypes.ToNativeValue<object>(null);

            Assert.Null(result);
        }

        class ToNativeValue_NonExistentElement : XmlRpcBaseType {
            public ToNativeValue_NonExistentElement() : base(null) { }

            public override XElement GetXml() {
                throw new NotImplementedException();
            }
        }

        [Fact]
        public static void ToNativeValue_Returns_Null_If_NonExistent_Element() {
            ToNativeValue_NonExistentElement element = new();
            var result = XmlRpcTypes.ToNativeValue<object>(element);

            Assert.Null(result);
        }

        public static IEnumerable<object[]> ToNativeValueData => new List<object[]> {
            new object[]{ new XmlRpcInteger(3462), 3462 },
            new object[]{ new XmlRpcDouble(345.2), 345.2 },
            new object[]{ new XmlRpcBoolean(false), false },
            new object[]{ new XmlRpcBoolean(true), true },
            new object[]{ new XmlRpcString("Test String"), "Test String" },
            new object[]{ new XmlRpcBase64(Base64.FromBase64String("VGVzdCBTdHJpbmc=")), Base64.FromBase64String("VGVzdCBTdHJpbmc=") },
            new object[]{ new XmlRpcDateTime(DateTime.Parse("2021-04-06T16:36:44.1557489+02:00")), DateTime.Parse("2021-04-06T16:36:44.1557489+02:00") },
            new object[]{ new XmlRpcArray(new XmlRpcBaseType[] { 
                new XmlRpcInteger(1),
                new XmlRpcInteger(2),
                new XmlRpcString("3"),
                new XmlRpcDouble(4),
            }), new object[] { 1, 2, "3", (double)4 } },
            new object[]{ new XmlRpcStruct(new Struct() {
                { "Key1", new XmlRpcInteger(1) },
                { "Key2", new XmlRpcInteger(2) },
                { "Key3", new XmlRpcString("3") },
                { "Key4", new XmlRpcDouble(4) }
            }), new DynamicObject(){
                { "Key1", 1 },
                { "Key2", 2 },
                { "Key3", "3" },
                { "Key4", (double)4 }
            }}
        };

        [Theory]
        [MemberData(nameof(ToNativeValueData))]
        public static void ToNativeValue_Returns_Correct_Type_For_Various_Basic_XmlRpcTypes(XmlRpcBaseType element, object expected) {
            object result = XmlRpcTypes.ToNativeValue<object>(element);

            Assert.Equal(expected, result);
        }
    }
}
