using System.Threading.Tasks;
using GbxRemoteNet.XmlRpc;

namespace GbxRemoteNet;

/// <summary>
/// Method Category: Session
/// </summary>
public partial class GbxRemoteClient
{
    public async Task<bool> AuthenticateAsync(string login, string password)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("Authenticate", login, password)
        );
    }

    public async Task<bool> ChangeAuthPasswordAsync(string login, string password)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("ChangeAuthPassword", login, password)
        );
    }

    public async Task<bool> EnableCallbacksAsync(bool enable)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("EnableCallbacks", enable)
        );
    }

    public async Task<bool> SetApiVersionAsync(string version)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("SetApiVersion", version)
        );
    }
}