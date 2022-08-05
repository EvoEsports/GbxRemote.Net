namespace GbxRemoteNet.Exceptions;

public class XmlRpcResponseNotACallbackException : XmlRpcResponseException
{
    public XmlRpcResponseNotACallbackException() : base("Response must be a callback.")
    {
    }
}