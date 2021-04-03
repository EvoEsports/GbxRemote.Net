namespace GbxRemoteNet.Structs {
    public class PlayerDetailedInfoStruct : PlayerStruct {
        public string NickName;
        public int PlayerId;
        public int TeamId;
        public string Path;
        public string Language;
        public string ClientVersion;
        public string ClientTitleVersion;
        public string IPAddress;
        public int DownloadRate;
        public int UploadRate;
        public bool IsSpectator;
        public bool IsInOfficialMode;
        public bool IsReferee;
        public FileDescStruct Avatar;
        public SkinStruct[] Skins;
        public LadderStatsStruct LadderStats;
        public int HoursSinceZoneInscription;
        public string BroadcasterLogin;
        public string[] Allies;
        public string ClubLink;
    }
}