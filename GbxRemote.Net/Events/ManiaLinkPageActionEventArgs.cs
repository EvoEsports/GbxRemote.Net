using GbxRemoteNet.Structs;

namespace GbxRemoteNet.Events;

public class ManiaLinkPageActionEventArgs : PlayerEventArgs
{
    /// <summary>
    /// Player's server ID.
    /// </summary>
    public int PlayerId { get; set; }
    /// <summary>
    /// String representing the answer.
    /// </summary>
    public string Answer { get; set; }
    /// <summary>
    /// Key/Value of entries.
    /// </summary>
    public SEntryVal[] Entries { get; set; }
}