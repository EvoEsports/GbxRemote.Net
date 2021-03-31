using GbxRemoteNet.XmlRpc.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GbxRemoteNet.XmlRpc.Packets {
    public class MethodCall : IPacket {
        public string Method;
        public object[] Arguments;
        public uint Handle;
        public XmlRpcCall Call;

        public MethodCall(string method, uint handle, XmlRpcBaseType[] args) {
            Method = method;
            Arguments = args;
            Call = new(method, args);
            Handle = handle;
        }

        public async Task<byte[]> Serialize() {
            string xml = Call.GenerateXML();

            byte[] lenBytes = BitConverter.GetBytes(xml.Length);
            byte[] handleBytes = BitConverter.GetBytes(Handle);
            byte[] xmlBytes = Encoding.UTF8.GetBytes(xml);
            byte[] serialized = new byte[lenBytes.Length + handleBytes.Length + xmlBytes.Length];

            Buffer.BlockCopy(lenBytes, 0, serialized, 0, lenBytes.Length);
            Buffer.BlockCopy(handleBytes, 0, serialized, lenBytes.Length, handleBytes.Length);
            Buffer.BlockCopy(xmlBytes, 0, serialized, lenBytes.Length+handleBytes.Length, xmlBytes.Length);

            return serialized;
        }
    }
}
