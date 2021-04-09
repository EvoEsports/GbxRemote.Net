using System;

namespace GbxRemoteNet.Structs {
    public class LadderStats {
        public float LastMatchScore;
        public int NbrMatchWins;
        public int NbrMatchDraws;
        public int NbrMatchLosses;
        public string TeamName;
        public ZoneRanking[] PlayerRankings;
        public Array TeamRankings;
    }
}