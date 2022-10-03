using System;

namespace GbxRemoteNet.Exceptions;

public class InvalidProtocolException : InvalidOperationException
{
    public InvalidProtocolException(string protocol) : base($"Invalid protocol: {protocol}") {}
}