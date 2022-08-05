using System;

namespace GbxRemoteNet.Events;

public class MapListModifiedEventArgs : EventArgs
{
    /// <summary>
    /// Index of the current map.
    /// </summary>
    public int CurrentMapIndex { get; set; }
    /// <summary>
    /// Index of the next map.
    /// </summary>
    public int NextMapIndex { get; set; }
    /// <summary>
    /// Whether the map list was modified or not.
    /// </summary>
    public bool IsListModified { get; set; }
}