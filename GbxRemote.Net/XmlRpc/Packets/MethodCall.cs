using GbxRemoteNet.XmlRpc.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GbxRemoteNet.XmlRpc.Packets
{
    public class MethodCall : IPacket
    {
        public string Method;
        public XmlRpcBaseType[] Arguments;
        public uint Handle;
        public XmlRpcCall Call;

        public MethodCall(string method, uint handle, XmlRpcBaseType[] args)
        {
            Method = method;
            Arguments = args;
            Call = new(method, args);
            Handle = handle;
        }

        public MethodCall(ResponseMessage response)
        {
            if (!response.IsCallback)
                throw new InvalidOperationException("Response must be a callback.");

            Handle = response.Header.Handle;

            // get method name
            var xmlCall = response.MessageXml.Elements(XmlRpcElementNames.MethodCall).First();
            Method = xmlCall.Elements(XmlRpcElementNames.MethodName).First().Value;

            // parse method parameters
            var xmlPars = xmlCall.Elements(XmlRpcElementNames.Params)
                                 .First()
                                 .Elements(XmlRpcElementNames.Param);

            Arguments = new XmlRpcBaseType[xmlPars.Count()];

            for (int i = 0; i < Arguments.Length; i++)
            {
                var xmlValue = xmlPars.ElementAt(i)
                                      .Elements(XmlRpcElementNames.Value)
                                      .First()
                                      .Elements()
                                      .First();

                Arguments[i] = XmlRpcTypes.ElementToInstance(xmlValue);
            }

            Call = new(Method, Arguments);
        }

        public async Task<byte[]> Serialize()
        {
            string xml = Call.GenerateXML();

            byte[] handleBytes = BitConverter.GetBytes(Handle);
            byte[] xmlBytes = Encoding.UTF8.GetBytes(xml);
            byte[] lenBytes = BitConverter.GetBytes(xmlBytes.Length);
            byte[] serialized = new byte[lenBytes.Length + handleBytes.Length + xmlBytes.Length];

            Buffer.BlockCopy(lenBytes, 0, serialized, 0, lenBytes.Length);
            Buffer.BlockCopy(handleBytes, 0, serialized, lenBytes.Length, handleBytes.Length);
            Buffer.BlockCopy(xmlBytes, 0, serialized, lenBytes.Length + handleBytes.Length, xmlBytes.Length);

            return serialized;
        }
    }
}
