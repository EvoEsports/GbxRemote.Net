using GbxRemoteNet.Structs;

namespace GbxRemoteNet.Events;

public class ManiaLinkPageActionGbxEventArgs : PlayerGbxEventArgs
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
    public TmSEntryVal[] Entries { get; set; }
}