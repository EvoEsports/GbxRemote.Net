namespace GbxRemoteNet.Structs {
    public class PlayerInfo : MinimalPlayerInfo {
        public string NickName { get; set; }
        public int PlayerId { get; set; }
        public int TeamId { get; set; }
        public bool IsInOfficialMode { get; set; }
        public int LadderRanking { get; set; }
        public int SpectatorStatus { get; set; }
        public int Flags { get; set; }
    }
}