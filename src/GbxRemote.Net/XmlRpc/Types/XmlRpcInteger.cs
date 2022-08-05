using System;
using System.Globalization;
using System.Xml.Linq;

namespace GbxRemoteNet.XmlRpc.Types;

/// <summary>
///     Represents an XML-RPC integer.
/// </summary>
public class XmlRpcInteger : XmlRpcBaseType, IEquatable<XmlRpcInteger>
{
    public int Value;

    public XmlRpcInteger(int value) : base(null)
    {
        Value = value;
    }

    public XmlRpcInteger(XElement element) : base(element)
    {
        Value = Convert.ToInt32(element.Value, CultureInfo.InvariantCulture);
    }

    public bool Equals(XmlRpcInteger other)
    {
        return Value.Equals(other.Value);
    }

    public override bool Equals(object obj)
    {
        return Equals((XmlRpcInteger) obj);
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
        return new XElement(XmlRpcElementNames.Integer, Value);
    }
}