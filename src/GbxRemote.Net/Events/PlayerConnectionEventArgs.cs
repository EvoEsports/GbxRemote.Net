namespace GbxRemoteNet.Events;

public class PlayerConnectGbxEventArgs : PlayerGbxEventArgs
{
    /// <summary>
    /// Whether the player is in spectator mode.
    /// </summary>
    public bool IsSpectator { get; set; }
}