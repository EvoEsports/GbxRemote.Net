using System.Xml.Linq;

namespace GbxRemoteNet.XmlRpc.Types;

/// <summary>
///     Base type for all XML-RPC types.
/// </summary>
public abstract class XmlRpcBaseType
{
    public XmlRpcBaseType(XElement element)
    {
    }

    /// <summary>
    ///     Generate the XML element for this value.
    /// </summary>
    /// <returns>Generated element</returns>
    public abstract XElement GetXml();
}