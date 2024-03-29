﻿using System;
using System.Threading.Tasks;
using Examples.Common;
using GbxRemoteNet;
using GbxRemoteNet.Structs.ModeScript;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace ModeScriptExample;

public class CallbacksList : TmModeScriptResponse
{
    public string[] Callbacks { get; set; }
}

internal class Program
{
    // create client instance
    private static readonly GbxRemoteClient client = new("127.0.0.1", 5001, Logger.New<Program>(LogLevel.Debug));

    private static async Task Main(string[] args)
    {
        // connect and login
        if (!await client.LoginAsync("SuperAdmin", "SuperAdmin"))
        {
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
        foreach (var callback in response.Callbacks) Console.WriteLine($"- {callback}");

        // wait indefinitely
        await Task.Delay(-1);
    }

    private static async Task Client_OnModeScriptCallback(string method, JObject data)
    {
        if (method == "Trackmania.Event.GiveUp")
        {
            var playerLogin = data["login"].Value<string>();
            var player = await client.GetPlayerInfoAsync(playerLogin);
            Console.WriteLine($"{player.NickName} gave up");
        }
    }
}