using GbxRemoteNet;
using GbxRemoteNet.Structs;
using GbxRemoteNet.XmlRpc.ExtraTypes;
using GbxRemoteNet.XmlRpc.Types;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Examples.Common;
using Microsoft.Extensions.Logging;

namespace MulticallExample {
    class Program {
        static async Task Main() {
            // create client instance
            GbxRemoteClient client = new("127.0.0.1", 5001, Logger.New<Program>(LogLevel.Debug));

            // connect and login
            if (!await client.LoginAsync("SuperAdmin", "SuperAdmin")) {
                Console.WriteLine("Failed to login.");
                return;
            }

            Console.WriteLine("Connected and authenticated!");

            // build the multicall
            MultiCall multicall = new();
            multicall.Add(client.GetChatLinesAsync)
                     .Add("system.methodHelp", "SetApiVersion")
                     .Add(nameof(client.GetVersionAsync))
                     .Add("NonExistentMethod");

            // send all the calls
            object[] results = await client.MultiCallAsync(multicall);

            foreach (var result in results) {
                if (result is XmlRpcFault) {
                    var fault = (XmlRpcFault)result;
                    Console.WriteLine($"Method call failed: ({fault.FaultCode}) {fault.FaultString}");
                } else {
                    Console.WriteLine($"Method Result: {result}");
                }
            }

            // disconnect and clean up
            await client.DisconnectAsync();
        }
    }
}
