using GbxRemoteNet;
using GbxRemoteNet.Structs;
using GbxRemoteNet.XmlRpc;
using GbxRemoteNet.XmlRpc.ExtraTypes;
using GbxRemoteNet.XmlRpc.Types;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

namespace ModeScriptExample {
    class Program {
        // create client instance
        static GbxRemoteClient client = new("trackmania.test.server", 5001);

        static async Task MainAsync(string[] args) {
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
            var ret = await client.GetModeScriptResponseAsync("XmlRpc.GetCallbacksList");
            Console.WriteLine("ModeScript Callbacks:");
            foreach (string callback in ret["callbacks"].Values<string>())
                Console.WriteLine($"- {callback}");

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

        static void Main(string[] args) {
            MainAsync(args).GetAwaiter().GetResult();
        }
    }
}
