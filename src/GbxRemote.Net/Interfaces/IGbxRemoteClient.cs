using System.Threading.Tasks;
using GbxRemoteNet.XmlRpc.Packets;
using GbxRemoteNet.XmlRpc.Types;

namespace GbxRemoteNet.Interfaces;

public interface IGbxRemoteClient
{
    /// <summary>
    /// Call a remote method and throw an exception if a fault occured.
    /// </summary>
    /// <param name="method">Name of the method to call.</param>
    /// <param name="args">Arguments to pass to the method.</param>
    /// <returns></returns>
    public Task<XmlRpcBaseType> CallOrFaultAsync(string method, params object[] args);

    /// <summary>
    /// Call a remote method on the server and return the received message.
    /// </summary>
    /// <param name="method">Name of the method to call.</param>
    /// <param name="args">Arguments to pass to the method.</param>
    /// <returns></returns>
    public Task<ResponseMessage> CallMethodAsync(string method, params object[] args);

    /// <summary>
    /// Convert C# type values to XML-RPC type values and create an argument list.
    /// </summary>
    /// <param name="args">Arguments to convert.</param>
    /// <returns></returns>
    public XmlRpcBaseType[] MethodArgs(params object[] args);

    /// <summary>
    /// Connect and login to GBXRemote.
    /// </summary>
    /// <param name="login">Username to login with.</param>
    /// <param name="password">Password for the provided username.</param>
    /// <returns></returns>
    public Task<bool> LoginAsync(string login, string password);
}