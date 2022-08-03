namespace GbxRemoteNet.Events;

public class PlayerEventArgs : EchoEventArgs
{
    /// <summary>
    /// Login name/id of the player.
    /// </summary>
    public string Login { get; set; }
}