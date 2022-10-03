using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GbxRemoteNet.XmlRpc.Packets;

public class ConnectHeader : IPacket
{
    public int Length;
    public string Protocol;

    public ConnectHeader()
    {
    }

    public ConnectHeader(int length, string protocol)
    {
        Length = length;
        Protocol = protocol;
    }

    public bool IsValid => Protocol.Equals("GBXRemote 2");

    public Task<byte[]> Serialize()
    {
        throw new NotImplementedException();
    }

    public static async Task<ConnectHeader> FromIOAsync(XmlRpcIO io)
    {
        var length = BitConverter.ToInt32(await io.ReadBytesAsync(4));
        var protocol = Encoding.ASCII.GetString(await io.ReadBytesAsync(length));

        return new ConnectHeader(length, protocol);
    }

    public static async Task<ConnectHeader> FromIOAsync(XmlRpcIO io, CancellationToken cancellationToken)
    {
        var length = BitConverter.ToInt32(await io.ReadBytesAsync(4, cancellationToken));
        var protocol = Encoding.ASCII.GetString(await io.ReadBytesAsync(length, cancellationToken));

        return new ConnectHeader(length, protocol);
    }
}