using GbxRemoteNet;
using GbxRemoteNet.Structs;
using GbxRemoteNet.XmlRpc.ExtraTypes;
using GbxRemoteNet.XmlRpc.Types;
using NLog.Config;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MulticallExample {
    class Program {
        static async Task MainAsync() {
            // create client instance
            GbxRemoteClient client = new("trackmania.test.server", 5001);

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

        static void Main(string[] args) {
            MainAsync().GetAwaiter().GetResult();
        }
    }
}
