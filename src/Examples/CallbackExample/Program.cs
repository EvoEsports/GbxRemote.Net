using System;
using System.Threading;
using System.Threading.Tasks;
using Examples.Common;
using GbxRemoteNet;
using GbxRemoteNet.Events;
using GbxRemoteNet.Structs;
using GbxRemoteNet.XmlRpc.Packets;
using Microsoft.Extensions.Logging;

namespace CallbackExample;

internal class Program
{
    private static readonly CancellationTokenSource cancelToken = new();

    private static async Task Main(string[] args)
    {
        // create client instance
        GbxRemoteClient client = new("172.25.112.179", 5000, Logger.New<Program>(LogLevel.Debug));

        // connect and login
        if (!await client.LoginAsync("SuperAdmin", "SuperAdmin"))
        {
            Console.WriteLine("Failed to login.");
            return;
        }

        Console.WriteLine("Connected and authenticated!");

        // register callback events
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
        client.OnPlayerManialinkPageAnswer += ClientOnOnPlayerManialinkPageAnswer;
        client.OnMapListModified += ClientOnOnMapListModified;

        client.OnConnected += Client_OnConnected;
        client.OnDisconnected += Client_OnDisconnected;

        client.OnAnyCallback += Client_OnAnyCallback;

        // enable callbacks
        await client.EnableCallbackTypeAsync();

        // wait indefinitely or until disconnect
        WaitHandle.WaitAny(new[] {cancelToken.Token.WaitHandle});
    }

    private static Task ClientOnOnMapListModified(object sender, MapListModifiedEventArgs e)
    {
        Console.WriteLine("Map list modified.");
        return Task.CompletedTask;
    }

    private static Task ClientOnOnPlayerManialinkPageAnswer(object sender, ManiaLinkPageActionEventArgs e)
    {
        Console.WriteLine($"Player page answer: {e.PlayerId} | {e.Login}, Answer: {e.Answer}");
        return Task.CompletedTask;
    }

    private static Task Client_OnDisconnected()
    {
        Console.WriteLine("Client disconnected, exiting ...");
        cancelToken.Cancel();
        return Task.CompletedTask;
    }

    private static Task Client_OnConnected()
    {
        Console.WriteLine("Connected!");
        return Task.CompletedTask;
    }

    private static Task Client_OnPlayerInfoChanged(object sender, PlayerInfoChangedEventArgs e)
    {
        Console.WriteLine($"Player info changed for: {e.PlayerInfo.NickName}");
        return Task.CompletedTask;
    }

    private static Task Client_OnStatusChanged(object sender, StatusChangedEventArgs e)
    {
        Console.WriteLine($"[Status Changed] {e.StatusCode}: {e.StatusName}");
        return Task.CompletedTask;
    }

    private static Task Client_OnEndMap(object sender, MapEventArgs e)
    {
        Console.WriteLine($"End map: {e.Map.Name}");
        return Task.CompletedTask;
    }

    private static Task Client_OnBeginMap(object sender, MapEventArgs e)
    {
        Console.WriteLine($"Begin map: {e.Map.Name}");
        return Task.CompletedTask;
    }

    private static Task Client_OnEndMatch(object sender, EndMatchEventArgs e)
    {
        Console.WriteLine("Match ended, rankings:");
        foreach (var ranking in e.Rankings)
            Console.WriteLine($"- {ranking.Login}: {ranking.Rank}");

        return Task.CompletedTask;
    }

    private static Task Client_OnBeginMatch(object sender, EventArgs e)
    {
        Console.WriteLine("New match begun.");
        return Task.CompletedTask;
    }

    private static Task Client_OnEcho(object sender, EchoEventArgs e)
    {
        Console.WriteLine($"[Echo] internal: {e.InternalParam}, public: {e.InternalParam}");
        return Task.CompletedTask;
    }

    private static Task Client_OnPlayerChat(object sender, PlayerChatEventArgs e)
    {
        Console.WriteLine($"[Chat] {e.Login}: {e.Text}");
        return Task.CompletedTask;
    }

    private static Task Client_OnPlayerDisconnect(object sender, PlayerDisconnectEventArgs e)
    {
        Console.WriteLine($"Player disconnected: {e.Login}");
        return Task.CompletedTask;
    }

    private static Task Client_OnPlayerConnect(object sender, PlayerConnectEventArgs e)
    {
        Console.WriteLine($"Player connected: {e.Login}");
        return Task.CompletedTask;
    }

    private static Task Client_OnAnyCallback(object sender, CallbackEventArgs<object> e)
    {
        Console.WriteLine($"[Any callback] {e.Call.Method}:");
        foreach (var par in e.Parameters) Console.WriteLine($"- {par}");

        return Task.CompletedTask;
    }
}