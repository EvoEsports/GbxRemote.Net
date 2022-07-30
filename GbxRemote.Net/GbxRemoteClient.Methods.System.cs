using GbxRemoteNet.XmlRpc;
using GbxRemoteNet.XmlRpc.ExtraTypes;
using GbxRemoteNet.XmlRpc.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace GbxRemoteNet {
    /// <summary>
    /// Method Category: System
    /// </summary>
    public partial class GbxRemoteClient {
        /// <summary>
        /// Return an array of all available XML-RPC methods on this server.
        /// </summary>
        /// <returns></returns>
        public async Task<string[]> SystemListMethodsAsync() =>
            (string[])XmlRpcTypes.ToNativeValue<string>(
                await CallOrFaultAsync("system.listMethods")
            );

        /// <summary>
        /// Given the name of a method, return an array of legal signatures. Each signature is an array of strings. The first item of each signature is the return type, and any others items are parameter types.
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public async Task<string[][]> SystemMethodSignatureAsync(string method) =>
            XmlRpcTypes.ToNative2DArray<string>((XmlRpcArray)
                await CallOrFaultAsync("system.methodSignature", method)
            );

        /// <summary>
        /// Given the name of a method, return a help string.
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public async Task<string> SystemMethodHelpAsync(string method) =>
            (string)XmlRpcTypes.ToNativeValue<string>(
                await CallOrFaultAsync("system.methodHelp", method)
            );

        /// <summary>
        /// Call multiple methods without multiple round-trip times.
        /// </summary>
        /// <param name="multicall">MultiCall object containing the calls to perform.</param>
        /// <returns>An array of results for each call.</returns>
        public async Task<object[]> MultiCallAsync(MultiCall multicall) {
            List<XmlRpcBaseType> calls = new();

            // build the call
            foreach (var call in multicall.MethodCalls) {
                string methodName = call.MethodName;
                if (methodName.EndsWith("Async"))
                    methodName = methodName.Substring(0, methodName.Length - 5);

                XmlRpcBaseType[] args = MethodArgs(call.Arguments);
                XmlRpcStruct callStruct = new(new Struct() {
                    { "methodName", new XmlRpcString(methodName) },
                    { "params", new XmlRpcArray(args) }
                });

                calls.Add(callStruct);
            }

            // run the call
            XmlRpcArray multicallArgs = new(calls.ToArray());
            var msg = await CallAsync("system.multicall", multicallArgs);

            if (msg.IsFault) {
                _logger?.LogError("Multicall failed with reason: {Message}", (XmlRpcFault)msg.ResponseData);
                throw new XmlRpcFaultException((XmlRpcFault)msg.ResponseData);
            }

            // convert response to native values, should always be array if no error
            XmlRpcArray results = (XmlRpcArray)msg.ResponseData;
            object[] converted = new object[results.Values.Length];

            for (int i = 0; i < converted.Length; i++) {
                if (results.Values[i] is XmlRpcStruct) {
                    // if struct we have a fault instead
                    XmlRpcStruct faultStruct = (XmlRpcStruct)results.Values[i];

                    converted[i] = new XmlRpcFault(
                        ((XmlRpcInteger)faultStruct.Fields["faultCode"]).Value,
                        ((XmlRpcString)faultStruct.Fields["faultString"]).Value
                    );
                } else {
                    // else normal resutl
                    XmlRpcArray resultArr = (XmlRpcArray)results.Values[i];

                    if (resultArr.Values.Length == 0)
                        converted[i] = null;
                    else
                        converted[i] = XmlRpcTypes.ToNativeValue<object>(resultArr.Values[0]);
                }
            }

            return converted;
        }
    }
}
