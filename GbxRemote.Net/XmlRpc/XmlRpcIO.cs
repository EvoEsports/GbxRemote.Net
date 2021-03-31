using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace GbxRemoteNet.XmlRpc {
    public class XmlRpcIO {
        TcpClient tcpClient;
        Stream stream;

        public XmlRpcIO(TcpClient tcpClient) {
            stream = tcpClient.GetStream();
        }

        public async Task<byte[]> ReadBytesAsync(int n) {
            byte[] data = new byte[n];
            int count = 0;
            while (n - count > 0) {
                count += await stream.ReadAsync(data, count, n - count);
            }
            return data;
        }

        public async Task WriteBytesAsync(byte[] bytes) {
            await stream.WriteAsync(bytes, 0, bytes.Length);
            await stream.FlushAsync();
        }
    }
}
