using System;

namespace GbxRemoteNet.Events;

public class ScriptCloudGbxEventArgs : EventArgs
{
    public string Type { get; set; }
    public string Id { get; set; }
}