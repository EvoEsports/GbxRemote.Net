using System;
using System.Threading.Tasks;
using GbxRemoteNet.Structs;
using GbxRemoteNet.XmlRpc;

namespace GbxRemoteNet;

/// <summary>
/// Method Category: Game Flow
/// </summary>
public partial class GbxRemoteClient
{
    public async Task<bool> RestartMapAsync()
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("RestartMap")
        );
    }

    public async Task<bool> NextMapAsync()
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("NextMap")
        );
    }

    public async Task<bool> AutoTeamBalanceAsync()
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("AutoTeamBalance")
        );
    }

    public async Task<TmGameInfo> GetCurrentGameInfoAsync()
    {
        return (TmGameInfo) XmlRpcTypes.ToNativeValue<TmGameInfo>(
            await CallOrFaultAsync("GetCurrentGameInfo")
        );
    }

    public async Task<TmGameInfo> GetNextGameInfoAsync()
    {
        return (TmGameInfo) XmlRpcTypes.ToNativeValue<TmGameInfo>(
            await CallOrFaultAsync("GetNextGameInfo")
        );
    }

    public async Task<TmCurrentNextValue<TmGameInfo>> GetGameInfosAsync()
    {
        return (TmCurrentNextValue<TmGameInfo>) XmlRpcTypes.ToNativeValue<TmCurrentNextValue<TmGameInfo>>(
            await CallOrFaultAsync("GetGameInfos")
        );
    }

    public async Task<bool> SetForceShowAllOpponentsAsync(int playersPreferenceOverride)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("SetForceShowAllOpponents", playersPreferenceOverride)
        );
    }

    public async Task<bool> SetScriptNameAsync(string scriptName)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("SetScriptName", scriptName)
        );
    }

    public async Task<TmCurrentNextValue<string>> GetScriptNameAsync()
    {
        return (TmCurrentNextValue<string>) XmlRpcTypes.ToNativeValue<TmCurrentNextValue<string>>(
            await CallOrFaultAsync("GetScriptName")
        );
    }

    public async Task<TmCurrentNextValue<int>> GetTimeAttackLimitAsync()
    {
        return (TmCurrentNextValue<int>) XmlRpcTypes.ToNativeValue<TmCurrentNextValue<int>>(
            await CallOrFaultAsync("GetTimeAttackLimit")
        );
    }
}