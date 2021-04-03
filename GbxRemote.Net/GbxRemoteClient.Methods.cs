using GbxRemoteNet.Structs;
using GbxRemoteNet.XmlRpc;
using GbxRemoteNet.XmlRpc.ExtraTypes;
using GbxRemoteNet.XmlRpc.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GbxRemoteNet {
    public partial class GbxRemoteClient {
        /*
         * Methods Reference: https://wiki.trackmania.io/en/dedicated-server/XML-RPC/Methods
         */

        #region System Methods
        /// <summary>
        /// Return an array of all available XML-RPC methods on this server.
        /// </summary>
        /// <returns></returns>
        public async Task<string[]> SystemListMethodsAsync() =>
            (string[])XmlRpcTypes.ToNativeValue<string>((XmlRpcArray)
                await CallOrFaultAsync("system.listMethods")
            );

        /// <summary>
        /// Given the name of a method, return an array of legal signatures. Each signature is an array of strings. The first item of each signature is the return type, and any others items are parameter types.
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public async Task<string[][]> SystemMethodSignatureAsync(string method) => 
            XmlRpcTypes.ToNative2DArray<string>((XmlRpcArray)
                await CallOrFaultAsync("system.methodSignature", method)
            );

        /// <summary>
        /// Given the name of a method, return a help string.
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public async Task<string> SystemMethodHelpAsync(string method) =>
            (string)XmlRpcTypes.ToNativeValue<string>(
                await CallOrFaultAsync("system.methodHelp", method)
            );

        // todo: multicall
        #endregion

        #region Session Methods
        /// <summary>
        /// Allow user authentication by specifying a login and a password, to gain access to the set of functionalities corresponding to this authorization level.
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<bool> AuthenticateAsync(string login, string password) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("Authenticate", login, password)
            );

        /// <summary>
        /// Change the password for the specified login/user. Only available to SuperAdmin.
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<bool> ChangeAuthPasswordAsync(string login, string password) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("ChangeAuthPassword", login, password)
            );

        /// <summary>
        /// Allow the GameServer to call you back.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> EnableCallbacksAsync(bool enable) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("EnableCallbacks", enable)
            );

        /// <summary>
        /// Define the wanted api.
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        public async Task<bool> SetApiVersionAsync(string version) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetApiVersion", version)
            );
        #endregion

        #region Server
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

        #endregion

        #region Teams
        [Obsolete("Deprecated and not used in TM2 and later games.")]
        public async Task<string> SetTeamInfo(string par1, double par2, string par3, string par4, double par5, string par6, string par7, double par8, string par9) =>
            (string)XmlRpcTypes.ToNativeValue<string>(
                await CallOrFaultAsync("SetTeamInfo", par1, par2, par3, par4, par5, par6, par7, par8, par9)
            );

        public async Task<TeamInfoStruct> GetTeamInfoAsync(int team) =>
            (TeamInfoStruct)XmlRpcTypes.ToNativeValue<TeamInfoStruct>(
                await CallOrFaultAsync("GetTeamInfo", team)
            );

        public async Task<bool> SetForcedClubLinksAsync(string clubLink1, string clubLink2) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetForcedClubLinks", clubLink1, clubLink2)
            );

        public async Task<ClubLinksStruct> GetForcedClubLinksAsync(int team) =>
            (ClubLinksStruct)XmlRpcTypes.ToNativeValue<ClubLinksStruct>(
                await CallOrFaultAsync("GetForcedClubLinks", team)
            );

        public async Task<bool> SetForcedTeamsAsync(bool forced) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetForcedTeams", forced)
            );

        public async Task<bool> GetForcedTeamsAsync() =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("GetForcedTeams")
            );
        #endregion

        #region Votes
        /// <summary>
        /// Call a vote for a cmd. The command is a XML string corresponding to an XmlRpc request. Only available to Admin.
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public async Task<bool> CallVoteAsync(string cmd) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("CallVote", cmd)
            );

        /// <summary>
        /// Extended call vote. Same as CallVote, but you can additionally supply specific parameters for this vote: a ratio, a time out and who is voting. Special timeout values: a ratio of '-1' means default; a timeout of '0' means default, '1' means indefinite; Voters values: '0' means only active players, '1' means any player, '2' is for everybody, pure spectators included. Only available to Admin.
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="ratio"></param>
        /// <param name="timeout"></param>
        /// <param name="who"></param>
        /// <returns></returns>
        public async Task<bool> CallVoteExAsync(string cmd, double ratio, int timeout, int who) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("CallVoteEx", cmd, ratio, timeout, who)
            );

        /// <summary>
        /// Used internally by game.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> InternalCallVoteAsync() =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("InternalCallVote")
            );

        /// <summary>
        /// Cancel the current vote. Only available to Admin.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> CancelVoteVoteAsync() =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("CancelVoteVote")
            );

        /// <summary>
        /// Returns the vote currently in progress. The returned structure is { CallerLogin, CmdName, CmdParam }.
        /// </summary>
        /// <returns></returns>
        public async Task<CurrentCallVoteStruct> GetCurrentCallVoteAsync() =>
            (CurrentCallVoteStruct)XmlRpcTypes.ToNativeValue<CurrentCallVoteStruct>(
                await CallOrFaultAsync("GetCurrentCallVote")
            );

        /// <summary>
        /// Set a new timeout for waiting for votes. A zero value disables callvote. Only available to Admin. Requires a map restart to be taken into account.
        /// </summary>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public async Task<bool> SetCallVoteTimeOutAsync(int timeout) => 
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetCallVoteTimeOut", timeout)
            );

        /// <summary>
        /// Get the current and next timeout for waiting for votes. The struct returned contains two fields 'CurrentValue' and 'NextValue'.
        /// </summary>
        /// <returns></returns>
        public async Task<CurrentNextValueStruct<int>> GetCallVoteTimeOutAsync() =>
            (CurrentNextValueStruct<int>)XmlRpcTypes.ToNativeValue<CurrentNextValueStruct<int>>(
                await CallOrFaultAsync("GetCallVoteTimeOut")
            );

        /// <summary>
        /// Set a new default ratio for passing a vote. Must lie between 0 and 1. Only available to Admin.
        /// </summary>
        /// <param name="ratio"></param>
        /// <returns></returns>
        public async Task<bool> SetCallVoteRatioAsync(double ratio) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetCallVoteRatio", ratio)
            );

        /// <summary>
        /// Get the current default ratio for passing a vote. This value lies between 0 and 1.
        /// </summary>
        /// <param name="ratio"></param>
        /// <returns></returns>
        public async Task<double> GetCallVoteRatioAsync() =>
            (double)XmlRpcTypes.ToNativeValue<double>(
                await CallOrFaultAsync("GetCallVoteRatio")
            );

        /// <summary>
        /// Set the ratios list for passing specific votes. The parameter is an array of structs {string Command, double Ratio}, ratio is in [0,1] or -1 for vote disabled. Only available to Admin.
        /// </summary>
        /// <param name="ratio"></param>
        /// <returns></returns>
        public async Task<bool> SetCallVoteRatiosAsync(CallVoteRatioStruct[] ratios) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetCallVoteRatios", ratios)
            );

        /// <summary>
        /// Get the current ratios for passing votes.
        /// </summary>
        /// <returns></returns>
        public async Task<CallVoteRatioStruct[]> GetCallVoteRatiosAsync() =>
            (CallVoteRatioStruct[])XmlRpcTypes.ToNativeValue<CallVoteRatioStruct>(
                await CallOrFaultAsync("GetCallVoteRatios")
            );
        #endregion

        #region Chat
        /// <summary>
        /// Send a text message to all clients without the server login. Only available to Admin.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<bool> ChatSendServerMessageAsync(string message) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("ChatSendServerMessage", message)
            );

        /// <summary>
        /// Send a localised text message to all clients without the server login, or optionally to a Login (which can be a single login or a list of comma-separated logins). The parameter is an array of structures {Lang='xx', Text='...'}. If no matching language is found, the last text in the array is used. Only available to Admin.
        /// </summary>
        /// <param name="lang"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<bool> ChatSendServerMessageToLanguageAsync(LanguageStruct[] lang, string message) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("ChatSendServerMessage", lang, message)
            );

        /// <summary>
        /// Send a text message without the server login to the client with the specified PlayerId. Only available to Admin.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="loginId"></param>
        /// <returns></returns>
        public async Task<bool> ChatSendServerMessageToIdAsync(string message, int loginId) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("ChatSendServerMessageToId", message, loginId)
            );

        /// <summary>
        /// Send a text message without the server login to the client with the specified login. Login can be a single login or a list of comma-separated logins. Only available to Admin.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="playerLogins"></param>
        /// <returns></returns>
        public async Task<bool> ChatSendServerMessageToLoginAsync(string message, string playerLogins) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("ChatSendServerMessageToLogin", message, playerLogins)
            );

        /// <summary>
        /// Send a text message to all clients. Only available to Admin.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<bool> ChatSendAsync(string message) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("ChatSend", message)
            );

        /// <summary>
        /// Send a localised text message to all clients, or optionally to a Login (which can be a single login or a list of comma-separated logins). The parameter is an array of structures {Lang='xx', Text='...'}. If no matching language is found, the last text in the array is used. Only available to Admin.
        /// </summary>
        /// <param name="lang"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<bool> ChatSendToLanguageAsync(LanguageStruct[] lang, string message) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("ChatSendToLanguage", lang, message)
            );

        /// <summary>
        /// Send a text message to the client with the specified login. Login can be a single login or a list of comma-separated logins. Only available to Admin.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="playerLogins"></param>
        /// <returns></returns>
        public async Task<bool> ChatSendToLoginAsync(string message, string playerLogins) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("ChatSendToLogin", message, playerLogins)
            );

        /// <summary>
        /// Send a text message to the client with the specified PlayerId. Only available to Admin.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="playerId"></param>
        /// <returns></returns>
        public async Task<bool> ChatSendToIdAsync(string message, int playerId) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("ChatSendToId", message, playerId)
            );

        /// <summary>
        /// Returns the last chat lines. Maximum of 40 lines. Only available to Admin.
        /// </summary>
        /// <returns></returns>
        public async Task<string[]> GetChatLines() =>
            (string[])XmlRpcTypes.ToNativeValue<string[]>(
                await CallOrFaultAsync("GetChatLines")
            );

        /// <summary>
        /// The chat messages are no longer dispatched to the players, they only go to the rpc callback and the controller has to manually forward them. The second (optional) parameter allows all messages from the server to be automatically forwarded. Only available to Admin.
        /// </summary>
        /// <param name="enable"></param>
        /// <param name="forward"></param>
        /// <returns></returns>
        public async Task<bool> ChatEnableManualRoutingAsync(bool enable, bool forward) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("ChatSendToId", enable, forward)
            );

        /// <summary>
        /// (Text, SenderLogin, DestLogin) Send a text message to the specified DestLogin (or everybody if empty) on behalf of SenderLogin. DestLogin can be a single login or a list of comma-separated logins. Only available if manual routing is enabled. Only available to Admin.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="senderLogin"></param>
        /// <param name="destinationLogin"></param>
        /// <returns></returns>
        public async Task<bool> ChatForwardToLoginAsync(string text, string senderLogin, string destinationLogin) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("ChatForwardToLogin", text, senderLogin, destinationLogin)
            );
        #endregion

        #region Client
        /// <summary>
        /// Display a notice on all clients. The parameters are the text message to display, and the login of the avatar to display next to it (or '' for no avatar), and an optional 'variant' in [0 = normal, 1 = Sad, 2 = Happy]. Only available to Admin.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="playerLogin"></param>
        /// <param name="variant"></param>
        /// <returns></returns>
        public async Task<bool> SendNoticeAsync(string message, string playerLogin, int variant) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SendNotice", message, playerLogin, variant)
            );

        /// <summary>
        /// Display a notice on the client with the specified UId. The parameters are the Uid of the client to whom the notice is sent, the text message to display, and the UId of the avatar to display next to it (or '255' for no avatar), and an optional 'variant' in [0 = normal, 1 = Sad, 2 = Happy]. Only available to Admin.
        /// </summary>
        /// <param name="clientUid"></param>
        /// <param name="message"></param>
        /// <param name="avatarUid"></param>
        /// <param name="variant"></param>
        /// <returns></returns>
        public async Task<bool> SendNoticeToIdAsync(int clientUid, string message, int avatarUid, int variant) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SendNoticeToId", clientUid, message, avatarUid, variant)
            );

        /// <summary>
        /// Display a notice on the client with the specified login. The parameters are the login of the client to whom the notice is sent, the text message to display, and the login of the avatar to display next to it (or '' for no avatar), and an optional 'variant' in [0 = normal, 1 = Sad, 2 = Happy]. Login can be a single login or a list of comma-separated logins. Only available to Admin.
        /// </summary>
        /// <param name="clientLogin"></param>
        /// <param name="message"></param>
        /// <param name="avatarLogin"></param>
        /// <param name="variant"></param>
        /// <returns></returns>
        public async Task<bool> SendNoticeToLoginAsync(string clientLogin, string message, string avatarLogin, int variant) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SendNoticeToLogin", clientLogin, message, avatarLogin, variant)
            );

        /// <summary>
        /// Display a manialink page on all clients. The parameters are the xml description of the page to display, a timeout to autohide it (0 = permanent), and a boolean to indicate whether the page must be hidden as soon as the user clicks on a page option. Only available to Admin.
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="timeout"></param>
        /// <param name="autohide"></param>
        /// <returns></returns>
        public async Task<bool> SendDisplayManialinkPageAsync(string xml, int timeout, bool autohide) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SendDisplayManialinkPage", xml, timeout, autohide)
            );

        /// <summary>
        /// Display a manialink page on the client with the specified UId. The first parameter is the UId of the player, the other are identical to 'SendDisplayManialinkPage'. Only available to Admin.
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="xml"></param>
        /// <param name="timeout"></param>
        /// <param name="autohide"></param>
        /// <returns></returns>
        public async Task<bool> SendDisplayManialinkPageToIdAsync(int clientId, string xml, int timeout, bool autohide) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SendDisplayManialinkPageToId", clientId, xml, timeout, autohide)
            );

        /// <summary>
        /// Display a manialink page on the client with the specified login. The first parameter is the login of the player, the other are identical to 'SendDisplayManialinkPage'. Login can be a single login or a list of comma-separated logins. Only available to Admin.
        /// </summary>
        /// <param name="playerLogin"></param>
        /// <param name="xml"></param>
        /// <param name="timeout"></param>
        /// <param name="autohide"></param>
        /// <returns></returns>
        public async Task<bool> SendDisplayManialinkPageToLoginAsync(string playerLogin, string xml, int timeout, bool autohide) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SendDisplayManialinkPageToLogin", playerLogin, xml, timeout, autohide)
            );

        /// <summary>
        /// Hide the displayed manialink page on all clients. Only available to Admin.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> SendHideManialinkPageAsync() =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SendHideManialinkPage")
            );

        /// <summary>
        /// Hide the displayed manialink page on the client with the specified UId. Only available to Admin.
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public async Task<bool> SendHideManialinkPageToIdAsync(int clientId) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SendHideManialinkPageToId", clientId)
            );

        /// <summary>
        /// Hide the displayed manialink page on the client with the specified login. Login can be a single login or a list of comma-separated logins. Only available to Admin.
        /// </summary>
        /// <param name="playerLogin"></param>
        /// <returns></returns>
        public async Task<bool> SendHideManialinkPageToLoginAsync(string playerLogin) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SendHideManialinkPageToLogin", playerLogin)
            );

        /// <summary>
        /// Returns the latest results from the current manialink page, as an array of structs {string Login, int PlayerId, int Result} Result==0 -> no answer, Result>0.... -> answer from the player.
        /// </summary>
        /// <returns></returns>
        public async Task<ManialinkPageAnswerStruct[]> GetManialinkPageAnswersAsync() =>
            (ManialinkPageAnswerStruct[])XmlRpcTypes.ToNativeValue<ManialinkPageAnswerStruct>(
                await CallOrFaultAsync("GetManialinkPageAnswers")
            );

        /// <summary>
        /// Opens a link in the client with the specified UId. The parameters are the Uid of the client to whom the link to open is sent, the link url, and the 'LinkType' (0 in the external browser, 1 in the internal manialink browser). Only available to Admin.
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="url"></param>
        /// <param name="linkType"></param>
        /// <returns></returns>
        public async Task<bool> SendOpenLinkToId(int clientId, string url, int linkType) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SendOpenLinkToId", clientId, url, linkType)
            );

        /// <summary>
        /// Opens a link in the client with the specified login. The parameters are the login of the client to whom the link to open is sent, the link url, and the 'LinkType' (0 in the external browser, 1 in the internal manialink browser). Login can be a single login or a list of comma-separated logins. Only available to Admin.
        /// </summary>
        /// <param name="playerLogin"></param>
        /// <param name="url"></param>
        /// <param name="linkType"></param>
        /// <returns></returns>
        public async Task<bool> SendOpenLinkToLogin(string playerLogin, string url, int linkType) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SendOpenLinkToId", playerLogin, url, linkType)
            );

        public async Task<bool> SetBuddyNotificationAsync(string login, bool enabled) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetBuddyNotification", login, enabled)
            );

        public async Task<bool> GetBuddyNotificationAsync(string login) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("GetBuddyNotification", login)
            );

        public async Task<bool> CustomizeQuitDialogAsync(string manialinkPage, string sendToServerUrl, bool proposeAddToFavorites, int delayQuiteButton) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("CustomizeQuitDialog", manialinkPage, sendToServerUrl, proposeAddToFavorites, delayQuiteButton)
            );
        #endregion

        #region Players
        #region Kicking
        public async Task<bool> KickAsync(string login, string message=null) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("Kick", login, message)
            );

        public async Task<bool> KickIdAsync(int id, string message = null) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("KickId", id, message)
            );
        #endregion

        #region Ban List
        public async Task<bool> BanAsync(string login, string message = null) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("Ban", login, message)
            );

        public async Task<bool> BanAndBlackListAsync(string login, string message, string saveToFile=null) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("BanAndBlackList", login, message, saveToFile)
            );

        public async Task<bool> BanIdAsync(int id, string message) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("BanId", id, message)
            );

        public async Task<bool> UnBanAsync(string login) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("UnBan", login)
            );

        public async Task<bool> CleanBanListAsync() =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("CleanBanList")
            );

        public async Task<BanListEntryStruct[]> GetBanListAsync(int maxInfos, int startIndex) =>
            (BanListEntryStruct[])XmlRpcTypes.ToNativeValue<BanListEntryStruct>(
                await CallOrFaultAsync("GetBanList", maxInfos, startIndex)
            );
        #endregion

        #region Black List
        public async Task<bool> BlackListAsync(string login) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("BlackList", login)
            );

        public async Task<bool> BlackListIdAsync(int id) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("BlackListId", id)
            );

        public async Task<bool> UnBlackListAsync(string login) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("UnBlackList", login)
            );

        public async Task<BlackListEntryStruct[]> GetBlackListAsync(int maxInfos, int startIndex) =>
            (BlackListEntryStruct[])XmlRpcTypes.ToNativeValue<BlackListEntryStruct>(
                await CallOrFaultAsync("GetBanList", maxInfos, startIndex)
            );

        public async Task<bool> LoadBlackListAsync(string fileName) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("LoadBlackList", fileName)
            );

        public async Task<bool> SaveBlackListAsync(string fileName) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SaveBlackList", fileName)
            );
        #endregion

        #region Guest List
        public async Task<bool> AddGuestAsync(string login) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("AddGuest", login)
            );

        public async Task<bool> AddGuestIdAsync(int id) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("AddGuestId", id)
            );

        public async Task<bool> RemoveGuestAsync(string login) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("RemoveGuest", login)
            );

        public async Task<bool> RemoveGuestIdAsync(int id) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("RemoveGuestId", id)
            );

        public async Task<bool> CleanGuestListAsync() =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("CleanGuestList")
            );

        public async Task<GuestListEntryStruct[]> GuestListEntryStructAsync(int maxInfos, int startIndex) =>
            (GuestListEntryStruct[])XmlRpcTypes.ToNativeValue<GuestListEntryStruct>(
                await CallOrFaultAsync("GuestListEntryStruct", maxInfos, startIndex)
            );

        public async Task<bool> LoadGuestListAsync(string fileName) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("LoadGuestList", fileName)
            );

        public async Task<bool> SaveGuestListAsync(string fileName) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SaveGuestList", fileName)
            );

        #endregion

        #region Ignore List
        public async Task<bool> IgnoreAsync(string login) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("Ignore", login)
            );

        public async Task<bool> IgnoreIdAsync(int id) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("IgnoreId", id)
            );

        public async Task<bool> UnIgnoreAsync(string login) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("UnIgnore", login)
            );

        public async Task<bool> UnIgnoreIdAsync(int id) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("UnIgnoreId", id)
            );

        public async Task<bool> CleanIgnoreListAsync() =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("CleanIgnoreList")
            );

        public async Task<IgnoreListEntryStruct[]> GetIgnoreListAsync(int maxInfos, int startIndex) =>
            (IgnoreListEntryStruct[])XmlRpcTypes.ToNativeValue<IgnoreListEntryStruct>(
                await CallOrFaultAsync("CleanIgnoreList", maxInfos, startIndex)
            );
        #endregion

        #region Payments & Bills
        public async Task<int> PayAsync(string login, int planets, string label) =>
            (int)XmlRpcTypes.ToNativeValue<int>(
                await CallOrFaultAsync("Pay", login, planets, label)
            );

        public async Task<int> SendBillAsync(string loginFrom, int planets, string label, string loginTo=null) =>
            (int)XmlRpcTypes.ToNativeValue<int>(
                await CallOrFaultAsync("SendBill", loginFrom, planets, label, loginTo)
            );

        public async Task<BillStateStruct> GetBillStateAsync(int billId) =>
            (BillStateStruct)XmlRpcTypes.ToNativeValue<BillStateStruct>(
                await CallOrFaultAsync("SendBill", billId)
            );
        #endregion
        #endregion

        #region Replays
        public async Task<bool> AutoSaveReplaysAsync(string autoSave) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("AutoSaveReplays", autoSave)
            );

        public async Task<bool> AutoSaveValidationReplaysAsync(string autoSave) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("AutoSaveValidationReplays", autoSave)
            );

        public async Task<bool> IsAutoSaveValidationReplaysEnabledAsync() =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("IsAutoSaveValidationReplaysEnabled")
            );

        public async Task<bool> SaveCurrentReplayAsync(string fileName) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SaveCurrentReplay", fileName)
            );

        public async Task<bool> SaveBestGhostsReplayAsync(string login, string fileName) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SaveBestGhostsReplay", login, fileName)
            );

        public async Task<Base64> GetValidationReplayAsync(string login) =>
            (Base64)XmlRpcTypes.ToNativeValue<Base64>(
                await CallOrFaultAsync("GetValidationReplay", login)
            );

        /// <summary>
        /// Set a new number of maximum points per round for team mode. Only available to Admin. Requires a map restart to be taken into account.
        /// </summary>
        /// <param name="maxPoints"></param>
        /// <returns></returns>
        public async Task<bool> SetMaxPointsTeamAsync(int maxPoints) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetMaxPointsTeam", maxPoints)
            );
        
        /// <summary>
        /// Get the current and next number of maximum points per round for team mode. The struct returned contains two fields CurrentValue and NextValue.
        /// </summary>
        /// <returns></returns>
        public async Task<CurrentNextValueStruct<int>> GetMaxPointsTeamAsync() =>
            (CurrentNextValueStruct<int>)XmlRpcTypes.ToNativeValue<CurrentNextValueStruct<int>>(
                await CallOrFaultAsync("GetMaxPointsTeam")
            );
        
        /// <summary>
        /// Set if new rules are used for team mode. Only available to Admin. Requires a map restart to be taken into account.
        /// </summary>
        /// <param name="newRules"></param>
        /// <returns></returns>
        public async Task<bool> SetUseNewRulesTeamAsync(bool newRules) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetUseNewRulesTeam", newRules)
            );
        
        /// <summary>
        /// Get if the new rules are used for team mode (Current and next values). The struct returned contains two fields CurrentValue and NextValue.
        /// </summary>
        /// <returns></returns>
        public async Task<CurrentNextValueStruct<string>> GetUseNewRulesTeamAsync() =>
            (CurrentNextValueStruct<string>)XmlRpcTypes.ToNativeValue<CurrentNextValueStruct<string>>(
                await CallOrFaultAsync("GetUseNewRulesTeam")
            );

        /// <summary>
        /// Set the points needed for victory in Cup mode. Only available to Admin. Requires a map restart to be taken into account.
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public async Task<bool> SetCupPointsLimitAsync(int points) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetCupPointsLimit", points)
            );

        /// <summary>
        /// Get the points needed for victory in Cup mode. The struct returned contains two fields CurrentValue and NextValue.
        /// </summary>
        /// <returns></returns>
        public async Task<CurrentNextValueStruct<int>> GetCupPointsLimitAsync() =>
            (CurrentNextValueStruct<int>)XmlRpcTypes.ToNativeValue<CurrentNextValueStruct<int>>(
                await CallOrFaultAsync("GetCupPointsLimit")
            );

        /// <summary>
        /// Sets the number of rounds before going to next map in Cup mode. Only available to Admin. Requires a map restart to be taken into account.
        /// </summary>
        /// <param name="nbrRounds"></param>
        /// <returns></returns>
        public async Task<bool> SetCupRoundsPerMapAsync(int nbrRounds) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetCupRoundsPerMap", nbrRounds)
            );
        
        /// <summary>
        /// Get the number of rounds before going to next map in Cup mode. The struct returned contains two fields CurrentValue and NextValue.
        /// </summary>
        /// <returns></returns>
        public async Task<CurrentNextValueStruct<int>> GetCupRoundsPerMapAsync() =>
            (CurrentNextValueStruct<int>)XmlRpcTypes.ToNativeValue<CurrentNextValueStruct<int>>(
                await CallOrFaultAsync("GetCupRoundsPerMap")
            );

        /// <summary>
        /// Set whether to enable the automatic warm-up phase in Cup mode. 0 = no, otherwise it's the duration of the phase, expressed in number of rounds. Only available to Admin. Requires a map restart to be taken into account.
        /// </summary>
        /// <param name="duration">0 = no, otherwise it's the duration of the phase, expressed in number of rounds.</param>
        /// <returns></returns>
        public async Task<bool> SetCupWarmUpDurationAsync(int duration) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetCupWarmUpDuration", duration)
            );

        /// <summary>
        /// Get whether the automatic warm-up phase is enabled in Cup mode. The struct returned contains two fields CurrentValue and NextValue.
        /// </summary>
        /// <returns></returns>
        public async Task<CurrentNextValueStruct<int>> GetCupWarmUpDurationAsync() =>
            (CurrentNextValueStruct<int>)XmlRpcTypes.ToNativeValue<CurrentNextValueStruct<int>>(
                await CallOrFaultAsync("GetCupWarmUpDuration")
            );

        /// <summary>
        /// Set the number of winners to determine before the match is considered over. Only available to Admin. Requires a map restart to be taken into account.
        /// </summary>
        /// <param name="nbWinners">Number of winners</param>
        /// <returns></returns>
        public async Task<bool> SetCupNbWinnersAsync(int nbWinners) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetCupNbWinners", nbWinners)
            );

        /// <summary>
        /// Get the number of winners to determine before the match is considered over. The struct returned contains two fields CurrentValue and NextValue.
        /// </summary>
        /// <returns></returns>
        public async Task<CurrentNextValueStruct<int>> GetCupNbWinnersAsync() =>
            (CurrentNextValueStruct<int>)XmlRpcTypes.ToNativeValue<CurrentNextValueStruct<int>>(
                await CallOrFaultAsync("GetCupNbWinners")
            );

        /// <summary>
        /// Returns the current map index in the selection, or -1 if the map is no longer in the selection.
        /// </summary>
        /// <returns></returns>
        public async Task<int> GetCurrentMapIndexAsync() =>
            (int)XmlRpcTypes.ToNativeValue<int>(
                await CallOrFaultAsync("GetCurrentMapIndex")
            );
        
        /// <summary>
        /// Returns the map index in the selection that will be played next (unless the current one is restarted...)
        /// </summary>
        /// <returns></returns>
        public async Task<int> GetNextMapIndexAsync() =>
            (int)XmlRpcTypes.ToNativeValue<int>(
                await CallOrFaultAsync("GetNextMapIndex")
            );

        /// <summary>
        /// Sets the map index in the selection that will be played next (unless the current one is restarted...)
        /// </summary>
        /// <param name="mapIndex"></param>
        /// <returns></returns>
        public async Task<bool> SetNextMapIndexAsync(int mapIndex) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetNextMapIndex")
            );
        
        /// <summary>
        /// Immediately jumps to the map designated by its identifier (it must be in the selection).
        /// </summary>
        /// <param name="mapId"></param>
        /// <returns></returns>
        public async Task<bool> SetNextMapIdentAsync(string mapId) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetNextMapIdent", mapId)
            );

        /// <summary>
        /// Immediately jumps to the map designated by the index in the selection.
        /// </summary>
        /// <param name="mapIndex"></param>
        /// <returns></returns>
        public async Task<bool> JumpToMapIndexAsync(int mapIndex) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("JumpToMapIndex")
            );
        
        /// <summary>
        /// Immediately jumps to the map designated by its identifier (it must be in the selection).
        /// </summary>
        /// <param name="mapId"></param>
        /// <returns></returns>
        public async Task<bool> JumpToMapIdentAsync(string mapId) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("JumpToMapIdent", mapId)
            );

        /// <summary>
        /// Returns a struct containing the infos for the current map. The struct contains the following fields : Name, UId, FileName, Author, Environnement, Mood, BronzeTime, SilverTime, GoldTime, AuthorTime, CopperPrice, LapRace, MapType, MapStyle.
        /// (NbLaps and NbCheckpoints are also present but always set to -1)
        /// </summary>
        /// <returns></returns>
        public async Task<MapStruct> GetCurrentMapInfoAsync() =>
            (MapStruct)XmlRpcTypes.ToNativeValue<MapStruct>(
                await CallOrFaultAsync("GetCurrentMapInfo")
            );

        /// <summary>
        /// Returns a struct containing the infos for the next map. The struct contains the following fields : Name, UId, FileName, Author, Environnement, Mood, BronzeTime, SilverTime, GoldTime, AuthorTime, CopperPrice, LapRace, MapType, MapStyle.
        /// (NbLaps and NbCheckpoints are also present but always set to -1)
        /// </summary>
        /// <returns></returns>
        public async Task<MapStruct> GetNextMapInfoAsync() =>
            (MapStruct)XmlRpcTypes.ToNativeValue<MapStruct>(
                await CallOrFaultAsync("GetNextMapInfo")
            );

        /// <summary>
        /// Returns a struct containing the infos for the map with the specified filename. The struct contains the following fields : Name, UId, FileName, Author, Environnement, Mood, BronzeTime, SilverTime, GoldTime, AuthorTime, CopperPrice, LapRace, MapType, MapStyle.
        /// (NbLaps and NbCheckpoints are also present but always set to -1)
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public async Task<MapStruct> GetMapInfoAsync(string filename) =>
            (MapStruct)XmlRpcTypes.ToNativeValue<MapStruct>(
                await CallOrFaultAsync("GetMapInfo", filename)
            );
        
        /// <summary>
        /// Returns a boolean if the map with the specified filename matches the current server settings.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public async Task<bool> CheckMapForCurrentServerParamsAsync(string filename) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("CheckMapForCurrentServerParams", filename)
            );

        /// <summary>
        /// Returns a list of maps among the current selection of the server. This method take two parameters. The first parameter specifies the maximum number of infos to be returned, and the second one the starting index in the selection.
        /// The list is an array of structures. Each structure contains the following fields : Name, UId, FileName, Environnement, Author, GoldTime, CopperPrice, MapType, MapStyle.
        /// </summary>
        /// <param name="maxInfos"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public async Task<MapStruct[]> GetMapListAsync(int maxInfos, int startIndex) =>
            (MapStruct[])XmlRpcTypes.ToNativeValue<MapStruct[]>(
                await CallOrFaultAsync("GetMapList", maxInfos, startIndex)
            );

        /// <summary>
        /// Add the map with the specified filename at the end of the current selection. Only available to Admin.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public async Task<bool> AddMapAsync(string filename) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("AddMap", filename)
            );

        /// <summary>
        /// Add the list of maps with the specified filenames at the end of the current selection. The list of maps to add is an array of strings. Only available to Admin.
        /// </summary>
        /// <param name="filenames"></param>
        /// <returns></returns>
        public async Task<int> AddMapListAsync(Array filenames) =>
            (int)XmlRpcTypes.ToNativeValue<int>(
                await CallOrFaultAsync("AddMapList", filenames)
            );

        /// <summary>
        /// Remove the map with the specified filename from the current selection. Only available to Admin.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public async Task<bool> RemoveMapAsync(string filename) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("RemoveMap", filename)
            );

        /// <summary>
        /// Remove the list of maps with the specified filenames from the current selection. The list of maps to remove is an array of strings. Only available to Admin.
        /// </summary>
        /// <param name="filenames"></param>
        /// <returns></returns>
        public async Task<int> RemoveMapListAsync(Array filenames) =>
            (int)XmlRpcTypes.ToNativeValue<int>(
                await CallOrFaultAsync("RemoveMapList", filenames)
            );

        /// <summary>
        /// Insert the map with the specified filename after the current map. Only available to Admin.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public async Task<bool> InsertMapAsync(string filename) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("InsertMap", filename)
            );
        
        /// <summary>
        /// Insert the list of maps with the specified filenames after the current map. The list of maps to insert is an array of strings. Only available to Admin.
        /// </summary>
        /// <param name="filenames"></param>
        /// <returns></returns>
        public async Task<int> InsertMapListAsync(Array filenames) =>
            (int)XmlRpcTypes.ToNativeValue<int>(
                await CallOrFaultAsync("InsertMapList", filenames)
            );

        /// <summary>
        /// Set as next map the one with the specified filename, if it is present in the selection. Only available to Admin.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public async Task<bool> ChooseNextMapAsync(string filename) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("ChooseNextMap", filename)
            );

        /// <summary>
        /// Set as next maps the list of maps with the specified filenames, if they are present in the selection. The list of maps to choose is an array of strings. Only available to Admin.
        /// </summary>
        /// <param name="filenames"></param>
        /// <returns></returns>
        public async Task<int> ChooseNextMapListAsync(Array filenames) =>
            (int)XmlRpcTypes.ToNativeValue<int>(
                await CallOrFaultAsync("ChooseNextMapList", filenames)
            );

        /// <summary>
        /// Set a list of maps defined in the playlist with the specified filename as the current selection of the server, and load the gameinfos from the same file. Only available to Admin.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public async Task<int> LoadMatchSettingsAsync(string filename) =>
            (int)XmlRpcTypes.ToNativeValue<int>(
                await CallOrFaultAsync("LoadMatchSettings", filename)
            );

        /// <summary>
        /// Add a list of maps defined in the playlist with the specified filename at the end of the current selection. Only available to Admin.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public async Task<int> AppendPlaylistFromMatchSettingsAsync(string filename) =>
            (int)XmlRpcTypes.ToNativeValue<int>(
                await CallOrFaultAsync("AppendPlaylistFromMatchSettings", filename)
            );

        /// <summary>
        /// Save the current selection of map in the playlist with the specified filename, as well as the current gameinfos. Only available to Admin.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public async Task<int> SaveMatchSettingsAsync(string filename) =>
            (int)XmlRpcTypes.ToNativeValue<int>(
                await CallOrFaultAsync("SaveMatchSettings", filename)
            );

        /// <summary>
        /// Returns the list of players on the server. This method take two parameters. The first parameter specifies the maximum number of infos to be returned, and the second one the starting index in the list, an optional 3rd parameter is used for compatibility: struct version (0 = united, 1 = forever, 2 = forever, including the servers).
        /// The list is an array of PlayerInfo structures. Forever PlayerInfo struct is: Login, NickName, PlayerId, TeamId, SpectatorStatus, LadderRanking, and Flags.
        /// LadderRanking is 0 when not in official mode,
        /// Flags = ForceSpectator(0,1,2) + IsReferee * 10 + IsPodiumReady * 100 + StereoDisplayMode * 1000 + IsManagedByAnOtherServer * 10000 + IsServer * 100000 + HasPlayerSlot * 1000000 + IsBroadcasting * 10000000 + HasJoinedGame * 100000000
        /// SpectatorStatus = Spectator + TemporarySpectator * 10 + PureSpectator * 100 + AutoTarget * 1000 + CurrentTargetId * 10000
        /// </summary>
        /// <param name="maxInfos"></param>
        /// <param name="startIndex"></param>
        /// <param name="serverType">OPTIONAL: Used for compatibility: struct version (0 = united, 1 = forever, 2 = forever, including the servers)</param>
        /// <returns></returns>
        public async Task<PlayerInfoStruct[]> GetPlayerListAsync(int maxInfos, int startIndex, int? serverType = -1) =>
            (PlayerInfoStruct[])XmlRpcTypes.ToNativeValue<PlayerInfoStruct[]>(
                await CallOrFaultAsync("GetPlayerList", maxInfos, startIndex, serverType)
            );

        /// <summary>
        /// Returns a struct containing the infos on the player with the specified login, with an optional parameter for compatibility: struct version (0 = united, 1 = forever). The structure is identical to the ones from GetPlayerList. Forever PlayerInfo struct is: Login, NickName, PlayerId, TeamId, SpectatorStatus, LadderRanking, and Flags.
        /// LadderRanking is 0 when not in official mode,
        /// Flags = ForceSpectator(0,1,2) + IsReferee * 10 + IsPodiumReady * 100 + StereoDisplayMode * 1000 + IsManagedByAnOtherServer * 10000 + IsServer * 100000 + HasPlayerSlot * 1000000 + IsBroadcasting * 10000000 + HasJoinedGame * 100000000
        /// SpectatorStatus = Spectator + TemporarySpectator * 10 + PureSpectator * 100 + AutoTarget * 1000 + CurrentTargetId * 10000
        /// Each structure of the array Skins contains two fields Environnement and a struct PackDesc. Each structure PackDesc, as well as the struct Avatar, contains two fields FileName and Checksum.
        /// </summary>
        /// <param name="playerLogin"></param>
        /// <param name="serverType"></param>
        /// <returns></returns>
        public async Task<PlayerInfoStruct> GetPlayerInfoAsync(string playerLogin, int serverType) =>
            (PlayerInfoStruct)XmlRpcTypes.ToNativeValue<PlayerInfoStruct>(
                await CallOrFaultAsync("GetPlayerInfo", playerLogin, serverType)
            );

        /// <summary>
        /// Returns a struct containing the infos on the player with the specified login. The structure contains the following fields : Login, NickName, PlayerId, TeamId, IPAddress, DownloadRate, UploadRate, Language, IsSpectator, IsInOfficialMode, a structure named Avatar, an array of structures named Skins, a structure named LadderStats, HoursSinceZoneInscription and OnlineRights (0: nations account, 3: united account).
        /// Each structure of the array Skins contains two fields Environnement and a struct PackDesc. Each structure PackDesc, as well as the struct Avatar, contains two fields FileName and Checksum.
        /// </summary>
        /// <param name="playerLogin"></param>
        /// <returns></returns>
        public async Task<PlayerDetailedInfoStruct> GetDetailedPlayerInfoAsync(string playerLogin) =>
            (PlayerDetailedInfoStruct)XmlRpcTypes.ToNativeValue<PlayerDetailedInfoStruct>(
                await CallOrFaultAsync("GetDetailedPlayerInfo", playerLogin)
            );
        
        /// <summary>
        /// Returns a struct containing the player infos of the game server (ie: in case of a basic server, itself; in case of a relay server, the main server), with an optional parameter for compatibility: struct version (0 = united, 1 = forever).
        /// The structure is identical to the ones from GetPlayerList. Forever PlayerInfo struct is: Login, NickName, PlayerId, TeamId, SpectatorStatus, LadderRanking, and Flags.
        /// LadderRanking is 0 when not in official mode,
        /// Flags = ForceSpectator(0,1,2) + IsReferee * 10 + IsPodiumReady * 100 + StereoDisplayMode * 1000 + IsManagedByAnOtherServer * 10000 + IsServer * 100000 + HasPlayerSlot * 1000000 + IsBroadcasting * 10000000 + HasJoinedGame * 100000000
        /// SpectatorStatus = Spectator + TemporarySpectator * 10 + PureSpectator * 100 + AutoTarget * 1000 + CurrentTargetId * 10000
        /// Each structure contains the following fields : Login, NickName, PlayerId and Rank. In addition, for legacy trackmania modes it also contains BestTime, Score, NbrLapsFinished, LadderScore, and an array BestCheckpoints that contains the checkpoint times for the best race. 
        /// </summary>
        /// <param name="serverType"></param>
        /// <returns></returns>
        public async Task<PlayerInfoStruct> GetMainServerPlayerInfoAsync(int serverType) =>
            (PlayerInfoStruct)XmlRpcTypes.ToNativeValue<PlayerInfoStruct>(
                await CallOrFaultAsync("GetMainServerPlayerInfo", serverType)
            );

        /// <summary>
        /// Returns the current rankings for the race in progress. (In trackmania legacy team modes, the scores for the two teams are returned. In other modes, it's the individual players' scores) This method take two parameters. The first parameter specifies the maximum number of infos to be returned, and the second one the starting index in the ranking.  The ranking returned is a list of structures.
        /// Each structure contains the following fields : Login, NickName, PlayerId and Rank. In addition, for legacy trackmania modes it also contains BestTime, Score, NbrLapsFinished, LadderScore, and an array BestCheckpoints that contains the checkpoint times for the best race. 
        /// </summary>
        /// <param name="playerLogin"></param>
        /// <returns></returns>
        public async Task<PlayerRankingStruct[]> GetCurrentRankingAsync(int maxInfos, int startRatingIndex) =>
            (PlayerRankingStruct[])XmlRpcTypes.ToNativeValue<PlayerRankingStruct[]>(
                await CallOrFaultAsync("GetCurrentRanking")
            );

        /// <summary>
        /// Returns the current ranking for the race in progressof the player with the specified login (or list of comma-separated logins). The ranking returned is a list of structures.
        /// Each structure contains the following fields : Login, NickName, PlayerId and Rank. In addition, for legacy trackmania modes it also contains BestTime, Score, NbrLapsFinished, LadderScore, and an array BestCheckpoints that contains the checkpoint times for the best race. 
        /// </summary>
        /// <param name="playerLogin"></param>
        /// <returns></returns>
        public async Task<PlayerRankingStruct[]> GetCurrentRankingForLoginAsync(string playerLogin) =>
            (PlayerRankingStruct[])XmlRpcTypes.ToNativeValue<PlayerRankingStruct[]>(
                await CallOrFaultAsync("GetCurrentRankingForLogin")
            );
        
        /// <summary>
        /// Returns the current winning team for the race in progress. (-1: if not in team mode, or draw match)
        /// </summary>
        /// <returns></returns>
        public async Task<int> GetCurrentWinnerTeamAsync() =>
            (int)XmlRpcTypes.ToNativeValue<int>(
                await CallOrFaultAsync("GetCurrentWinnerTeam")
            );

        /// <summary>
        /// Force the scores of the current game. Only available in rounds and team mode. You have to pass an array of structs {int PlayerId, int Score}.
        /// And a boolean SilentMode - if true, the scores are silently updated (only available for SuperAdmin), allowing an external controller to do its custom counting... Only available to Admin/SuperAdmin.
        /// </summary>
        /// <param name="playerScores"></param>
        /// <param name="silentMode"></param>
        /// <returns></returns>
        public async Task<bool> ForceScoresAsync(PlayerScoreStruct[] playerScores, bool silentMode) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("ForceScores", playerScores, silentMode)
            );

        /// <summary>
        /// Force the team of the player. Only available in team mode. You have to pass the login and the team number (0 or 1). Only available to Admin.
        /// </summary>
        /// <param name="playerLogin"></param>
        /// <param name="cameraType"></param>
        /// <returns></returns>
        public async Task<bool> ForcePlayerTeamAsync(int playerLogin, int cameraType) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("ForcePlayerTeam", playerLogin, cameraType)
            );

        /// <summary>
        /// Force the team of the player. Only available in team mode. You have to pass the playerid and the team number (0 or 1). Only available to Admin.
        /// </summary>
        /// <param name="playerId"></param>
        /// <param name="cameraType"></param>
        /// <returns></returns>
        public async Task<bool> ForcePlayerTeamIdAsync(int playerId, int cameraType) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("ForcePlayerTeamId", playerId, cameraType)
            );

        /// <summary>
        /// Force the spectating status of the player. You have to pass the login and the spectator mode (0: user selectable, 1: spectator, 2: player, 3: spectator but keep selectable). Only available to Admin.
        /// </summary>
        /// <param name="playerLogin"></param>
        /// <param name="cameraType"></param>
        /// <returns></returns>
        public async Task<bool> ForceSpectatorAsync(int playerLogin, int cameraType) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("ForceSpectator", playerLogin, cameraType)
            );

        /// <summary>
        /// Force the spectating status of the player. You have to pass the playerid and the spectator mode (0: user selectable, 1: spectator, 2: player, 3: spectator but keep selectable). Only available to Admin.
        /// </summary>
        /// <param name="playerId"></param>
        /// <param name="cameraType"></param>
        /// <returns></returns>
        public async Task<bool> ForceSpectatorIdAsync(int playerId, int cameraType) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("ForceSpectatorId", playerId, cameraType)
            );

        /// <summary>
        /// Force spectators to look at a specific player. You have to pass the login of the spectator (or '' for all) and the login of the target (or '' for automatic),
        /// and an integer for the camera type to use (-1 = leave unchanged, 0 = replay, 1 = follow, 2 = free). Only available to Admin.
        /// </summary>
        /// <param name="playerLogin"></param>
        /// <param name="targetLogin"></param>
        /// <param name="cameraType"></param>
        /// <returns></returns>
        public async Task<bool> ForceSpectatorTargetAsync(string playerLogin, string targetLogin, int cameraType) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("ForceSpectatorTarget", playerLogin, targetLogin, cameraType)
            );
        
        /// <summary>
        /// Force spectators to look at a specific player. You have to pass the id of the spectator (or -1 for all) and the id of the target (or -1 for automatic),
        /// and an integer for the camera type to use (-1 = leave unchanged, 0 = replay, 1 = follow, 2 = free). Only available to Admin.
        /// </summary>
        /// <param name="playerId"></param>
        /// <param name="targetId"></param>
        /// <param name="cameraType"></param>
        /// <returns></returns>
        public async Task<bool> ForceSpectatorTargetIdAsync(int playerId, int targetId, int cameraType) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("ForceSpectatorTargetId", playerId, targetId, cameraType)
            );

        /// <summary>
        /// Pass the login of the spectator. A spectator that once was a player keeps his player slot, so that he can go back to race mode.
        /// Calling this function frees this slot for another player to connect. Only available to Admin.
        /// </summary>
        /// <param name="playerLogin"></param>
        /// <returns></returns>
        public async Task<bool> SpectatorReleasePlayerSlotAsync(string playerLogin) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SpectatorReleasePlayerSlot", playerLogin)
            );

        /// <summary>
        /// Pass the playerid of the spectator. A spectator that once was a player keeps his player slot, so that he can go back to race mode.
        /// Calling this function frees this slot for another player to connect. Only available to Admin.
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns>
        public async Task<bool> SpectatorReleasePlayerSlotIdAsync(int playerId) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SpectatorReleasePlayerSlotId", playerId)
            );
        
        /// <summary>
        /// Enable control of the game flow: the game will wait for the caller to validate state transitions. Only available to Admin.
        /// </summary>
        /// <param name="enabled"></param>
        /// <returns></returns>
        public async Task<bool> ManualFlowControlEnableAsync(bool enabled) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("ManualFlowControlEnable", enabled)
            );

        /// <summary>
        /// Allows the game to proceed. Only available to Admin.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> ManualFlowControlProceedAsync() =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("ManualFlowControlProceed")
            );

        /// <summary>
        /// Returns whether the manual control of the game flow is enabled. 0 = no, 1 = yes by the xml-rpc client making the call, 2 = yes, by some other xml-rpc client. Only available to Admin.
        /// </summary>
        /// <returns></returns>
        public async Task<int> ManualFlowControlIsEnabledAsync() =>
            (int)XmlRpcTypes.ToNativeValue<int>(
                await CallOrFaultAsync("ManualFlowControlIsEnabled")
            );

        /// <summary>
        /// Returns the transition that is currently blocked, or '' if none. (That's exactly the value last received by the callback.) Only available to Admin.
        /// </summary>
        /// <returns></returns>
        public async Task<string> ManualFlowControlGetCurTransitionAsync() =>
            (string)XmlRpcTypes.ToNativeValue<string>(
                await CallOrFaultAsync("ManualFlowControlGetCurTransition")
            );
        
        /// <summary>
        /// Returns the current match ending condition. Return values are: 'Playing', 'ChangeMap' or 'Finished'.
        /// </summary>
        /// <returns></returns>
        public async Task<string> CheckEndMatchConditionAsync() =>
            (string)XmlRpcTypes.ToNativeValue<string>(
                await CallOrFaultAsync("CheckEndMatchCondition")
            );

        /// <summary>
        /// Returns a struct containing the networks stats of the server. The structure contains the following fields : Uptime, NbrConnection, MeanConnectionTime, MeanNbrPlayer, RecvNetRate, SendNetRate, TotalReceivingSize, TotalSendingSize and an array of structures named PlayerNetInfos.
        /// Each structure of the array PlayerNetInfos contains the following fields : Login, IPAddress, LastTransferTime, DeltaBetweenTwoLastNetState, PacketLossRate. Only available to SuperAdmin.
        /// </summary>
        /// <returns></returns>
        public async Task<NetworkStatsStruct> GetNetworkStatsAsync() =>
            (NetworkStatsStruct)XmlRpcTypes.ToNativeValue<NetworkStatsStruct>(
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
        #endregion
    }
}
