using System.Threading.Tasks;

namespace GbxRemoteNet.XmlRpc.Packets;

public interface IPacket
{
    public Task<byte[]> Serialize();
}