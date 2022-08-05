using System;
using System.Xml.Linq;

namespace GbxRemoteNet.XmlRpc.Types;

/// <summary>
///     Represents an XML-RPC string.
/// </summary>
public class XmlRpcString : XmlRpcBaseType, IEquatable<XmlRpcString>
{
    public string Value;

    public XmlRpcString(string value) : base(null)
    {
        Value = value;
    }

    public XmlRpcString(XElement element) : base(element)
    {
        Value = element.Value;
    }

    public bool Equals(XmlRpcString other)
    {
        return Value.Equals(other.Value);
    }

    public override bool Equals(object obj)
    {
        return Equals((XmlRpcString) obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    /// <summary>
    ///     Generate the XML element for this value.
    /// </summary>
    /// <returns>Generated element</returns>
    public override XElement GetXml()
    {
        return new XElement(XmlRpcElementNames.String, Value);
    }
}