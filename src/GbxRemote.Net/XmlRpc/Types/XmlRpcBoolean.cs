using System;
using System.Xml.Linq;

namespace GbxRemoteNet.XmlRpc.Types;

/// <summary>
///     Represents an XML-RPC boolean.
/// </summary>
public class XmlRpcBoolean : XmlRpcBaseType, IEquatable<XmlRpcBoolean>
{
    public bool Value;

    public XmlRpcBoolean(bool value) : base(null)
    {
        Value = value;
    }

    public XmlRpcBoolean(XElement element) : base(element)
    {
        var value = element.Value.Trim();

        if (value == "0" || value == "1")
            Value = Convert.ToBoolean(Convert.ToInt32(value));
        else
            Value = Convert.ToBoolean(value);
    }

    public bool Equals(XmlRpcBoolean other)
    {
        return Value.Equals(other.Value);
    }

    public override bool Equals(object obj)
    {
        return Equals((XmlRpcBoolean) obj);
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
        return new XElement(XmlRpcElementNames.Boolean, Value ? '1' : '0');
    }
}