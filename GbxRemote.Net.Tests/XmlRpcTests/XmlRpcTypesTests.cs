using GbxRemoteNet.XmlRpc;
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
    }
}
