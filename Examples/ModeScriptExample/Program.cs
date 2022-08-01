using GbxRemoteNet;
using GbxRemoteNet.Structs;
using GbxRemoteNet.XmlRpc;
using GbxRemoteNet.XmlRpc.ExtraTypes;
using GbxRemoteNet.XmlRpc.Types;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;
using Examples.Common;
using GbxRemoteNet.Structs.ModeScript;
using Microsoft.Extensions.Logging;

namespace ModeScriptExample {
    public class CallbacksList : ModeScriptResponse
    {
        public string[] Callbacks { get; set; }
    }
    
    class Program {
        // create client instance
        static GbxRemoteClient client = new("127.0.0.1", 5001, Logger.New<Program>(LogLevel.Debug));

        static async Task Main(string[] args) {
            // connect and login
            if (!await client.LoginAsync("SuperAdmin", "SuperAdmin")) {
                Console.WriteLine("Failed to login.");
                return;
            }

            Console.WriteLine("Connected and authenticated!");

            // register event that gets all modescript callbacks
            client.OnModeScriptCallback += Client_OnModeScriptCallback;

            // enable callbacks
            await client.EnableCallbackTypeAsync();

            // get all modescript callbacks
            var response = await client.GetModeScriptResponseAsync<CallbacksList>("XmlRpc.GetCallbacksList");
            Console.WriteLine("ModeScript Callbacks:");
            foreach (var callback in response.Callbacks)
            {
                Console.WriteLine($"- {callback}");
            }

            // wait indefinitely
            await Task.Delay(-1);
        }

        private static async Task Client_OnModeScriptCallback(string method, JObject data) {
            if (method == "Trackmania.Event.GiveUp") {
                string playerLogin = data["login"].Value<string>();
                var player = await client.GetPlayerInfoAsync(playerLogin);
                Console.WriteLine($"{player.NickName } gave up");
            }
        }
    }
}
