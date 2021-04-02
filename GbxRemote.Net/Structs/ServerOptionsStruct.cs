using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GbxRemoteNet.Structs {
    public class ServerOptionsStruct {
        public string? Name;
        public string? Comment;
        public string? Password;
        public string? PasswordForSpectator;
        public int? CurrentMaxPlayers;
        public int? NextMaxPlayers;
        public int? CurrentMaxSpectators;
        public int? NextMaxSpectators;
        public bool? IsP2PUpload;
        public bool? IsP2PDownload;
        public int? CurrentLadderMode;
        public int? NextLadderMode;
        public int? CurrentVehicleNetQuality;
        public int? NextVehicleNetQuality;
        public int? CurrentCallVoteTimeOut;
        public int? NextCallVoteTimeOut;
        public double? CallVoteRatio;
        public bool? AllowChallengeDownload;
        public bool? AutoSaveReplays;
        public bool? AllowMapDownload;
        public bool? KeepPlayerSlots;
        public string? RefereePassword;
        public int? RefereeMode;
        public bool? AutoSaveValidationReplays;
        public bool? HideServer;
        public string? CurrentUseChangingValidationSeed;
        public string? NextUseChangingValidationSeed;
        public int? ClientInputsMaxLatency;
        public bool? DisableHorns;
        public bool? DisableServiceAnnounces;
    }
}
