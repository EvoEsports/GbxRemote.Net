namespace GbxRemoteNet.Structs {
    public class ServerOptions {
        public string Name { get; set; }
        public string Comment { get; set; }
        public string Password { get; set; }
        public string PasswordForSpectator { get; set; }
        public int? CurrentMaxPlayers { get; set; }
        public int? NextMaxPlayers { get; set; }
        public int? CurrentMaxSpectators { get; set; }
        public int? NextMaxSpectators { get; set; }
        public bool? IsP2PUpload { get; set; }
        public bool? IsP2PDownload { get; set; }
        public int? CurrentLadderMode { get; set; }
        public int? NextLadderMode { get; set; }
        public int? CurrentVehicleNetQuality { get; set; }
        public int? NextVehicleNetQuality { get; set; }
        public int? CurrentCallVoteTimeOut { get; set; }
        public int? NextCallVoteTimeOut { get; set; }
        public double? CallVoteRatio { get; set; }
        public bool? AllowChallengeDownload { get; set; }
        public bool? AutoSaveReplays { get; set; }
        public bool? AllowMapDownload { get; set; }
        public bool? KeepPlayerSlots { get; set; }
        public string RefereePassword { get; set; }
        public int? RefereeMode { get; set; }
        public bool? AutoSaveValidationReplays { get; set; }
        public bool? HideServer { get; set; }
        public string CurrentUseChangingValidationSeed { get; set; }
        public string NextUseChangingValidationSeed { get; set; }
        public int? ClientInputsMaxLatency { get; set; }
        public bool? DisableHorns { get; set; }
        public bool? DisableServiceAnnounces { get; set; }
    }
}
