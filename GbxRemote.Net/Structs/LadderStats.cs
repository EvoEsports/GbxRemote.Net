namespace GbxRemoteNet.Structs {
    public class LadderStats {
        public double LastMatchScore { get; set; }
        public int NbrMatchWins { get; set; }
        public int NbrMatchDraws { get; set; }
        public int NbrMatchLosses { get; set; }
        public string TeamName { get; set; }
        public ZoneRanking[] PlayerRankings { get; set; }
        public object[] TeamRankings { get; set; }
    }
}