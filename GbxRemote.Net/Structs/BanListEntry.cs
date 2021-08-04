using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GbxRemoteNet.Structs {
    public class BanListEntry {
        public string Login { get; set; }
        public string ClientName { get; set; }
        public string IPAddress { get; set; }
    }
}
