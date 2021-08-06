namespace GbxRemoteNet.Structs {
    public class PlayerInfo : MinimalPlayerInfo {
        public string NickName { get; set; }
        public int PlayerId { get; set; }
        public int TeamId { get; set; }
        public int IsSpectator { get; set; }
        public bool IsInOfficialMode { get; set; }
        public int LadderRanking { get; set; }
        public int SpectatorStatus { get; set; }
        public int Flags { get; set; }

        // Flags
        public int ForceSpectator { get; set; }
        public bool IsReferee { get; set; }
        public bool IsPodiumReady { get; set; }
        public bool IsUsingStereoscopy { get; set; }
        public bool IsManagedByAnOtherServer { get; set; }
        public bool IsServer { get; set; }
        public bool HasPlayerSlot { get; set; }
        public bool IsBroadcasting { get; set; }
        public bool HasJoinedGame { get; set; }

        // SpectatorStatus
        public bool Spectator { get; set; }
        public bool TemporarySpectator { get; set; }
        public bool PureSpectator { get; set; }
        public bool AutoTarget { get; set; }
        public int CurrentTargetId { get; set; }
    }
}