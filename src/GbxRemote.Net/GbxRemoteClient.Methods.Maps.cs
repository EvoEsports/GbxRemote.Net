using System;
using System.Threading.Tasks;
using GbxRemoteNet.Structs;
using GbxRemoteNet.XmlRpc;

namespace GbxRemoteNet;

/// <summary>
///     Method Category: Maps
/// </summary>
public partial class GbxRemoteClient
{
    /// <summary>
    ///     Returns the current map index in the selection, or -1 if the map is no longer in the selection.
    /// </summary>
    /// <returns></returns>
    public async Task<int> GetCurrentMapIndexAsync()
    {
        return (int) XmlRpcTypes.ToNativeValue<int>(
            await CallOrFaultAsync("GetCurrentMapIndex")
        );
    }

    /// <summary>
    ///     Returns the map index in the selection that will be played next (unless the current one is restarted...)
    /// </summary>
    /// <returns></returns>
    public async Task<int> GetNextMapIndexAsync()
    {
        return (int) XmlRpcTypes.ToNativeValue<int>(
            await CallOrFaultAsync("GetNextMapIndex")
        );
    }

    /// <summary>
    ///     Sets the map index in the selection that will be played next (unless the current one is restarted...)
    /// </summary>
    /// <param name="mapIndex"></param>
    /// <returns></returns>
    public async Task<bool> SetNextMapIndexAsync(int mapIndex)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("SetNextMapIndex")
        );
    }

    /// <summary>
    ///     Immediately jumps to the map designated by its identifier (it must be in the selection).
    /// </summary>
    /// <param name="mapId"></param>
    /// <returns></returns>
    public async Task<bool> SetNextMapIdentAsync(string mapId)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("SetNextMapIdent", mapId)
        );
    }

    /// <summary>
    ///     Immediately jumps to the map designated by the index in the selection.
    /// </summary>
    /// <param name="mapIndex"></param>
    /// <returns></returns>
    public async Task<bool> JumpToMapIndexAsync(int mapIndex)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("JumpToMapIndex")
        );
    }

    /// <summary>
    ///     Immediately jumps to the map designated by its identifier (it must be in the selection).
    /// </summary>
    /// <param name="mapId"></param>
    /// <returns></returns>
    public async Task<bool> JumpToMapIdentAsync(string mapId)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("JumpToMapIdent", mapId)
        );
    }

    /// <summary>
    ///     Returns a struct containing the infos for the current map. The struct contains the following fields : Name, UId,
    ///     FileName, Author, Environnement, Mood, BronzeTime, SilverTime, GoldTime, AuthorTime, CopperPrice, LapRace, MapType,
    ///     MapStyle.
    ///     (NbLaps and NbCheckpoints are also present but always set to -1)
    /// </summary>
    /// <returns></returns>
    public async Task<TmMapInfo> GetCurrentMapInfoAsync()
    {
        return (TmMapInfo) XmlRpcTypes.ToNativeValue<TmMapInfo>(
            await CallOrFaultAsync("GetCurrentMapInfo")
        );
    }

    /// <summary>
    ///     Returns a struct containing the infos for the next map. The struct contains the following fields : Name, UId,
    ///     FileName, Author, Environnement, Mood, BronzeTime, SilverTime, GoldTime, AuthorTime, CopperPrice, LapRace, MapType,
    ///     MapStyle.
    ///     (NbLaps and NbCheckpoints are also present but always set to -1)
    /// </summary>
    /// <returns></returns>
    public async Task<TmMapInfo> GetNextMapInfoAsync()
    {
        return (TmMapInfo) XmlRpcTypes.ToNativeValue<TmMapInfo>(
            await CallOrFaultAsync("GetNextMapInfo")
        );
    }

    /// <summary>
    ///     Returns a struct containing the infos for the map with the specified filename. The struct contains the following
    ///     fields : Name, UId, FileName, Author, Environnement, Mood, BronzeTime, SilverTime, GoldTime, AuthorTime,
    ///     CopperPrice, LapRace, MapType, MapStyle.
    ///     (NbLaps and NbCheckpoints are also present but always set to -1)
    /// </summary>
    /// <param name="filename"></param>
    /// <returns></returns>
    public async Task<TmMapInfo> GetMapInfoAsync(string filename)
    {
        return (TmMapInfo) XmlRpcTypes.ToNativeValue<TmMapInfo>(
            await CallOrFaultAsync("GetMapInfo", filename)
        );
    }

    /// <summary>
    ///     Returns a boolean if the map with the specified filename matches the current server settings.
    /// </summary>
    /// <param name="filename"></param>
    /// <returns></returns>
    public async Task<bool> CheckMapForCurrentServerParamsAsync(string filename)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("CheckMapForCurrentServerParams", filename)
        );
    }

    /// <summary>
    ///     Returns a list of maps among the current selection of the server. This method take two parameters. The first
    ///     parameter specifies the maximum number of infos to be returned, and the second one the starting index in the
    ///     selection.
    ///     The list is an array of structures. Each structure contains the following fields : Name, UId, FileName,
    ///     Environnement, Author, GoldTime, CopperPrice, MapType, MapStyle.
    /// </summary>
    /// <param name="maxInfos"></param>
    /// <param name="startIndex"></param>
    /// <returns></returns>
    public async Task<TmMapInfo[]> GetMapListAsync(int maxInfos, int startIndex)
    {
        return (TmMapInfo[]) XmlRpcTypes.ToNativeValue<TmMapInfo>(
            await CallOrFaultAsync("GetMapList", maxInfos, startIndex)
        );
    }

    /// <summary>
    ///     Add the map with the specified filename at the end of the current selection. Only available to Admin.
    /// </summary>
    /// <param name="filename"></param>
    /// <returns></returns>
    public async Task<bool> AddMapAsync(string filename)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("AddMap", filename)
        );
    }

    /// <summary>
    ///     Add the list of maps with the specified filenames at the end of the current selection. The list of maps to add is
    ///     an array of strings. Only available to Admin.
    /// </summary>
    /// <param name="filenames"></param>
    /// <returns></returns>
    public async Task<int> AddMapListAsync(Array filenames)
    {
        return (int) XmlRpcTypes.ToNativeValue<int>(
            await CallOrFaultAsync("AddMapList", filenames)
        );
    }

    /// <summary>
    ///     Remove the map with the specified filename from the current selection. Only available to Admin.
    /// </summary>
    /// <param name="filename"></param>
    /// <returns></returns>
    public async Task<bool> RemoveMapAsync(string filename)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("RemoveMap", filename)
        );
    }

    /// <summary>
    ///     Remove the list of maps with the specified filenames from the current selection. The list of maps to remove is an
    ///     array of strings. Only available to Admin.
    /// </summary>
    /// <param name="filenames"></param>
    /// <returns></returns>
    public async Task<int> RemoveMapListAsync(Array filenames)
    {
        return (int) XmlRpcTypes.ToNativeValue<int>(
            await CallOrFaultAsync("RemoveMapList", filenames)
        );
    }

    /// <summary>
    ///     Insert the map with the specified filename after the current map. Only available to Admin.
    /// </summary>
    /// <param name="filename"></param>
    /// <returns></returns>
    public async Task<bool> InsertMapAsync(string filename)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("InsertMap", filename)
        );
    }

    /// <summary>
    ///     Insert the list of maps with the specified filenames after the current map. The list of maps to insert is an array
    ///     of strings. Only available to Admin.
    /// </summary>
    /// <param name="filenames"></param>
    /// <returns></returns>
    public async Task<int> InsertMapListAsync(Array filenames)
    {
        return (int) XmlRpcTypes.ToNativeValue<int>(
            await CallOrFaultAsync("InsertMapList", filenames)
        );
    }

    /// <summary>
    ///     Set as next map the one with the specified filename, if it is present in the selection. Only available to Admin.
    /// </summary>
    /// <param name="filename"></param>
    /// <returns></returns>
    public async Task<bool> ChooseNextMapAsync(string filename)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("ChooseNextMap", filename)
        );
    }

    /// <summary>
    ///     Set as next maps the list of maps with the specified filenames, if they are present in the selection. The list of
    ///     maps to choose is an array of strings. Only available to Admin.
    /// </summary>
    /// <param name="filenames"></param>
    /// <returns></returns>
    public async Task<int> ChooseNextMapListAsync(Array filenames)
    {
        return (int) XmlRpcTypes.ToNativeValue<int>(
            await CallOrFaultAsync("ChooseNextMapList", filenames)
        );
    }
}