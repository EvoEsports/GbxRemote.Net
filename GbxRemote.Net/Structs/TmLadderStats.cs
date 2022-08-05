namespace GbxRemoteNet.Structs;

public class TmLadderStats
{
    public double LastMatchScore { get; set; }
    public int NbrMatchWins { get; set; }
    public int NbrMatchDraws { get; set; }
    public int NbrMatchLosses { get; set; }
    public string TeamName { get; set; }
    public TmZoneRanking[] PlayerRankings { get; set; }
    public object[] TeamRankings { get; set; }
}