using System;
using System.Threading.Tasks;
using GbxRemoteNet.Structs;
using GbxRemoteNet.XmlRpc;
using GbxRemoteNet.XmlRpc.ExtraTypes;

namespace GbxRemoteNet;

/// <summary>
/// Method Category: Server
/// </summary>
public partial class GbxRemoteClient
{
    public async Task<TmVersionInfo> GetVersionAsync()
    {
        return (TmVersionInfo) XmlRpcTypes.ToNativeValue<TmVersionInfo>(
            await CallOrFaultAsync("GetVersion")
        );
    }

    public async Task<TmStatus> GetStatusAsync()
    {
        return (TmStatus) XmlRpcTypes.ToNativeValue<TmStatus>(
            await CallOrFaultAsync("GetStatus")
        );
    }

    public async Task<bool> QuitGameAsync()
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("QuitGame")
        );
    }

    public async Task<bool> WriteFileAsync(string fileName, GbxBase64 data)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("WriteFile", fileName, data)
        );
    }

    public async Task<bool> TunnelSendDataToIdAsync(int id, GbxBase64 data)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("TunnelSendDataToId", id, data)
        );
    }

    public async Task<bool> TunnelSendDataToLoginAsync(string login, GbxBase64 data)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("TunnelSendDataToLogin", login, data)
        );
    }

    public async Task<bool> EchoAsync(string par1, string par2)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("Echo", par1, par2)
        );
    }

    public async Task<int> GetServerPlanetsAsync()
    {
        return (int) XmlRpcTypes.ToNativeValue<int>(
            await CallOrFaultAsync("GetServerPlanets")
        );
    }

    public async Task<TmSystemInfo> GetSystemInfoAsync()
    {
        return (TmSystemInfo) XmlRpcTypes.ToNativeValue<TmSystemInfo>(
            await CallOrFaultAsync("GetSystemInfo")
        );
    }

    public async Task<bool> SetConnectionRatesAsync(int download, int upload)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("SetConnectionRates", download, upload)
        );
    }

    public async Task<TmServerTag[]> GetServerTagsAsync()
    {
        return (TmServerTag[]) XmlRpcTypes.ToNativeValue<TmServerTag>(
            await CallOrFaultAsync("GetServerTags")
        );
    }

    public async Task<bool> SetServerTagAsync(string name, string value)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("SetServerTag", name, value)
        );
    }

    public async Task<bool> UnsetServerTagAsync(string name)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("SetServerTag", name)
        );
    }

    public async Task<bool> ResetServerTagsAsync()
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("ResetServerTags")
        );
    }

    public async Task<bool> SetServerNameAsync(string name)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("SetServerName", name)
        );
    }

    public async Task<string> GetServerNameAsync()
    {
        return (string) XmlRpcTypes.ToNativeValue<string>(
            await CallOrFaultAsync("GetServerName")
        );
    }

    public async Task<bool> SetServerCommentAsync(string comment)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("SetServerComment", comment)
        );
    }

    public async Task<string> GetServerCommentAsync()
    {
        return (string) XmlRpcTypes.ToNativeValue<string>(
            await CallOrFaultAsync("GetServerComment")
        );
    }

    public async Task<bool> SetHideServerAsync(int hiddenState)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("SetHideServer", hiddenState)
        );
    }

    public async Task<int> GetHideServerAsync()
    {
        return (int) XmlRpcTypes.ToNativeValue<int>(
            await CallOrFaultAsync("GetHideServer")
        );
    }

    public async Task<bool> SetServerPasswordAsync(string password)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("SetServerPassword", password)
        );
    }

    public async Task<string> GetServerPasswordAsync()
    {
        return (string) XmlRpcTypes.ToNativeValue<string>(
            await CallOrFaultAsync("GetServerPassword")
        );
    }

    public async Task<bool> SetServerPasswordForSpectatorAsync(string password)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("SetServerPasswordForSpectator", password)
        );
    }

    public async Task<string> GetServerPasswordForSpectatorAsync()
    {
        return (string) XmlRpcTypes.ToNativeValue<string>(
            await CallOrFaultAsync("GetServerPasswordForSpectator")
        );
    }

    public async Task<bool> SetMaxPlayersAsync(int maxPlayers)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("SetMaxPlayers", maxPlayers)
        );
    }

    public async Task<TmCurrentNextValue<int>> GetMaxPlayersAsync()
    {
        return (TmCurrentNextValue<int>) XmlRpcTypes.ToNativeValue<TmCurrentNextValue<int>>(
            await CallOrFaultAsync("GetMaxPlayers")
        );
    }

    public async Task<bool> SetMaxSpectatorsAsync(int maxPlayers)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("SetMaxSpectators", maxPlayers)
        );
    }

    public async Task<TmCurrentNextValue<int>> GetMaxSpectatorsAsync()
    {
        return (TmCurrentNextValue<int>) XmlRpcTypes.ToNativeValue<TmCurrentNextValue<int>>(
            await CallOrFaultAsync("GetMaxSpectators")
        );
    }

    public async Task<bool> SetLobbyInfoAsync(bool isLobby, int lobbyPlayers, int lobbyMaxPlayers,
        double lobbyPlayersLevel)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("SetLobbyInfo", isLobby, lobbyPlayers, lobbyMaxPlayers, lobbyPlayersLevel)
        );
    }

    public async Task<TmLobbyInfo> GetLobbyInfoAsync()
    {
        return (TmLobbyInfo) XmlRpcTypes.ToNativeValue<TmLobbyInfo>(
            await CallOrFaultAsync("GetLobbyInfo")
        );
    }

    public async Task<bool> SendToServerAfterMatchEndAsync(string sendToServerUrl)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("SendToServerAfterMatchEnd", sendToServerUrl)
        );
    }

    public async Task<bool> KeepPlayerSlotsAsync(bool keepSlots)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("KeepPlayerSlots", keepSlots)
        );
    }

    public async Task<bool> IsKeepingPlayerSlotsAsync()
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("IsKeepingPlayerSlots")
        );
    }

    public async Task<bool> AllowMapDownloadAsync(bool allow)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("AllowMapDownload", allow)
        );
    }

    public async Task<bool> IsMapDownloadAllowedAsync()
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("IsMapDownloadAllowed")
        );
    }

    public async Task<string> GameDataDirectoryAsync()
    {
        return (string) XmlRpcTypes.ToNativeValue<string>(
            await CallOrFaultAsync("GameDataDirectory")
        );
    }

    public async Task<string> GetMapsDirectoryAsync()
    {
        return (string) XmlRpcTypes.ToNativeValue<string>(
            await CallOrFaultAsync("GetMapsDirectory")
        );
    }

    public async Task<string> GetSkinsDirectoryAsync()
    {
        return (string) XmlRpcTypes.ToNativeValue<string>(
            await CallOrFaultAsync("GetSkinsDirectory")
        );
    }

    public async Task<string> ConnectFakePlayerAsync()
    {
        return (string) XmlRpcTypes.ToNativeValue<string>(
            await CallOrFaultAsync("ConnectFakePlayer")
        );
    }

    public async Task<bool> DisconnectFakePlayerAsync(string login)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("DisconnectFakePlayer", login)
        );
    }

    public async Task<TmDemoTokenInfo> GetDemoTokenInfosForPlayerAsync(string login)
    {
        return (TmDemoTokenInfo) XmlRpcTypes.ToNativeValue<TmDemoTokenInfo>(
            await CallOrFaultAsync("GetDemoTokenInfosForPlayer", login)
        );
    }

    public async Task<bool> DisableHornsAsync(string disable)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("DisableHorns", disable)
        );
    }

    public async Task<bool> AreHornsDisabledAsync()
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("AreHornsDisabled")
        );
    }

    public async Task<bool> DisableServiceAnnouncesAsync(string disable)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("DisableServiceAnnounces", disable)
        );
    }

    public async Task<bool> AreServiceAnnouncesDisabledAsync()
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("AreServiceAnnouncesDisabled")
        );
    }

    public async Task<bool> SetServerOptionsAsync(TmServerOptions options)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("SetServerOptions", options)
        );
    }

    public async Task<TmServerOptions> GetServerOptionsAsync()
    {
        return (TmServerOptions) XmlRpcTypes.ToNativeValue<TmServerOptions>(
            await CallOrFaultAsync("GetServerOptions")
        );
    }

    public async Task<bool> SetForcedModsAsync(bool forced, TmMods mods)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("SetForcedMods", forced, mods)
        );
    }

    public async Task<TmForcedMods> GetForcedModsAsync()
    {
        return (TmForcedMods) XmlRpcTypes.ToNativeValue<TmForcedMods>(
            await CallOrFaultAsync("GetForcedMods")
        );
    }

    public async Task<bool> SetForcedMusicAsync(bool forced, string urlOrFileName)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("SetForcedMusic", forced, urlOrFileName)
        );
    }

    public async Task<TmMusicSetting> GetForcedMusicAsync()
    {
        return (TmMusicSetting) XmlRpcTypes.ToNativeValue<TmMusicSetting>(
            await CallOrFaultAsync("GetForcedMusic")
        );
    }

    public async Task<bool> SetForcedSkinsAsync(TmForcedSkin[] skins)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("SetForcedSkins", skins)
        );
    }

    public async Task<TmForcedSkin[]> GetForcedSkinsAsync()
    {
        return (TmForcedSkin[]) XmlRpcTypes.ToNativeValue<TmForcedSkin>(
            await CallOrFaultAsync("GetForcedSkins")
        );
    }

    public async Task<string> GetLastConnectionErrorMessageAsync()
    {
        return (string) XmlRpcTypes.ToNativeValue<string>(
            await CallOrFaultAsync("GetLastConnectionErrorMessage")
        );
    }

    public async Task<bool> SetClientInputsMaxLatencyAsync(int maxTime)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("SetClientInputsMaxLatency", maxTime)
        );
    }

    public async Task<int> GetClientInputsMaxLatencyAsync()
    {
        return (int) XmlRpcTypes.ToNativeValue<int>(
            await CallOrFaultAsync("GetClientInputsMaxLatency")
        );
    }

    public async Task<bool> StopServerAsync()
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("StopServer")
        );
    }

    [Obsolete]
    public async Task<TmNetworkStats> GetNetworkStatsAsync()
    {
        return (TmNetworkStats) XmlRpcTypes.ToNativeValue<TmNetworkStats>(
            await CallOrFaultAsync("GetNetworkStats")
        );
    }

    public async Task<bool> StartServerLanAsync()
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("StartServerLan")
        );
    }
    
    public async Task<bool> StartServerInternetAsync()
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("StartServerInternet")
        );
    }
}