using System;

namespace GbxRemoteNet.Structs {
    public class LadderStatsStruct {
        public float LastMatchScore;
        public int NbrMatchWins;
        public int NbrMatchDraws;
        public int NbrMatchLosses;
        public string TeamName;
        public ZoneRankingStruct[] PlayerRankings;
        public Array TeamRankings;
    }
}