using System;

namespace GbxRemoteNet.Events;

public class ScriptCloudEventArgs : EventArgs
{
    public string Type { get; set; }
    public string Id { get; set; }
}