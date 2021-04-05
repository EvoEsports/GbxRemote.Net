using GbxRemoteNet.XmlRpc;
using GbxRemoteNet.XmlRpc.Packets;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GbxRemote.Net.Tests.XmlRpcTests.PacketsTests {
    public class ConnectHeaderTests {
        [Fact]
        public void FromIOAsync_Parses_Correctly_From_Stream() {
            MemoryStream stream = new MemoryStream(
                new byte[] { 0xa, 0x0, 0x0, 0x0, 0x54, 0x65, 0x73, 0x74, 0x20, 0x49, 0x6e, 0x70, 0x75, 0x74 }
            );
            XmlRpcIO io = new XmlRpcIO(stream);
            ConnectHeader header = ConnectHeader.FromIOAsync(io).GetAwaiter().GetResult();

            Assert.Equal(10, header.Length);
            Assert.Equal("Test Input", header.Protocol);
        }
    }
}
