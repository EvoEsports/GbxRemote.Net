using System;
using System.Threading.Tasks;

namespace GbxRemoteNet.XmlRpc.Packets;

public class MessageHeader(int length, uint handle) : IPacket
{
    public uint Handle = handle;
    public int MessageLength = length;

    /// <summary>
    ///     Whether the message is a callback.
    /// </summary>
    public bool IsCallback => (Handle & 0x80000000) == 0;

    public Task<byte[]> Serialize()
    {
        throw new NotImplementedException();
    }

    public static async Task<MessageHeader> FromIOAsync(XmlRpcIO io)
    {
        var length = BitConverter.ToInt32(await io.ReadBytesAsync(4));
        var handle = BitConverter.ToUInt32(await io.ReadBytesAsync(4));

        return new MessageHeader(length, handle);
    }
}