using GbxRemoteNet.XmlRpc;
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
            new object[]{ new XElement("i4", "21"), new XmlRpcInteger(21) }
        };

        [Theory]
        [MemberData(nameof(ElementToInstanceData))]
        public void ElementToInstance_Converts_Types_Correctly(XElement element, object expected) {
            XmlRpcBaseType result = XmlRpcTypes.ElementToInstance(element);

            Assert.Equal(result, expected);
        }
    }
}
