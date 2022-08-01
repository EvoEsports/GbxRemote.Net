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
        public async Task<Structs.VersionInfo> GetVersionAsync() =>
            (Structs.VersionInfo)XmlRpcTypes.ToNativeValue<Structs.VersionInfo>(
                await CallOrFaultAsync("GetVersion")
            );

        /// <summary>
        /// Returns the current status of the server.
        /// </summary>
        /// <returns></returns>
        public async Task<Status> GetStatusAsync() =>
            (Status)XmlRpcTypes.ToNativeValue<Status>(
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

        /// <summary>
        /// Write the data to the specified file. The filename is relative to the Maps path. Only available to Admin.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<bool> WriteFileAsync(string fileName, Base64 data) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("WriteFile", fileName, data)
            );

        /// <summary>
        /// Send the data to the specified player. Only available to Admin.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<bool> TunnelSendDataToIdAsync(int id, Base64 data) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("TunnelSendDataToId", id, data)
            );

        /// <summary>
        /// Send the data to the specified player. Login can be a single login or a list of comma-separated logins. Only available to Admin.
        /// </summary>
        /// <param name="login"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<bool> TunnelSendDataToLoginAsync(string login, Base64 data) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("TunnelSendDataToLogin", login, data)
            );

        /// <summary>
        /// Just log the parameters and invoke a callback. Can be used to talk to other xmlrpc clients connected, or to make custom votes. If used in a callvote, the first parameter will be used as the vote message on the clients. Only available to Admin.
        /// </summary>
        /// <param name="par1"></param>
        /// <param name="par2"></param>
        /// <returns></returns>
        public async Task<bool> EchoAsync(string par1, string par2) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("Echo", par1, par2)
            );

        /// <summary>
        /// Returns the current number of planets on the server account.
        /// </summary>
        /// <returns></returns>
        public async Task<int> GetServerPlanetsAsync() =>
            (int)XmlRpcTypes.ToNativeValue<int>(
                await CallOrFaultAsync("GetServerPlanets")
            );

        /// <summary>
        /// Get some system infos, including connection rates (in kbps).
        /// </summary>
        /// <returns></returns>
        public async Task<SystemInfo> GetSystemInfoAsync() =>
            (SystemInfo)XmlRpcTypes.ToNativeValue<SystemInfo>(
                await CallOrFaultAsync("GetSystemInfo")
            );

        /// <summary>
        /// Set the download and upload rates (in kbps).
        /// </summary>
        /// <param name="download"></param>
        /// <param name="upload"></param>
        /// <returns></returns>
        public async Task<bool> SetConnectionRatesAsync(int download, int upload) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetConnectionRates", download, upload)
            );

        /// <summary>
        /// Returns the list of tags and associated values set on this server. Only available to Admin.
        /// </summary>
        /// <returns></returns>
        public async Task<ServerTag[]> GetServerTagsAsync() =>
            (ServerTag[])XmlRpcTypes.ToNativeValue<ServerTag>(
                await CallOrFaultAsync("GetServerTags")
            );

        /// <summary>
        /// Set a tag and its value on the server. This method takes two parameters. The first parameter specifies the name of the tag, and the second one its value. The list is an array of structures {string Name, string Value}. Only available to Admin.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<bool> SetServerTagAsync(string name, string value) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetServerTag", name, value)
            );

        /// <summary>
        /// Unset the tag with the specified name on the server. Only available to Admin.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<bool> UnsetServerTagAsync(string name) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetServerTag", name)
            );

        /// <summary>
        /// Reset all tags on the server. Only available to Admin.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> ResetServerTagsAsync() =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("ResetServerTags")
            );

        /// <summary>
        /// Set a new server name in utf8 format. Only available to Admin.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<bool> SetServerNameAsync(string name) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetServerName", name)
            );

        /// <summary>
        /// Get the server name in utf8 format.
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetServerNameAsync() =>
            (string)XmlRpcTypes.ToNativeValue<string>(
                await CallOrFaultAsync("GetServerName")
            );

        /// <summary>
        /// Set a new server comment in utf8 format. Only available to Admin.
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        public async Task<bool> SetServerCommentAsync(string comment) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetServerComment", comment)
            );

        /// <summary>
        /// Get the server comment in utf8 format.
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetServerCommentAsync() =>
            (string)XmlRpcTypes.ToNativeValue<string>(
                await CallOrFaultAsync("GetServerComment")
            );

        /// <summary>
        /// Set whether the server should be hidden from the public server list (0 = visible, 1 = always hidden, 2 = hidden from nations). Only available to Admin.
        /// </summary>
        /// <param name="hiddenState"></param>
        /// <returns></returns>
        public async Task<bool> SetHideServerAsync(int hiddenState) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetHideServer", hiddenState)
            );

        /// <summary>
        /// Get whether the server wants to be hidden from the public server list.
        /// </summary>
        /// <returns></returns>
        public async Task<int> GetHideServerAsync() =>
            (int)XmlRpcTypes.ToNativeValue<int>(
                await CallOrFaultAsync("GetHideServer")
            );

        /// <summary>
        /// Set a new password for the server. Only available to Admin.
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<bool> SetServerPasswordAsync(string password) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetServerPassword", password)
            );

        /// <summary>
        /// Get the server password if called as Admin or Super Admin, else returns if a password is needed or not.
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetServerPasswordAsync() =>
            (string)XmlRpcTypes.ToNativeValue<string>(
                await CallOrFaultAsync("GetServerPassword")
            );

        /// <summary>
        /// Set a new password for the spectator mode. Only available to Admin.
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<bool> SetServerPasswordForSpectatorAsync(string password) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetServerPasswordForSpectator", password)
            );

        /// <summary>
        /// Get the password for spectator mode if called as Admin or Super Admin, else returns if a password is needed or not.
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetServerPasswordForSpectatorAsync() =>
            (string)XmlRpcTypes.ToNativeValue<string>(
                await CallOrFaultAsync("GetServerPasswordForSpectator")
            );

        /// <summary>
        /// Set a new maximum number of players. Only available to Admin. Requires a map restart to be taken into account.
        /// </summary>
        /// <param name="maxPlayers"></param>
        /// <returns></returns>
        public async Task<bool> SetMaxPlayersAsync(int maxPlayers) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetMaxPlayers", maxPlayers)
            );

        /// <summary>
        /// Get the current and next maximum number of players allowed on server. The struct returned contains two fields CurrentValue and NextValue.
        /// </summary>
        /// <returns></returns>
        public async Task<CurrentNextValue<int>> GetMaxPlayersAsync() =>
            (CurrentNextValue<int>)XmlRpcTypes.ToNativeValue<CurrentNextValue<int>>(
                await CallOrFaultAsync("GetMaxPlayers")
            );

        /// <summary>
        /// Set a new maximum number of Spectators. Only available to Admin. Requires a map restart to be taken into account.
        /// </summary>
        /// <param name="maxPlayers"></param>
        /// <returns></returns>
        public async Task<bool> SetMaxSpectatorsAsync(int maxPlayers) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetMaxSpectators", maxPlayers)
            );

        /// <summary>
        /// Get the current and next maximum number of Spectators allowed on server. The struct returned contains two fields CurrentValue and NextValue.
        /// </summary>
        /// <returns></returns>
        public async Task<CurrentNextValue<int>> GetMaxSpectatorsAsync() =>
            (CurrentNextValue<int>)XmlRpcTypes.ToNativeValue<CurrentNextValue<int>>(
                await CallOrFaultAsync("GetMaxSpectators")
            );

        /// <summary>
        /// Declare if the server is a lobby, the number and maximum number of players currently managed by it, and the average level of the players. Only available to Admin.
        /// </summary>
        /// <param name="isLobby"></param>
        /// <param name="lobbyPlayers"></param>
        /// <param name="lobbyMaxPlayers"></param>
        /// <param name="lobbyPlayersLevel"></param>
        /// <returns></returns>
        public async Task<bool> SetLobbyInfoAsync(bool isLobby, int lobbyPlayers, int lobbyMaxPlayers, double lobbyPlayersLevel) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetLobbyInfo", isLobby, lobbyPlayers, lobbyMaxPlayers, lobbyPlayersLevel)
            );

        /// <summary>
        /// Get whether the server if a lobby, the number and maximum number of players currently managed by it. The struct returned contains 4 fields IsLobby, LobbyPlayers, LobbyMaxPlayers, and LobbyPlayersLevel.
        /// </summary>
        /// <returns></returns>
        public async Task<LobbyInfo> GetLobbyInfoAsync() =>
            (LobbyInfo)XmlRpcTypes.ToNativeValue<LobbyInfo>(
                await CallOrFaultAsync("GetLobbyInfo")
            );

        /// <summary>
        /// Prior to loading next map, execute SendToServer url '#qjoin=login@title'. Only available to Admin.
        /// </summary>
        /// <param name="sendToServerUrl"></param>
        /// <returns></returns>
        public async Task<bool> SendToServerAfterMatchEndAsync(string sendToServerUrl) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SendToServerAfterMatchEnd", sendToServerUrl)
            );

        /// <summary>
        /// Set whether, when a player is switching to spectator, the server should still consider him a player and keep his player slot, or not. Only available to Admin.
        /// </summary>
        /// <param name="keepSlots"></param>
        /// <returns></returns>
        public async Task<bool> KeepPlayerSlotsAsync(bool keepSlots) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("KeepPlayerSlots", keepSlots)
            );

        /// <summary>
        /// Get whether the server keeps player slots when switching to spectator.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> IsKeepingPlayerSlotsAsync() =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("IsKeepingPlayerSlots")
            );

        /// <summary>
        /// Allow clients to download maps from the server. Only available to Admin.
        /// </summary>
        /// <param name="allow"></param>
        /// <returns></returns>
        public async Task<bool> AllowMapDownloadAsync(bool allow) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("AllowMapDownload", allow)
            );

        /// <summary>
        /// Returns if clients can download maps from the server.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> IsMapDownloadAllowedAsync() =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("IsMapDownloadAllowed")
            );

        /// <summary>
        /// Returns the path of the game datas directory. Only available to Admin.
        /// </summary>
        /// <returns></returns>
        public async Task<string> GameDataDirectoryAsync() =>
            (string)XmlRpcTypes.ToNativeValue<string>(
                await CallOrFaultAsync("GameDataDirectory")
            );

        /// <summary>
        /// Returns the path of the maps directory. Only available to Admin.
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetMapsDirectoryAsync() =>
            (string)XmlRpcTypes.ToNativeValue<string>(
                await CallOrFaultAsync("GetMapsDirectory")
            );

        /// <summary>
        /// Returns the path of the skins directory. Only available to Admin.
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetSkinsDirectoryAsync() =>
            (string)XmlRpcTypes.ToNativeValue<string>(
                await CallOrFaultAsync("GetSkinsDirectory")
            );

        /// <summary>
        /// (debug tool) Connect a fake player to the server and returns the login. Only available to Admin.
        /// </summary>
        /// <returns></returns>
        public async Task<string> ConnectFakePlayerAsync() =>
            (string)XmlRpcTypes.ToNativeValue<string>(
                await CallOrFaultAsync("ConnectFakePlayer")
            );

        /// <summary>
        /// (debug tool) Disconnect a fake player, or all the fake players if login is '*'. Only available to Admin.
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public async Task<bool> DisconnectFakePlayerAsync(string login) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("DisconnectFakePlayer", login)
            );

        /// <summary>
        /// Returns the token infos for a player. The returned structure is { TokenCost, CanPayToken }.
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public async Task<DemoTokenInfo> GetDemoTokenInfosForPlayerAsync(string login) =>
            (DemoTokenInfo)XmlRpcTypes.ToNativeValue<DemoTokenInfo>(
                await CallOrFaultAsync("GetDemoTokenInfosForPlayer", login)
            );

        /// <summary>
        /// Disable player horns. Only available to Admin.
        /// </summary>
        /// <param name="disable"></param>
        /// <returns></returns>
        public async Task<bool> DisableHornsAsync(string disable) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("DisableHorns", disable)
            );

        /// <summary>
        /// Returns whether the horns are disabled.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> AreHornsDisabledAsync() =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("AreHornsDisabled")
            );

        /// <summary>
        /// Disable the automatic mesages when a player connects/disconnects from the server. Only available to Admin.
        /// </summary>
        /// <param name="disable"></param>
        /// <returns></returns>
        public async Task<bool> DisableServiceAnnouncesAsync(string disable) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("DisableServiceAnnounces", disable)
            );

        /// <summary>
        /// Returns whether the automatic mesages are disabled.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> AreServiceAnnouncesDisabledAsync() =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("AreServiceAnnouncesDisabled")
            );

        /// <summary>
        /// Set new server options using the struct passed as parameters. This struct must contain the following fields : Name, Comment, Password, PasswordForSpectator, NextCallVoteTimeOut, CallVoteRatio. May additionally include any of the other members listed in RpcGetServerOptions. Only available to Admin. A change of NextMaxPlayers, NextMaxSpectators, NextCallVoteTimeOut requires a map restart to be taken into account.
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public async Task<bool> SetServerOptionsAsync(ServerOptions options) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetServerOptions", options)
            );

        /// <summary>
        /// Returns a struct containing the server options: Name, Comment, Password, PasswordForSpectator, CurrentMaxPlayers, NextMaxPlayers, CurrentMaxSpectators, NextMaxSpectators, KeepPlayerSlots, IsP2PUpload, IsP2PDownload, CurrentLadderMode, NextLadderMode, CurrentVehicleNetQuality, NextVehicleNetQuality, CurrentCallVoteTimeOut, NextCallVoteTimeOut, CallVoteRatio, AllowMapDownload, AutoSaveReplays, RefereePassword, RefereeMode, AutoSaveValidationReplays, HideServer, CurrentUseChangingValidationSeed, NextUseChangingValidationSeed, ClientInputsMaxLatency, DisableHorns, DisableServiceAnnounces.
        /// </summary>
        /// <returns></returns>
        public async Task<ServerOptions> GetServerOptionsAsync() =>
            (ServerOptions)XmlRpcTypes.ToNativeValue<ServerOptions>(
                await CallOrFaultAsync("GetServerOptions")
            );

        /// <summary>
        /// Set the mods to apply on the clients. Parameters: Override, if true even the maps with a mod will be overridden by the server setting; and Mods, an array of structures [{EnvName, Url}, ...]. Requires a map restart to be taken into account. Only available to Admin.
        /// </summary>
        /// <param name="forced"></param>
        /// <param name="mods"></param>
        /// <returns></returns>
        public async Task<bool> SetForcedModsAsync(bool forced, Mods mods) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetForcedMods", forced, mods)
            );

        /// <summary>
        /// Get the mods settings.
        /// </summary>
        /// <returns></returns>
        public async Task<ForcedMods> GetForcedModsAsync() =>
            (ForcedMods)XmlRpcTypes.ToNativeValue<ForcedMods>(
                await CallOrFaultAsync("GetForcedMods")
            );

        /// <summary>
        /// Set the music to play on the clients. Parameters: Override, if true even the maps with a custom music will be overridden by the server setting, and a UrlOrFileName for the music. Requires a map restart to be taken into account. Only available to Admin.
        /// </summary>
        /// <param name="forced"></param>
        /// <param name="urlOrFileName"></param>
        /// <returns></returns>
        public async Task<bool> SetForcedMusicAsync(bool forced, string urlOrFileName) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetForcedMusic", forced, urlOrFileName)
            );

        /// <summary>
        /// Get the music setting.
        /// </summary>
        /// <returns></returns>
        public async Task<MusicSetting> GetForcedMusicAsync() =>
            (MusicSetting)XmlRpcTypes.ToNativeValue<MusicSetting>(
                await CallOrFaultAsync("GetForcedMusic")
            );

        /// <summary>
        /// Defines a list of remappings for player skins. It expects a list of structs Orig, Name, Checksum, Url. Orig is the name of the skin to remap, or '*' for any other. Name, Checksum, Url define the skin to use. (They are optional, you may set value '' for any of those. All 3 null means same as Orig). Will only affect players connecting after the value is set. Only available to Admin.
        /// </summary>
        /// <param name="skins"></param>
        /// <returns></returns>
        public async Task<bool> SetForcedSkinsAsync(ForcedSkin[] skins) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetForcedSkins", skins)
            );

        /// <summary>
        /// Get the current forced skins.
        /// </summary>
        /// <returns></returns>
        public async Task<ForcedSkin[]> GetForcedSkinsAsync() =>
            (ForcedSkin[])XmlRpcTypes.ToNativeValue<ForcedSkin>(
                await CallOrFaultAsync("GetForcedSkins")
            );

        /// <summary>
        /// Returns the last error message for an internet connection. Only available to Admin.
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetLastConnectionErrorMessageAsync() =>
            (string)XmlRpcTypes.ToNativeValue<string>(
                await CallOrFaultAsync("GetLastConnectionErrorMessage")
            );

        /// <summary>
        /// Set the maximum time the server must wait for inputs from the clients before dropping data, or '0' for auto-adaptation. Only used by ShootMania. Only available to Admin.
        /// </summary>
        /// <param name="maxTime"></param>
        /// <returns></returns>
        public async Task<bool> SetClientInputsMaxLatencyAsync(int maxTime) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetClientInputsMaxLatency", maxTime)
            );

        /// <summary>
        /// Get the current ClientInputsMaxLatency. Only used by ShootMania.
        /// </summary>
        /// <returns></returns>
        public async Task<int> GetClientInputsMaxLatencyAsync() =>
            (int)XmlRpcTypes.ToNativeValue<int>(
                await CallOrFaultAsync("GetClientInputsMaxLatency")
            );

        /// <summary>
        /// Stop the server. Only available to SuperAdmin.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> StopServerAsync() =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("StopServer")
            );

        /// <summary>
        /// Returns a struct containing the networks stats of the server. The structure contains the following fields : Uptime, NbrConnection, MeanConnectionTime, MeanNbrPlayer, RecvNetRate, SendNetRate, TotalReceivingSize, TotalSendingSize and an array of structures named PlayerNetInfos.
        /// Each structure of the array PlayerNetInfos contains the following fields : Login, IPAddress, LastTransferTime, DeltaBetweenTwoLastNetState, PacketLossRate. Only available to SuperAdmin.
        /// </summary>
        /// <returns></returns>
        public async Task<NetworkStats> GetNetworkStatsAsync() =>
            (NetworkStats)XmlRpcTypes.ToNativeValue<NetworkStats>(
                await CallOrFaultAsync("GetNetworkStats")
            );

        /// <summary>
        /// Start a server on lan, using the current configuration. Only available to SuperAdmin.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> StartServerLanAsync() =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("StartServerLan")
            );

        /// <summary>
        /// Start a server on internet, using the current configuration. Only available to SuperAdmin.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> StartServerInternetAsync() =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("StartServerInternet")
            );
    }
}
