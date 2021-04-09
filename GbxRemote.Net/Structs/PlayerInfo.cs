namespace GbxRemoteNet.Structs {
    public class PlayerInfo : MinimalPlayerInfo {
        public string NickName;
        public int PlayerId;
        public int TeamId;
        public int IsSpectator;
        public bool IsInOfficialMode;
        public int LadderRanking;
        public int SpectatorStatus;
        public int Flags;

        // Flags
        public int ForceSpectator;
        public bool IsReferee;
        public bool IsPodiumReady;
        public bool IsUsingStereoscopy;
        public bool IsManagedByAnOtherServer;
        public bool IsServer;
        public bool HasPlayerSlot;
        public bool IsBroadcasting;
        public bool HasJoinedGame;

        // SpectatorStatus
        public bool Spectator;
        public bool TemporarySpectator;
        public bool PureSpectator;
        public bool AutoTarget;
        public int CurrentTargetId;
    }
}