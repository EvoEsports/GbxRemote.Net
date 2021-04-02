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

        public async Task<bool> UnsetServerTagAsync(string name,) =>
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

        public async Task<MaxPlayersStruct> GetMaxPlayersAsync() =>
            (MaxPlayersStruct)XmlRpcTypes.ToNativeValue<MaxPlayersStruct>(
                await CallOrFaultAsync("GetMaxPlayers")
            );

        public async Task<bool> SetMaxSpectatorsAsync(int maxPlayers) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetMaxSpectators", maxPlayers)
            );

        public async Task<MaxPlayersStruct> GetMaxSpectatorsAsync() =>
            (MaxPlayersStruct)XmlRpcTypes.ToNativeValue<MaxPlayersStruct>(
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
        public async Task<CallVoteTimeOutStruct> GetCallVoteTimeOutAsync() =>
            (CallVoteTimeOutStruct)XmlRpcTypes.ToNativeValue<CallVoteTimeOutStruct>(
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
        #endregion

        #region Players
        public async Task<bool> KickAsync(string login, string message=null) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("Kick", login, message)
            );

        public async Task<bool> KickIdAsync(int id, string message = null) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("KickId", id, message)
            );

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
    }
}
