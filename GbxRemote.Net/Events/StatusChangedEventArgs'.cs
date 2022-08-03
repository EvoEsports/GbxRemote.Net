namespace GbxRemoteNet.Events;

public class StatusChangedEventArgs
{
    /// <summary>
    /// Code/ID of the status.
    /// </summary>
    public int StatusCode { get; set; }
    /// <summary>
    /// A friendly string that represents the status.
    /// </summary>
    public string StatusName { get; set; }
}