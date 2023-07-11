using System;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using GbxRemoteNet.Exceptions;
using GbxRemoteNet.Interfaces.XmlRpc;
using GbxRemoteNet.XmlRpc.Packets;
using GbxRemoteNet.XmlRpc.Types;
using Microsoft.Extensions.Logging;

namespace GbxRemoteNet.XmlRpc;

public class NadeoXmlRpcClient : INadeoXmlRpcClient
{
    private readonly ILogger _logger;

    // connection
    private readonly string _connectHost;
    private readonly int _connectPort;

    private uint _handler = 0x80000000;
    private readonly object _handlerLock = new();
    private CancellationTokenSource _recvCancel;
    private readonly ConcurrentDictionary<uint, ManualResetEvent> _responseHandles = new();
    private readonly ConcurrentDictionary<uint, ResponseMessage> _responseMessages = new();

    // recvieve
    private Task _taskRecvLoop;
    private TcpClient _tcpClient;
    private XmlRpcIO _xmlRpcIo;

    public NadeoXmlRpcClient(string host, int port, ILogger logger = null)
    {
        _connectHost = host;
        _connectPort = port;

        _logger = logger;
    }

    public event INadeoXmlRpcClient.TaskAction OnConnected;

    public event INadeoXmlRpcClient.CallbackAction OnCallback;

    public event INadeoXmlRpcClient.TaskAction OnDisconnected;

    public async Task<bool> ConnectAsync(int retries = 0, int retryTimeout = 1000)
    {
        _logger?.LogDebug("Client connecting to the remote XML-RPC server");
        var connectAddr = await Dns.GetHostAddressesAsync(_connectHost);

        _tcpClient = new TcpClient();

        // try to connect
        while (retries >= 0)
        {
            try
            {
                await _tcpClient.ConnectAsync(connectAddr[0], _connectPort);

                if (_tcpClient.Connected)
                    break;
            }
            catch (Exception e)
            {
                _logger?.LogError("Exception occured when trying to connect to server: {Msg}", e.Message);
            }

            _logger?.LogError("Failed to connect to server");

            retries--;

            if (retries < 0)
                break;

            await Task.Delay(retryTimeout);
        }

        if (retries < 0)
            return false; // connection failed

        _xmlRpcIo = new XmlRpcIO(_tcpClient);

        _logger?.LogDebug("Client connected to XML-RPC server with IP: {Address}", connectAddr.ToString());

        // Cancellation token to cancel task if it takes longer than a second
        var cancellationTokenSource = new CancellationTokenSource();
        cancellationTokenSource.CancelAfter(1000);

        // check header
        ConnectHeader header = null;
        try
        {
            header = await ConnectHeader.FromIOAsync(_xmlRpcIo, cancellationTokenSource.Token);
        }
        catch (Exception e)
        {
            _logger?.LogError("Exception occured when trying to get connect header: {Msg}", e.Message);
            _logger?.LogError("Failed to get connect header");
            return false;
        }

        if (!header.IsValid)
        {
            _logger?.LogError("Client is using an invalid header protocol: {Protocol}", header.Protocol);
            throw new InvalidProtocolException(header.Protocol);
        }

        _recvCancel = new CancellationTokenSource();
        _taskRecvLoop = new Task(RecvLoop, _recvCancel.Token);
        _taskRecvLoop.Start();

        OnConnected?.Invoke();
        return true;
    }

    public async Task DisconnectAsync()
    {
        _logger?.LogDebug("Client is disconnecting from XML-RPC server");
        try
        {
            _recvCancel.Cancel();
            await _taskRecvLoop;
            _tcpClient.Close();
        }
        catch (Exception e)
        {
            _logger?.LogWarning("An exception occured when trying to disconnect: {Message}", e.Message);
        }

        OnDisconnected?.Invoke();

        _logger?.LogDebug("Client disconnected from XML-RPC server");
    }

    public async Task<uint> GetNextHandle()
    {
        // lock because we may access this in multiple threads
        lock (_handlerLock)
        {
            if (_handler + 1 == 0xffffffff)
                _handler = 0x80000000;

            _logger?.LogTrace("Next handler value: {Handler}", _handler);

            return _handler++;
        }
    }

    public async Task<ResponseMessage> CallAsync(string method, params XmlRpcBaseType[] args)
    {
        if (!_tcpClient.Connected)
        {
            throw new InvalidOperationException("Client is not connected. Failed to call remote XMLRPC method.");
        }
        
        var handle = await GetNextHandle();
        MethodCall call = new(method, handle, args);

        _logger?.LogTrace("Calling remote method: {Method}", method);
        _logger?.LogTrace("================== CALL START ==================");
        _logger?.LogTrace("{Xml}", call.Call.MainDocument.ToString());
        _logger?.LogTrace("================== CALL END ==================");

        _responseHandles[handle] = new ManualResetEvent(false);

        var data = await call.Serialize();
        await _xmlRpcIo.WriteBytesAsync(data);


        // wait for response
        _responseHandles[handle].WaitOne();
        var message = _responseMessages[handle];
        return message;
    }
    
    /// <summary>
    ///     Handles all responses from the XML-RPC server.
    /// </summary>
    private async void RecvLoop()
    {
        try
        {
            _logger?.LogDebug("Receive loop initiated");

            while (!_recvCancel.IsCancellationRequested)
            {
                var response = await ResponseMessage.FromIOAsync(_xmlRpcIo);

                _logger?.LogTrace("================== MESSAGE START ==================");
                _logger?.LogTrace("Message length: {Length}", response.Header.MessageLength);
                _logger?.LogTrace("Handle: {Handle}", response.Header.Handle);
                _logger?.LogTrace("Is callback: {IsCallback}", response.Header.IsCallback);
                _logger?.LogTrace("{Xml}", response.MessageXml.ToString());
                _logger?.LogTrace("================== MESSAGE END ==================");

                if (response.IsCallback)
                {
                    // invoke listeners and
                    // run callback handler in a new thread to avoid blocking of new responses
                    _ = Task.Run(() => OnCallback?.Invoke(new MethodCall(response)));
                }
                else if (_responseHandles.ContainsKey(response.Header.Handle))
                {
                    // attempt to signal the call method
                    _responseMessages[response.Header.Handle] = response;
                    _responseHandles[response.Header.Handle].Set();
                }
            }
        }
        catch (Exception e)
        {
            _logger?.LogError("Receive loop raised an exception: {Msg}", e.Message);
            await DisconnectAsync();
        }
    }
}