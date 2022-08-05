using System;

namespace GbxRemoteNet.Exceptions;

public class XmlRpcResponseException : InvalidOperationException
{
    public XmlRpcResponseException(string message) : base(message)
    {
        
    }
}