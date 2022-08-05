namespace GbxRemoteNet.XmlRpc;

/// <summary>
///     The defined XML element names as defined by the XML-RPC protocol.
/// </summary>
public class XmlRpcElementNames
{
    public const string MethodCall = "methodCall";
    public const string MethodResponse = "methodResponse";
    public const string MethodName = "methodName";

    public const string Params = "params";
    public const string Fault = "fault";
    public const string Param = "param";
    public const string Value = "value";
    public const string Member = "member";

    public const string Struct = "struct";
    public const string Array = "array";
    public const string Name = "name";
    public const string Data = "data";

    public const string Integer = "int";
    public const string I4 = "i4";
    public const string Boolean = "boolean";
    public const string String = "string";
    public const string Double = "double";
    public const string DateTime = "dateTime.iso8601";
    public const string Base64 = "base64";
}