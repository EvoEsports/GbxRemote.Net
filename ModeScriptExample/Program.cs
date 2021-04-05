using GbxRemoteNet;
using System;
using System.Threading.Tasks;

namespace ModeScriptExample {
    class Program {
        static async Task MainAsync(string[] args) {
            // create client instance
            GbxRemoteClient client = new("192.168.1.57", 5001);

            // connect and login
            if (!await client.LoginAsync("SuperAdmin", "SuperAdmin")) {
                Console.WriteLine("Failed to login.");
                return;
            }

            Console.WriteLine("Connected and authenticated!");

            // enable callbacks
            await client.EnableCallbackTypeAsync();

            var ret = await client.GetModeScriptResponseAsync("XmlRpc.GetCallbacksList");
            Console.WriteLine(ret.ToString());

            // wait indefinitely
            await Task.Delay(-1);
        }

        static void Main(string[] args) {
            MainAsync(args).GetAwaiter().GetResult();
        }
    }
}
