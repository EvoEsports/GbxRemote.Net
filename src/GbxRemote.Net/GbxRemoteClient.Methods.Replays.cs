using System.Threading.Tasks;
using GbxRemoteNet.XmlRpc;
using GbxRemoteNet.XmlRpc.ExtraTypes;

namespace GbxRemoteNet;

/// <summary>
/// Method Category: Replays
/// </summary>
public partial class GbxRemoteClient
{
    public async Task<bool> AutoSaveReplaysAsync(string autoSave)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("AutoSaveReplays", autoSave)
        );
    }

    public async Task<bool> SaveCurrentReplayAsync(string fileName)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("SaveCurrentReplay", fileName)
        );
    }

    public async Task<bool> SaveBestGhostsReplayAsync(string login, string fileName)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("SaveBestGhostsReplay", login, fileName)
        );
    }
    
    public async Task<GbxBase64> GetValidationReplayAsync(string login)
    {
        return (GbxBase64) XmlRpcTypes.ToNativeValue<GbxBase64>(
            await CallOrFaultAsync("GetValidationReplay", login)
        );
    }
}