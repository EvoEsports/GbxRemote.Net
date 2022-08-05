using System.Threading.Tasks;
using GbxRemoteNet.XmlRpc;

namespace GbxRemoteNet;

/// <summary>
///     Method Category: Session
/// </summary>
public partial class GbxRemoteClient
{
    /// <summary>
    ///     Allow user authentication by specifying a login and a password, to gain access to the set of functionalities
    ///     corresponding to this authorization level.
    /// </summary>
    /// <param name="login"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public async Task<bool> AuthenticateAsync(string login, string password)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("Authenticate", login, password)
        );
    }

    /// <summary>
    ///     Change the password for the specified login/user. Only available to SuperAdmin.
    /// </summary>
    /// <param name="login"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public async Task<bool> ChangeAuthPasswordAsync(string login, string password)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("ChangeAuthPassword", login, password)
        );
    }

    /// <summary>
    ///     Allow the GameServer to call you back.
    /// </summary>
    /// <returns></returns>
    public async Task<bool> EnableCallbacksAsync(bool enable)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("EnableCallbacks", enable)
        );
    }

    /// <summary>
    ///     Define the wanted api.
    /// </summary>
    /// <param name="version"></param>
    /// <returns></returns>
    public async Task<bool> SetApiVersionAsync(string version)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("SetApiVersion", version)
        );
    }
}