using System;

namespace GbxRemoteNet.Exceptions;

public class InvalidProtocolException(string protocol) : InvalidOperationException($"Invalid protocol: {protocol}");