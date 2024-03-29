﻿using System.Linq;
using System.Threading.Tasks;
using GbxRemoteNet.Exceptions;
using GbxRemoteNet.Interfaces;
using GbxRemoteNet.Interfaces.XmlRpc;
using GbxRemoteNet.XmlRpc;
using GbxRemoteNet.XmlRpc.Packets;
using GbxRemoteNet.XmlRpc.Types;
using Microsoft.Extensions.Logging;

namespace GbxRemoteNet;

/// <summary>
///     GBXRemote client for connecting to and managing TrackMania servers through XML-RPC.
/// </summary>
public partial class GbxRemoteClient : NadeoXmlRpcClient, IGbxRemoteClient
{
    /// <summary>
    ///     This is the API version the client will be using.
    /// </summary>
    public const string DefaultApiVersion = "2023-03-24";

    private readonly ILogger _logger;
    private readonly GbxRemoteClientOptions _options;

    /// <summary>
    ///     Create a new instance of the GBXRemote client.
    /// </summary>
    /// <param name="host">The address to the TrackMania server. Default: 127.0.0.1</param>
    /// <param name="port">The port the XML-RPC server is listening to on your TrackMania server. Default: 5000</param>
    /// <param name="logger">Logger to use.</param>
    public GbxRemoteClient(string host, int port, ILogger logger = null) : base(host, port, logger)
    {
        OnCallback += GbxRemoteClient_OnCallback;
        _options = new GbxRemoteClientOptions();

        _logger = logger;
    }

    /// <summary>
    ///     Create a new instance of the GBXRemote client.
    /// </summary>
    /// <param name="host">The address to the TrackMania server. Default: 127.0.0.1</param>
    /// <param name="port">The port the XML-RPC server is listening to on your TrackMania server. Default: 5000</param>
    /// <param name="options">Options for the Gbx client.</param>
    /// <param name="logger">Logger to use.</param>
    public GbxRemoteClient(string host, int port, GbxRemoteClientOptions options, ILogger logger = null) : base(host,
        port)
    {
        OnCallback += GbxRemoteClient_OnCallback;
        _options = options;

        _logger = logger;
    }

    public async Task<XmlRpcBaseType> CallOrFaultAsync(string method, params object[] args)
    {
        var msg = await CallAsync(method, MethodArgs(args));

        if (msg.IsFault)
        {
            _logger?.LogError("Remote call failed with reason: {Message}", (XmlRpcFault) msg.ResponseData);
            throw new XmlRpcFaultException((XmlRpcFault) msg.ResponseData);
        }

        return msg.ResponseData;
    }

    public async Task<ResponseMessage> CallMethodAsync(string method, params object[] args)
    {
        return await CallAsync(method, MethodArgs(args));
    }
    
    public XmlRpcBaseType[] MethodArgs(params object[] args)
    {
        _logger?.LogTrace("Converting C# types to XML-RPC. Types to convert: {List}",
            string.Join(", ", args.Select(a => a?.GetType().ToString())));
        
        var xmlRpcArgs = new XmlRpcBaseType[args.Length];

        for (var i = 0; i < args.Length; i++)
            xmlRpcArgs[i] = XmlRpcTypes.ToXmlRpcValue(args[i]);

        return xmlRpcArgs;
    }

    public async Task<bool> LoginAsync(string login, string password)
    {
        _logger?.LogDebug("Client connecting to GbxRemote");

        if (!await ConnectAsync(_options.ConnectionRetries, _options.ConnectionRetryTimeout))
            return false;

        await SetApiVersionAsync(DefaultApiVersion);

        if (await AuthenticateAsync(login, password))
        {
            _logger?.LogDebug("Client connected to GbxRemote");
            return true;
        }

        // disconnect if login failed
        await DisconnectAsync();
        _logger?.LogError("Client failed to connect to GbxRemote");
        return false;
    }
}