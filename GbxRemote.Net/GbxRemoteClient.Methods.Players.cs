using System.Threading.Tasks;
using GbxRemoteNet.Structs;
using GbxRemoteNet.XmlRpc;

namespace GbxRemoteNet;

/// <summary>
///     Method Category: Players
/// </summary>
public partial class GbxRemoteClient
{
    /// <summary>
    ///     Returns the list of players on the server. This method take two parameters. The first parameter specifies the
    ///     maximum number of infos to be returned, and the second one the starting index in the list, an optional 3rd
    ///     parameter is used for compatibility: struct version (0 = united, 1 = forever, 2 = forever, including the servers).
    ///     The list is an array of PlayerInfo structures. Forever PlayerInfo struct is: Login, NickName, PlayerId, TeamId,
    ///     SpectatorStatus, LadderRanking, and Flags.
    ///     LadderRanking is 0 when not in official mode,
    ///     Flags = ForceSpectator(0,1,2) + IsReferee * 10 + IsPodiumReady * 100 + StereoDisplayMode * 1000 +
    ///     IsManagedByAnOtherServer * 10000 + IsServer * 100000 + HasPlayerSlot * 1000000 + IsBroadcasting * 10000000 +
    ///     HasJoinedGame * 100000000
    ///     SpectatorStatus = Spectator + TemporarySpectator * 10 + PureSpectator * 100 + AutoTarget * 1000 + CurrentTargetId *
    ///     10000
    /// </summary>
    /// <param name="maxInfos"></param>
    /// <param name="startIndex"></param>
    /// <param name="serverType">
    ///     OPTIONAL: Used for compatibility: struct version (0 = united, 1 = forever, 2 = forever,
    ///     including the servers)
    /// </param>
    /// <returns></returns>
    public async Task<PlayerInfo[]> GetPlayerListAsync(int maxInfos = -1, int startIndex = 0, int serverType = -1)
    {
        return (PlayerInfo[]) XmlRpcTypes.ToNativeValue<PlayerInfo>(
            await CallOrFaultAsync("GetPlayerList", maxInfos, startIndex, serverType)
        );
    }

    /// <summary>
    ///     Returns a struct containing the infos on the player with the specified login, with an optional parameter for
    ///     compatibility: struct version (0 = united, 1 = forever). The structure is identical to the ones from GetPlayerList.
    ///     Forever PlayerInfo struct is: Login, NickName, PlayerId, TeamId, SpectatorStatus, LadderRanking, and Flags.
    ///     LadderRanking is 0 when not in official mode,
    ///     Flags = ForceSpectator(0,1,2) + IsReferee * 10 + IsPodiumReady * 100 + StereoDisplayMode * 1000 +
    ///     IsManagedByAnOtherServer * 10000 + IsServer * 100000 + HasPlayerSlot * 1000000 + IsBroadcasting * 10000000 +
    ///     HasJoinedGame * 100000000
    ///     SpectatorStatus = Spectator + TemporarySpectator * 10 + PureSpectator * 100 + AutoTarget * 1000 + CurrentTargetId *
    ///     10000
    ///     Each structure of the array Skins contains two fields Environnement and a struct PackDesc. Each structure PackDesc,
    ///     as well as the struct Avatar, contains two fields FileName and Checksum.
    /// </summary>
    /// <param name="playerLogin"></param>
    /// <param name="serverType"></param>
    /// <returns></returns>
    public async Task<PlayerInfo> GetPlayerInfoAsync(string playerLogin, int serverType = 1)
    {
        return (PlayerInfo) XmlRpcTypes.ToNativeValue<PlayerInfo>(
            await CallOrFaultAsync("GetPlayerInfo", playerLogin, serverType)
        );
    }

    /// <summary>
    ///     Returns a struct containing the infos on the player with the specified login. The structure contains the following
    ///     fields : Login, NickName, PlayerId, TeamId, IPAddress, DownloadRate, UploadRate, Language, IsSpectator,
    ///     IsInOfficialMode, a structure named Avatar, an array of structures named Skins, a structure named LadderStats,
    ///     HoursSinceZoneInscription and OnlineRights (0: nations account, 3: united account).
    ///     Each structure of the array Skins contains two fields Environnement and a struct PackDesc. Each structure PackDesc,
    ///     as well as the struct Avatar, contains two fields FileName and Checksum.
    /// </summary>
    /// <param name="playerLogin"></param>
    /// <returns></returns>
    public async Task<PlayerDetailedInfo> GetDetailedPlayerInfoAsync(string playerLogin)
    {
        return (PlayerDetailedInfo) XmlRpcTypes.ToNativeValue<PlayerDetailedInfo>(
            await CallOrFaultAsync("GetDetailedPlayerInfo", playerLogin)
        );
    }

    /// <summary>
    ///     Returns a struct containing the player infos of the game server (ie: in case of a basic server, itself; in case of
    ///     a relay server, the main server), with an optional parameter for compatibility: struct version (0 = united, 1 =
    ///     forever).
    ///     The structure is identical to the ones from GetPlayerList. Forever PlayerInfo struct is: Login, NickName, PlayerId,
    ///     TeamId, SpectatorStatus, LadderRanking, and Flags.
    ///     LadderRanking is 0 when not in official mode,
    ///     Flags = ForceSpectator(0,1,2) + IsReferee * 10 + IsPodiumReady * 100 + StereoDisplayMode * 1000 +
    ///     IsManagedByAnOtherServer * 10000 + IsServer * 100000 + HasPlayerSlot * 1000000 + IsBroadcasting * 10000000 +
    ///     HasJoinedGame * 100000000
    ///     SpectatorStatus = Spectator + TemporarySpectator * 10 + PureSpectator * 100 + AutoTarget * 1000 + CurrentTargetId *
    ///     10000
    ///     Each structure contains the following fields : Login, NickName, PlayerId and Rank. In addition, for legacy
    ///     trackmania modes it also contains BestTime, Score, NbrLapsFinished, LadderScore, and an array BestCheckpoints that
    ///     contains the checkpoint times for the best race.
    /// </summary>
    /// <param name="serverType"></param>
    /// <returns></returns>
    public async Task<PlayerInfo> GetMainServerPlayerInfoAsync(int serverType)
    {
        return (PlayerInfo) XmlRpcTypes.ToNativeValue<PlayerInfo>(
            await CallOrFaultAsync("GetMainServerPlayerInfo", serverType)
        );
    }

    /// <summary>
    ///     Returns the current rankings for the race in progress. (In trackmania legacy team modes, the scores for the two
    ///     teams are returned. In other modes, it's the individual players' scores) This method take two parameters. The first
    ///     parameter specifies the maximum number of infos to be returned, and the second one the starting index in the
    ///     ranking.  The ranking returned is a list of structures.
    ///     Each structure contains the following fields : Login, NickName, PlayerId and Rank. In addition, for legacy
    ///     trackmania modes it also contains BestTime, Score, NbrLapsFinished, LadderScore, and an array BestCheckpoints that
    ///     contains the checkpoint times for the best race.
    /// </summary>
    /// <param name="playerLogin"></param>
    /// <returns></returns>
    public async Task<PlayerRanking[]> GetCurrentRankingAsync(int maxInfos, int startRatingIndex)
    {
        return (PlayerRanking[]) XmlRpcTypes.ToNativeValue<PlayerRanking>(
            await CallOrFaultAsync("GetCurrentRanking", maxInfos, startRatingIndex)
        );
    }

    /// <summary>
    ///     Returns the current ranking for the race in progressof the player with the specified login (or list of
    ///     comma-separated logins). The ranking returned is a list of structures.
    ///     Each structure contains the following fields : Login, NickName, PlayerId and Rank. In addition, for legacy
    ///     trackmania modes it also contains BestTime, Score, NbrLapsFinished, LadderScore, and an array BestCheckpoints that
    ///     contains the checkpoint times for the best race.
    /// </summary>
    /// <param name="playerLogin"></param>
    /// <returns></returns>
    public async Task<PlayerRanking[]> GetCurrentRankingForLoginAsync(string playerLogin)
    {
        return (PlayerRanking[]) XmlRpcTypes.ToNativeValue<PlayerRanking>(
            await CallOrFaultAsync("GetCurrentRankingForLogin")
        );
    }

    /// <summary>
    ///     Force the scores of the current game. Only available in rounds and team mode. You have to pass an array of structs
    ///     {int PlayerId, int Score}.
    ///     And a boolean SilentMode - if true, the scores are silently updated (only available for SuperAdmin), allowing an
    ///     external controller to do its custom counting... Only available to Admin/SuperAdmin.
    /// </summary>
    /// <param name="playerScores"></param>
    /// <param name="silentMode"></param>
    /// <returns></returns>
    public async Task<bool> ForceScoresAsync(PlayerScore[] playerScores, bool silentMode)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("ForceScores", playerScores, silentMode)
        );
    }

    /// <summary>
    ///     Force the spectating status of the player. You have to pass the login and the spectator mode (0: user selectable,
    ///     1: spectator, 2: player, 3: spectator but keep selectable). Only available to Admin.
    /// </summary>
    /// <param name="playerLogin"></param>
    /// <param name="cameraType"></param>
    /// <returns></returns>
    public async Task<bool> ForceSpectatorAsync(int playerLogin, int cameraType)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("ForceSpectator", playerLogin, cameraType)
        );
    }

    /// <summary>
    ///     Force the spectating status of the player. You have to pass the playerid and the spectator mode (0: user
    ///     selectable, 1: spectator, 2: player, 3: spectator but keep selectable). Only available to Admin.
    /// </summary>
    /// <param name="playerId"></param>
    /// <param name="cameraType"></param>
    /// <returns></returns>
    public async Task<bool> ForceSpectatorIdAsync(int playerId, int cameraType)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("ForceSpectatorId", playerId, cameraType)
        );
    }

    /// <summary>
    ///     Force spectators to look at a specific player. You have to pass the login of the spectator (or '' for all) and the
    ///     login of the target (or '' for automatic),
    ///     and an integer for the camera type to use (-1 = leave unchanged, 0 = replay, 1 = follow, 2 = free). Only available
    ///     to Admin.
    /// </summary>
    /// <param name="playerLogin"></param>
    /// <param name="targetLogin"></param>
    /// <param name="cameraType"></param>
    /// <returns></returns>
    public async Task<bool> ForceSpectatorTargetAsync(string playerLogin, string targetLogin, int cameraType)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("ForceSpectatorTarget", playerLogin, targetLogin, cameraType)
        );
    }

    /// <summary>
    ///     Force spectators to look at a specific player. You have to pass the id of the spectator (or -1 for all) and the id
    ///     of the target (or -1 for automatic),
    ///     and an integer for the camera type to use (-1 = leave unchanged, 0 = replay, 1 = follow, 2 = free). Only available
    ///     to Admin.
    /// </summary>
    /// <param name="playerId"></param>
    /// <param name="targetId"></param>
    /// <param name="cameraType"></param>
    /// <returns></returns>
    public async Task<bool> ForceSpectatorTargetIdAsync(int playerId, int targetId, int cameraType)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("ForceSpectatorTargetId", playerId, targetId, cameraType)
        );
    }

    /// <summary>
    ///     Pass the login of the spectator. A spectator that once was a player keeps his player slot, so that he can go back
    ///     to race mode.
    ///     Calling this function frees this slot for another player to connect. Only available to Admin.
    /// </summary>
    /// <param name="playerLogin"></param>
    /// <returns></returns>
    public async Task<bool> SpectatorReleasePlayerSlotAsync(string playerLogin)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("SpectatorReleasePlayerSlot", playerLogin)
        );
    }

    /// <summary>
    ///     Pass the playerid of the spectator. A spectator that once was a player keeps his player slot, so that he can go
    ///     back to race mode.
    ///     Calling this function frees this slot for another player to connect. Only available to Admin.
    /// </summary>
    /// <param name="playerId"></param>
    /// <returns></returns>
    public async Task<bool> SpectatorReleasePlayerSlotIdAsync(int playerId)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("SpectatorReleasePlayerSlotId", playerId)
        );
    }

    /// <summary>
    ///     Ignore players profile skin customisation. Only available to Admin.
    /// </summary>
    /// <param name="disabled"></param>
    /// <returns></returns>
    public async Task<bool> DisableProfileSkinsAsync(bool disabled = true)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("DisableProfileSkins", disabled)
        );
    }

    /// <summary>
    ///     Returns whether the custom skins are disabled.
    /// </summary>
    /// <returns></returns>
    public async Task<bool> AreProfileSkinsDisabledAsync()
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("AreProfileSkinsDisabled")
        );
    }

    #region Kicking

    /// <summary>
    ///     Kick the player with the specified login, with an optional message. Only available to Admin.
    /// </summary>
    /// <param name="login"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    public async Task<bool> KickAsync(string login, string message = null)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("Kick", login, message)
        );
    }

    /// <summary>
    ///     Kick the player with the specified PlayerId, with an optional message. Only available to Admin.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    public async Task<bool> KickIdAsync(int id, string message = null)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("KickId", id, message)
        );
    }

    #endregion

    #region Ban List

    /// <summary>
    ///     Ban the player with the specified login, with an optional message. Only available to Admin.
    /// </summary>
    /// <param name="login"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    public async Task<bool> BanAsync(string login, string message = null)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("Ban", login, message)
        );
    }

    /// <summary>
    ///     Ban the player with the specified login, with a message. Add it to the black list, and optionally save the new
    ///     list. Only available to Admin.
    /// </summary>
    /// <param name="login"></param>
    /// <param name="message"></param>
    /// <param name="saveToFile"></param>
    /// <returns></returns>
    public async Task<bool> BanAndBlackListAsync(string login, string message, bool saveToFile = false)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("BanAndBlackList", login, message, saveToFile)
        );
    }

    /// <summary>
    ///     Ban the player with the specified PlayerId, with an optional message. Only available to Admin.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    public async Task<bool> BanIdAsync(int id, string message)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("BanId", id, message)
        );
    }

    /// <summary>
    ///     Unban the player with the specified login. Only available to Admin.
    /// </summary>
    /// <param name="login"></param>
    /// <returns></returns>
    public async Task<bool> UnBanAsync(string login)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("UnBan", login)
        );
    }

    /// <summary>
    ///     Clean the ban list of the server. Only available to Admin.
    /// </summary>
    /// <returns></returns>
    public async Task<bool> CleanBanListAsync()
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("CleanBanList")
        );
    }

    /// <summary>
    ///     Returns the list of banned players. This method takes two parameters. The first parameter specifies the maximum
    ///     number of infos to be returned, and the second one the starting index in the list. The list is an array of
    ///     structures. Each structure contains the following fields : Login, ClientName and IPAddress.
    /// </summary>
    /// <param name="maxInfos"></param>
    /// <param name="startIndex"></param>
    /// <returns></returns>
    public async Task<BanListEntry[]> GetBanListAsync(int maxInfos, int startIndex)
    {
        return (BanListEntry[]) XmlRpcTypes.ToNativeValue<BanListEntry>(
            await CallOrFaultAsync("GetBanList", maxInfos, startIndex)
        );
    }

    #endregion

    #region Black List

    /// <summary>
    ///     Blacklist the player with the specified login. Only available to SuperAdmin.
    /// </summary>
    /// <param name="login"></param>
    /// <returns></returns>
    public async Task<bool> BlackListAsync(string login)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("BlackList", login)
        );
    }

    /// <summary>
    ///     Blacklist the player with the specified PlayerId. Only available to SuperAdmin.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<bool> BlackListIdAsync(int id)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("BlackListId", id)
        );
    }

    /// <summary>
    /// </summary>
    /// <param name="login"></param>
    /// <returns></returns>
    public async Task<bool> UnBlackListAsync(string login)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("UnBlackList", login)
        );
    }

    /// <summary>
    ///     Clean the blacklist of the server. Only available to SuperAdmin.
    /// </summary>
    /// <returns></returns>
    public async Task<bool> CleanBlackListAsync()
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("CleanBlackList")
        );
    }

    /// <summary>
    ///     Returns the list of blacklisted players. This method takes two parameters. The first parameter specifies the
    ///     maximum number of infos to be returned, and the second one the starting index in the list. The list is an array of
    ///     structures. Each structure contains the following fields : Login.
    /// </summary>
    /// <param name="maxInfos"></param>
    /// <param name="startIndex"></param>
    /// <returns></returns>
    public async Task<BlackListEntry[]> GetBlackListAsync(int maxInfos, int startIndex)
    {
        return (BlackListEntry[]) XmlRpcTypes.ToNativeValue<BlackListEntry>(
            await CallOrFaultAsync("GetBanList", maxInfos, startIndex)
        );
    }

    /// <summary>
    ///     Load the black list file with the specified file name. Only available to Admin.
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public async Task<bool> LoadBlackListAsync(string fileName)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("LoadBlackList", fileName)
        );
    }

    /// <summary>
    ///     Save the black list in the file with specified file name. Only available to Admin.
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public async Task<bool> SaveBlackListAsync(string fileName)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("SaveBlackList", fileName)
        );
    }

    #endregion

    #region Guest List

    /// <summary>
    ///     Add the player with the specified login on the guest list. Only available to Admin.
    /// </summary>
    /// <param name="login"></param>
    /// <returns></returns>
    public async Task<bool> AddGuestAsync(string login)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("AddGuest", login)
        );
    }

    /// <summary>
    ///     Add the player with the specified PlayerId on the guest list. Only available to Admin.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<bool> AddGuestIdAsync(int id)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("AddGuestId", id)
        );
    }

    /// <summary>
    ///     Remove the player with the specified login from the guest list. Only available to Admin.
    /// </summary>
    /// <param name="login"></param>
    /// <returns></returns>
    public async Task<bool> RemoveGuestAsync(string login)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("RemoveGuest", login)
        );
    }

    /// <summary>
    ///     Remove the player with the specified PlayerId from the guest list. Only available to Admin.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<bool> RemoveGuestIdAsync(int id)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("RemoveGuestId", id)
        );
    }

    /// <summary>
    ///     Clean the guest list of the server. Only available to Admin.
    /// </summary>
    /// <returns></returns>
    public async Task<bool> CleanGuestListAsync()
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("CleanGuestList")
        );
    }

    /// <summary>
    ///     Returns the list of players on the guest list. This method takes two parameters. The first parameter specifies the
    ///     maximum number of infos to be returned, and the second one the starting index in the list. The list is an array of
    ///     structures. Each structure contains the following fields : Login.
    /// </summary>
    /// <param name="maxInfos"></param>
    /// <param name="startIndex"></param>
    /// <returns></returns>
    public async Task<GuestListEntry[]> GetGuestListAsync(int maxInfos, int startIndex)
    {
        return (GuestListEntry[]) XmlRpcTypes.ToNativeValue<GuestListEntry>(
            await CallOrFaultAsync("GetGuestList", maxInfos, startIndex)
        );
    }

    /// <summary>
    ///     Load the guest list file with the specified file name. Only available to Admin.
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public async Task<bool> LoadGuestListAsync(string fileName)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("LoadGuestList", fileName)
        );
    }

    /// <summary>
    ///     Save the guest list in the file with specified file name. Only available to Admin.
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public async Task<bool> SaveGuestListAsync(string fileName)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("SaveGuestList", fileName)
        );
    }

    #endregion

    #region Ignore List

    /// <summary>
    ///     Ignore the player with the specified login. Only available to Admin.
    /// </summary>
    /// <param name="login"></param>
    /// <returns></returns>
    public async Task<bool> IgnoreAsync(string login)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("Ignore", login)
        );
    }

    /// <summary>
    ///     Ignore the player with the specified PlayerId. Only available to Admin.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<bool> IgnoreIdAsync(int id)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("IgnoreId", id)
        );
    }

    /// <summary>
    ///     Unignore the player with the specified login. Only available to Admin.
    /// </summary>
    /// <param name="login"></param>
    /// <returns></returns>
    public async Task<bool> UnIgnoreAsync(string login)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("UnIgnore", login)
        );
    }

    /// <summary>
    ///     Unignore the player with the specified PlayerId. Only available to Admin.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<bool> UnIgnoreIdAsync(int id)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("UnIgnoreId", id)
        );
    }

    /// <summary>
    ///     Clean the ignore list of the server. Only available to Admin.
    /// </summary>
    /// <returns></returns>
    public async Task<bool> CleanIgnoreListAsync()
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("CleanIgnoreList")
        );
    }

    /// <summary>
    ///     Returns the list of ignored players. This method takes two parameters. The first parameter specifies the maximum
    ///     number of infos to be returned, and the second one the starting index in the list. The list is an array of
    ///     structures. Each structure contains the following fields : Login.
    /// </summary>
    /// <param name="maxInfos"></param>
    /// <param name="startIndex"></param>
    /// <returns></returns>
    public async Task<IgnoreListEntry[]> GetIgnoreListAsync(int maxInfos, int startIndex)
    {
        return (IgnoreListEntry[]) XmlRpcTypes.ToNativeValue<IgnoreListEntry>(
            await CallOrFaultAsync("CleanIgnoreList", maxInfos, startIndex)
        );
    }

    #endregion

    #region Payments & Bills

    /// <summary>
    ///     Pay planets from the server account to a player, returns the BillId. This method takes three parameters: Login of
    ///     the payee, Cost in planets to pay and a Label to send with the payment. The creation of the transaction itself may
    ///     cost planets, so you need to have planets on the server account. Only available to Admin.
    /// </summary>
    /// <param name="login"></param>
    /// <param name="planets"></param>
    /// <param name="label"></param>
    /// <returns></returns>
    public async Task<int> PayAsync(string login, int planets, string label)
    {
        return (int) XmlRpcTypes.ToNativeValue<int>(
            await CallOrFaultAsync("Pay", login, planets, label)
        );
    }

    /// <summary>
    ///     Create a bill, send it to a player, and return the BillId. This method takes four parameters: LoginFrom of the
    ///     payer, Cost in planets the player has to pay, Label of the transaction and an optional LoginTo of the payee (if
    ///     empty string, then the server account is used). The creation of the transaction itself may cost planets, so you
    ///     need to have planets on the server account. Only available to Admin.
    /// </summary>
    /// <param name="loginFrom"></param>
    /// <param name="planets"></param>
    /// <param name="label"></param>
    /// <param name="loginTo"></param>
    /// <returns></returns>
    public async Task<int> SendBillAsync(string loginFrom, int planets, string label, string loginTo = null)
    {
        return (int) XmlRpcTypes.ToNativeValue<int>(
            await CallOrFaultAsync("SendBill", loginFrom, planets, label, loginTo)
        );
    }

    /// <summary>
    ///     Returns the current state of a bill. This method takes one parameter, the BillId. Returns a struct containing
    ///     State, StateName and TransactionId. Possible enum values are: CreatingTransaction, Issued, ValidatingPayement,
    ///     Payed, Refused, Error.
    /// </summary>
    /// <param name="billId"></param>
    /// <returns></returns>
    public async Task<BillState> GetBillStateAsync(int billId)
    {
        return (BillState) XmlRpcTypes.ToNativeValue<BillState>(
            await CallOrFaultAsync("SendBill", billId)
        );
    }

    #endregion
}