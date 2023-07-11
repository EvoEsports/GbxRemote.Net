using System.Threading.Tasks;
using GbxRemoteNet.XmlRpc.Packets;
using GbxRemoteNet.XmlRpc.Types;

namespace GbxRemoteNet.Interfaces.XmlRpc;

public interface INadeoXmlRpcClient
{
    /// <summary>
    ///     Action for the OnCallback event.
    /// </summary>
    /// <param name="call">Information about the call.</param>
    /// <returns></returns>
    public delegate Task CallbackAction(MethodCall call);
    
    /// <summary>
    ///     Generic action for events.
    /// </summary>
    /// <returns></returns>
    public delegate Task TaskAction();
    
    /// <summary>
    ///     Invoked when the client is connected to the XML-RPC server.
    /// </summary>
    public event TaskAction OnConnected;

    /// <summary>
    ///     Called when a callback occured from the XML-RPC server.
    /// </summary>
    public event CallbackAction OnCallback;

    /// <summary>
    ///     Triggered when the client has been disconnected from the server.
    /// </summary>
    public event TaskAction OnDisconnected;

    /// <summary>
    ///     Connect to the remote XMLRPC server.
    /// </summary>
    /// <param name="retries">Number of times to re-try connection.</param>
    /// <param name="retryTimeout">Number of milliseconds to wait between each re-try.</param>
    /// <returns></returns>
    public Task<bool> ConnectAsync(int retries = 0, int retryTimeout = 1000);

    /// <summary>
    ///     Stop the recieve loop and disconnect.
    /// </summary>
    /// <returns></returns>
    public Task DisconnectAsync();

    /// <summary>
    ///     Get the next handler value.
    /// </summary>
    /// <returns>The next handle value.</returns>
    public Task<uint> GetNextHandle();

    /// <summary>
    ///     Call a remote method.
    /// </summary>
    /// <param name="method">Method name</param>
    /// <param name="args">Arguments to the method if available.</param>
    /// <returns>Response returned by the call.</returns>
    public Task<ResponseMessage> CallAsync(string method, params XmlRpcBaseType[] args);
}