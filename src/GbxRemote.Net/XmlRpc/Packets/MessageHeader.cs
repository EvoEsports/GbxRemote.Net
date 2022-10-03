using System;
using System.Threading.Tasks;

namespace GbxRemoteNet.XmlRpc.Packets;

public class MessageHeader : IPacket
{
    public uint Handle;
    public int MessageLength;

    public MessageHeader()
    {
    }

    public MessageHeader(int length, uint handle)
    {
        MessageLength = length;
        Handle = handle;
    }

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