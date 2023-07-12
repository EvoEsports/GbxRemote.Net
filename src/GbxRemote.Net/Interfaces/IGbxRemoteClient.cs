using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GbxRemoteNet.Enums;
using GbxRemoteNet.Events;
using GbxRemoteNet.Interfaces.XmlRpc;
using GbxRemoteNet.Structs;
using GbxRemoteNet.XmlRpc.ExtraTypes;
using GbxRemoteNet.XmlRpc.Packets;
using GbxRemoteNet.XmlRpc.Types;
using Newtonsoft.Json.Linq;

namespace GbxRemoteNet.Interfaces;

public interface IGbxRemoteClient : INadeoXmlRpcClient
{
    #region General Methods

    /// <summary>
    /// Call a remote method and throw an exception if a fault occured.
    /// </summary>
    /// <param name="method">Name of the method to call.</param>
    /// <param name="args">Arguments to pass to the method.</param>
    /// <returns></returns>
    public Task<XmlRpcBaseType> CallOrFaultAsync(string method, params object[] args);

    /// <summary>
    /// Call a remote method on the server and return the received message.
    /// </summary>
    /// <param name="method">Name of the method to call.</param>
    /// <param name="args">Arguments to pass to the method.</param>
    /// <returns></returns>
    public Task<ResponseMessage> CallMethodAsync(string method, params object[] args);

    /// <summary>
    /// Convert C# type values to XML-RPC type values and create an argument list.
    /// </summary>
    /// <param name="args">Arguments to convert.</param>
    /// <returns></returns>
    public XmlRpcBaseType[] MethodArgs(params object[] args);

    /// <summary>
    /// Connect and login to GBXRemote.
    /// </summary>
    /// <param name="login">Username to login with.</param>
    /// <param name="password">Password for the provided username.</param>
    /// <returns></returns>
    public Task<bool> LoginAsync(string login, string password);

    #endregion

    #region Callbacks

    public delegate Task AsyncEventHandler(object sender, EventArgs e);

    public delegate Task AsyncEventHandler<TArgs>(object sender, TArgs e);
    
    /// <summary>
    /// Triggered for all possible callbacks.
    /// </summary>
    public event AsyncEventHandler<CallbackGbxEventArgs<object>> OnAnyCallback;
    
    /// <summary>
    /// When a player connects to the server.
    /// </summary>
    public event AsyncEventHandler<PlayerConnectGbxEventArgs> OnPlayerConnect;

    /// <summary>
    /// When a player disconnects from the server.
    /// </summary>
    public event AsyncEventHandler<PlayerDisconnectGbxEventArgs> OnPlayerDisconnect;

    /// <summary>
    /// When a player sends a chat message.
    /// </summary>
    public event AsyncEventHandler<PlayerChatGbxEventArgs> OnPlayerChat;

    /// <summary>
    /// When a echo message is sent. Can be used for communication with other.
    /// XMLRPC-clients.
    /// </summary>
    public event AsyncEventHandler<EchoGbxEventArgs> OnEcho;

    /// <summary>
    /// When the match itself starts, triggered after begin map.
    /// </summary>
    public event AsyncEventHandler OnBeginMatch;

    /// <summary>
    /// When the match ends, does not give a lot of info in TM2020.
    /// </summary>
    public event AsyncEventHandler<EndMatchGbxEventArgs> OnEndMatch;

    /// <summary>
    /// When the map has loaded on the server.
    /// </summary>
    public event AsyncEventHandler<MapGbxEventArgs> OnBeginMap;

    /// <summary>
    /// When the map unloads from the server.
    /// </summary>
    public event AsyncEventHandler<MapGbxEventArgs> OnEndMap;

    /// <summary>
    /// When the server status changed.
    /// </summary>
    public event AsyncEventHandler<StatusChangedGbxEventArgs> OnStatusChanged;

    /// <summary>
    /// When data about a player changed, it is usually called when
    /// a player joins or leaves. Gives you more detailed info about a player.
    /// </summary>
    public event AsyncEventHandler<PlayerInfoChangedGbxEventArgs> OnPlayerInfoChanged;

    /// <summary>
    /// When a user triggers the page answer callback from a manialink.
    /// </summary>
    public event AsyncEventHandler<ManiaLinkPageActionGbxEventArgs> OnPlayerManialinkPageAnswer;

    /// <summary>
    /// Triggered when the map list changed.
    /// </summary>
    public event AsyncEventHandler<MapListModifiedGbxEventArgs> OnMapListModified;

    /// <summary>
    /// When the server is about to start.
    /// </summary>
    public event AsyncEventHandler OnServerStart;

    /// <summary>
    /// When the server is about to stop.
    /// </summary>
    public event AsyncEventHandler OnServerStop;

    /// <summary>
    /// Tunnel data received from a player.
    /// </summary>
    public event AsyncEventHandler<TunnelDataGbxEventArgs> OnTunnelDataReceived;

    /// <summary>
    /// When a current vote has been updated.
    /// </summary>
    public event AsyncEventHandler<VoteUpdatedGbxEventArgs> OnVoteUpdated;

    /// <summary>
    /// When a player bill is updated.
    /// </summary>
    public event AsyncEventHandler<BillUpdatedGbxEventArgs> OnBillUpdated;

    /// <summary>
    /// When a player changed allies.
    /// </summary>
    public event AsyncEventHandler<PlayerGbxEventArgs> OnPlayerAlliesChanged;

    /// <summary>
    /// When a variable from the script cloud is loaded.
    /// </summary>
    public event AsyncEventHandler<ScriptCloudGbxEventArgs> OnScriptCloudLoadData;

    /// <summary>
    /// When a variable from the script cloud is saved.
    /// </summary>
    public event AsyncEventHandler<ScriptCloudGbxEventArgs> OnScriptCloudSaveData;

    /// <summary>
    /// Enable callbacks.
    /// </summary>
    /// <param name="gbxCallbackType">The callback type to enable. Can be combined.</param>
    /// <returns></returns>
    public Task EnableCallbackTypeAsync(GbxCallbackType gbxCallbackType);

    /// <summary>
    /// Enable all callbacks.
    /// </summary>
    /// <returns></returns>
    public Task EnableCallbackTypeAsync();
    #endregion

    #region Methods

    #region Chat

    /// <summary>
    /// Send a text message to all clients without the server login. Only available to Admin.
    /// </summary>
    /// <param name="message">The message to send.</param>
    /// <returns></returns>
    public Task<bool> ChatSendServerMessageAsync(string message);

    /// <summary>
    /// Send a localised text message to all clients without the server login, or optionally to a Login (which can be a
    /// single login or a list of comma-separated logins). The parameter is an array of structures {Lang='xx', Text='...'}.
    /// If no matching language is found, the last text in the array is used. Only available to Admin.
    /// </summary>
    /// <param name="lang">Localization language to use.</param>
    /// <param name="message">The default message to use if no language is matched.</param>
    /// <returns></returns>
    public Task<bool> ChatSendServerMessageToLanguageAsync(TmLanguage[] lang, string message);

    /// <summary>
    /// Send a text message without the server login to the client with the specified PlayerId. Only available to Admin.
    /// </summary>
    /// <param name="message">The message to send.</param>
    /// <param name="loginId">The ID of the player to send to.</param>
    /// <returns></returns>
    public Task<bool> ChatSendServerMessageToIdAsync(string message, int loginId);

    /// <summary>
    /// Send a text message without the server login to the client with the specified login. Login can be a single login or
    /// a list of comma-separated logins. Only available to Admin.
    /// </summary>
    /// <param name="message">The message to send.</param>
    /// <param name="playerLogin">Single login or comma separated list of logins.</param>
    /// <returns></returns>
    public Task<bool> ChatSendServerMessageToLoginAsync(string message, string playerLogin);

    /// <summary>
    /// Send a text message without the server login to the client with the specified login. Login can be a single login or
    /// a list of comma-separated logins. Only available to Admin.
    /// </summary>
    /// <param name="message">The message to send.</param>
    /// <param name="playerLogins">Logins to send the message to.</param>
    /// <returns></returns>
    public Task<bool> ChatSendServerMessageToLoginAsync(string message, IEnumerable<string> playerLogins);

    /// <summary>
    /// Send a text message to all clients. Only available to Admin.
    /// </summary>
    /// <param name="message">Message to send in the chat.</param>
    /// <returns></returns>
    public Task<bool> ChatSendAsync(string message);

    /// <summary>
    /// Send a localised text message to all clients, or optionally to a Login (which can be a single login or a list of
    /// comma-separated logins). The parameter is an array of structures {Lang='xx', Text='...'}. If no matching language
    /// is found, the last text in the array is used. Only available to Admin.
    /// </summary>
    /// <param name="lang">List of localizations.</param>
    /// <param name="message">Default message to use.</param>
    /// <returns></returns>
    public Task<bool> ChatSendToLanguageAsync(TmLanguage[] lang, string message);

    /// <summary>
    /// Send a text message to the client with the specified login. Login can be a single login or a list of
    /// comma-separated logins. Only available to Admin.
    /// </summary>
    /// <param name="message">Message to send to the chat.</param>
    /// <param name="playerLogins">Single login or list of comma separated list of logins.</param>
    /// <returns></returns>
    public Task<bool> ChatSendToLoginAsync(string message, string playerLogins);

    /// <summary>
    /// Send a text message to the client with the specified login. Login can be a single login or a list of
    /// comma-separated logins. Only available to Admin.
    /// </summary>
    /// <param name="message">Message to send to the chat.</param>
    /// <param name="playerLogins">List of logins to send the message to.</param>
    public Task<bool> ChatSendToLoginAsync(string message, IEnumerable<string> playerLogins);

    /// <summary>
    /// Send a text message to the client with the specified PlayerId. Only available to Admin.
    /// </summary>
    /// <param name="message">message to send.</param>
    /// <param name="playerId">The player ID to send the message to.</param>
    /// <returns></returns>
    public Task<bool> ChatSendToIdAsync(string message, int playerId);

    /// <summary>
    /// Returns the last chat lines. Maximum of 40 lines. Only available to Admin.
    /// </summary>
    /// <returns></returns>
    public Task<string[]> GetChatLinesAsync();

    /// <summary>
    /// The chat messages are no longer dispatched to the players, they only go to the rpc callback and the controller has
    /// to manually forward them. The second (optional) parameter allows all messages from the server to be automatically
    /// forwarded. Only available to Admin.
    /// </summary>
    /// <param name="enable">Whether to enable manual chat routing or not.</param>
    /// <param name="forward">Whether to forward all messages to the chat.</param>
    /// <returns></returns>
    public Task<bool> ChatEnableManualRoutingAsync(bool enable, bool forward);
    
    /// <summary>
    /// Enable manual chat routing and disable forwarding.
    /// </summary>
    /// <returns></returns>
    public Task<bool> ChatEnableManualRoutingAsync();
    
    /// <summary>
    /// (Text, SenderLogin, DestLogin) Send a text message to the specified DestLogin (or everybody if empty) on behalf of
    /// SenderLogin. DestLogin can be a single login or a list of comma-separated logins. Only available if manual routing
    /// is enabled. Only available to Admin.
    /// </summary>
    /// <param name="text">Message to send.</param>
    /// <param name="senderLogin">The login that sent the message.</param>
    /// <param name="destinationLogin">The login that will receive the message.</param>
    /// <returns></returns>
    public Task<bool> ChatForwardToLoginAsync(string text, string senderLogin, string destinationLogin);

    #endregion

    #region Client

    /// <summary>
    /// Display a notice on all clients. The parameters are the text message to display, and the login of the avatar to
    /// display next to it (or '' for no avatar), and an optional 'variant' in [0 = normal, 1 = Sad, 2 = Happy]. Only
    /// available to Admin.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="playerLogin"></param>
    /// <param name="variant"></param>
    /// <returns></returns>
    public Task<bool> SendNoticeAsync(string message, string playerLogin, int variant);

    /// <summary>
    /// Display a notice on the client with the specified UId. The parameters are the Uid of the client to whom the notice
    /// is sent, the text message to display, and the UId of the avatar to display next to it (or '255' for no avatar), and
    /// an optional 'variant' in [0 = normal, 1 = Sad, 2 = Happy]. Only available to Admin.
    /// </summary>
    /// <param name="clientUid"></param>
    /// <param name="message"></param>
    /// <param name="avatarUid"></param>
    /// <param name="variant"></param>
    /// <returns></returns>
    public Task<bool> SendNoticeToIdAsync(int clientUid, string message, int avatarUid, int variant);

    /// <summary>
    /// Display a notice on the client with the specified login. The parameters are the login of the client to whom the
    /// notice is sent, the text message to display, and the login of the avatar to display next to it (or '' for no
    /// avatar), and an optional 'variant' in [0 = normal, 1 = Sad, 2 = Happy]. Login can be a single login or a list of
    /// comma-separated logins. Only available to Admin.
    /// </summary>
    /// <param name="clientLogin"></param>
    /// <param name="message"></param>
    /// <param name="avatarLogin"></param>
    /// <param name="variant"></param>
    /// <returns></returns>
    public Task<bool> SendNoticeToLoginAsync(string clientLogin, string message, string avatarLogin, int variant);

    /// <summary>
    /// Display a manialink page on all clients. The parameters are the xml description of the page to display, a timeout
    /// to autohide it (0 = permanent), and a boolean to indicate whether the page must be hidden as soon as the user
    /// clicks on a page option. Only available to Admin.
    /// </summary>
    /// <param name="xml"></param>
    /// <param name="timeout"></param>
    /// <param name="autohide"></param>
    /// <returns></returns>
    public Task<bool> SendDisplayManialinkPageAsync(string xml, int timeout, bool autohide);

    /// <summary>
    /// Display a manialink page on the client with the specified UId. The first parameter is the UId of the player, the
    /// other are identical to 'SendDisplayManialinkPage'. Only available to Admin.
    /// </summary>
    /// <param name="clientId"></param>
    /// <param name="xml"></param>
    /// <param name="timeout"></param>
    /// <param name="autohide"></param>
    /// <returns></returns>
    public Task<bool> SendDisplayManialinkPageToIdAsync(int clientId, string xml, int timeout, bool autohide);

    /// <summary>
    /// Display a manialink page on the client with the specified login. The first parameter is the login of the player,
    /// the other are identical to 'SendDisplayManialinkPage'. Login can be a single login or a list of comma-separated
    /// logins. Only available to Admin.
    /// </summary>
    /// <param name="playerLogin"></param>
    /// <param name="xml"></param>
    /// <param name="timeout"></param>
    /// <param name="autohide"></param>
    /// <returns></returns>
    public Task<bool> SendDisplayManialinkPageToLoginAsync(string playerLogin, string xml, int timeout,
        bool autohide);

    /// <summary>
    /// Hide the displayed manialink page on all clients. Only available to Admin.
    /// </summary>
    /// <returns></returns>
    public Task<bool> SendHideManialinkPageAsync();

    /// <summary>
    /// Hide the displayed manialink page on the client with the specified UId. Only available to Admin.
    /// </summary>
    /// <param name="clientId"></param>
    /// <returns></returns>
    public Task<bool> SendHideManialinkPageToIdAsync(int clientId);

    /// <summary>
    /// Hide the displayed manialink page on the client with the specified login. Login can be a single login or a list of
    /// comma-separated logins. Only available to Admin.
    /// </summary>
    /// <param name="playerLogin"></param>
    /// <returns></returns>
    public Task<bool> SendHideManialinkPageToLoginAsync(string playerLogin);

    /// <summary>
    /// Returns the latest results from the current manialink page, as an array of structs {string Login, int PlayerId, int
    /// Result} Result==0 -> no answer, Result>0.... -> answer from the player.
    /// </summary>
    /// <returns></returns>
    public Task<TmManialinkPageAnswer[]> GetManialinkPageAnswersAsync();

    /// <summary>
    /// Opens a link in the client with the specified UId. The parameters are the Uid of the client to whom the link to
    /// open is sent, the link url, and the 'LinkType' (0 in the external browser, 1 in the internal manialink browser).
    /// Only available to Admin.
    /// </summary>
    /// <param name="clientId"></param>
    /// <param name="url"></param>
    /// <param name="linkType"></param>
    /// <returns></returns>
    public Task<bool> SendOpenLinkToIdAsync(int clientId, string url, int linkType);

    /// <summary>
    /// Opens a link in the client with the specified login. The parameters are the login of the client to whom the link to
    /// open is sent, the link url, and the 'LinkType' (0 in the external browser, 1 in the internal manialink browser).
    /// Login can be a single login or a list of comma-separated logins. Only available to Admin.
    /// </summary>
    /// <param name="playerLogin"></param>
    /// <param name="url"></param>
    /// <param name="linkType"></param>
    /// <returns></returns>
    public Task<bool> SendOpenLinkToLoginAsync(string playerLogin, string url, int linkType);

    /// <summary>
    /// Sets whether buddy notifications should be sent in the chat. login is the login of the player, or '' for global
    /// setting, and enabled is the value. Only available to Admin.
    /// </summary>
    /// <param name="login"></param>
    /// <param name="enabled"></param>
    /// <returns></returns>
    public Task<bool> SetBuddyNotificationAsync(string login, bool enabled);

    /// <summary>
    /// Gets whether buddy notifications are enabled for login, or '' to get the global setting.
    /// </summary>
    /// <param name="login"></param>
    /// <returns></returns>
    public Task<bool> GetBuddyNotificationAsync(string login);

    /// <summary>
    /// Customize the clients 'leave server' dialog box. Parameters are: ManialinkPage, SendToServer url
    /// '#qjoin=login@title', ProposeAddToFavorites and DelayQuitButton (in milliseconds). Only available to Admin.
    /// </summary>
    /// <param name="manialinkPage"></param>
    /// <param name="sendToServerUrl"></param>
    /// <param name="proposeAddToFavorites"></param>
    /// <param name="delayQuiteButton"></param>
    /// <returns></returns>
    public Task<bool> CustomizeQuitDialogAsync(string manialinkPage, string sendToServerUrl, bool proposeAddToFavorites,
        int delayQuiteButton);

    #endregion

    #region GameFlow

    /// <summary>
    /// Restarts the map, with an optional boolean parameter DontClearCupScores (only available in cup mode). Only
    /// available to Admin.
    /// </summary>
    /// <returns></returns>
    public Task<bool> RestartMapAsync();

    /// <summary>
    /// Switch to next map, with an optional boolean parameter DontClearCupScores (only available in cup mode). Only
    /// available to Admin.
    /// </summary>
    /// <returns></returns>
    public Task<bool> NextMapAsync();

    /// <summary>
    /// Attempt to balance teams. Only available to Admin.
    /// </summary>
    /// <returns></returns>
    public Task<bool> AutoTeamBalanceAsync();

    /// <summary>
    /// Returns a struct containing the current game settings.
    /// </summary>
    /// <returns></returns>
    [Obsolete]
    public Task<TmGameInfo> GetCurrentGameInfoAsync();

    /// <summary>
    /// Returns a struct containing the game settings for the next map.
    /// </summary>
    /// <returns></returns>
    [Obsolete]
    public Task<TmGameInfo> GetNextGameInfoAsync();

    /// <summary>
    /// Returns a struct containing two other structures, the first containing the current game settings and the second the
    /// game settings for next map. The first structure is named CurrentGameInfos and the second NextGameInfos.
    /// </summary>
    /// <returns></returns>
    [Obsolete]
    public Task<TmCurrentNextValue<TmGameInfo>> GetGameInfosAsync();

    /// <summary>
    /// Set whether to override the players preferences and always display all opponents). Only available to Admin.
    /// Requires a map restart to be taken into account.
    /// </summary>
    /// <param name="playersPreferenceOverride">0=no override, 1=show all, other value=minimum number of opponents</param>
    /// <returns></returns>
    public Task<bool> SetForceShowAllOpponentsAsync(int playersPreferenceOverride);


    /// <summary>
    /// Set a new mode script name for script mode. Only available to Admin. Requires a map restart to be taken into
    /// account.
    /// </summary>
    /// <param name="scriptName"></param>
    /// <returns></returns>
    public Task<bool> SetScriptNameAsync(string scriptName);

    /// <summary>
    /// Get the current and next mode script name for script mode. The struct returned contains two fields CurrentValue and
    /// NextValue.
    /// </summary>
    /// <returns></returns>
    public Task<TmCurrentNextValue<string>> GetScriptNameAsync();


    /// <summary>
    /// Get the current and next time limit for time attack mode. The struct returned contains two fields CurrentValue and
    /// NextValue.
    /// </summary>
    /// <returns></returns>
    public Task<TmCurrentNextValue<int>> GetTimeAttackLimitAsync();

    #endregion

    #region Maps

    /// <summary>
    /// Returns the current map index in the selection, or -1 if the map is no longer in the selection.
    /// </summary>
    /// <returns></returns>
    public Task<int> GetCurrentMapIndexAsync();

    /// <summary>
    /// Returns the map index in the selection that will be played next (unless the current one is restarted...)
    /// </summary>
    /// <returns></returns>
    public Task<int> GetNextMapIndexAsync();

    /// <summary>
    /// Sets the map index in the selection that will be played next (unless the current one is restarted...)
    /// </summary>
    /// <param name="mapIndex"></param>
    /// <returns></returns>
    public Task<bool> SetNextMapIndexAsync(int mapIndex);

    /// <summary>
    /// Immediately jumps to the map designated by its identifier (it must be in the selection).
    /// </summary>
    /// <param name="mapId"></param>
    /// <returns></returns>
    public Task<bool> SetNextMapIdentAsync(string mapId);

    /// <summary>
    /// Immediately jumps to the map designated by the index in the selection.
    /// </summary>
    /// <param name="mapIndex"></param>
    /// <returns></returns>
    public Task<bool> JumpToMapIndexAsync(int mapIndex);

    /// <summary>
    /// Immediately jumps to the map designated by its identifier (it must be in the selection).
    /// </summary>
    /// <param name="mapId"></param>
    /// <returns></returns>
    public Task<bool> JumpToMapIdentAsync(string mapId);

    /// <summary>
    /// Returns a struct containing the infos for the current map. The struct contains the following fields : Name, UId,
    /// FileName, Author, Environnement, Mood, BronzeTime, SilverTime, GoldTime, AuthorTime, CopperPrice, LapRace, MapType,
    /// MapStyle.
    /// (NbLaps and NbCheckpoints are also present but always set to -1)
    /// </summary>
    /// <returns></returns>
    public Task<TmMapInfo> GetCurrentMapInfoAsync();

    /// <summary>
    /// Returns a struct containing the infos for the next map. The struct contains the following fields : Name, UId,
    /// FileName, Author, Environnement, Mood, BronzeTime, SilverTime, GoldTime, AuthorTime, CopperPrice, LapRace, MapType,
    /// MapStyle.
    /// (NbLaps and NbCheckpoints are also present but always set to -1)
    /// </summary>
    /// <returns></returns>
    public Task<TmMapInfo> GetNextMapInfoAsync();

    /// <summary>
    /// Returns a struct containing the infos for the map with the specified filename. The struct contains the following
    /// fields : Name, UId, FileName, Author, Environnement, Mood, BronzeTime, SilverTime, GoldTime, AuthorTime,
    /// CopperPrice, LapRace, MapType, MapStyle.
    /// (NbLaps and NbCheckpoints are also present but always set to -1)
    /// </summary>
    /// <param name="filename"></param>
    /// <returns></returns>
    public Task<TmMapInfo> GetMapInfoAsync(string filename);

    /// <summary>
    /// Returns a boolean if the map with the specified filename matches the current server settings.
    /// </summary>
    /// <param name="filename"></param>
    /// <returns></returns>
    public Task<bool> CheckMapForCurrentServerParamsAsync(string filename);

    /// <summary>
    /// Returns a list of maps among the current selection of the server. This method take two parameters. The first
    /// parameter specifies the maximum number of infos to be returned, and the second one the starting index in the
    /// selection.
    /// The list is an array of structures. Each structure contains the following fields : Name, UId, FileName,
    /// Environnement, Author, GoldTime, CopperPrice, MapType, MapStyle.
    /// </summary>
    /// <param name="maxInfos"></param>
    /// <param name="startIndex"></param>
    /// <returns></returns>
    public Task<TmMapInfo[]> GetMapListAsync(int maxInfos, int startIndex);

    /// <summary>
    /// Add the map with the specified filename at the end of the current selection. Only available to Admin.
    /// </summary>
    /// <param name="filename"></param>
    /// <returns></returns>
    public Task<bool> AddMapAsync(string filename);

    /// <summary>
    /// Add the list of maps with the specified filenames at the end of the current selection. The list of maps to add is
    /// an array of strings. Only available to Admin.
    /// </summary>
    /// <param name="filenames"></param>
    /// <returns></returns>
    public Task<int> AddMapListAsync(Array filenames);

    /// <summary>
    /// Remove the map with the specified filename from the current selection. Only available to Admin.
    /// </summary>
    /// <param name="filename"></param>
    /// <returns></returns>
    public Task<bool> RemoveMapAsync(string filename);

    /// <summary>
    /// Remove the list of maps with the specified filenames from the current selection. The list of maps to remove is an
    /// array of strings. Only available to Admin.
    /// </summary>
    /// <param name="filenames"></param>
    /// <returns></returns>
    public Task<int> RemoveMapListAsync(Array filenames);

    /// <summary>
    /// Insert the map with the specified filename after the current map. Only available to Admin.
    /// </summary>
    /// <param name="filename"></param>
    /// <returns></returns>
    public Task<bool> InsertMapAsync(string filename);

    /// <summary>
    /// Insert the list of maps with the specified filenames after the current map. The list of maps to insert is an array
    /// of strings. Only available to Admin.
    /// </summary>
    /// <param name="filenames"></param>
    /// <returns></returns>
    public Task<int> InsertMapListAsync(Array filenames);

    /// <summary>
    /// Set as next map the one with the specified filename, if it is present in the selection. Only available to Admin.
    /// </summary>
    /// <param name="filename"></param>
    /// <returns></returns>
    public Task<bool> ChooseNextMapAsync(string filename);

    /// <summary>
    /// Set as next maps the list of maps with the specified filenames, if they are present in the selection. The list of
    /// maps to choose is an array of strings. Only available to Admin.
    /// </summary>
    /// <param name="filenames"></param>
    /// <returns></returns>
    public Task<int> ChooseNextMapListAsync(Array filenames);

    #endregion

    #region MatchSettings

    /// <summary>
    /// Set a list of maps defined in the playlist with the specified filename as the current selection of the server, and
    /// load the gameinfos from the same file. Only available to Admin.
    /// </summary>
    /// <param name="filename"></param>
    /// <returns></returns>
    public Task<int> LoadMatchSettingsAsync(string filename);

    /// <summary>
    /// Add a list of maps defined in the playlist with the specified filename at the end of the current selection. Only
    /// available to Admin.
    /// </summary>
    /// <param name="filename"></param>
    /// <returns></returns>
    public Task<int> AppendPlaylistFromMatchSettingsAsync(string filename);

    /// <summary>
    /// Save the current selection of map in the playlist with the specified filename, as well as the current gameinfos.
    /// Only available to Admin.
    /// </summary>
    /// <param name="filename"></param>
    /// <returns></returns>
    public Task<int> SaveMatchSettingsAsync(string filename);

    #endregion

    #region Players

    /// <summary>
    /// Returns the list of players on the server. This method take two parameters. The first parameter specifies the
    /// maximum number of infos to be returned, and the second one the starting index in the list, an optional 3rd
    /// parameter is used for compatibility: struct version (0 = united, 1 = forever, 2 = forever, including the servers).
    /// The list is an array of PlayerInfo structures. Forever PlayerInfo struct is: Login, NickName, PlayerId, TeamId,
    /// SpectatorStatus, LadderRanking, and Flags.
    /// LadderRanking is 0 when not in official mode,
    /// Flags = ForceSpectator(0,1,2) + IsReferee * 10 + IsPodiumReady * 100 + StereoDisplayMode * 1000 +
    /// IsManagedByAnOtherServer * 10000 + IsServer * 100000 + HasPlayerSlot * 1000000 + IsBroadcasting * 10000000 +
    /// HasJoinedGame * 100000000
    /// SpectatorStatus = Spectator + TemporarySpectator * 10 + PureSpectator * 100 + AutoTarget * 1000 + CurrentTargetId *
    /// 10000
    /// </summary>
    /// <param name="maxInfos"></param>
    /// <param name="startIndex"></param>
    /// <param name="serverType">
    /// OPTIONAL: Used for compatibility: struct version (0 = united, 1 = forever, 2 = forever,
    /// including the servers)
    /// </param>
    /// <returns></returns>
    public Task<TmPlayerInfo[]> GetPlayerListAsync(int maxInfos, int startIndex, int serverType);
    
    /// <summary>
    /// Returns the list of players on the server. This method take two parameters. The first parameter specifies the
    /// maximum number of infos to be returned, and the second one the starting index in the list, an optional 3rd
    /// parameter is used for compatibility: struct version (0 = united, 1 = forever, 2 = forever, including the servers).
    /// The list is an array of PlayerInfo structures. Forever PlayerInfo struct is: Login, NickName, PlayerId, TeamId,
    /// SpectatorStatus, LadderRanking, and Flags.
    /// LadderRanking is 0 when not in official mode,
    /// Flags = ForceSpectator(0,1,2) + IsReferee * 10 + IsPodiumReady * 100 + StereoDisplayMode * 1000 +
    /// IsManagedByAnOtherServer * 10000 + IsServer * 100000 + HasPlayerSlot * 1000000 + IsBroadcasting * 10000000 +
    /// HasJoinedGame * 100000000
    /// SpectatorStatus = Spectator + TemporarySpectator * 10 + PureSpectator * 100 + AutoTarget * 1000 + CurrentTargetId *
    /// 10000
    /// </summary>
    /// OPTIONAL: Used for compatibility: struct version (0 = united, 1 = forever, 2 = forever,
    /// including the servers)
    /// </param>
    /// <returns></returns>
    public Task<TmPlayerInfo[]> GetPlayerListAsync();
    
    /// <summary>
    /// Returns the list of players on the server. This method take two parameters. The first parameter specifies the
    /// maximum number of infos to be returned, and the second one the starting index in the list, an optional 3rd
    /// parameter is used for compatibility: struct version (0 = united, 1 = forever, 2 = forever, including the servers).
    /// The list is an array of PlayerInfo structures. Forever PlayerInfo struct is: Login, NickName, PlayerId, TeamId,
    /// SpectatorStatus, LadderRanking, and Flags.
    /// LadderRanking is 0 when not in official mode,
    /// Flags = ForceSpectator(0,1,2) + IsReferee * 10 + IsPodiumReady * 100 + StereoDisplayMode * 1000 +
    /// IsManagedByAnOtherServer * 10000 + IsServer * 100000 + HasPlayerSlot * 1000000 + IsBroadcasting * 10000000 +
    /// HasJoinedGame * 100000000
    /// SpectatorStatus = Spectator + TemporarySpectator * 10 + PureSpectator * 100 + AutoTarget * 1000 + CurrentTargetId *
    /// 10000
    /// </summary>
    /// <param name="maxInfos"></param>
    /// OPTIONAL: Used for compatibility: struct version (0 = united, 1 = forever, 2 = forever,
    /// including the servers)
    /// </param>
    /// <returns></returns>
    public Task<TmPlayerInfo[]> GetPlayerListAsync(int maxInfos);
    
    /// <summary>
    /// Returns the list of players on the server. This method take two parameters. The first parameter specifies the
    /// maximum number of infos to be returned, and the second one the starting index in the list, an optional 3rd
    /// parameter is used for compatibility: struct version (0 = united, 1 = forever, 2 = forever, including the servers).
    /// The list is an array of PlayerInfo structures. Forever PlayerInfo struct is: Login, NickName, PlayerId, TeamId,
    /// SpectatorStatus, LadderRanking, and Flags.
    /// LadderRanking is 0 when not in official mode,
    /// Flags = ForceSpectator(0,1,2) + IsReferee * 10 + IsPodiumReady * 100 + StereoDisplayMode * 1000 +
    /// IsManagedByAnOtherServer * 10000 + IsServer * 100000 + HasPlayerSlot * 1000000 + IsBroadcasting * 10000000 +
    /// HasJoinedGame * 100000000
    /// SpectatorStatus = Spectator + TemporarySpectator * 10 + PureSpectator * 100 + AutoTarget * 1000 + CurrentTargetId *
    /// 10000
    /// </summary>
    /// <param name="maxInfos"></param>
    /// <param name="startIndex"></param>
    /// OPTIONAL: Used for compatibility: struct version (0 = united, 1 = forever, 2 = forever,
    /// including the servers)
    /// </param>
    /// <returns></returns>
    public Task<TmPlayerInfo[]> GetPlayerListAsync(int maxInfos, int startIndex);

    /// <summary>
    /// Returns a struct containing the infos on the player with the specified login, with an optional parameter for
    /// compatibility: struct version (0 = united, 1 = forever). The structure is identical to the ones from GetPlayerList.
    /// Forever PlayerInfo struct is: Login, NickName, PlayerId, TeamId, SpectatorStatus, LadderRanking, and Flags.
    /// LadderRanking is 0 when not in official mode,
    /// Flags = ForceSpectator(0,1,2) + IsReferee * 10 + IsPodiumReady * 100 + StereoDisplayMode * 1000 +
    /// IsManagedByAnOtherServer * 10000 + IsServer * 100000 + HasPlayerSlot * 1000000 + IsBroadcasting * 10000000 +
    /// HasJoinedGame * 100000000
    /// SpectatorStatus = Spectator + TemporarySpectator * 10 + PureSpectator * 100 + AutoTarget * 1000 + CurrentTargetId *
    /// 10000
    /// Each structure of the array Skins contains two fields Environnement and a struct PackDesc. Each structure PackDesc,
    /// as well as the struct Avatar, contains two fields FileName and Checksum.
    /// </summary>
    /// <param name="playerLogin"></param>
    /// <param name="serverType"></param>
    /// <returns></returns>
    public Task<TmPlayerInfo> GetPlayerInfoAsync(string playerLogin, int serverType);
    
    /// <summary>
    /// Returns a struct containing the infos on the player with the specified login, with an optional parameter for
    /// compatibility: struct version (0 = united, 1 = forever). The structure is identical to the ones from GetPlayerList.
    /// Forever PlayerInfo struct is: Login, NickName, PlayerId, TeamId, SpectatorStatus, LadderRanking, and Flags.
    /// LadderRanking is 0 when not in official mode,
    /// Flags = ForceSpectator(0,1,2) + IsReferee * 10 + IsPodiumReady * 100 + StereoDisplayMode * 1000 +
    /// IsManagedByAnOtherServer * 10000 + IsServer * 100000 + HasPlayerSlot * 1000000 + IsBroadcasting * 10000000 +
    /// HasJoinedGame * 100000000
    /// SpectatorStatus = Spectator + TemporarySpectator * 10 + PureSpectator * 100 + AutoTarget * 1000 + CurrentTargetId *
    /// 10000
    /// Each structure of the array Skins contains two fields Environnement and a struct PackDesc. Each structure PackDesc,
    /// as well as the struct Avatar, contains two fields FileName and Checksum.
    /// </summary>
    /// <param name="playerLogin"></param>
    /// <returns></returns>
    public Task<TmPlayerInfo> GetPlayerInfoAsync(string playerLogin);

    /// <summary>
    /// Returns a struct containing the infos on the player with the specified login. The structure contains the following
    /// fields : Login, NickName, PlayerId, TeamId, IPAddress, DownloadRate, UploadRate, Language, IsSpectator,
    /// IsInOfficialMode, a structure named Avatar, an array of structures named Skins, a structure named LadderStats,
    /// HoursSinceZoneInscription and OnlineRights (0: nations account, 3: united account).
    /// Each structure of the array Skins contains two fields Environnement and a struct PackDesc. Each structure PackDesc,
    /// as well as the struct Avatar, contains two fields FileName and Checksum.
    /// </summary>
    /// <param name="playerLogin"></param>
    /// <returns></returns>
    public Task<TmPlayerDetailedInfo> GetDetailedPlayerInfoAsync(string playerLogin);

    /// <summary>
    /// Returns a struct containing the player infos of the game server (ie: in case of a basic server, itself; in case of
    /// a relay server, the main server), with an optional parameter for compatibility: struct version (0 = united, 1 =
    /// forever).
    /// The structure is identical to the ones from GetPlayerList. Forever PlayerInfo struct is: Login, NickName, PlayerId,
    /// TeamId, SpectatorStatus, LadderRanking, and Flags.
    /// LadderRanking is 0 when not in official mode,
    /// Flags = ForceSpectator(0,1,2) + IsReferee * 10 + IsPodiumReady * 100 + StereoDisplayMode * 1000 +
    /// IsManagedByAnOtherServer * 10000 + IsServer * 100000 + HasPlayerSlot * 1000000 + IsBroadcasting * 10000000 +
    /// HasJoinedGame * 100000000
    /// SpectatorStatus = Spectator + TemporarySpectator * 10 + PureSpectator * 100 + AutoTarget * 1000 + CurrentTargetId *
    /// 10000
    /// Each structure contains the following fields : Login, NickName, PlayerId and Rank. In addition, for legacy
    /// trackmania modes it also contains BestTime, Score, NbrLapsFinished, LadderScore, and an array BestCheckpoints that
    /// contains the checkpoint times for the best race.
    /// </summary>
    /// <param name="serverType"></param>
    /// <returns></returns>
    public Task<TmPlayerInfo> GetMainServerPlayerInfoAsync(int serverType);

    /// <summary>
    /// Returns the current rankings for the race in progress. (In trackmania legacy team modes, the scores for the two
    /// teams are returned. In other modes, it's the individual players' scores) This method take two parameters. The first
    /// parameter specifies the maximum number of infos to be returned, and the second one the starting index in the
    /// ranking.  The ranking returned is a list of structures.
    /// Each structure contains the following fields : Login, NickName, PlayerId and Rank. In addition, for legacy
    /// trackmania modes it also contains BestTime, Score, NbrLapsFinished, LadderScore, and an array BestCheckpoints that
    /// contains the checkpoint times for the best race.
    /// </summary>
    /// <param name="maxInfos"></param>
    /// <param name="startRatingIndex"></param>
    /// <returns></returns>
    public Task<TmPlayerRanking[]> GetCurrentRankingAsync(int maxInfos, int startRatingIndex);

    /// <summary>
    /// Returns the current ranking for the race in progressof the player with the specified login (or list of
    /// comma-separated logins). The ranking returned is a list of structures.
    /// Each structure contains the following fields : Login, NickName, PlayerId and Rank. In addition, for legacy
    /// trackmania modes it also contains BestTime, Score, NbrLapsFinished, LadderScore, and an array BestCheckpoints that
    /// contains the checkpoint times for the best race.
    /// </summary>
    /// <param name="playerLogin"></param>
    /// <returns></returns>
    public Task<TmPlayerRanking[]> GetCurrentRankingForLoginAsync(string playerLogin);

    /// <summary>
    /// Force the scores of the current game. Only available in rounds and team mode. You have to pass an array of structs
    /// {int PlayerId, int Score}.
    /// And a boolean SilentMode - if true, the scores are silently updated (only available for SuperAdmin), allowing an
    /// external controller to do its custom counting... Only available to Admin/SuperAdmin.
    /// </summary>
    /// <param name="playerScores"></param>
    /// <param name="silentMode"></param>
    /// <returns></returns>
    public Task<bool> ForceScoresAsync(TmPlayerScore[] playerScores, bool silentMode);

    /// <summary>
    /// Force the spectating status of the player. You have to pass the login and the spectator mode (0: user selectable,
    /// 1: spectator, 2: player, 3: spectator but keep selectable). Only available to Admin.
    /// </summary>
    /// <param name="playerLogin"></param>
    /// <param name="cameraType"></param>
    /// <returns></returns>
    public Task<bool> ForceSpectatorAsync(string playerLogin, int cameraType);

    /// <summary>
    /// Force the spectating status of the player. You have to pass the playerid and the spectator mode (0: user
    /// selectable, 1: spectator, 2: player, 3: spectator but keep selectable). Only available to Admin.
    /// </summary>
    /// <param name="playerId"></param>
    /// <param name="cameraType"></param>
    /// <returns></returns>
    public Task<bool> ForceSpectatorIdAsync(int playerId, int cameraType);

    /// <summary>
    /// Force spectators to look at a specific player. You have to pass the login of the spectator (or '' for all) and the
    /// login of the target (or '' for automatic),
    /// and an integer for the camera type to use (-1 = leave unchanged, 0 = replay, 1 = follow, 2 = free). Only available
    /// to Admin.
    /// </summary>
    /// <param name="playerLogin"></param>
    /// <param name="targetLogin"></param>
    /// <param name="cameraType"></param>
    /// <returns></returns>
    public Task<bool> ForceSpectatorTargetAsync(string playerLogin, string targetLogin, int cameraType);

    /// <summary>
    /// Force spectators to look at a specific player. You have to pass the id of the spectator (or -1 for all) and the id
    /// of the target (or -1 for automatic),
    /// and an integer for the camera type to use (-1 = leave unchanged, 0 = replay, 1 = follow, 2 = free). Only available
    /// to Admin.
    /// </summary>
    /// <param name="playerId"></param>
    /// <param name="targetId"></param>
    /// <param name="cameraType"></param>
    /// <returns></returns>
    public Task<bool> ForceSpectatorTargetIdAsync(int playerId, int targetId, int cameraType);

    /// <summary>
    /// Pass the login of the spectator. A spectator that once was a player keeps his player slot, so that he can go back
    /// to race mode.
    /// Calling this function frees this slot for another player to connect. Only available to Admin.
    /// </summary>
    /// <param name="playerLogin"></param>
    /// <returns></returns>
    public Task<bool> SpectatorReleasePlayerSlotAsync(string playerLogin);

    /// <summary>
    /// Pass the playerid of the spectator. A spectator that once was a player keeps his player slot, so that he can go
    /// back to race mode.
    /// Calling this function frees this slot for another player to connect. Only available to Admin.
    /// </summary>
    /// <param name="playerId"></param>
    /// <returns></returns>
    public Task<bool> SpectatorReleasePlayerSlotIdAsync(int playerId);

    /// <summary>
    /// Ignore players profile skin customisation. Only available to Admin.
    /// </summary>
    /// <param name="disabled"></param>
    /// <returns></returns>
    public Task<bool> DisableProfileSkinsAsync(bool disabled);
    
    /// <summary>
    /// Ignore players profile skin customisation. Only available to Admin.
    /// </summary>
    /// <returns></returns>
    public Task<bool> DisableProfileSkinsAsync();

    /// <summary>
    /// Returns whether the custom skins are disabled.
    /// </summary>
    /// <returns></returns>
    public Task<bool> AreProfileSkinsDisabledAsync();

    #region Kicking

    /// <summary>
    /// Kick the player with the specified login, with an optional message. Only available to Admin.
    /// </summary>
    /// <param name="login"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    public Task<bool> KickAsync(string login, string message);
    
    /// <summary>
    /// Kick the player with the specified login, with an optional message. Only available to Admin.
    /// </summary>
    /// <param name="login"></param>
    /// <returns></returns>
    public Task<bool> KickAsync(string login);

    /// <summary>
    /// Kick the player with the specified PlayerId, with an optional message. Only available to Admin.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    public Task<bool> KickIdAsync(int id, string message);
    
    /// <summary>
    /// Kick the player with the specified PlayerId, with an optional message. Only available to Admin.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Task<bool> KickIdAsync(int id);

    #endregion

    #region Ban List

    /// <summary>
    /// Ban the player with the specified login, with an optional message. Only available to Admin.
    /// </summary>
    /// <param name="login"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    public Task<bool> BanAsync(string login, string message);
    
    /// <summary>
    /// Ban the player with the specified login, with an optional message. Only available to Admin.
    /// </summary>
    /// <param name="login"></param>
    /// <returns></returns>
    public Task<bool> BanAsync(string login);

    /// <summary>
    /// Ban the player with the specified login, with a message. Add it to the black list, and optionally save the new
    /// list. Only available to Admin.
    /// </summary>
    /// <param name="login"></param>
    /// <param name="message"></param>
    /// <param name="saveToFile"></param>
    /// <returns></returns>
    public Task<bool> BanAndBlackListAsync(string login, string message, bool saveToFile);
    
    /// <summary>
    /// Ban the player with the specified login, with a message. Add it to the black list, and optionally save the new
    /// list. Only available to Admin.
    /// </summary>
    /// <param name="login"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    public Task<bool> BanAndBlackListAsync(string login, string message);

    /// <summary>
    /// Ban the player with the specified PlayerId, with an optional message. Only available to Admin.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    public Task<bool> BanIdAsync(int id, string message);

    /// <summary>
    /// Unban the player with the specified login. Only available to Admin.
    /// </summary>
    /// <param name="login"></param>
    /// <returns></returns>
    public Task<bool> UnBanAsync(string login);

    /// <summary>
    /// Clean the ban list of the server. Only available to Admin.
    /// </summary>
    /// <returns></returns>
    public Task<bool> CleanBanListAsync();

    /// <summary>
    /// Returns the list of banned players. This method takes two parameters. The first parameter specifies the maximum
    /// number of infos to be returned, and the second one the starting index in the list. The list is an array of
    /// structures. Each structure contains the following fields : Login, ClientName and IPAddress.
    /// </summary>
    /// <param name="maxInfos"></param>
    /// <param name="startIndex"></param>
    /// <returns></returns>
    public Task<TmBanListEntry[]> GetBanListAsync(int maxInfos, int startIndex);

    #endregion

    #region Black List

    /// <summary>
    /// Blacklist the player with the specified login. Only available to SuperAdmin.
    /// </summary>
    /// <param name="login"></param>
    /// <returns></returns>
    public Task<bool> BlackListAsync(string login);

    /// <summary>
    /// Blacklist the player with the specified PlayerId. Only available to SuperAdmin.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Task<bool> BlackListIdAsync(int id);

    /// <summary>
    /// </summary>
    /// <param name="login"></param>
    /// <returns></returns>
    public Task<bool> UnBlackListAsync(string login);

    /// <summary>
    /// Clean the blacklist of the server. Only available to SuperAdmin.
    /// </summary>
    /// <returns></returns>
    public Task<bool> CleanBlackListAsync();

    /// <summary>
    /// Returns the list of blacklisted players. This method takes two parameters. The first parameter specifies the
    /// maximum number of infos to be returned, and the second one the starting index in the list. The list is an array of
    /// structures. Each structure contains the following fields : Login.
    /// </summary>
    /// <param name="maxInfos"></param>
    /// <param name="startIndex"></param>
    /// <returns></returns>
    public Task<TmBlackListEntry[]> GetBlackListAsync(int maxInfos, int startIndex);

    /// <summary>
    /// Load the black list file with the specified file name. Only available to Admin.
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public Task<bool> LoadBlackListAsync(string fileName);

    /// <summary>
    /// Save the black list in the file with specified file name. Only available to Admin.
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public Task<bool> SaveBlackListAsync(string fileName);

    #endregion

    #region Guest List

    /// <summary>
    /// Add the player with the specified login on the guest list. Only available to Admin.
    /// </summary>
    /// <param name="login"></param>
    /// <returns></returns>
    public Task<bool> AddGuestAsync(string login);

    /// <summary>
    /// Add the player with the specified PlayerId on the guest list. Only available to Admin.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Task<bool> AddGuestIdAsync(int id);

    /// <summary>
    /// Remove the player with the specified login from the guest list. Only available to Admin.
    /// </summary>
    /// <param name="login"></param>
    /// <returns></returns>
    public Task<bool> RemoveGuestAsync(string login);

    /// <summary>
    /// Remove the player with the specified PlayerId from the guest list. Only available to Admin.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Task<bool> RemoveGuestIdAsync(int id);

    /// <summary>
    /// Clean the guest list of the server. Only available to Admin.
    /// </summary>
    /// <returns></returns>
    public Task<bool> CleanGuestListAsync();

    /// <summary>
    /// Returns the list of players on the guest list. This method takes two parameters. The first parameter specifies the
    /// maximum number of infos to be returned, and the second one the starting index in the list. The list is an array of
    /// structures. Each structure contains the following fields : Login.
    /// </summary>
    /// <param name="maxInfos"></param>
    /// <param name="startIndex"></param>
    /// <returns></returns>
    public Task<TmGuestListEntry[]> GetGuestListAsync(int maxInfos, int startIndex);

    /// <summary>
    /// Load the guest list file with the specified file name. Only available to Admin.
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public Task<bool> LoadGuestListAsync(string fileName);

    /// <summary>
    /// Save the guest list in the file with specified file name. Only available to Admin.
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public Task<bool> SaveGuestListAsync(string fileName);

    #endregion

    #region Ignore List

    /// <summary>
    /// Ignore the player with the specified login. Only available to Admin.
    /// </summary>
    /// <param name="login"></param>
    /// <returns></returns>
    public Task<bool> IgnoreAsync(string login);

    /// <summary>
    /// Ignore the player with the specified PlayerId. Only available to Admin.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Task<bool> IgnoreIdAsync(int id);

    /// <summary>
    /// Unignore the player with the specified login. Only available to Admin.
    /// </summary>
    /// <param name="login"></param>
    /// <returns></returns>
    public Task<bool> UnIgnoreAsync(string login);

    /// <summary>
    /// Unignore the player with the specified PlayerId. Only available to Admin.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Task<bool> UnIgnoreIdAsync(int id);

    /// <summary>
    /// Clean the ignore list of the server. Only available to Admin.
    /// </summary>
    /// <returns></returns>
    public Task<bool> CleanIgnoreListAsync();

    /// <summary>
    /// Returns the list of ignored players. This method takes two parameters. The first parameter specifies the maximum
    /// number of infos to be returned, and the second one the starting index in the list. The list is an array of
    /// structures. Each structure contains the following fields : Login.
    /// </summary>
    /// <param name="maxInfos"></param>
    /// <param name="startIndex"></param>
    /// <returns></returns>
    public Task<TmIgnoreListEntry[]> GetIgnoreListAsync(int maxInfos, int startIndex);

    #endregion

    #region Payments & Bills

    /// <summary>
    /// Pay planets from the server account to a player, returns the BillId. This method takes three parameters: Login of
    /// the payee, Cost in planets to pay and a Label to send with the payment. The creation of the transaction itself may
    /// cost planets, so you need to have planets on the server account. Only available to Admin.
    /// </summary>
    /// <param name="login"></param>
    /// <param name="planets"></param>
    /// <param name="label"></param>
    /// <returns></returns>
    [Obsolete]
    public Task<int> PayAsync(string login, int planets, string label);

    /// <summary>
    /// Create a bill, send it to a player, and return the BillId. This method takes four parameters: LoginFrom of the
    /// payer, Cost in planets the player has to pay, Label of the transaction and an optional LoginTo of the payee (if
    /// empty string, then the server account is used). The creation of the transaction itself may cost planets, so you
    /// need to have planets on the server account. Only available to Admin.
    /// </summary>
    /// <param name="loginFrom"></param>
    /// <param name="planets"></param>
    /// <param name="label"></param>
    /// <param name="loginTo"></param>
    /// <returns></returns>
    [Obsolete]
    public Task<int> SendBillAsync(string loginFrom, int planets, string label, string loginTo = null);

    /// <summary>
    /// Returns the current state of a bill. This method takes one parameter, the BillId. Returns a struct containing
    /// State, StateName and TransactionId. Possible enum values are: CreatingTransaction, Issued, ValidatingPayement,
    /// Payed, Refused, Error.
    /// </summary>
    /// <param name="billId"></param>
    /// <returns></returns>
    [Obsolete]
    public Task<TmBillState> GetBillStateAsync(int billId);

    #endregion

    #endregion

    #region Replays

    /// <summary>
    /// Enable the autosaving of all replays (vizualisable replays with all players, but not validable) on the server. Only
    /// available to SuperAdmin.
    /// </summary>
    /// <param name="autoSave"></param>
    /// <returns></returns>
    public Task<bool> AutoSaveReplaysAsync(string autoSave);

    /// <summary>
    /// Saves the current replay (vizualisable replays with all players, but not validable). Pass a filename, or '' for an
    /// automatic filename. Only available to Admin.
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public Task<bool> SaveCurrentReplayAsync(string fileName);

    /// <summary>
    /// Saves a replay with the ghost of all the players' best race. First parameter is the login of the player (or '' for
    /// all players), Second parameter is the filename, or '' for an automatic filename. Only available to Admin.
    /// </summary>
    /// <param name="login"></param>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public Task<bool> SaveBestGhostsReplayAsync(string login, string fileName);

    /// <summary>
    /// Returns a replay containing the data needed to validate the current best time of the player. The parameter is the
    /// login of the player.
    /// </summary>
    /// <param name="login"></param>
    /// <returns></returns>
    public Task<GbxBase64> GetValidationReplayAsync(string login);

    #endregion

    #region Script

    /// <summary>
    /// Get the current mode script.
    /// </summary>
    /// <returns></returns>
    public Task<string> GetModeScriptTextAsync();
    /// <summary>
    /// Set the mode script and restart. Only available to Admin.
    /// </summary>
    /// <param name="script"></param>
    /// <returns></returns>
    public Task<bool> SetModeScriptTextAsync(string script);

    /// <summary>
    /// Returns the description of the current mode script, as a structure containing: Name, CompatibleTypes, Description,
    /// Version and the settings available.
    /// </summary>
    /// <returns></returns>
    public Task<TmScriptInfo> GetModeScriptInfoAsync();

    /// <summary>
    /// Returns the current settings of the mode script.
    /// </summary>
    /// <returns></returns>
    public Task<GbxDynamicObject> GetModeScriptSettingsAsync();

    /// <summary>
    /// Change the settings of the mode script. Only available to Admin.
    /// </summary>
    /// <param name="modescriptSettings"></param>
    /// <returns></returns>
    public Task<bool> SetModeScriptSettingsAsync(GbxDynamicObject modescriptSettings);

    /// <summary>
    /// Send commands to the mode script. Only available to Admin.
    /// </summary>
    /// <param name="commands"></param>
    /// <returns></returns>
    public Task<bool> SendModeScriptCommandsAsync(GbxDynamicObject commands);

    /// <summary>
    /// Change the settings and send commands to the mode script. Only available to Admin.
    /// </summary>
    /// <param name="settings"></param>
    /// <param name="modeScript"></param>
    /// <returns></returns>
    public Task<bool> SetModeScriptSettingsAndCommandsAsync(GbxDynamicObject settings,
        GbxDynamicObject modeScript);

    /// <summary>
    /// Returns the current xml-rpc variables of the mode script.
    /// </summary>
    /// <returns></returns>
    public Task<GbxDynamicObject> GetModeScriptVariablesAsync();

    /// <summary>
    /// Set the xml-rpc variables of the mode script. Only available to Admin.
    /// </summary>
    /// <param name="xmlRpcVar"></param>
    /// <returns></returns>
    public Task<bool> SetModeScriptVariablesAsync(GbxDynamicObject xmlRpcVar);

    /// <summary>
    /// Send an event to the mode script. Only available to Admin.
    /// </summary>
    /// <param name="modeScript"></param>
    /// <param name="eventName"></param>
    /// <returns></returns>
    public Task<bool> TriggerModeScriptEventAsync(string modeScript, string eventName);

    /// <summary>
    /// Send an event to the mode script. Only available to Admin.
    /// </summary>
    /// <param name="method"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public Task<bool> TriggerModeScriptEventArrayAsync(string method, params string[] parameters);

    /// <summary>
    /// Set the ServerPlugin settings.
    /// </summary>
    /// <param name="forceReload">Whether to reload from disk</param>
    /// <param name="filename">OPTIONAL: Name the filename relative to Scripts/directory</param>
    /// <param name="script">OPTIONAL: The script #Settings to apply.</param>
    /// <returns></returns>
    public Task<bool> SetServerPluginAsync(bool forceReload, string filename, GbxDynamicObject script);
    
    /// <summary>
    /// Set the ServerPlugin settings.
    /// </summary>
    /// <param name="forceReload">Whether to reload from disk</param>
    /// <returns></returns>
    public Task<bool> SetServerPluginAsync(bool forceReload);
    
    /// <summary>
    /// Set the ServerPlugin settings.
    /// </summary>
    /// <param name="forceReload">Whether to reload from disk</param>
    /// <param name="filename">OPTIONAL: Name the filename relative to Scripts/directory</param>
    /// <returns></returns>
    public Task<bool> SetServerPluginAsync(bool forceReload, string filename);

    /// <summary>
    /// Get the ServerPlugin current settings.
    /// </summary>
    /// <returns></returns>
    public Task<GbxDynamicObject> GetServerPluginAsync();

    /// <summary>
    /// Send an event to the server script. Only available to Admin.
    /// </summary>
    /// <returns></returns>
    public Task<GbxDynamicObject> GetServerPluginVariablesAsync();

    /// <summary>
    /// Returns the current xml-rpc variables of the server script.
    /// </summary>
    /// <param name="method"></param>
    /// <param name="param2"></param>
    /// <returns></returns>
    public Task<bool> TriggerServerPluginEventAsync(string method, string param2);

    /// <summary>
    /// Send an event to the server script. Only available to Admin.
    /// </summary>
    /// <param name="method"></param>
    /// <param name="param2"></param>
    /// <returns></returns>
    public Task<bool> TriggerServerPluginEventArrayAsync(string method, string[] param2);

    /// <summary>
    /// Get the script cloud variables of given object. Only available to Admin.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public Task<GbxDynamicObject> GetScriptCloudVariablesAsync(string type, string id);

    /// <summary>
    /// Set the script cloud variables of given object. Only available to Admin.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="id"></param>
    /// <param name="variables"></param>
    /// <returns></returns>
    public Task<bool> SetScriptCloudVariablesAsync(string type, string id, GbxDynamicObject variables);

    #endregion

    #region Server

    /// <summary>
    /// Returns a struct with the Name, TitleId, Version, Build and ApiVersion of the application remotely controlled.
    /// </summary>
    /// <returns></returns>
    public Task<TmVersionInfo> GetVersionAsync();

    /// <summary>
    /// Returns the current status of the server.
    /// </summary>
    /// <returns></returns>
    public Task<TmStatus> GetStatusAsync();

    /// <summary>
    /// Quit the application. Only available to SuperAdmin.
    /// </summary>
    /// <returns></returns>
    public Task<bool> QuitGameAsync();

    /// <summary>
    /// Write the data to the specified file. The filename is relative to the Maps path. Only available to Admin.
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    public Task<bool> WriteFileAsync(string fileName, GbxBase64 data);

    /// <summary>
    /// Send the data to the specified player. Only available to Admin.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    public Task<bool> TunnelSendDataToIdAsync(int id, GbxBase64 data);

    /// <summary>
    /// Send the data to the specified player. Login can be a single login or a list of comma-separated logins. Only
    /// available to Admin.
    /// </summary>
    /// <param name="login"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    public Task<bool> TunnelSendDataToLoginAsync(string login, GbxBase64 data);

    /// <summary>
    /// Just log the parameters and invoke a callback. Can be used to talk to other xmlrpc clients connected, or to make
    /// custom votes. If used in a callvote, the first parameter will be used as the vote message on the clients. Only
    /// available to Admin.
    /// </summary>
    /// <param name="par1"></param>
    /// <param name="par2"></param>
    /// <returns></returns>
    public Task<bool> EchoAsync(string par1, string par2);

    /// <summary>
    /// Returns the current number of planets on the server account.
    /// </summary>
    /// <returns></returns>
    public Task<int> GetServerPlanetsAsync();

    /// <summary>
    /// Get some system infos, including connection rates (in kbps).
    /// </summary>
    /// <returns></returns>
    public Task<TmSystemInfo> GetSystemInfoAsync();

    /// <summary>
    /// Set the download and upload rates (in kbps).
    /// </summary>
    /// <param name="download"></param>
    /// <param name="upload"></param>
    /// <returns></returns>
    public Task<bool> SetConnectionRatesAsync(int download, int upload);

    /// <summary>
    /// Returns the list of tags and associated values set on this server. Only available to Admin.
    /// </summary>
    /// <returns></returns>
    public Task<TmServerTag[]> GetServerTagsAsync();

    /// <summary>
    /// Set a tag and its value on the server. This method takes two parameters. The first parameter specifies the name of
    /// the tag, and the second one its value. The list is an array of structures {string Name, string Value}. Only
    /// available to Admin.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public Task<bool> SetServerTagAsync(string name, string value);

    /// <summary>
    /// Unset the tag with the specified name on the server. Only available to Admin.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public Task<bool> UnsetServerTagAsync(string name);

    /// <summary>
    /// Reset all tags on the server. Only available to Admin.
    /// </summary>
    /// <returns></returns>
    public Task<bool> ResetServerTagsAsync();

    /// <summary>
    /// Set a new server name in utf8 format. Only available to Admin.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public Task<bool> SetServerNameAsync(string name);

    /// <summary>
    /// Get the server name in utf8 format.
    /// </summary>
    /// <returns></returns>
    public Task<string> GetServerNameAsync();

    /// <summary>
    /// Set a new server comment in utf8 format. Only available to Admin.
    /// </summary>
    /// <param name="comment"></param>
    /// <returns></returns>
    public Task<bool> SetServerCommentAsync(string comment);

    /// <summary>
    /// Get the server comment in utf8 format.
    /// </summary>
    /// <returns></returns>
    public Task<string> GetServerCommentAsync();

    /// <summary>
    /// Set whether the server should be hidden from the public server list (0 = visible, 1 = always hidden, 2 = hidden
    /// from nations). Only available to Admin.
    /// </summary>
    /// <param name="hiddenState"></param>
    /// <returns></returns>
    public Task<bool> SetHideServerAsync(int hiddenState);

    /// <summary>
    /// Get whether the server wants to be hidden from the public server list.
    /// </summary>
    /// <returns></returns>
    public Task<int> GetHideServerAsync();

    /// <summary>
    /// Set a new password for the server. Only available to Admin.
    /// </summary>
    /// <param name="password"></param>
    /// <returns></returns>
    public Task<bool> SetServerPasswordAsync(string password);

    /// <summary>
    /// Get the server password if called as Admin or Super Admin, else returns if a password is needed or not.
    /// </summary>
    /// <returns></returns>
    public Task<string> GetServerPasswordAsync();

    /// <summary>
    /// Set a new password for the spectator mode. Only available to Admin.
    /// </summary>
    /// <param name="password"></param>
    /// <returns></returns>
    public Task<bool> SetServerPasswordForSpectatorAsync(string password);

    /// <summary>
    /// Get the password for spectator mode if called as Admin or Super Admin, else returns if a password is needed or not.
    /// </summary>
    /// <returns></returns>
    public Task<string> GetServerPasswordForSpectatorAsync();

    /// <summary>
    /// Set a new maximum number of players. Only available to Admin. Requires a map restart to be taken into account.
    /// </summary>
    /// <param name="maxPlayers"></param>
    /// <returns></returns>
    public Task<bool> SetMaxPlayersAsync(int maxPlayers);

    /// <summary>
    /// Get the current and next maximum number of players allowed on server. The struct returned contains two fields
    /// CurrentValue and NextValue.
    /// </summary>
    /// <returns></returns>
    public Task<TmCurrentNextValue<int>> GetMaxPlayersAsync();

    /// <summary>
    /// Set a new maximum number of Spectators. Only available to Admin. Requires a map restart to be taken into account.
    /// </summary>
    /// <param name="maxPlayers"></param>
    /// <returns></returns>
    public Task<bool> SetMaxSpectatorsAsync(int maxPlayers);

    /// <summary>
    /// Get the current and next maximum number of Spectators allowed on server. The struct returned contains two fields
    /// CurrentValue and NextValue.
    /// </summary>
    /// <returns></returns>
    public Task<TmCurrentNextValue<int>> GetMaxSpectatorsAsync();

    /// <summary>
    /// Declare if the server is a lobby, the number and maximum number of players currently managed by it, and the average
    /// level of the players. Only available to Admin.
    /// </summary>
    /// <param name="isLobby"></param>
    /// <param name="lobbyPlayers"></param>
    /// <param name="lobbyMaxPlayers"></param>
    /// <param name="lobbyPlayersLevel"></param>
    /// <returns></returns>
    public Task<bool> SetLobbyInfoAsync(bool isLobby, int lobbyPlayers, int lobbyMaxPlayers,
        double lobbyPlayersLevel);

    /// <summary>
    /// Get whether the server if a lobby, the number and maximum number of players currently managed by it. The struct
    /// returned contains 4 fields IsLobby, LobbyPlayers, LobbyMaxPlayers, and LobbyPlayersLevel.
    /// </summary>
    /// <returns></returns>
    public Task<TmLobbyInfo> GetLobbyInfoAsync();

    /// <summary>
    /// Prior to loading next map, execute SendToServer url '#qjoin=login@title'. Only available to Admin.
    /// </summary>
    /// <param name="sendToServerUrl"></param>
    /// <returns></returns>
    public Task<bool> SendToServerAfterMatchEndAsync(string sendToServerUrl);

    /// <summary>
    /// Set whether, when a player is switching to spectator, the server should still consider him a player and keep his
    /// player slot, or not. Only available to Admin.
    /// </summary>
    /// <param name="keepSlots"></param>
    /// <returns></returns>
    public Task<bool> KeepPlayerSlotsAsync(bool keepSlots);

    /// <summary>
    /// Get whether the server keeps player slots when switching to spectator.
    /// </summary>
    /// <returns></returns>
    public Task<bool> IsKeepingPlayerSlotsAsync();

    /// <summary>
    /// Allow clients to download maps from the server. Only available to Admin.
    /// </summary>
    /// <param name="allow"></param>
    /// <returns></returns>
    public Task<bool> AllowMapDownloadAsync(bool allow);

    /// <summary>
    /// Returns if clients can download maps from the server.
    /// </summary>
    /// <returns></returns>
    public Task<bool> IsMapDownloadAllowedAsync();

    /// <summary>
    /// Returns the path of the game datas directory. Only available to Admin.
    /// </summary>
    /// <returns></returns>
    public Task<string> GameDataDirectoryAsync();

    /// <summary>
    /// Returns the path of the maps directory. Only available to Admin.
    /// </summary>
    /// <returns></returns>
    public Task<string> GetMapsDirectoryAsync();

    /// <summary>
    /// Returns the path of the skins directory. Only available to Admin.
    /// </summary>
    /// <returns></returns>
    public Task<string> GetSkinsDirectoryAsync();

    /// <summary>
    /// (debug tool) Connect a fake player to the server and returns the login. Only available to Admin.
    /// </summary>
    /// <returns></returns>
    public Task<string> ConnectFakePlayerAsync();

    /// <summary>
    /// (debug tool) Disconnect a fake player, or all the fake players if login is '*'. Only available to Admin.
    /// </summary>
    /// <param name="login"></param>
    /// <returns></returns>
    public Task<bool> DisconnectFakePlayerAsync(string login);

    /// <summary>
    /// Returns the token infos for a player. The returned structure is { TokenCost, CanPayToken }.
    /// </summary>
    /// <param name="login"></param>
    /// <returns></returns>
    public Task<TmDemoTokenInfo> GetDemoTokenInfosForPlayerAsync(string login);

    /// <summary>
    /// Disable player horns. Only available to Admin.
    /// </summary>
    /// <param name="disable"></param>
    /// <returns></returns>
    public Task<bool> DisableHornsAsync(string disable);

    /// <summary>
    /// Returns whether the horns are disabled.
    /// </summary>
    /// <returns></returns>
    public Task<bool> AreHornsDisabledAsync();

    /// <summary>
    /// Disable the automatic mesages when a player connects/disconnects from the server. Only available to Admin.
    /// </summary>
    /// <param name="disable"></param>
    /// <returns></returns>
    public Task<bool> DisableServiceAnnouncesAsync(string disable);

    /// <summary>
    /// Returns whether the automatic mesages are disabled.
    /// </summary>
    /// <returns></returns>
    public Task<bool> AreServiceAnnouncesDisabledAsync();

    /// <summary>
    /// Set new server options using the struct passed as parameters. This struct must contain the following fields : Name,
    /// Comment, Password, PasswordForSpectator, NextCallVoteTimeOut, CallVoteRatio. May additionally include any of the
    /// other members listed in RpcGetServerOptions. Only available to Admin. A change of NextMaxPlayers,
    /// NextMaxSpectators, NextCallVoteTimeOut requires a map restart to be taken into account.
    /// </summary>
    /// <param name="options"></param>
    /// <returns></returns>
    public Task<bool> SetServerOptionsAsync(TmServerOptions options);

    /// <summary>
    /// Returns a struct containing the server options: Name, Comment, Password, PasswordForSpectator, CurrentMaxPlayers,
    /// NextMaxPlayers, CurrentMaxSpectators, NextMaxSpectators, KeepPlayerSlots, IsP2PUpload, IsP2PDownload,
    /// CurrentLadderMode, NextLadderMode, CurrentVehicleNetQuality, NextVehicleNetQuality, CurrentCallVoteTimeOut,
    /// NextCallVoteTimeOut, CallVoteRatio, AllowMapDownload, AutoSaveReplays, RefereePassword, RefereeMode,
    /// AutoSaveValidationReplays, HideServer, CurrentUseChangingValidationSeed, NextUseChangingValidationSeed,
    /// ClientInputsMaxLatency, DisableHorns, DisableServiceAnnounces.
    /// </summary>
    /// <returns></returns>
    public Task<TmServerOptions> GetServerOptionsAsync();

    /// <summary>
    /// Set the mods to apply on the clients. Parameters: Override, if true even the maps with a mod will be overridden by
    /// the server setting; and Mods, an array of structures [{EnvName, Url}, ...]. Requires a map restart to be taken into
    /// account. Only available to Admin.
    /// </summary>
    /// <param name="forced"></param>
    /// <param name="mods"></param>
    /// <returns></returns>
    public Task<bool> SetForcedModsAsync(bool forced, TmMods mods);

    /// <summary>
    /// Get the mods settings.
    /// </summary>
    /// <returns></returns>
    public Task<TmForcedMods> GetForcedModsAsync();

    /// <summary>
    /// Set the music to play on the clients. Parameters: Override, if true even the maps with a custom music will be
    /// overridden by the server setting, and a UrlOrFileName for the music. Requires a map restart to be taken into
    /// account. Only available to Admin.
    /// </summary>
    /// <param name="forced"></param>
    /// <param name="urlOrFileName"></param>
    /// <returns></returns>
    public Task<bool> SetForcedMusicAsync(bool forced, string urlOrFileName);

    /// <summary>
    /// Get the music setting.
    /// </summary>
    /// <returns></returns>
    public Task<TmMusicSetting> GetForcedMusicAsync();

    /// <summary>
    /// Defines a list of remappings for player skins. It expects a list of structs Orig, Name, Checksum, Url. Orig is the
    /// name of the skin to remap, or '*' for any other. Name, Checksum, Url define the skin to use. (They are optional,
    /// you may set value '' for any of those. All 3 null means same as Orig). Will only affect players connecting after
    /// the value is set. Only available to Admin.
    /// </summary>
    /// <param name="skins"></param>
    /// <returns></returns>
    public Task<bool> SetForcedSkinsAsync(TmForcedSkin[] skins);

    /// <summary>
    /// Get the current forced skins.
    /// </summary>
    /// <returns></returns>
    public Task<TmForcedSkin[]> GetForcedSkinsAsync();

    /// <summary>
    /// Returns the last error message for an internet connection. Only available to Admin.
    /// </summary>
    /// <returns></returns>
    public Task<string> GetLastConnectionErrorMessageAsync();

    /// <summary>
    /// Set the maximum time the server must wait for inputs from the clients before dropping data, or '0' for
    /// auto-adaptation. Only used by ShootMania. Only available to Admin.
    /// </summary>
    /// <param name="maxTime"></param>
    /// <returns></returns>
    public Task<bool> SetClientInputsMaxLatencyAsync(int maxTime);

    /// <summary>
    /// Get the current ClientInputsMaxLatency. Only used by ShootMania.
    /// </summary>
    /// <returns></returns>
    public Task<int> GetClientInputsMaxLatencyAsync();

    /// <summary>
    /// Stop the server. Only available to SuperAdmin.
    /// </summary>
    /// <returns></returns>
    public Task<bool> StopServerAsync();

    /// <summary>
    /// Returns a struct containing the networks stats of the server. The structure contains the following fields : Uptime,
    /// NbrConnection, MeanConnectionTime, MeanNbrPlayer, RecvNetRate, SendNetRate, TotalReceivingSize, TotalSendingSize
    /// and an array of structures named PlayerNetInfos.
    /// Each structure of the array PlayerNetInfos contains the following fields : Login, IPAddress, LastTransferTime,
    /// DeltaBetweenTwoLastNetState, PacketLossRate. Only available to SuperAdmin.
    /// </summary>
    /// <returns></returns>
    [Obsolete]
    public Task<TmNetworkStats> GetNetworkStatsAsync();

    /// <summary>
    /// Start a server on lan, using the current configuration. Only available to SuperAdmin.
    /// </summary>
    /// <returns></returns>
    public Task<bool> StartServerLanAsync();

    /// <summary>
    /// Start a server on internet, using the current configuration. Only available to SuperAdmin.
    /// </summary>
    /// <returns></returns>
    public Task<bool> StartServerInternetAsync();

    #endregion

    #region Session

    /// <summary>
    /// Allow user authentication by specifying a login and a password, to gain access to the set of functionalities
    /// corresponding to this authorization level.
    /// </summary>
    /// <param name="login"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public Task<bool> AuthenticateAsync(string login, string password);

    /// <summary>
    /// Change the password for the specified login/user. Only available to SuperAdmin.
    /// </summary>
    /// <param name="login"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public Task<bool> ChangeAuthPasswordAsync(string login, string password);

    /// <summary>
    /// Allow the GameServer to call you back.
    /// </summary>
    /// <returns></returns>
    public Task<bool> EnableCallbacksAsync(bool enable);

    /// <summary>
    /// Define the wanted api.
    /// </summary>
    /// <param name="version"></param>
    /// <returns></returns>
    public Task<bool> SetApiVersionAsync(string version);

    #endregion

    #region System

    /// <summary>
    /// Return an array of all available XML-RPC methods on this server.
    /// </summary>
    /// <returns></returns>
    public Task<string[]> SystemListMethodsAsync();

    /// <summary>
    /// Given the name of a method, return an array of legal signatures. Each signature is an array of strings. The first
    /// item of each signature is the return type, and any others items are parameter types.
    /// </summary>
    /// <param name="method"></param>
    /// <returns></returns>
    public Task<string[][]> SystemMethodSignatureAsync(string method);

    /// <summary>
    /// Given the name of a method, return a help string.
    /// </summary>
    /// <param name="method"></param>
    /// <returns></returns>
    public Task<string> SystemMethodHelpAsync(string method);

    /// <summary>
    /// Call multiple methods without multiple round-trip times.
    /// </summary>
    /// <param name="multicall">MultiCall object containing the calls to perform.</param>
    /// <returns>An array of results for each call.</returns>
    public Task<object[]> MultiCallAsync(MultiCall multicall);

    #endregion

    #region Teams

    /// <summary>
    /// Set Team names and colors (deprecated). Only available to Admin.
    /// </summary>
    /// <param name="par1"></param>
    /// <param name="par2"></param>
    /// <param name="par3"></param>
    /// <param name="par4"></param>
    /// <param name="par5"></param>
    /// <param name="par6"></param>
    /// <param name="par7"></param>
    /// <param name="par8"></param>
    /// <param name="par9"></param>
    /// <returns></returns>
    [Obsolete("Deprecated and not used in TM2 and later games.")]
    public Task<string> SetTeamInfoAsync(string par1, double par2, string par3, string par4, double par5,
        string par6, string par7, double par8, string par9);

    /// <summary>
    /// Return Team info for a given clan (0 = no clan, 1, 2). The structure contains: Name, ZonePath, City, EmblemUrl,
    /// HuePrimary, HueSecondary, RGB, ClubLinkUrl. Only available to Admin.
    /// </summary>
    /// <param name="team"></param>
    /// <returns></returns>
    public Task<TmTeamInfo> GetTeamInfoAsync(int team);

    /// <summary>
    /// Set the clublinks to use for the two clans. Only available to Admin.
    /// </summary>
    /// <param name="clubLink1"></param>
    /// <param name="clubLink2"></param>
    /// <returns></returns>
    public Task<bool> SetForcedClubLinksAsync(string clubLink1, string clubLink2);

    /// <summary>
    /// Get the forced clublinks.
    /// </summary>
    /// <param name="team"></param>
    /// <returns></returns>
    public Task<TmClubLinks> GetForcedClubLinksAsync(int team);

    /// <summary>
    /// Set whether the players can choose their side or if the teams are forced by the server (using ForcePlayerTeam()).
    /// Only available to Admin.
    /// </summary>
    /// <param name="forced"></param>
    /// <returns></returns>
    public Task<bool> SetForcedTeamsAsync(bool forced);

    /// <summary>
    /// Returns whether the players can choose their side or if the teams are forced by the server.
    /// </summary>
    /// <returns></returns>
    public Task<bool> GetForcedTeamsAsync();

    /// <summary>
    /// Returns the current winning team for the race in progress. (-1: if not in team mode, or draw match)
    /// </summary>
    /// <returns></returns>
    public Task<int> GetCurrentWinnerTeamAsync();

    /// <summary>
    /// Force the team of the player. Only available in team mode. You have to pass the login and the team number (0 or 1).
    /// Only available to Admin.
    /// </summary>
    /// <param name="playerLogin"></param>
    /// <param name="cameraType"></param>
    /// <returns></returns>
    public Task<bool> ForcePlayerTeamAsync(string playerLogin, int cameraType);

    /// <summary>
    /// Force the team of the player. Only available in team mode. You have to pass the playerid and the team number (0 or
    /// 1). Only available to Admin.
    /// </summary>
    /// <param name="playerId"></param>
    /// <param name="cameraType"></param>
    /// <returns></returns>
    public Task<bool> ForcePlayerTeamIdAsync(int playerId, int cameraType);

    #endregion

    #region Votes

    /// <summary>
    /// Call a vote for a cmd. The command is a XML string corresponding to an XmlRpc request. Only available to Admin.
    /// </summary>
    /// <param name="cmd"></param>
    /// <returns></returns>
    public Task<bool> CallVoteAsync(string cmd);

    /// <summary>
    /// Extended call vote. Same as CallVote, but you can additionally supply specific parameters for this vote: a ratio, a
    /// time out and who is voting. Special timeout values: a ratio of '-1' means default; a timeout of '0' means default,
    /// '1' means indefinite; Voters values: '0' means only active players, '1' means any player, '2' is for everybody,
    /// pure spectators included. Only available to Admin.
    /// </summary>
    /// <param name="cmd"></param>
    /// <param name="ratio"></param>
    /// <param name="timeout"></param>
    /// <param name="who"></param>
    /// <returns></returns>
    public Task<bool> CallVoteExAsync(string cmd, double ratio, int timeout, int who);

    /// <summary>
    /// Used internally by game.
    /// </summary>
    /// <returns></returns>
    public Task<bool> InternalCallVoteAsync();

    /// <summary>
    /// Cancel the current vote. Only available to Admin.
    /// </summary>
    /// <returns></returns>
    public Task<bool> CancelVoteVoteAsync();

    /// <summary>
    /// Returns the vote currently in progress. The returned structure is { CallerLogin, CmdName, CmdParam }.
    /// </summary>
    /// <returns></returns>
    public Task<TmCurrentCallVote> GetCurrentCallVoteAsync();

    /// <summary>
    /// Set a new timeout for waiting for votes. A zero value disables callvote. Only available to Admin. Requires a map
    /// restart to be taken into account.
    /// </summary>
    /// <param name="timeout"></param>
    /// <returns></returns>
    public Task<bool> SetCallVoteTimeOutAsync(int timeout);

    /// <summary>
    /// Get the current and next timeout for waiting for votes. The struct returned contains two fields 'CurrentValue' and
    /// 'NextValue'.
    /// </summary>
    /// <returns></returns>
    public Task<TmCurrentNextValue<int>> GetCallVoteTimeOutAsync();

    /// <summary>
    /// Set a new default ratio for passing a vote. Must lie between 0 and 1. Only available to Admin.
    /// </summary>
    /// <param name="ratio"></param>
    /// <returns></returns>
    public Task<bool> SetCallVoteRatioAsync(double ratio);
    /// <summary>
    /// Get the current default ratio for passing a vote. This value lies between 0 and 1.
    /// </summary>
    /// <returns></returns>
    public Task<double> GetCallVoteRatioAsync();

    /// <summary>
    /// Set the ratios list for passing specific votes. The parameter is an array of structs {string Command, double
    /// Ratio}, ratio is in [0,1] or -1 for vote disabled. Only available to Admin.
    /// </summary>
    /// <param name="ratios"></param>
    /// <returns></returns>
    public Task<bool> SetCallVoteRatiosAsync(TmCallVoteRatio[] ratios);

    /// <summary>
    /// Get the current ratios for passing votes.
    /// </summary>
    /// <returns></returns>
    public Task<TmCallVoteRatio[]> GetCallVoteRatiosAsync();

    #endregion

    #endregion

    #region ModeScript

    /// <summary>
    /// Action for the OnModeScriptCallback event-
    /// </summary>
    /// <param name="method">Name of the method that was called.</param>
    /// <param name="data">Parsed JSON data that came with the callback.</param>
    /// <returns></returns>
    public delegate Task ModeScriptCallbackAction(string method, JObject data);

    /// <summary>
    /// Triggered when any ModeScript callback is sent from the server.
    /// </summary>
    public event ModeScriptCallbackAction OnModeScriptCallback;

    /// <summary>
    /// Call a ModeScript method and wait for the response.
    /// </summary>
    /// <param name="method">Name of the method.</param>
    /// <param name="args">Parameters to be passed with the method call.</param>
    /// <returns>Parsed JSON result from the method call.</returns>
    public Task<(JObject, XmlRpcBaseType[])> GetModeScriptResponseAsync(string method, params string[] args);

    /// <summary>
    /// Call a ModeScript method and wait for the response and convert it to a native type.
    /// </summary>
    /// <param name="method">Name of the method.</param>
    /// <param name="args">Parameters to be passed with the method call.</param>
    /// <typeparam name="TResponse">Type of the response data.</typeparam>
    /// <returns>Parsed JSON result from the method call.</returns>
    /// <exception cref="InvalidOperationException"></exception>
    public Task<TResponse> GetModeScriptResponseAsync<TResponse>(string method, params string[] args);

    /// <summary>
    /// Call a ModeScript method and wait for the response and convert it to a native type.
    /// </summary>
    /// <param name="method">Name of the method.</param>
    /// <param name="args">Parameters to be passed with the method call.</param>
    /// <typeparam name="TResponse">Type of the response data.</typeparam>
    /// <typeparam name="TExtraArg">Type of the extra argument of the callback.</typeparam>
    /// <returns>Parsed JSON result from the method call.</returns>
    /// <exception cref="InvalidOperationException"></exception>
    public Task<(TResponse, TExtraArg)> GetModeScriptResponseAsync<TResponse, TExtraArg>(string method, params string[] args);

    #endregion
}