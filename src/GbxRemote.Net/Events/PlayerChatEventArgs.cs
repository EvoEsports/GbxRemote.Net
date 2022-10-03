namespace GbxRemoteNet.Events;

public class PlayerChatEventArgs : PlayerEventArgs
{
    /// <summary>
    /// The Id of the player on the server.
    /// </summary>
    public int PlayerId { get; set; }
    /// <summary>
    /// Contents of the chat message.
    /// </summary>
    public string Text { get; set; }
    /// <summary>
    /// Whether the message is a command or not.
    /// </summary>
    public bool IsRegisteredCmd { get; set; }
}