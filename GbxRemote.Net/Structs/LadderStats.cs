using System;

namespace GbxRemoteNet.Structs {
    public class LadderStats {
        public double LastMatchScore;
        public int NbrMatchWins;
        public int NbrMatchDraws;
        public int NbrMatchLosses;
        public string TeamName;
        public ZoneRanking[] PlayerRankings;
        public object[] TeamRankings;
    }
}