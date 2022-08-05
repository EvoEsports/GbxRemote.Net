using System;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using GbxRemoteNet.XmlRpc.ExtraTypes;

namespace GbxRemoteNet.XmlRpc.Types;

/// <summary>
///     Represents an XML-RPC struct.
/// </summary>
public class XmlRpcStruct : XmlRpcBaseType, IEquatable<XmlRpcStruct>
{
    public GbxStruct Fields;

    public XmlRpcStruct(GbxStruct fields) : base(null)
    {
        Fields = fields;
    }

    public XmlRpcStruct(XElement element) : base(element)
    {
        Fields = new GbxStruct();
        var members = element.Elements(XmlRpcElementNames.Member);

        foreach (var member in members)
        {
            var name = member.Elements(XmlRpcElementNames.Name).First().Value;
            var valueElement = member.Elements(XmlRpcElementNames.Value)
                .First()
                .Elements()
                .First();

            var value = XmlRpcTypes.ElementToInstance(valueElement);

            Fields.Add(name, value);
        }
    }

    /// <summary>
    ///     Create a struct from an object.
    /// </summary>
    /// <param name="obj"></param>
    public XmlRpcStruct(object obj) : base(null)
    {
        var t = obj.GetType();
        var fields = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        Fields = new GbxStruct();

        foreach (var field in fields)
            Fields.Add(field.Name, XmlRpcTypes.ToXmlRpcValue(t.GetProperty(field.Name).GetValue(obj)));
    }

    /// <summary>
    ///     Create a struct from a dynamic object.
    /// </summary>
    /// <param name="obj"></param>
    public XmlRpcStruct(DynamicObject obj) : base(null)
    {
        Fields = new GbxStruct();

        foreach (var kv in obj) Fields.Add(kv.Key, XmlRpcTypes.ToXmlRpcValue(kv.Value));
    }

    public bool Equals(XmlRpcStruct other)
    {
        return Fields.SequenceEqual(other.Fields);
    }

    public override bool Equals(object obj)
    {
        return Equals((XmlRpcStruct) obj);
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
        XElement structElement = new(XmlRpcElementNames.Struct);

        foreach (var kv in Fields)
            structElement.Add(new XElement(XmlRpcElementNames.Member,
                new XElement(XmlRpcElementNames.Name, kv.Key),
                new XElement(XmlRpcElementNames.Value, kv.Value.GetXml())
            ));

        return structElement;
    }
}