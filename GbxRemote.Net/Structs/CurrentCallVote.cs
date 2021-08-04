using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GbxRemoteNet.Structs {
    public class CurrentCallVote {
        public string CallerLogin { get; set; }
        public string CmdName { get; set; }
        public string CmdParam { get; set; }
    }
}
