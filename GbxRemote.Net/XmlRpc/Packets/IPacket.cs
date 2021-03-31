using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GbxRemoteNet.XmlRpc.Packets {
    public interface IPacket {
        public Task<byte[]> Serialize();
    }
}
