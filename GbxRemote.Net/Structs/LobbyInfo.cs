using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GbxRemoteNet.Structs {
    public class LobbyInfo {
        public bool IsLobby { get; set; }
        public int LobbyPlayers { get; set; }
        public int LobbyMaxPlayers { get; set; }
        public double LobbyPlayersLevel { get; set; }
    }
}
