namespace GbxRemoteNet.Events;

public class PlayerConnectEventArgs : PlayerEventArgs
{
    /// <summary>
    /// Whether the player is in spectator mode.
    /// </summary>
    public bool IsSpectator { get; set; }
}