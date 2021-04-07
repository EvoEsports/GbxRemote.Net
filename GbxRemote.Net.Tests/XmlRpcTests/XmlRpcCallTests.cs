using GbxRemoteNet.XmlRpc;
using GbxRemoteNet.XmlRpc.ExtraTypes;
using GbxRemoteNet.XmlRpc.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GbxRemote.Net.Tests.XmlRpcTests {
    public class XmlRpcCallTests {
        private class XmlRpcRequestTest : XmlRpcRequest {
            public XmlRpcRequestTest() : base("TestElement") {
            }
        }

        [Fact]
        public void XmlRpcRequest_Correctly_Generates_XML_Output() {
            XmlRpcRequestTest test = new();

            string xml = test.GenerateXML();
            string expected = @"<?xml version=""1.0"" encoding=""utf-16""?>
<TestElement />";

            Assert.Equal(expected, xml);
        }

        [Fact]
        public void XmlRpcCall_Generates_Correct_Method_Call_With_Various_Params() {
            XmlRpcCall call = new("TestMethod",
                new XmlRpcString("Test String"),
                new XmlRpcInteger(123),
                new XmlRpcDouble(2134.523),
                new XmlRpcBase64(Base64.FromBase64String("VGVzdCBTdHJpbmc=")),
                new XmlRpcBoolean(true),
                new XmlRpcDateTime(DateTime.Parse("2021-04-06T16:36:44.1557489+02:00")),
                new XmlRpcArray(new XmlRpcBaseType[] {
                    new XmlRpcInteger(1),
                    new XmlRpcInteger(2),
                    new XmlRpcInteger(3)
                }),
                new XmlRpcStruct(new Struct() {
                    { "Key1", new XmlRpcString("Value 1") },
                    { "Key2", new XmlRpcString("Value 2") },
                    { "Key3", new XmlRpcString("Value 3") }
                })
            );

            string value = call.GenerateXML();
            string expected = @"<?xml version=""1.0"" encoding=""utf-16""?>
<methodCall>
  <methodName>TestMethod</methodName>
  <params>
    <param>
      <value>
        <string>Test String</string>
      </value>
    </param>
    <param>
      <value>
        <int>123</int>
      </value>
    </param>
    <param>
      <value>
        <double>2134.523</double>
      </value>
    </param>
    <param>
      <value>
        <base64>VGVzdCBTdHJpbmc=</base64>
      </value>
    </param>
    <param>
      <value>
        <boolean>1</boolean>
      </value>
    </param>
    <param>
      <value>
        <dateTime.iso8601>2021-04-06T16:36:44.1557489+02:00</dateTime.iso8601>
      </value>
    </param>
    <param>
      <value>
        <array>
          <data>
            <value>
              <int>1</int>
            </value>
            <value>
              <int>2</int>
            </value>
            <value>
              <int>3</int>
            </value>
          </data>
        </array>
      </value>
    </param>
    <param>
      <value>
        <struct>
          <member>
            <name>Key1</name>
            <value>
              <string>Value 1</string>
            </value>
          </member>
          <member>
            <name>Key2</name>
            <value>
              <string>Value 2</string>
            </value>
          </member>
          <member>
            <name>Key3</name>
            <value>
              <string>Value 3</string>
            </value>
          </member>
        </struct>
      </value>
    </param>
  </params>
</methodCall>";

            Assert.Equal(expected, value);
        }
    }
}
