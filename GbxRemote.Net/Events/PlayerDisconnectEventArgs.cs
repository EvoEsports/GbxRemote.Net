namespace GbxRemoteNet.Events;

public class PlayerDisconnectEventArgs : PlayerEventArgs
{
    /// <summary>
    /// The reason the player disconnected.
    /// </summary>
    public string Reason { get; set; }
}