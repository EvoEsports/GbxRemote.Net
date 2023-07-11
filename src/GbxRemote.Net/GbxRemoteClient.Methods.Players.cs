using System;
using System.Threading.Tasks;
using GbxRemoteNet.Structs;
using GbxRemoteNet.XmlRpc;

namespace GbxRemoteNet;

/// <summary>
/// Method Category: Players
/// </summary>
public partial class GbxRemoteClient
{
    public async Task<TmPlayerInfo[]> GetPlayerListAsync(int maxInfos = -1, int startIndex = 0, int serverType = -1)
    {
        return (TmPlayerInfo[]) XmlRpcTypes.ToNativeValue<TmPlayerInfo>(
            await CallOrFaultAsync("GetPlayerList", maxInfos, startIndex, serverType)
        );
    }

    public async Task<TmPlayerInfo> GetPlayerInfoAsync(string playerLogin, int serverType = 1)
    {
        return (TmPlayerInfo) XmlRpcTypes.ToNativeValue<TmPlayerInfo>(
            await CallOrFaultAsync("GetPlayerInfo", playerLogin, serverType)
        );
    }

    public async Task<TmPlayerDetailedInfo> GetDetailedPlayerInfoAsync(string playerLogin)
    {
        return (TmPlayerDetailedInfo) XmlRpcTypes.ToNativeValue<TmPlayerDetailedInfo>(
            await CallOrFaultAsync("GetDetailedPlayerInfo", playerLogin)
        );
    }

    public async Task<TmPlayerInfo> GetMainServerPlayerInfoAsync(int serverType)
    {
        return (TmPlayerInfo) XmlRpcTypes.ToNativeValue<TmPlayerInfo>(
            await CallOrFaultAsync("GetMainServerPlayerInfo", serverType)
        );
    }

    public async Task<TmPlayerRanking[]> GetCurrentRankingAsync(int maxInfos, int startRatingIndex)
    {
        return (TmPlayerRanking[]) XmlRpcTypes.ToNativeValue<TmPlayerRanking>(
            await CallOrFaultAsync("GetCurrentRanking", maxInfos, startRatingIndex)
        );
    }

    public async Task<TmPlayerRanking[]> GetCurrentRankingForLoginAsync(string playerLogin)
    {
        return (TmPlayerRanking[]) XmlRpcTypes.ToNativeValue<TmPlayerRanking>(
            await CallOrFaultAsync("GetCurrentRankingForLogin")
        );
    }

    public async Task<bool> ForceScoresAsync(TmPlayerScore[] playerScores, bool silentMode)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("ForceScores", playerScores, silentMode)
        );
    }

    public async Task<bool> ForceSpectatorAsync(string playerLogin, int cameraType)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("ForceSpectator", playerLogin, cameraType)
        );
    }

    public async Task<bool> ForceSpectatorIdAsync(int playerId, int cameraType)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("ForceSpectatorId", playerId, cameraType)
        );
    }

    public async Task<bool> ForceSpectatorTargetAsync(string playerLogin, string targetLogin, int cameraType)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("ForceSpectatorTarget", playerLogin, targetLogin, cameraType)
        );
    }

    public async Task<bool> ForceSpectatorTargetIdAsync(int playerId, int targetId, int cameraType)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("ForceSpectatorTargetId", playerId, targetId, cameraType)
        );
    }

    public async Task<bool> SpectatorReleasePlayerSlotAsync(string playerLogin)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("SpectatorReleasePlayerSlot", playerLogin)
        );
    }

    public async Task<bool> SpectatorReleasePlayerSlotIdAsync(int playerId)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("SpectatorReleasePlayerSlotId", playerId)
        );
    }

    public async Task<bool> DisableProfileSkinsAsync(bool disabled = true)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("DisableProfileSkins", disabled)
        );
    }

    public async Task<bool> AreProfileSkinsDisabledAsync()
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("AreProfileSkinsDisabled")
        );
    }

    #region Kicking

    public async Task<bool> KickAsync(string login, string message = null)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("Kick", login, message)
        );
    }

    public async Task<bool> KickIdAsync(int id, string message = null)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("KickId", id, message)
        );
    }

    #endregion

    #region Ban List

    public async Task<bool> BanAsync(string login, string message = null)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("Ban", login, message)
        );
    }

    public async Task<bool> BanAndBlackListAsync(string login, string message, bool saveToFile = false)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("BanAndBlackList", login, message, saveToFile)
        );
    }

    public async Task<bool> BanIdAsync(int id, string message)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("BanId", id, message)
        );
    }

    public async Task<bool> UnBanAsync(string login)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("UnBan", login)
        );
    }

    public async Task<bool> CleanBanListAsync()
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("CleanBanList")
        );
    }

    public async Task<TmBanListEntry[]> GetBanListAsync(int maxInfos, int startIndex)
    {
        return (TmBanListEntry[]) XmlRpcTypes.ToNativeValue<TmBanListEntry>(
            await CallOrFaultAsync("GetBanList", maxInfos, startIndex)
        );
    }

    #endregion

    #region Black List
    
    public async Task<bool> BlackListAsync(string login)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("BlackList", login)
        );
    }

    public async Task<bool> BlackListIdAsync(int id)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("BlackListId", id)
        );
    }

    public async Task<bool> UnBlackListAsync(string login)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("UnBlackList", login)
        );
    }

    public async Task<bool> CleanBlackListAsync()
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("CleanBlackList")
        );
    }

    public async Task<TmBlackListEntry[]> GetBlackListAsync(int maxInfos, int startIndex)
    {
        return (TmBlackListEntry[]) XmlRpcTypes.ToNativeValue<TmBlackListEntry>(
            await CallOrFaultAsync("GetBanList", maxInfos, startIndex)
        );
    }

    public async Task<bool> LoadBlackListAsync(string fileName)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("LoadBlackList", fileName)
        );
    }

    public async Task<bool> SaveBlackListAsync(string fileName)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("SaveBlackList", fileName)
        );
    }

    #endregion

    #region Guest List

    public async Task<bool> AddGuestAsync(string login)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("AddGuest", login)
        );
    }

    public async Task<bool> AddGuestIdAsync(int id)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("AddGuestId", id)
        );
    }

    public async Task<bool> RemoveGuestAsync(string login)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("RemoveGuest", login)
        );
    }

    public async Task<bool> RemoveGuestIdAsync(int id)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("RemoveGuestId", id)
        );
    }

    public async Task<bool> CleanGuestListAsync()
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("CleanGuestList")
        );
    }

    public async Task<TmGuestListEntry[]> GetGuestListAsync(int maxInfos, int startIndex)
    {
        return (TmGuestListEntry[]) XmlRpcTypes.ToNativeValue<TmGuestListEntry>(
            await CallOrFaultAsync("GetGuestList", maxInfos, startIndex)
        );
    }

    public async Task<bool> LoadGuestListAsync(string fileName)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("LoadGuestList", fileName)
        );
    }

    public async Task<bool> SaveGuestListAsync(string fileName)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("SaveGuestList", fileName)
        );
    }

    #endregion

    #region Ignore List

    public async Task<bool> IgnoreAsync(string login)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("Ignore", login)
        );
    }

    public async Task<bool> IgnoreIdAsync(int id)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("IgnoreId", id)
        );
    }

    public async Task<bool> UnIgnoreAsync(string login)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("UnIgnore", login)
        );
    }

    public async Task<bool> UnIgnoreIdAsync(int id)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("UnIgnoreId", id)
        );
    }

    public async Task<bool> CleanIgnoreListAsync()
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("CleanIgnoreList")
        );
    }

    public async Task<TmIgnoreListEntry[]> GetIgnoreListAsync(int maxInfos, int startIndex)
    {
        return (TmIgnoreListEntry[]) XmlRpcTypes.ToNativeValue<TmIgnoreListEntry>(
            await CallOrFaultAsync("CleanIgnoreList", maxInfos, startIndex)
        );
    }

    #endregion

    #region Payments & Bills

    [Obsolete]
    public async Task<int> PayAsync(string login, int planets, string label)
    {
        return (int) XmlRpcTypes.ToNativeValue<int>(
            await CallOrFaultAsync("Pay", login, planets, label)
        );
    }

    [Obsolete]
    public async Task<int> SendBillAsync(string loginFrom, int planets, string label, string loginTo = null)
    {
        return (int) XmlRpcTypes.ToNativeValue<int>(
            await CallOrFaultAsync("SendBill", loginFrom, planets, label, loginTo)
        );
    }

    [Obsolete]
    public async Task<TmBillState> GetBillStateAsync(int billId)
    {
        return (TmBillState) XmlRpcTypes.ToNativeValue<TmBillState>(
            await CallOrFaultAsync("SendBill", billId)
        );
    }

    #endregion
}