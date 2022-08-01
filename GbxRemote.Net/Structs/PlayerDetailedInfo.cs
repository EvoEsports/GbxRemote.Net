namespace GbxRemoteNet.Structs {
    public class PlayerDetailedInfo : MinimalPlayerInfo {
        public string NickName { get; set; } 
        public int PlayerId { get; set; }
        public int TeamId { get; set; }
        public string Path { get; set; }
        public string Language { get; set; }
        public string ClientVersion { get; set; }
        public string ClientTitleVersion { get; set; }
        public string IPAddress { get; set; }
        public int DownloadRate { get; set; }
        public int UploadRate { get; set; }
        public bool IsInOfficialMode { get; set; }
        public bool IsReferee { get; set; }
        public PackDesc Avatar { get; set; }
        public Skin[] Skins { get; set; }
        public LadderStats LadderStats { get; set; }
        public int HoursSinceZoneInscription { get; set; }
        public string BroadcasterLogin { get; set; }
        public string[] Allies { get; set; }
        public string ClubLink { get; set; }
        public bool IsSpectator { get; set; }
    }
}