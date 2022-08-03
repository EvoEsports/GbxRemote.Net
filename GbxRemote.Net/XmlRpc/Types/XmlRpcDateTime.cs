using System;
using System.Xml.Linq;

namespace GbxRemoteNet.XmlRpc.Types;

/// <summary>
///     Represents an XML-RPC date/time object.
/// </summary>
public class XmlRpcDateTime : XmlRpcBaseType, IEquatable<XmlRpcDateTime>
{
    public DateTime Value;

    public XmlRpcDateTime(DateTime value) : base(null)
    {
        Value = value.ToUniversalTime();
    }

    public XmlRpcDateTime(XElement element) : base(element)
    {
        Value = DateTime.Parse(element.Value).ToUniversalTime();
    }

    public bool Equals(XmlRpcDateTime other)
    {
        return Value.Equals(other.Value);
    }

    public override bool Equals(object obj)
    {
        return Equals((XmlRpcDateTime) obj);
    }

    public override int GetHashCode()
    {
        return GetHashCode();
    }

    /// <summary>
    ///     Generate the XML element for this value.
    /// </summary>
    /// <returns>Generated element</returns>
    public override XElement GetXml()
    {
        return new XElement(XmlRpcElementNames.DateTime, Value.ToString("o") /* ISO 8601 */);
    }
}