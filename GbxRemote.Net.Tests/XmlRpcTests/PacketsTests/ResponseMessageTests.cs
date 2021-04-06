using GbxRemoteNet.XmlRpc;
using GbxRemoteNet.XmlRpc.Packets;
using GbxRemoteNet.XmlRpc.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GbxRemote.Net.Tests.XmlRpcTests.PacketsTests {
    public class ResponseMessageTests : IClassFixture<MessageFixture> {
        MessageFixture fixture;

        public ResponseMessageTests(MessageFixture fixture) {
            this.fixture = fixture;
        }

        [Fact]
        public void Constructor_Correctly_Parses_Method_Response() {
            ResponseMessage response = new(fixture.ExampleMethodResponseHeader, fixture.MethodResponseString);

            var responseValue = ((XmlRpcString)response.ResponseData).Value;

            Assert.False(response.IsFault);
            Assert.False(response.IsCallback);
            Assert.Equal("Example Response Value", responseValue);
        }

        [Fact]
        public void Detects_And_Extracts_Fault_Message() {
            ResponseMessage response = new(fixture.ExampleMethodResponseHeader, fixture.FaultResponseString);

            var fault = (XmlRpcFault)response.ResponseData;

            Assert.True(response.IsFault);
            Assert.Equal(4, fault.FaultCode);
            Assert.Equal("Too many parameters.", fault.FaultString);
        }

        [Fact]
        public void Detects_If_Message_Is_Callback() {
            ResponseMessage response = new(fixture.ExampleMethodCallHeader, fixture.MethodCallString);

            Assert.True(response.IsCallback);
        }

        [Fact]
        public void FromIOAsync_Correctly_Parses_Message() {
            XmlRpcIO io = fixture.NewIO(fixture.MethodResponseBytes);
            ResponseMessage response = ResponseMessage.FromIOAsync(io).GetAwaiter().GetResult();

            var responseValue = ((XmlRpcString)response.ResponseData).Value;

            Assert.False(response.IsCallback);
            Assert.False(response.IsFault);
            Assert.Equal("Example Response Value", responseValue);
        }
    }
}
