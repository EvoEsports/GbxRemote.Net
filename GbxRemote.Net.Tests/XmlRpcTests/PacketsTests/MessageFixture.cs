using GbxRemoteNet.XmlRpc;
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

        public void Dispose() {
        }

        public XmlRpcIO NewIO(byte[] bytes) {
            MemoryStream ms = new(bytes);
            return new XmlRpcIO(ms);
        }
    }
}
