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
    }
}
