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
    public class MessageHeaderTests : IClassFixture<MessageFixture> {
        MessageFixture fixture;

        public MessageHeaderTests(MessageFixture fixture) {
            this.fixture = fixture;
        }

        [Fact]
        public void FromIOAsync_Correctly_Parses_MethodCall_Message() {
            XmlRpcIO io = fixture.NewIO(fixture.MethodCallHeaderBytes);

            MessageHeader header = MessageHeader.FromIOAsync(io).GetAwaiter().GetResult();

            Assert.Equal(1123, header.MessageLength);
            Assert.Equal((uint)5, header.Handle);
        }

        [Fact]
        public void FromIOAsync_Correctly_Parses_MethodResponse_Message() {
            XmlRpcIO io = fixture.NewIO(fixture.MethodResponseBytes);

            MessageHeader header = MessageHeader.FromIOAsync(io).GetAwaiter().GetResult();

            Assert.Equal(159, header.MessageLength);
            Assert.Equal(0x80000004, header.Handle);
        }

        [Fact]
        public void IsCallback_Returns_True_On_MethodCall() {
            XmlRpcIO io = fixture.NewIO(fixture.MethodCallHeaderBytes);

            MessageHeader header = MessageHeader.FromIOAsync(io).GetAwaiter().GetResult();

            Assert.True(header.IsCallback);
        }

        [Fact]
        public void IsCallback_Returns_False_On_MethodResponse() {
            XmlRpcIO io = fixture.NewIO(fixture.MethodResponseBytes);

            MessageHeader header = MessageHeader.FromIOAsync(io).GetAwaiter().GetResult();

            Assert.False(header.IsCallback);
        }
    }
}
