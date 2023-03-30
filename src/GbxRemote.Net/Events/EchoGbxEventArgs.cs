using System;

namespace GbxRemoteNet.Events;

public class EchoGbxEventArgs : EventArgs
{
    /// <summary>
    /// First argument, in-case of a vote, it is the vote message.
    /// </summary>
    public string InternalParam { get; set; }
    /// <summary>
    /// Second argument.
    /// </summary>
    public string PublicParam { get; set; }
}