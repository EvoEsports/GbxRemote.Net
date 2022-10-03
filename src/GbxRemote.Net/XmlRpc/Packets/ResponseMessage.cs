using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using GbxRemoteNet.Exceptions;
using GbxRemoteNet.XmlRpc.Types;

namespace GbxRemoteNet.XmlRpc.Packets;

public class ResponseMessage : IPacket
{
    public MessageHeader Header;
    public bool IsFault;
    public XDocument MessageXml;
    public string RawMessage;
    public XmlRpcBaseType ResponseData;

    public ResponseMessage()
    {
    }

    public ResponseMessage(MessageHeader header, string message)
    {
        Header = header;
        RawMessage = message;
        MessageXml = XDocument.Parse(message);
        IsFault = false;

        if (!IsCallback)
        {
            IsFault = MessageXml.Elements(XmlRpcElementNames.MethodResponse)
                .First()
                .Elements(XmlRpcElementNames.Fault)
                .Any();
            ResponseData = GetResponseData();
        }
    }

    public bool IsCallback => Header.IsCallback;

    public Task<byte[]> Serialize()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     Convert the xml response to XML-RPC type data.
    /// </summary>
    /// <returns></returns>
    public XmlRpcBaseType GetResponseData()
    {
        if (IsCallback)
            throw new XmlRpcNotAResponseException();

        var response = MessageXml.Elements(XmlRpcElementNames.MethodResponse).First();

        if (IsFault)
            return new XmlRpcFault(response.Elements(XmlRpcElementNames.Fault)
                .First()
                .Elements(XmlRpcElementNames.Value)
                .First()
                .Elements(XmlRpcElementNames.Struct)
                .First());

        var valueElement = response.Elements(XmlRpcElementNames.Params)
            .First()
            .Elements(XmlRpcElementNames.Param)
            .First()
            .Elements(XmlRpcElementNames.Value)
            .First()
            .Elements()
            .First();

        return XmlRpcTypes.ElementToInstance(valueElement);
    }

    public static async Task<ResponseMessage> FromIOAsync(XmlRpcIO io)
    {
        var header = await MessageHeader.FromIOAsync(io);
        var message = Encoding.UTF8.GetString(await io.ReadBytesAsync(header.MessageLength));

        return new ResponseMessage(header, message);
    }
}