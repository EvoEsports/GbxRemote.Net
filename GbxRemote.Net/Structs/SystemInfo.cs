using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GbxRemoteNet.Structs {
    public class SystemInfo {
        public string PublishedIp;
        public int Port;
        public int P2PPort;
        public string TitleId;
        public string ServerLogin;
        public int ServerPlayerId;
        public int ConnectionDownloadRate;
        public int ConnectionUploadRate;
        public bool IsServer;
        public bool IsDedicated;
    }
}
