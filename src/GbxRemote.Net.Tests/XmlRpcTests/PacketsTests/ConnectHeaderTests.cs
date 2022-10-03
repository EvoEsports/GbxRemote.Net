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
    public class ConnectHeaderTests : IClassFixture<MessageFixture> {
        MessageFixture fixture;

        public ConnectHeaderTests(MessageFixture fixture) {
            this.fixture = fixture;
        }

        [Fact]
        public void FromIOAsync_Parses_Correctly_From_Stream() {
            XmlRpcIO io = fixture.NewIO(fixture.ConnectionHeaderBytes);
            ConnectHeader header = ConnectHeader.FromIOAsync(io).GetAwaiter().GetResult();

            Assert.Equal(10, header.Length);
            Assert.Equal("Test Input", header.Protocol);
        }
    }
}
