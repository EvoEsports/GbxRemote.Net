using GbxRemoteNet.XmlRpc.ExtraTypes;
using System;
using Xunit;

namespace GbxRemote.Net.Tests.XmlRpcTests.ExtraTypesTests {
    public class Base64Tests {
        [Fact]
        public void Base64_String_Constructor_Correctly_Encodes_Bytes() {
            GbxBase64 base64 = new("Test Input");

            Assert.Equal(
                new byte[] { 0x54, 0x65, 0x73, 0x74, 0x20, 0x49, 0x6e, 0x70, 0x75, 0x74 }, 
                base64.Data
            );
        }

        [Fact]
        public void Base64_FromBase64String_Parses_Correctly() {
            GbxBase64 base64 = GbxBase64.FromBase64String("VGVzdCBJbnB1dA==");

            Assert.Equal(
                new byte[] { 0x54, 0x65, 0x73, 0x74, 0x20, 0x49, 0x6e, 0x70, 0x75, 0x74 },
                base64.Data
            );
        }

        [Fact]
        public void Base64_Correctly_Encodes_Into_Base64() {
            GbxBase64 base64 = new("Test Input");

            string result = base64.ToString();

            Assert.Equal("VGVzdCBJbnB1dA==", result);
        }
    }
}
