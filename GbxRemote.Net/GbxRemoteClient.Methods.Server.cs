using GbxRemoteNet.Structs;
using GbxRemoteNet.XmlRpc;
using GbxRemoteNet.XmlRpc.ExtraTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GbxRemoteNet {
    /// <summary>
    /// Method Category: Server
    /// </summary>
    public partial class GbxRemoteClient {
        /// <summary>
        /// Returns a struct with the Name, TitleId, Version, Build and ApiVersion of the application remotely controlled.
        /// </summary>
        /// <returns></returns>
        public async Task<VersionStruct> GetVersionAsync() =>
            (VersionStruct)XmlRpcTypes.ToNativeValue<VersionStruct>(
                await CallOrFaultAsync("GetVersion")
            );

        /// <summary>
        /// Returns the current status of the server.
        /// </summary>
        /// <returns></returns>
        public async Task<StatusStruct> GetStatusAsync() =>
            (StatusStruct)XmlRpcTypes.ToNativeValue<StatusStruct>(
                await CallOrFaultAsync("GetStatus")
            );

        /// <summary>
        /// Quit the application. Only available to SuperAdmin.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> QuitGameAsync() =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("QuitGame")
            );

        public async Task<bool> WriteFileAsync(string fileName, Base64 data) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("WriteFile", fileName, data)
            );

        public async Task<bool> TunnelSendDataToIdAsync(int id, Base64 data) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("TunnelSendDataToId", id, data)
            );

        public async Task<bool> TunnelSendDataToLoginAsync(string login, Base64 data) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("TunnelSendDataToLogin", login, data)
            );

        public async Task<bool> EchoAsync(string par1, string par2) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("Echo", par1, par2)
            );

        public async Task<int> GetServerPlanetsAsync() =>
            (int)XmlRpcTypes.ToNativeValue<int>(
                await CallOrFaultAsync("GetServerPlanets")
            );

        public async Task<SystemInfoStruct> SystemInfoStructAsync() =>
            (SystemInfoStruct)XmlRpcTypes.ToNativeValue<SystemInfoStruct>(
                await CallOrFaultAsync("SystemInfoStruct")
            );

        public async Task<bool> SetConnectionRatesAsync(int download, int upload) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetConnectionRates", download, upload)
            );

        public async Task<ServerTagStruct[]> GetServerTagsAsync() =>
            (ServerTagStruct[])XmlRpcTypes.ToNativeValue<ServerTagStruct>(
                await CallOrFaultAsync("GetServerTags")
            );

        public async Task<bool> SetServerTagAsync(string name, string value) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetServerTag", name, value)
            );

        public async Task<bool> UnsetServerTagAsync(string name) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetServerTag", name)
            );

        public async Task<bool> ResetServerTagsAsync() =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("ResetServerTags")
            );

        public async Task<bool> SetServerNameAsync(string name) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetServerName", name)
            );

        public async Task<string> GetServerNameAsync() =>
            (string)XmlRpcTypes.ToNativeValue<string>(
                await CallOrFaultAsync("GetServerName")
            );

        public async Task<bool> SetServerCommentAsync(string comment) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetServerComment", comment)
            );

        public async Task<string> GetServerCommentAsync() =>
            (string)XmlRpcTypes.ToNativeValue<string>(
                await CallOrFaultAsync("GetServerComment")
            );

        public async Task<bool> SetHideServerAsync(int hiddenState) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetHideServer", hiddenState)
            );

        public async Task<int> GetHideServerAsync() =>
            (int)XmlRpcTypes.ToNativeValue<int>(
                await CallOrFaultAsync("GetHideServer")
            );

        public async Task<bool> IsRelayServerAsync() =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("IsRelayServer")
            );

        public async Task<bool> SetServerPasswordAsync(string password) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetServerPassword", password)
            );

        public async Task<string> GetServerPasswordAsync() =>
            (string)XmlRpcTypes.ToNativeValue<string>(
                await CallOrFaultAsync("GetServerPassword")
            );

        public async Task<bool> SetServerPasswordForSpectatorAsync(string password) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetServerPasswordForSpectator", password)
            );

        public async Task<string> GetServerPasswordForSpectatorAsync() =>
            (string)XmlRpcTypes.ToNativeValue<string>(
                await CallOrFaultAsync("GetServerPasswordForSpectator")
            );

        public async Task<bool> SetMaxPlayersAsync(int maxPlayers) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetMaxPlayers", maxPlayers)
            );

        public async Task<CurrentNextValueStruct<int>> GetMaxPlayersAsync() =>
            (CurrentNextValueStruct<int>)XmlRpcTypes.ToNativeValue<CurrentNextValueStruct<int>>(
                await CallOrFaultAsync("GetMaxPlayers")
            );

        public async Task<bool> SetMaxSpectatorsAsync(int maxPlayers) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetMaxSpectators", maxPlayers)
            );

        public async Task<CurrentNextValueStruct<int>> GetMaxSpectatorsAsync() =>
            (CurrentNextValueStruct<int>)XmlRpcTypes.ToNativeValue<CurrentNextValueStruct<int>>(
                await CallOrFaultAsync("GetMaxSpectators")
            );

        public async Task<bool> SetLobbyInfoAsync(bool isLobby, int lobbyPlayers, int lobbyMaxPlayers, double lobbyPlayersLevel) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetLobbyInfo", isLobby, lobbyPlayers, lobbyMaxPlayers, lobbyPlayersLevel)
            );

        public async Task<LobbyInfoStruct> GetLobbyInfoAsync() =>
            (LobbyInfoStruct)XmlRpcTypes.ToNativeValue<LobbyInfoStruct>(
                await CallOrFaultAsync("GetLobbyInfo")
            );

        public async Task<bool> SendToServerAfterMatchEndAsync(string sendToServerUrl) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SendToServerAfterMatchEnd", sendToServerUrl)
            );

        public async Task<bool> KeepPlayerSlotsAsync(bool keepSlots) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("KeepPlayerSlots", keepSlots)
            );

        public async Task<bool> IsKeepingPlayerSlotsAsync() =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("IsKeepingPlayerSlots")
            );

        public async Task<bool> EnableP2PUploadAsync(bool enable) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("EnableP2PUpload", enable)
            );

        public async Task<bool> IsP2PUploadAsync() =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("IsP2PUpload")
            );

        public async Task<bool> EnableP2PDownloadAsync(bool enable) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("EnableP2PDownload", enable)
            );

        public async Task<bool> IsP2PDownloadAsync() =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("IsP2PDownload")
            );

        public async Task<bool> AllowMapDownloadAsync(bool allow) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("AllowMapDownload", allow)
            );

        public async Task<bool> IsMapDownloadAllowedAsync() =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("IsMapDownloadAllowed")
            );

        public async Task<string> GameDataDirectoryAsync() =>
            (string)XmlRpcTypes.ToNativeValue<string>(
                await CallOrFaultAsync("GameDataDirectory")
            );

        public async Task<string> GetMapsDirectoryAsync() =>
            (string)XmlRpcTypes.ToNativeValue<string>(
                await CallOrFaultAsync("GetMapsDirectory")
            );

        public async Task<string> GetSkinsDirectoryAsync() =>
            (string)XmlRpcTypes.ToNativeValue<string>(
                await CallOrFaultAsync("GetSkinsDirectory")
            );

        public async Task<string> ConnectFakePlayerAsync() =>
            (string)XmlRpcTypes.ToNativeValue<string>(
                await CallOrFaultAsync("ConnectFakePlayer")
            );

        public async Task<bool> DisconnectFakePlayerAsync(string login) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("DisconnectFakePlayer", login)
            );

        public async Task<DemoTokenInfoStruct> GetDemoTokenInfosForPlayerAsync(string login) =>
            (DemoTokenInfoStruct)XmlRpcTypes.ToNativeValue<DemoTokenInfoStruct>(
                await CallOrFaultAsync("GetDemoTokenInfosForPlayer", login)
            );

        public async Task<bool> DisableHorns(string disable) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("DisableHorns", disable)
            );

        public async Task<bool> AreHornsDisabledAsync() =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("AreHornsDisabled")
            );

        public async Task<bool> DisableServiceAnnouncesAsync(string disable) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("DisableServiceAnnounces", disable)
            );

        public async Task<bool> AreServiceAnnouncesDisabledAsync() =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("AreServiceAnnouncesDisabled")
            );

        public async Task<bool> SetLadderModeAsync(int mode) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetLadderMode", mode)
            );

        public async Task<CurrentNextValueStruct<int>> GetLadderModeAsync() =>
            (CurrentNextValueStruct<int>)XmlRpcTypes.ToNativeValue<CurrentNextValueStruct<int>>(
                await CallOrFaultAsync("GetLadderMode")
            );

        public async Task<LadderServerLimitsStruct> GetLadderServerLimitsAsync() =>
            (LadderServerLimitsStruct)XmlRpcTypes.ToNativeValue<LadderServerLimitsStruct>(
                await CallOrFaultAsync("GetLadderServerLimits")
            );

        public async Task<bool> SetVehicleNetQualityAsync(int quality) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetVehicleNetQuality", quality)
            );

        public async Task<CurrentNextValueStruct<int>> GetVehicleNetQualityAsync() =>
            (CurrentNextValueStruct<int>)XmlRpcTypes.ToNativeValue<CurrentNextValueStruct<int>>(
                await CallOrFaultAsync("GetVehicleNetQuality")
            );

        public async Task<bool> SetServerOptionsAsync(ServerOptionsStruct options) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetServerOptions", options)
            );

        public async Task<ServerOptionsStruct> GetServerOptionsAsync() =>
            (ServerOptionsStruct)XmlRpcTypes.ToNativeValue<ServerOptionsStruct>(
                await CallOrFaultAsync("GetServerOptions")
            );

        public async Task<bool> SetForcedModsAsync(bool forced, ModsStruct mods) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetForcedMods", forced, mods)
            );

        public async Task<ForcedModsStruct> GetForcedModsAsync() =>
            (ForcedModsStruct)XmlRpcTypes.ToNativeValue<ForcedModsStruct>(
                await CallOrFaultAsync("GetForcedMods")
            );

        public async Task<bool> SetForcedMusicAsync(bool forced, string urlOrFileName) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetForcedMusic", forced, urlOrFileName)
            );

        public async Task<MusicSettingStruct> GetForcedMusicAsync() =>
            (MusicSettingStruct)XmlRpcTypes.ToNativeValue<MusicSettingStruct>(
                await CallOrFaultAsync("GetForcedMusic")
            );

        public async Task<bool> SetForcedSkinsAsync(ForcedSkinStruct[] skins) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetForcedSkins", skins)
            );

        public async Task<ForcedSkinStruct[]> GetForcedSkinsAsync() =>
            (ForcedSkinStruct[])XmlRpcTypes.ToNativeValue<ForcedSkinStruct>(
                await CallOrFaultAsync("GetForcedSkins")
            );

        public async Task<string> GetLastConnectionErrorMessageAsync() =>
            (string)XmlRpcTypes.ToNativeValue<string>(
                await CallOrFaultAsync("GetLastConnectionErrorMessage")
            );

        public async Task<bool> SetRefereePasswordAsync(string password) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetRefereePassword", password)
            );

        public async Task<string> GetRefereePasswordAsync() =>
            (string)XmlRpcTypes.ToNativeValue<string>(
                await CallOrFaultAsync("GetRefereePassword")
            );

        public async Task<bool> SetRefereeModeAsync(int mode) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetRefereeMode", mode)
            );

        public async Task<int> GetRefereeModeAsync() =>
            (int)XmlRpcTypes.ToNativeValue<int>(
                await CallOrFaultAsync("GetRefereeMode")
            );

        public async Task<bool> SetUseChangingValidationSeedAsync(int useValidationSeed) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetUseChangingValidationSeed", useValidationSeed)
            );

        public async Task<CurrentNextValueStruct<bool>> GetUseChangingValidationSeedAsync() =>
            (CurrentNextValueStruct<bool>)XmlRpcTypes.ToNativeValue<CurrentNextValueStruct<bool>>(
                await CallOrFaultAsync("GetUseChangingValidationSeed")
            );

        public async Task<bool> SetClientInputsMaxLatencyAsync(int maxTime) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetClientInputsMaxLatency", maxTime)
            );

        public async Task<int> GetClientInputsMaxLatencyAsync() =>
            (int)XmlRpcTypes.ToNativeValue<int>(
                await CallOrFaultAsync("GetClientInputsMaxLatency")
            );

        public async Task<bool> SetWarmUpAsync(bool useWarmup) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetWarmUp", useWarmup)
            );

        public async Task<bool> GetWarmUpAsync() =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("GetWarmUp")
            );

        public async Task<string> GetModeScriptText() =>
            (string)XmlRpcTypes.ToNativeValue<string>(
                await CallOrFaultAsync("GetModeScriptText")
            );

        public async Task<bool> SetModeScriptTextAsync(string script) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetModeScriptText", script)
            );

        public async Task<ScriptInfoStruct> GetModeScriptInfoAsync() =>
            (ScriptInfoStruct)XmlRpcTypes.ToNativeValue<ScriptInfoStruct>(
                await CallOrFaultAsync("GetModeScriptInfo")
            );

        public async Task<DynamicObject> GetModeScriptSettingsAsync() =>
            (DynamicObject)XmlRpcTypes.ToNativeValue<DynamicObject>(
                await CallOrFaultAsync("GetModeScriptSettings")
            );
    }
}
