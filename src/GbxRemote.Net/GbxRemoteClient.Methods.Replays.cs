using System.Threading.Tasks;
using GbxRemoteNet.XmlRpc;
using GbxRemoteNet.XmlRpc.ExtraTypes;

namespace GbxRemoteNet;

/// <summary>
///     Method Category: Replays
/// </summary>
public partial class GbxRemoteClient
{
    /// <summary>
    ///     Enable the autosaving of all replays (vizualisable replays with all players, but not validable) on the server. Only
    ///     available to SuperAdmin.
    /// </summary>
    /// <param name="autoSave"></param>
    /// <returns></returns>
    public async Task<bool> AutoSaveReplaysAsync(string autoSave)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("AutoSaveReplays", autoSave)
        );
    }


    /// <summary>
    ///     Saves the current replay (vizualisable replays with all players, but not validable). Pass a filename, or '' for an
    ///     automatic filename. Only available to Admin.
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public async Task<bool> SaveCurrentReplayAsync(string fileName)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("SaveCurrentReplay", fileName)
        );
    }

    /// <summary>
    ///     Saves a replay with the ghost of all the players' best race. First parameter is the login of the player (or '' for
    ///     all players), Second parameter is the filename, or '' for an automatic filename. Only available to Admin.
    /// </summary>
    /// <param name="login"></param>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public async Task<bool> SaveBestGhostsReplayAsync(string login, string fileName)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("SaveBestGhostsReplay", login, fileName)
        );
    }

    /// <summary>
    ///     Returns a replay containing the data needed to validate the current best time of the player. The parameter is the
    ///     login of the player.
    /// </summary>
    /// <param name="login"></param>
    /// <returns></returns>
    public async Task<Base64> GetValidationReplayAsync(string login)
    {
        return (Base64) XmlRpcTypes.ToNativeValue<Base64>(
            await CallOrFaultAsync("GetValidationReplay", login)
        );
    }
}