using GbxRemoteNet.XmlRpc.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GbxRemoteNet.XmlRpc.Packets {
    public class Message : IPacket {
        public MessageHeader Header;
        public string RawMessage;
        public XDocument MessageXml;
        public bool IsFault;
        public bool IsCallback => Header.IsCallback;
        public XmlRpcBaseType ResponseData;

        public Message() { }
        public Message(MessageHeader header, string message) {
            Header = header;
            RawMessage = message;
            MessageXml = XDocument.Parse(message);
            IsFault = false;

            if (!IsCallback) {
                IsFault = MessageXml.Elements(XmlRpcElementNames.MethodResponse)
                                    .First()
                                    .Elements(XmlRpcElementNames.Fault)
                                    .Any();
                ResponseData = GetResponseData();
            }
        }

        /// <summary>
        /// Convert the xml response to XML-RPC type data.
        /// </summary>
        /// <returns></returns>
        public XmlRpcBaseType GetResponseData() {
            if (IsCallback)
                throw new InvalidOperationException("Message is not a response.");

            XElement response = MessageXml.Elements(XmlRpcElementNames.MethodResponse).First();

            if (IsFault)
                return new XmlRpcFault(response.Elements(XmlRpcElementNames.Fault)
                                               .First()
                                               .Elements(XmlRpcElementNames.Value)
                                               .First()
                                               .Elements(XmlRpcElementNames.Struct)
                                               .First());

            XElement valueElement = response.Elements(XmlRpcElementNames.Params)
                                            .First()
                                            .Elements(XmlRpcElementNames.Param)
                                            .First()
                                            .Elements(XmlRpcElementNames.Value)
                                            .First()
                                            .Elements()
                                            .First();

            return XmlRpcTypes.ElementToInstance(valueElement);
        }

        public static async Task<Message> FromIOAsync(XmlRpcIO io) {
            var header = await MessageHeader.FromIOAsync(io);
            var message = Encoding.UTF8.GetString(await io.ReadBytesAsync(header.MessageLength));

            return new Message(header, message);
        }

        public Task<byte[]> Serialize() {
            throw new NotImplementedException();
        }
    }
}
