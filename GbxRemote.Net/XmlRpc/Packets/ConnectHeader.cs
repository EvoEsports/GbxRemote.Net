using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GbxRemoteNet.XmlRpc.Packets {
    public class ConnectHeader : IPacket {
        public int Length;
        public string Protocol;

        public bool IsValid => Protocol.Equals("GBXRemote 2");

        public ConnectHeader() { }
        public ConnectHeader(int length, string protocol) {
            Length = length;
            Protocol = protocol;
        }

        public static async Task<ConnectHeader> FromIOAsync(XmlRpcIO io) {
            int length = BitConverter.ToInt32(await io.ReadBytesAsync(4));
            string protocol = Encoding.ASCII.GetString(await io.ReadBytesAsync(length));
            
            return new ConnectHeader(length, protocol);
        }
        
        public static async Task<ConnectHeader> FromIOAsync(XmlRpcIO io, CancellationToken cancellationToken) {
            int length = BitConverter.ToInt32(await io.ReadBytesAsync(4, cancellationToken));
            string protocol = Encoding.ASCII.GetString(await io.ReadBytesAsync(length, cancellationToken));
            
            return new ConnectHeader(length, protocol);
        }

        public Task<byte[]> Serialize() {
            throw new NotImplementedException();
        }
    }
}
