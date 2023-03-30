using GbxRemoteNet.XmlRpc.ExtraTypes;

namespace GbxRemoteNet.Events;

public class TunnelDataGbxEventArgs : PlayerGbxEventArgs
{
    /// <summary>
    /// ID of the player on the server.
    /// </summary>
    public int PlayerId { get; set; }
    /// <summary>
    /// Data received from the player.
    /// </summary>
    public GbxBase64 Data { get; set; }
}