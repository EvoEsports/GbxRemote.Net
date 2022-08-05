using System;
using System.Threading.Tasks;
using GbxRemoteNet.Structs;
using GbxRemoteNet.XmlRpc;

namespace GbxRemoteNet;

/// <summary>
///     Method Category: Game Flow
/// </summary>
public partial class GbxRemoteClient
{
    /// <summary>
    ///     Restarts the map, with an optional boolean parameter DontClearCupScores (only available in cup mode). Only
    ///     available to Admin.
    /// </summary>
    /// <returns></returns>
    public async Task<bool> RestartMapAsync()
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("RestartMap")
        );
    }

    /// <summary>
    ///     Switch to next map, with an optional boolean parameter DontClearCupScores (only available in cup mode). Only
    ///     available to Admin.
    /// </summary>
    /// <returns></returns>
    public async Task<bool> NextMapAsync()
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("NextMap")
        );
    }

    /// <summary>
    ///     Attempt to balance teams. Only available to Admin.
    /// </summary>
    /// <returns></returns>
    public async Task<bool> AutoTeamBalanceAsync()
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("AutoTeamBalance")
        );
    }

    /// <summary>
    ///     Returns a struct containing the current game settings.
    /// </summary>
    /// <returns></returns>
    [Obsolete]
    public async Task<TmGameInfo> GetCurrentGameInfoAsync()
    {
        return (TmGameInfo) XmlRpcTypes.ToNativeValue<TmGameInfo>(
            await CallOrFaultAsync("GetCurrentGameInfo")
        );
    }

    /// <summary>
    ///     Returns a struct containing the game settings for the next map.
    /// </summary>
    /// <returns></returns>
    [Obsolete]
    public async Task<TmGameInfo> GetNextGameInfoAsync()
    {
        return (TmGameInfo) XmlRpcTypes.ToNativeValue<TmGameInfo>(
            await CallOrFaultAsync("GetNextGameInfo")
        );
    }

    /// <summary>
    ///     Returns a struct containing two other structures, the first containing the current game settings and the second the
    ///     game settings for next map. The first structure is named CurrentGameInfos and the second NextGameInfos.
    /// </summary>
    /// <returns></returns>
    [Obsolete]
    public async Task<TmCurrentNextValue<TmGameInfo>> GetGameInfosAsync()
    {
        return (TmCurrentNextValue<TmGameInfo>) XmlRpcTypes.ToNativeValue<TmCurrentNextValue<TmGameInfo>>(
            await CallOrFaultAsync("GetGameInfos")
        );
    }


    /// <summary>
    ///     Set whether to override the players preferences and always display all opponents). Only available to Admin.
    ///     Requires a map restart to be taken into account.
    /// </summary>
    /// <param name="playersPreferenceOverride">0=no override, 1=show all, other value=minimum number of opponents</param>
    /// <returns></returns>
    public async Task<bool> SetForceShowAllOpponentsAsync(int playersPreferenceOverride)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("SetForceShowAllOpponents", playersPreferenceOverride)
        );
    }


    /// <summary>
    ///     Set a new mode script name for script mode. Only available to Admin. Requires a map restart to be taken into
    ///     account.
    /// </summary>
    /// <param name="scriptName"></param>
    /// <returns></returns>
    public async Task<bool> SetScriptNameAsync(string scriptName)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("SetScriptName", scriptName)
        );
    }

    /// <summary>
    ///     Get the current and next mode script name for script mode. The struct returned contains two fields CurrentValue and
    ///     NextValue.
    /// </summary>
    /// <returns></returns>
    public async Task<TmCurrentNextValue<string>> GetScriptNameAsync()
    {
        return (TmCurrentNextValue<string>) XmlRpcTypes.ToNativeValue<TmCurrentNextValue<string>>(
            await CallOrFaultAsync("GetScriptName")
        );
    }


    /// <summary>
    ///     Get the current and next time limit for time attack mode. The struct returned contains two fields CurrentValue and
    ///     NextValue.
    /// </summary>
    /// <returns></returns>
    public async Task<TmCurrentNextValue<int>> GetTimeAttackLimitAsync()
    {
        return (TmCurrentNextValue<int>) XmlRpcTypes.ToNativeValue<TmCurrentNextValue<int>>(
            await CallOrFaultAsync("GetTimeAttackLimit")
        );
    }
}