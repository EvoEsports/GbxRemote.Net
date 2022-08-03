using GbxRemoteNet.XmlRpc.ExtraTypes;

namespace GbxRemoteNet.Events;

public class TunnelDataEventArgs : PlayerEventArgs
{
    /// <summary>
    /// ID of the player on the server.
    /// </summary>
    public int PlayerId { get; set; }
    /// <summary>
    /// Data received from the player.
    /// </summary>
    public Base64 Data { get; set; }
}