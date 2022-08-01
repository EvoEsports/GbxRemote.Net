using System;

namespace GbxRemoteNet.Structs {
    [Obsolete]
    public class BillState {
        public string State { get; set; }
        public string StateName { get; set; }
        public int TransactionId { get; set; }
    }
}
