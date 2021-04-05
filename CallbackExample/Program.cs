using GbxRemoteNet;
using GbxRemoteNet.Enums;
using GbxRemoteNet.XmlRpc.Packets;
using GbxRemoteNet.XmlRpc.Types;
using System;
using System.Dynamic;
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

            client.OnPlayerConnect += Client_OnPlayerConnect;
            client.OnPlayerDisconnect += Client_OnPlayerDisconnect;
            client.OnPlayerChat += Client_OnPlayerChat;
            client.OnEcho += Client_OnEcho;
            client.OnBeginMatch += Client_OnBeginMatch;
            client.OnEndMatch += Client_OnEndMatch;
            client.OnBeginMap += Client_OnBeginMap;
            client.OnEndMap += Client_OnEndMap;
            client.OnStatusChanged += Client_OnStatusChanged;
            client.OnPlayerInfoChanged += Client_OnPlayerInfoChanged;

            await client.EnableCallbackTypeAsync();

            // wait indefinitely
            await Task.Delay(-1);
        }

        private static Task Client_OnPlayerInfoChanged(GbxRemoteNet.Structs.SPlayerInfo playerInfo) {
            Console.WriteLine($"Player info changed for: {playerInfo.NickName}");
            return Task.CompletedTask;
        }

        private static Task Client_OnStatusChanged(int statusCode, string statusName) {
            Console.WriteLine($"[Status Changed] {statusCode}: {statusName}");
            return Task.CompletedTask;
        }

        private static Task Client_OnEndMap(GbxRemoteNet.Structs.SMapInfo map) {
            Console.WriteLine($"End map: {map.Name}");
            return Task.CompletedTask;
        }

        private static Task Client_OnBeginMap(GbxRemoteNet.Structs.SMapInfo map) {
            Console.WriteLine($"Begin map: {map.Name}");
            return Task.CompletedTask;
        }

        private static Task Client_OnEndMatch(GbxRemoteNet.Structs.SPlayerRanking[] rankings, int winnerTeam) {
            Console.WriteLine("Match ended, rankings:");
            foreach (var ranking in rankings)
                Console.WriteLine($"- {ranking.Login}: {ranking.Rank}");

            return Task.CompletedTask;
        }

        private static Task Client_OnBeginMatch() {
            Console.WriteLine("New match begun.");
            return Task.CompletedTask;
        }

        private static Task Client_OnEcho(string internalParam, string publicParam) {
            Console.WriteLine($"[Echo] internal: {internalParam}, public: {publicParam}");
            return Task.CompletedTask;
        }

        private static Task Client_OnPlayerChat(int playerUid, string login, string text, bool isRegisteredCmd) {
            Console.WriteLine($"[Chat] {login}: {text}");
            return Task.CompletedTask;
        }

        private static Task Client_OnPlayerDisconnect(string login, string reason) {
            Console.WriteLine($"Player disconnected: {login}");
            return Task.CompletedTask;
        }

        private static Task Client_OnPlayerConnect(string login, bool isSpectator) {
            Console.WriteLine($"Player connected: {login}");
            return Task.CompletedTask;
        }

        private static Task Client_OnAnyCallback(MethodCall call, object[] pars) {
            Console.WriteLine($"[Any callback] {call.Method}:");
            foreach (var par in pars) {
                Console.WriteLine($"- {par}");
            }

            return Task.CompletedTask;
        }

        static void Main(string[] args) {
            MainAsync(args).GetAwaiter().GetResult();
        }
    }
}
