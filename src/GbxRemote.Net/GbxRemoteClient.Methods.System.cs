using System.Collections.Generic;
using System.Threading.Tasks;
using GbxRemoteNet.Exceptions;
using GbxRemoteNet.XmlRpc;
using GbxRemoteNet.XmlRpc.ExtraTypes;
using GbxRemoteNet.XmlRpc.Types;
using Microsoft.Extensions.Logging;

namespace GbxRemoteNet;

/// <summary>
/// Method Category: System
/// </summary>
public partial class GbxRemoteClient
{
    public async Task<string[]> SystemListMethodsAsync()
    {
        return (string[]) XmlRpcTypes.ToNativeValue<string>(
            await CallOrFaultAsync("system.listMethods")
        );
    }

    public async Task<string[][]> SystemMethodSignatureAsync(string method)
    {
        return XmlRpcTypes.ToNative2DArray<string>((XmlRpcArray)
            await CallOrFaultAsync("system.methodSignature", method)
        );
    }

    public async Task<string> SystemMethodHelpAsync(string method)
    {
        return (string) XmlRpcTypes.ToNativeValue<string>(
            await CallOrFaultAsync("system.methodHelp", method)
        );
    }

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
            XmlRpcStruct callStruct = new(new GbxStruct
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
                // else normal result
                var resultArr = (XmlRpcArray) results.Values[i];

                if (resultArr.Values.Length == 0)
                    converted[i] = null;
                else
                    converted[i] = XmlRpcTypes.ToNativeValue<object>(resultArr.Values[0]);
            }

        return converted;
    }
}