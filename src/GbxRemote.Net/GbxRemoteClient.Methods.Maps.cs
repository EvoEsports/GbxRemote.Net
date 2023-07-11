using System;
using System.Threading.Tasks;
using GbxRemoteNet.Structs;
using GbxRemoteNet.XmlRpc;

namespace GbxRemoteNet;

/// <summary>
/// Method Category: Maps
/// </summary>
public partial class GbxRemoteClient
{
    public async Task<int> GetCurrentMapIndexAsync()
    {
        return (int) XmlRpcTypes.ToNativeValue<int>(
            await CallOrFaultAsync("GetCurrentMapIndex")
        );
    }

    public async Task<int> GetNextMapIndexAsync()
    {
        return (int) XmlRpcTypes.ToNativeValue<int>(
            await CallOrFaultAsync("GetNextMapIndex")
        );
    }

    public async Task<bool> SetNextMapIndexAsync(int mapIndex)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("SetNextMapIndex")
        );
    }
    
    public async Task<bool> SetNextMapIdentAsync(string mapId)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("SetNextMapIdent", mapId)
        );
    }

    public async Task<bool> JumpToMapIndexAsync(int mapIndex)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("JumpToMapIndex")
        );
    }

    public async Task<bool> JumpToMapIdentAsync(string mapId)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("JumpToMapIdent", mapId)
        );
    }

    public async Task<TmMapInfo> GetCurrentMapInfoAsync()
    {
        return (TmMapInfo) XmlRpcTypes.ToNativeValue<TmMapInfo>(
            await CallOrFaultAsync("GetCurrentMapInfo")
        );
    }

    public async Task<TmMapInfo> GetNextMapInfoAsync()
    {
        return (TmMapInfo) XmlRpcTypes.ToNativeValue<TmMapInfo>(
            await CallOrFaultAsync("GetNextMapInfo")
        );
    }

    public async Task<TmMapInfo> GetMapInfoAsync(string filename)
    {
        return (TmMapInfo) XmlRpcTypes.ToNativeValue<TmMapInfo>(
            await CallOrFaultAsync("GetMapInfo", filename)
        );
    }

    public async Task<bool> CheckMapForCurrentServerParamsAsync(string filename)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("CheckMapForCurrentServerParams", filename)
        );
    }

    public async Task<TmMapInfo[]> GetMapListAsync(int maxInfos, int startIndex)
    {
        return (TmMapInfo[]) XmlRpcTypes.ToNativeValue<TmMapInfo>(
            await CallOrFaultAsync("GetMapList", maxInfos, startIndex)
        );
    }

    public async Task<bool> AddMapAsync(string filename)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("AddMap", filename)
        );
    }

    public async Task<int> AddMapListAsync(Array filenames)
    {
        return (int) XmlRpcTypes.ToNativeValue<int>(
            await CallOrFaultAsync("AddMapList", filenames)
        );
    }

    public async Task<bool> RemoveMapAsync(string filename)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("RemoveMap", filename)
        );
    }

    public async Task<int> RemoveMapListAsync(Array filenames)
    {
        return (int) XmlRpcTypes.ToNativeValue<int>(
            await CallOrFaultAsync("RemoveMapList", filenames)
        );
    }

    public async Task<bool> InsertMapAsync(string filename)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("InsertMap", filename)
        );
    }

    public async Task<int> InsertMapListAsync(Array filenames)
    {
        return (int) XmlRpcTypes.ToNativeValue<int>(
            await CallOrFaultAsync("InsertMapList", filenames)
        );
    }

    public async Task<bool> ChooseNextMapAsync(string filename)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("ChooseNextMap", filename)
        );
    }

    public async Task<int> ChooseNextMapListAsync(Array filenames)
    {
        return (int) XmlRpcTypes.ToNativeValue<int>(
            await CallOrFaultAsync("ChooseNextMapList", filenames)
        );
    }
}