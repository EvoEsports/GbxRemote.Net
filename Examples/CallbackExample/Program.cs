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
        GbxRemoteClient client = new("127.0.0.1", 5001, Logger.New<Program>(LogLevel.Debug));

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

        client.AnyCallback += ClientOnAnyCallback;

        // enable callbacks
        await client.EnableCallbackTypeAsync();

        // wait indefinitely or until disconnect
        WaitHandle.WaitAny(new[] {cancelToken.Token.WaitHandle});
    }

    private static void ClientOnAnyCallback(object sender, CallbackEventArgs<object> e)
    {
        Console.WriteLine($"Callback received: {e.Call.Method}\nParameters:");
        foreach (var parameter in e.Parameters) Console.WriteLine($"- {parameter}");
    }

    private static Task ClientOnOnMapListModified(int curmapindex, int nextmapindex, bool islistmodified)
    {
        Console.WriteLine("Map list modified.");
        return Task.CompletedTask;
    }

    private static Task ClientOnOnPlayerManialinkPageAnswer(int playerUid, string login, string answer,
        TmSEntryVal[] entries)
    {
        Console.WriteLine($"Player page answer: {playerUid} | {login}, Answer: {answer}");
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

    private static Task Client_OnPlayerInfoChanged(TmSPlayerInfo playerInfo)
    {
        Console.WriteLine($"Player info changed for: {playerInfo.NickName}");
        return Task.CompletedTask;
    }

    private static Task Client_OnStatusChanged(int statusCode, string statusName)
    {
        Console.WriteLine($"[Status Changed] {statusCode}: {statusName}");
        return Task.CompletedTask;
    }

    private static Task Client_OnEndMap(TmSMapInfo map)
    {
        Console.WriteLine($"End map: {map.Name}");
        return Task.CompletedTask;
    }

    private static Task Client_OnBeginMap(TmSMapInfo map)
    {
        Console.WriteLine($"Begin map: {map.Name}");
        return Task.CompletedTask;
    }

    private static Task Client_OnEndMatch(TmSPlayerRanking[] rankings, int winnerTeam)
    {
        Console.WriteLine("Match ended, rankings:");
        foreach (var ranking in rankings)
            Console.WriteLine($"- {ranking.Login}: {ranking.Rank}");

        return Task.CompletedTask;
    }

    private static Task Client_OnBeginMatch()
    {
        Console.WriteLine("New match begun.");
        return Task.CompletedTask;
    }

    private static Task Client_OnEcho(string internalParam, string publicParam)
    {
        Console.WriteLine($"[Echo] internal: {internalParam}, public: {publicParam}");
        return Task.CompletedTask;
    }

    private static Task Client_OnPlayerChat(int playerUid, string login, string text, bool isRegisteredCmd)
    {
        Console.WriteLine($"[Chat] {login}: {text}");
        return Task.CompletedTask;
    }

    private static Task Client_OnPlayerDisconnect(string login, string reason)
    {
        Console.WriteLine($"Player disconnected: {login}");
        return Task.CompletedTask;
    }

    private static Task Client_OnPlayerConnect(string login, bool isSpectator)
    {
        Console.WriteLine($"Player connected: {login}");
        return Task.CompletedTask;
    }

    private static Task Client_OnAnyCallback(MethodCall call, object[] pars)
    {
        Console.WriteLine($"[Any callback] {call.Method}:");
        foreach (var par in pars) Console.WriteLine($"- {par}");

        return Task.CompletedTask;
    }
}