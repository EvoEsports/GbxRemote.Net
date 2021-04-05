using GbxRemoteNet.XmlRpc.ExtraTypes;
using System;
using Xunit;

namespace GbxRemote.Net.Tests {
    public class ExtraTypes {
        [Fact]
        public void Base64_Constructor_Correctly_Parses_Encoded_Text() {
            Base64 base64 = new("VGVzdElucHV0");

            string result = base64.ToString();

            Assert.Equal("VGVzdElucHV0", result);
        }

        [Fact]
        public void Base64_FromBase64String_Parses_Correctly() {
            Base64 base64 = Base64.FromBase64String("VGVzdElucHV0");

            string result = base64.ToString();

            Assert.Equal("VGVzdElucHV0", result);
        }
    }
}
