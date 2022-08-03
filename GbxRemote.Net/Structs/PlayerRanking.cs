using System;

namespace GbxRemoteNet.Structs;

public class PlayerRanking
{
    public string Login { get; set; }
    public string NickName { get; set; }
    public int PlayerId { get; set; }
    public int Rank { get; set; }

    [Obsolete] public int BestTime { get; set; }

    [Obsolete] public int[] BestCheckpoints { get; set; }

    [Obsolete] public int Score { get; set; }

    [Obsolete] public int NbrLapsFinished { get; set; }

    [Obsolete] public double LadderScore { get; set; }
}