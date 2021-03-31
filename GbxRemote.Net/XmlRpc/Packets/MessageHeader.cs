using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GbxRemoteNet.XmlRpc.Packets {
    public class MessageHeader : IPacket {
        public int MessageLength;
        public uint Handle;

        /// <summary>
        /// Whether the message is a callback.
        /// </summary>
        public bool IsCallback => (Handle & 0x80000000) == 0;

        public MessageHeader() { }
        public MessageHeader(int length, uint handle) {
            MessageLength = length;
            Handle = handle;
        }

        public static async Task<MessageHeader> FromIOAsync(XmlRpcIO io) {
            int length = BitConverter.ToInt32(await io.ReadBytesAsync(4));
            uint handle = BitConverter.ToUInt32(await io.ReadBytesAsync(4));

            return new MessageHeader(length, handle);
        }

        public Task<byte[]> Serialize() {
            throw new NotImplementedException();
        }
    }
}
