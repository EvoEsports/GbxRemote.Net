using System;
using GbxRemoteNet.XmlRpc.Packets;

namespace GbxRemoteNet.Events;

public class CallbackEventArgs<T> : EventArgs
{
    public MethodCall Call { get; set; }
    public T[] Parameters { get; set; }
}