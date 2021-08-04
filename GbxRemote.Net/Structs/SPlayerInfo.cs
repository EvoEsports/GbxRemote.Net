using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GbxRemoteNet.Structs {
    public class SPlayerInfo {
		public string Login { get; set; }
		public string NickName { get; set; }
		public int PlayerId { get; set; }
		public int TeamId { get; set; }
		public int SpectatorStatus { get; set; }
		public int LadderRanking { get; set; }
		public int Flags { get; set; }
	}
}
