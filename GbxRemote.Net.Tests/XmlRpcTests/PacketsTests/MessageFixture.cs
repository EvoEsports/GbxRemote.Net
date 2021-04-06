using GbxRemoteNet.XmlRpc;
using GbxRemoteNet.XmlRpc.Packets;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GbxRemote.Net.Tests.XmlRpcTests.PacketsTests {
    public class MessageFixture : IDisposable {
        public readonly byte[] ConnectionHeaderBytes = new byte[] { 0xa, 0x0, 0x0, 0x0, 0x54, 0x65, 0x73, 0x74, 0x20, 0x49, 0x6e, 0x70, 0x75, 0x74 };

        public readonly byte[] MethodCallHeaderBytes = new byte[] { 0x63, 0x04, 0x0, 0x0, 0x05, 0x0, 0x0, 0x00 };

        public readonly byte[] MethodResponseHeaderBytes = new byte[] { 0x8c, 0x0, 0x0, 0x0, 0x04, 0x0, 0x0, 0x80 };

        public readonly string MethodCallString = @"<methodCall>
  <methodName>Example.Method.Name</methodName>
  <params>
    <param>
      <value>
        <string>Example Value 1</string>
      </value>
    </param>
    <param>
      <value>
        <i4>42</i4>
      </value>
    </param>
  </params>
</methodCall>";

        public readonly string FaultResponseString = @"<methodResponse>
    <fault>
        <value>
            <struct>
                <member>
                    <name>faultCode</name>
                    <value><int>4</int></value>
                    </member>
                <member>
                    <name>faultString</name>
                    <value><string>Too many parameters.</string></value>
                </member>
            </struct>
        </value>
    </fault>
</methodResponse>";

        public readonly string MethodResponseString = @"<methodResponse>
  <params>
    <param>
      <value>
        <string>Example Response Value</string>
      </value>
    </param>
  </params>
</methodResponse>";

        public readonly MessageHeader ExampleMethodResponseHeader = new(10, 0x80000005);
        public readonly MessageHeader ExampleMethodCallHeader = new(10, 0x5);

        public void Dispose() {
        }

        public XmlRpcIO NewIO(byte[] bytes) {
            MemoryStream ms = new(bytes);
            return new XmlRpcIO(ms);
        }

        public XmlRpcIO NewIO(string s) {
            return NewIO(Encoding.UTF8.GetBytes(s));
        }
    }
}
