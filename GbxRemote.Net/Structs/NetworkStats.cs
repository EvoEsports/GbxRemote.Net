using System;

namespace GbxRemoteNet.Structs {
    [Obsolete]
    public class NetworkStats {
        public int Uptime { get; set; }
        public int NbrConnection { get; set; }
        public int MeanConnectionTime { get; set; }
        public int MeanNbrPlayer { get; set; }
        public int RecvNetRate { get; set; }
        public int SendNetRate { get; set; }
        public int TotalReceivingSize { get; set; }
        public int TotalSendingSize { get; set; }
    }
}