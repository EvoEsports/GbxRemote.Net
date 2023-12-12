using System;

namespace GbxRemoteNet.Exceptions;

public class XmlRpcResponseException(string message) : InvalidOperationException(message);