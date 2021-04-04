using GbxRemoteNet;
using GbxRemoteNet.Enums;
using GbxRemoteNet.XmlRpc.Packets;
using GbxRemoteNet.XmlRpc.Types;
using System;
using System.Threading.Tasks;

namespace CallbackExample {
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

            client.OnCallback += Client_OnCallback;
            await client.EnableCallbackTypeAsync();

            // wait indefinitely
            await Task.Delay(-1);
        }

        private static Task Client_OnCallback(MethodCall call) {
            return Task.CompletedTask;
        }

        static void Main(string[] args) {
            MainAsync(args).GetAwaiter().GetResult();
        }
    }
}
