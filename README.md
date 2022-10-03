![GbxRemote.NET](logo.png)
<div align="center">
    
[![Nuget](https://img.shields.io/nuget/v/GbxRemote.NET?style=flat-square)](https://www.nuget.org/packages/GbxRemote.Net)
[![GitHub](https://img.shields.io/github/license/EvoTM/GbxRemote.NET?style=flat-square)](./LICENSE)
[![Discord](https://img.shields.io/discord/384138149686935562?label=Discord&style=flat-square)](https://discord.gg/EvoTM)
    
</div>

A library for interacting with the [XML-RPC](http://xmlrpc.com/) protocol of [TrackMania](https://www.trackmania.com/) servers and similar titles built with [.NET 5](https://dotnet.microsoft.com/download). It is built using the [task async pattern (TAP)](https://docs.microsoft.com/en-us/dotnet/standard/asynchronous-programming-patterns/task-based-asynchronous-pattern-tap). It comes with pre-made methods for all the [documented XML-RPC methods](https://wiki.trackmania.io/en/dedicated-server/XML-RPC/Methods) provided by the trackmania server and allows you to easily hook into and react on callbacks. Interacting with [ModeScript](https://wiki.trackmania.io/en/dedicated-server/XML-RPC/Modescript-documentation) is also simplified through special features.

# Quickstart
Install the library from [Nuget](https://www.nuget.org/packages/GbxRemote.Net): `dotnet add package GbxRemote.Net`

Make sure you have an updated version of the Trackmania Server installed and running.

The following basic program connects to the server host 127.0.0.1 and the port 5000 (default XMLRPC port). It retrieves a list of players currently on the server and prints them out to the console.
```cs
using System;
using GbxRemoteNet;

// create client instance
GbxRemoteClient client = new("127.0.0.2", 5000);

// connect and login
if (!await client.LoginAsync("SuperAdmin", "SuperAdmin"))
{
    Console.WriteLine("Failed to login.");
    return;
}

Console.WriteLine("Connected and authenticated!");

// get player list
var players = await client.GetPlayerListAsync();

// print player logins and nicknames to the console
foreach (var player in players)
{
    var info = await client.GetDetailedPlayerInfoAsync(player.Login);
    Console.WriteLine($"Login: {player.Login}, NickName: {player.NickName}");
}

// disconnect and clean up
await client.DisconnectAsync();
```

To learn more about the usage of this library, head over to the [main documentation](https://docs.evotm.com/).

# Documentation
The full documentation with API reference can be found [here](https://docs.evotm.com/).

# Support
We give limited support for this library via our [Discord server](https://discord.gg/evotm) - if you have any questions on how to use this, please check out the User Guide below and the source code documentation first and foremost. If you're still stuck afterwards, feel free to ask in the #support channel on our Discord server.
Please do not message any of the maintainers privately about getting support for this library.

# Contributing
If you have any questions, issues, bugs or suggestions, don't hesitate create open an [Issue](https://github.com/EvoTM/GbxRemote.Net/issues/new)! You can also join our [Discord](https://discord.gg/4PKKesS) for questions.

You may also help with development by creating a pull request, _but please create a new branch first_.
