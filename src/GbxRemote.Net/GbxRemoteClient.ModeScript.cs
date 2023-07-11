using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GbxRemoteNet.Exceptions;
using GbxRemoteNet.Interfaces;
using GbxRemoteNet.XmlRpc;
using GbxRemoteNet.XmlRpc.Packets;
using GbxRemoteNet.XmlRpc.Types;
using Newtonsoft.Json.Linq;

namespace GbxRemoteNet;

public partial class GbxRemoteClient
{
    private readonly ConcurrentDictionary<string, (JObject, XmlRpcBaseType[])> _msResponses = new();
    private readonly ConcurrentDictionary<string, ManualResetEvent> _msSignals = new();

    private (JObject, XmlRpcBaseType[]) ParseModeScriptCallback(MethodCall call)
    {
        var dataArr = (XmlRpcArray) call.Arguments[1];
        var dataStr = (XmlRpcString) dataArr.Values[0];
        var responseData = JObject.Parse(dataStr.Value);
        var extraArgs = dataArr.Values[1..];
        return (responseData, extraArgs);
    }

    /// <summary>
    ///     Triggered when any ModeScript callback is sent from the server.
    /// </summary>
    public event IGbxRemoteClient.ModeScriptCallbackAction OnModeScriptCallback;

    /// <summary>
    ///     Handle modescript callbacks and parse them.
    /// </summary>
    /// <param name="call"></param>
    /// <returns></returns>
    private Task HandleModeScriptCallback(MethodCall call)
    {
        var (data, extraArgs) = ParseModeScriptCallback(call);

        if (data.ContainsKey("responseid"))
        {
            var responseId = data["responseid"].Value<string>();
            if (_msSignals.ContainsKey(responseId))
            {
                // we have a modescript callback response
                _msResponses[responseId] = (data, extraArgs);
                _msSignals[responseId].Set();

                if (!_options.InvokeEventOnModeScriptMethodResponse)
                    return Task.CompletedTask;
            }
        }

        // invoke the generic event
        OnModeScriptCallback?.Invoke(
            ((XmlRpcString) call.Arguments[0]).Value,
            data
        );

        return Task.CompletedTask;
    }

    /// <summary>
    ///     Call a ModeScript method and wait for the response.
    /// </summary>
    /// <param name="method">Name of the method.</param>
    /// <param name="args">Parameters to be passed with the method call.</param>
    /// <returns>Parsed JSON result from the method call.</returns>
    public async Task<(JObject, XmlRpcBaseType[])> GetModeScriptResponseAsync(string method, params string[] args)
    {
        var responseId = Guid.NewGuid().ToString();
        var passArgs = new List<string>(args);
        passArgs.Add(responseId);

        // send call
        _msSignals[responseId] = new ManualResetEvent(false);
        await TriggerModeScriptEventArrayAsync(method, passArgs.ToArray());

        // wait for response
        _msSignals[responseId].WaitOne();
        _msResponses.Remove(responseId, out var response);
        _msSignals.Remove(responseId, out _);

        return response;
    }

    /// <summary>
    ///     Call a ModeScript method and wait for the response and convert it to a native type.
    /// </summary>
    /// <param name="method">Name of the method.</param>
    /// <param name="args">Parameters to be passed with the method call.</param>
    /// <typeparam name="TResponse">Type of the response data.</typeparam>
    /// <returns>Parsed JSON result from the method call.</returns>
    /// <exception cref="InvalidOperationException"></exception>
    public async Task<TResponse> GetModeScriptResponseAsync<TResponse>(string method, params string[] args)
    {
        var (data, _) = await GetModeScriptResponseAsync(method, args);

        return data.ToObject<TResponse>();
    }

    /// <summary>
    ///     Call a ModeScript method and wait for the response and convert it to a native type.
    /// </summary>
    /// <param name="method">Name of the method.</param>
    /// <param name="args">Parameters to be passed with the method call.</param>
    /// <typeparam name="TResponse">Type of the response data.</typeparam>
    /// <typeparam name="TExtraArg">Type of the extra argument of the callback.</typeparam>
    /// <returns>Parsed JSON result from the method call.</returns>
    /// <exception cref="InvalidOperationException"></exception>
    public async Task<(TResponse, TExtraArg)> GetModeScriptResponseAsync<TResponse, TExtraArg>(string method,
        params string[] args)
    {
        var (data, extraArg) = await GetModeScriptResponseAsync(method, args);
        var dataNative = data.ToObject<TResponse>();

        if (extraArg.Length < 1)
            throw new XmlRpcResponseException("The response does not contain an extra argument.");

        var extraArgNative = XmlRpcTypes.ToNativeValue<TExtraArg>(extraArg[0]);

        return (dataNative, (TExtraArg) extraArgNative);
    }
}