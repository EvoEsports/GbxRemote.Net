using System;
using System.Xml.Linq;

namespace GbxRemoteNet.XmlRpc.Types;

/// <summary>
///     Represents an XML-RPC fault.
/// </summary>
public class XmlRpcFault : XmlRpcBaseType, IEquatable<XmlRpcFault>
{
    public int FaultCode;
    public string FaultString;

    public XmlRpcFault(int faultCode, string faultString) : base(null)
    {
        FaultCode = faultCode;
        FaultString = faultString;
    }

    public XmlRpcFault(XElement element) : base(element)
    {
        XmlRpcStruct faultStruct = new(element);
        FaultCode = ((XmlRpcInteger) faultStruct.Fields["faultCode"]).Value;
        FaultString = ((XmlRpcString) faultStruct.Fields["faultString"]).Value;
    }

    public bool Equals(XmlRpcFault other)
    {
        return FaultCode.Equals(other.FaultCode) && FaultString.Equals(other.FaultString);
    }

    /// <summary>
    ///     Generate the XML element for this value.
    /// </summary>
    /// <returns>Generated element</returns>
    public override XElement GetXml()
    {
        throw new NotImplementedException();
    }

    public override bool Equals(object obj)
    {
        return Equals((XmlRpcFault) obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string ToString()
    {
        return $"({FaultCode}) {FaultString}";
    }
}