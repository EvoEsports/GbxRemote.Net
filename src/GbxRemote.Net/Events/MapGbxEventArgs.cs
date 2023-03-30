using System;
using GbxRemoteNet.Structs;

namespace GbxRemoteNet.Events;

public class MapGbxEventArgs : EventArgs
{
    /// <summary>
    /// Information about the map that will be/was played.
    /// </summary>
    public TmSMapInfo Map { get; set; }
}