using System.Collections.Generic;
using System.Threading.Tasks;
using GbxRemoteNet.Exceptions;
using GbxRemoteNet.XmlRpc;
using GbxRemoteNet.XmlRpc.ExtraTypes;
using GbxRemoteNet.XmlRpc.Types;
using Microsoft.Extensions.Logging;

namespace GbxRemoteNet;

/// <summary>
///     Method Category: System
/// </summary>
public partial class GbxRemoteClient
{
    /// <summary>
    ///     Return an array of all available XML-RPC methods on this server.
    /// </summary>
    /// <returns></returns>
    public async Task<string[]> SystemListMethodsAsync()
    {
        return (string[]) XmlRpcTypes.ToNativeValue<string>(
            await CallOrFaultAsync("system.listMethods")
        );
    }

    /// <summary>
    ///     Given the name of a method, return an array of legal signatures. Each signature is an array of strings. The first
    ///     item of each signature is the return type, and any others items are parameter types.
    /// </summary>
    /// <param name="method"></param>
    /// <returns></returns>
    public async Task<string[][]> SystemMethodSignatureAsync(string method)
    {
        return XmlRpcTypes.ToNative2DArray<string>((XmlRpcArray)
            await CallOrFaultAsync("system.methodSignature", method)
        );
    }

    /// <summary>
    ///     Given the name of a method, return a help string.
    /// </summary>
    /// <param name="method"></param>
    /// <returns></returns>
    public async Task<string> SystemMethodHelpAsync(string method)
    {
        return (string) XmlRpcTypes.ToNativeValue<string>(
            await CallOrFaultAsync("system.methodHelp", method)
        );
    }

    /// <summary>
    ///     Call multiple methods without multiple round-trip times.
    /// </summary>
    /// <param name="multicall">MultiCall object containing the calls to perform.</param>
    /// <returns>An array of results for each call.</returns>
    public async Task<object[]> MultiCallAsync(MultiCall multicall)
    {
        List<XmlRpcBaseType> calls = new();

        // build the call
        foreach (var call in multicall.MethodCalls)
        {
            var methodName = call.MethodName;
            if (methodName.EndsWith("Async"))
                methodName = methodName.Substring(0, methodName.Length - 5);

            var args = MethodArgs(call.Arguments);
            XmlRpcStruct callStruct = new(new Struct
            {
                {"methodName", new XmlRpcString(methodName)},
                {"params", new XmlRpcArray(args)}
            });

            calls.Add(callStruct);
        }

        // run the call
        XmlRpcArray multicallArgs = new(calls.ToArray());
        var msg = await CallAsync("system.multicall", multicallArgs);

        if (msg.IsFault)
        {
            _logger?.LogError("Multicall failed with reason: {Message}", (XmlRpcFault) msg.ResponseData);
            throw new XmlRpcFaultException((XmlRpcFault) msg.ResponseData);
        }

        // convert response to native values, should always be array if no error
        var results = (XmlRpcArray) msg.ResponseData;
        var converted = new object[results.Values.Length];

        for (var i = 0; i < converted.Length; i++)
            if (results.Values[i] is XmlRpcStruct)
            {
                // if struct we have a fault instead
                var faultStruct = (XmlRpcStruct) results.Values[i];

                converted[i] = new XmlRpcFault(
                    ((XmlRpcInteger) faultStruct.Fields["faultCode"]).Value,
                    ((XmlRpcString) faultStruct.Fields["faultString"]).Value
                );
            }
            else
            {
                // else normal resutl
                var resultArr = (XmlRpcArray) results.Values[i];

                if (resultArr.Values.Length == 0)
                    converted[i] = null;
                else
                    converted[i] = XmlRpcTypes.ToNativeValue<object>(resultArr.Values[0]);
            }

        return converted;
    }
}