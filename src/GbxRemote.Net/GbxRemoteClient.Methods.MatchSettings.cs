using System.Threading.Tasks;
using GbxRemoteNet.XmlRpc;

namespace GbxRemoteNet;

/// <summary>
/// Method Category: Match Settings
/// </summary>
public partial class GbxRemoteClient
{
    public async Task<int> LoadMatchSettingsAsync(string filename)
    {
        return (int) XmlRpcTypes.ToNativeValue<int>(
            await CallOrFaultAsync("LoadMatchSettings", filename)
        );
    }
    
    public async Task<int> AppendPlaylistFromMatchSettingsAsync(string filename)
    {
        return (int) XmlRpcTypes.ToNativeValue<int>(
            await CallOrFaultAsync("AppendPlaylistFromMatchSettings", filename)
        );
    }

    public async Task<int> SaveMatchSettingsAsync(string filename)
    {
        return (int) XmlRpcTypes.ToNativeValue<int>(
            await CallOrFaultAsync("SaveMatchSettings", filename)
        );
    }
}