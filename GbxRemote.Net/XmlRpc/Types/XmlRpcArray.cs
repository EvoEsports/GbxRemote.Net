using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace GbxRemoteNet.XmlRpc.Types;

/// <summary>
///     Represents an XML-RPC array-
/// </summary>
public class XmlRpcArray : XmlRpcBaseType, IEquatable<XmlRpcArray>
{
    /// <summary>
    ///     The values within this array.
    /// </summary>
    public XmlRpcBaseType[] Values;

    /// <summary>
    ///     Create a new array instance from a set of XML-RPC values.
    /// </summary>
    /// <param name="values">Values of the array.</param>
    public XmlRpcArray(XmlRpcBaseType[] values) : base(null)
    {
        Values = values;
    }

    /// <summary>
    ///     Parse a XML element and create the array instance from it.
    /// </summary>
    /// <param name="element">Element to parse.</param>
    public XmlRpcArray(XElement element) : base(element)
    {
        var arrayValues = element.Elements(XmlRpcElementNames.Data)
            .First()
            .Elements(XmlRpcElementNames.Value);
        List<XmlRpcBaseType> values = new();

        foreach (var valueElement in arrayValues)
        {
            var value = XmlRpcTypes.ElementToInstance(valueElement.Elements().First());
            values.Add(value);
        }

        Values = values.ToArray();
    }

    /// <summary>
    ///     Check the equality of another array.
    /// </summary>
    /// <param name="other">Other array to check.</param>
    /// <returns>True if equal, false if not.</returns>
    public bool Equals(XmlRpcArray other)
    {
        return Values.SequenceEqual(other.Values);
    }

    /// <summary>
    ///     Check the equality of another array.
    /// </summary>
    /// <param name="obj">Other array to check.</param>
    /// <returns>True if equal, false if not.</returns>
    public override bool Equals(object obj)
    {
        return Equals((XmlRpcArray) obj);
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
        XElement arrayElement = new(XmlRpcElementNames.Array);
        XElement dataElement = new(XmlRpcElementNames.Data);
        arrayElement.Add(dataElement);

        foreach (var value in Values)
            dataElement.Add(new XElement(XmlRpcElementNames.Value,
                value.GetXml()
            ));

        return arrayElement;
    }
}