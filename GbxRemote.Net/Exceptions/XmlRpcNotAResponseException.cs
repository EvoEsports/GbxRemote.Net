namespace GbxRemoteNet.Exceptions;

public class XmlRpcNotAResponseException : XmlRpcResponseException
{
    public XmlRpcNotAResponseException() : base("Message is not a response.")
    {
    }
}