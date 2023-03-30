namespace GbxRemoteNet.Events;

public class PlayerDisconnectGbxEventArgs : PlayerGbxEventArgs
{
    /// <summary>
    /// The reason the player disconnected.
    /// </summary>
    public string Reason { get; set; }
}