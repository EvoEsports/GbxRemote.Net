namespace GbxRemoteNet.Structs;

public class TmServerOptions
{
    public string Name { get; set; }
    public string Comment { get; set; }
    public string Password { get; set; }
    public string PasswordForSpectator { get; set; }
    public int? CurrentMaxPlayers { get; set; }
    public int? NextMaxPlayers { get; set; }
    public int? CurrentMaxSpectators { get; set; }
    public int? NextMaxSpectators { get; set; }
    public int? CurrentCallVoteTimeOut { get; set; }
    public int NextCallVoteTimeOut { get; set; }
    public double CallVoteRatio { get; set; }
    public bool? AutoSaveReplays { get; set; }
    public bool? AllowMapDownload { get; set; }
    public bool? KeepPlayerSlots { get; set; }
    public int? HideServer { get; set; }
    public int? ClientInputsMaxLatency { get; set; }
    public bool? DisableHorns { get; set; }
    public bool? DisableServiceAnnounces { get; set; }
    public int? PacketAssembly_PacketsPerFrame { get; set; }
    public int? PacketAssembly_FullPacketsPerFrame { get; set; }
    public int? TrustClientSimu_ClientToServer_SendingRate { get; set; }
    public int? DelayedVisuals_ServerToClient_SendingRate { get; set; }
}