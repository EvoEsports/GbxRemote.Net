using GbxRemoteNet;
using GbxRemoteNet.Structs;
using GbxRemoteNet.XmlRpc;
using GbxRemoteNet.XmlRpc.ExtraTypes;
using GbxRemoteNet.XmlRpc.Packets;
using System;
using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BasicExample {
    class Program {
        static async Task Main(string[] args) {
            // create client instance
            GbxRemoteClient client = new("127.0.0.1", 5000);

            // connect and login
            if (!await client.LoginAsync("SuperAdmin", "SuperAdmin")) {
                Console.WriteLine("Failed to login.");
                return;
            }

            Console.WriteLine("Connected and authenticated!");

            // get player list
            var players = await client.GetPlayerListAsync();

            foreach (var player in players) {
                var info = await client.GetDetailedPlayerInfoAsync(player.Login);
                Console.WriteLine($"Login: {player.Login}, NickName: {player.NickName}");
            }

            // disconnect and clean up
            await client.DisconnectAsync();
        }
    }
}
