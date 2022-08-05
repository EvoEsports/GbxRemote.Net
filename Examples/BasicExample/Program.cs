using System;
using System.Threading.Tasks;
using Examples.Common;
using GbxRemoteNet;
using Microsoft.Extensions.Logging;

namespace BasicExample;

internal class Program
{
    private static async Task Main(string[] args)
    {
        // create client instance
        GbxRemoteClient client = new("127.0.0.1", 5001, Logger.New<Program>(LogLevel.Debug));

        // connect and login
        if (!await client.LoginAsync("SuperAdmin", "SuperAdmin"))
        {
            Console.WriteLine("Failed to login.");
            return;
        }

        Console.WriteLine("Connected and authenticated!");

        // get player list
        var players = await client.GetPlayerListAsync();

        foreach (var player in players)
        {
            var info = await client.GetDetailedPlayerInfoAsync(player.Login);
            Console.WriteLine($"Login: {player.Login}, NickName: {player.NickName}");
        }

        // disconnect and clean up
        await client.DisconnectAsync();
    }
}