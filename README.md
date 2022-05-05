![GbxRemote.NET](logo.png)
<div align="center">
    
[![Nuget](https://img.shields.io/nuget/v/GbxRemote.NET?style=flat-square)](https://www.nuget.org/packages/GbxRemote.Net)
[![GitHub](https://img.shields.io/github/license/EvoTM/GbxRemote.NET?style=flat-square)](./LICENSE)
![Lines of code](https://img.shields.io/tokei/lines/github/EvoTM/GbxRemote.NET?style=flat-square)
[![Discord](https://img.shields.io/discord/384138149686935562?label=Discord&style=flat-square)](https://discord.gg/4PKKesS)
    
</div>

A library for interacting with the [XML-RPC](http://xmlrpc.com/) protocol of [TrackMania](https://www.trackmania.com/) servers and similar titles built with [.NET 5](https://dotnet.microsoft.com/download). It is built using the [task async pattern (TAP)](https://docs.microsoft.com/en-us/dotnet/standard/asynchronous-programming-patterns/task-based-asynchronous-pattern-tap). It comes with pre-made methods for all the [documented XML-RPC methods](https://wiki.trackmania.io/en/dedicated-server/XML-RPC/Methods) provided by the trackmania server and allows you to easily hook into and react on callbacks. Interacting with [ModeScript](https://wiki.trackmania.io/en/dedicated-server/XML-RPC/Modescript-documentation) is also simplified through special features.

# Table of Contents
- [Table of Contents](#table-of-contents)
- [Installation](#installation)
- [User Guide](#user-guide)
  - [Creating an Async Context](#creating-an-async-context)
  - [Server Connection](#server-connection)
  - [Disconnecting](#disconnecting)
  - [Calling Methods](#calling-methods)
    - [Calling the main XML-RPC Methods](#calling-the-main-xml-rpc-methods)
    - [Some useful methods.](#some-useful-methods)
      - [Chat Methods](#chat-methods)
      - [Player List](#player-list)
      - [Kick/Ban/Blacklist/Ignore(mute) Players](#kickbanblacklistignoremute-players)
      - [Send chat messages](#send-chat-messages)
      - [Set Server Password](#set-server-password)
  - [Multicall](#multicall)
  - [Callbacks](#callbacks)
    - [Enable Callbacks](#enable-callbacks)
  - [ModeScript Functions](#modescript-functions)
    - [ModeScript Callbacks](#modescript-callbacks)
- [Documentation](#documentation)
- [Contributing](#contributing)

# Support
We give limited support for this library via our [Discord server](https://discord.gg/evotm) - if you have any questions on how to use this, please check out the User Guide below and the source code documentation first and foremost. If you're still stuck afterwards, feel free to ask in the #support channel on our Discord server.
Please do not message any of the maintainers privately about getting support for this library.

# Installation
The client library is available on [Nuget](https://www.nuget.org/packages/GbxRemote.Net).

**Install with Nuget Manager:**
```
Install-Package GbxRemote.Net
```
**Install with dotnet CLI:**
```
dotnet add package GbxRemote.Net
```

# User Guide
The following guide will give you an introduction on how to use the library. Make sure you also check out the [Examples](Examples/) for more complete examples on the usage.

## Creating an Async Context
The client uses the task async pattern, which means you will have to manage an async context. There are two ways for you to do this.

If your program just starts directly off in an async context, all you need to do is use the async main method:
```csharp
static async Task Main(string[] args) {
    // program code here ...

    await Task.Delay(-1); // wait forever
}
```

If you don't have an async context already from the start of the program, the other way is using the classic method with GetAwaiter:
```csharp
static async Task MyAsyncMethod() {
    // program code here ...
    
    await Task.Delay(-1); // wait forever
}

static void Main() {
    MyAsyncMethod().GetAwaiter().GetResult();
}
```

## Server Connection
Connection to a server is simple. Create an instance of `GbxRemoteClient` and call the `LoginAsync` method to connect and login automatically:
```csharp
// 5000 is the default XML-RPC port for TrackMania, change this as needed.
GbxRemoteClient client = new("<server address>", 5000);
await client.LoginAsync("SuperAdmin", "SuperAdmin")
```

## Disconnecting
To properly disconnect from the server you can call the `DisconnectAsync` method:
```csharp
await client.DisconnectAsync();
```
This will stop the recieve loop and dispose of the socket connection.

## Calling Methods
The client implements automatic parsing for most of the documented methods of GBXRemote. Therefore for most functions, you just need to call the class method implemented for a specific XML-RPC method.

### Calling the main XML-RPC Methods
Calling XML-RPC methods is super simple, just like calling a C# method. For example, let's say you want to get the chat lines.
```csharp
string[] chatLines = await client.GetChatLinesAsync();

foreach (string line in chatLines)
    Console.WriteLine(line);
```
Here the code calls the "GetChatLines" XML-RPC method and the client automatically parses the response into a native c# string array for you to simply print out.

Some methods returns structs of data. Let's say you wanted to get the player list:
```csharp
PlayerInfoStruct[] players = await client.GetPlayerListAsync();
```
This returns an array of structs containing information about players.

### Some useful methods.
Here are a few out of the hundres of available methods. For a complete list, either check library reference or click [here](https://wiki.trackmania.io/en/dedicated-server/XML-RPC/Methods).

#### Chat Methods
**Get chat:**
Returns the lines that appeared in the in-game chat.
```csharp
string[] GetChatLinesAsync()
```
---
#### Player List
Returns all players currently connected to the server.
```csharp
PlayerInfoStruct[] GetPlayerListAsync(int maxInfos=-1, int startIndex=0, int? serverType = -1)
```
The paramters are optional:
- `maxInfos`: Maximum number of players to return.
- `startIndex`: Start index in the list to begin the list.
- `serverType`: Usually not used, 0 = united, 1, 2 = forever

The `PlayerInfoStruct` contains the following fields:
```csharp
public class PlayerInfoStruct : PlayerStruct {
    public string NickName;
    public int PlayerId;
    public int TeamId;
    public int IsSpectator;
    public bool IsInOfficialMode;
    public int LadderRanking;
    public int SpectatorStatus;
    public int Flags;

    // Flags
    public int ForceSpectator;
    public bool IsReferee;
    public bool IsPodiumReady;
    public bool IsUsingStereoscopy;
    public bool IsManagedByAnOtherServer;
    public bool IsServer;
    public bool HasPlayerSlot;
    public bool IsBroadcasting;
    public bool HasJoinedGame;

    // SpectatorStatus
    public bool Spectator;
    public bool TemporarySpectator;
    public bool PureSpectator;
    public bool AutoTarget;
    public int CurrentTargetId;
}
```
---
#### Kick/Ban/Blacklist/Ignore(mute) Players
All the functions returns a boolean indicating whether the operation was successful or not.

**Kick Player:**
```csharp
bool KickAsync(string login, string message=null)
```
**Ban Player:**
```csharp
bool BanAsync(string login, string message=null)
```
**Unban Player:**
```csharp
bool UnBanAsync(string login)
```
**Blacklist Player:**
```csharp
bool BlackListAsync(string login)
```
**Remove from Blacklist:**
```csharp
bool UnBlackListAsync(string login)
```
**Ignore (mute) Player:**
```csharp
bool IgnoreAsync(string login)
```
**Unignore (unmute) Player:**
```csharp
bool UnIgnoreAsync(string login)
```
---
#### Send chat messages
There are two ways to send a chat message, with or without the server name.

**With server name**
```csharp
bool ChatSendAsync(string message)
```
**Without server name**
```csharp
bool ChatSendServerMessageAsync(string message)
```
---
#### Set Server Password
```csharp
bool SetServerPasswordAsync(string password)
```

## Multicall
Multicall is a technique to send several calls in the same payload and get back one response for all the calls. This can avoid round-trip times for each call when you need to do multiple smaller calls. Sometimes this saves saves several seconds. The client have implemented support for this. To perform a multicall you first need to create a multicall with the MultiCall builder:
```csharp
MultiCall multicall = new();

multicall.Add(client.GetChatLinesAsync)
         .Add("system.methodHelp", "SetApiVersion")
         .Add(nameof(client.GetVersionAsync))
         .Add("NonExistentMethod");
```
The multicall class supports the builder pattern and there are several ways to add a call. Arguments to the methods can be passed like normally in the `Add` method. Executing the multicall is done with the `MultiCallAsync` method:
```csharp
object[] results = await client.MultiCallAsync(multicall);
```
Due to the nature of XML-RPC in that it can return different value types, the `MultiCallAsync` returns an object array as each result. They are properly converted but it's up to the programmer to cast then to the correct type.

If a fault occured the result will be of type `XmlRpcFault` containing fault information.

## Callbacks
You can also subscribe to callbacks from the server. To subscribe to any callback you can create a listener to the `OnAnyCallback` event:
```csharp
client.OnAnyCallback += Client_OnAnyCallback;

// .
// . 
// .

private static Task Client_OnAnyCallback(MethodCall call, object[] pars) {
    // code to react on callback ...
    
    return Task.CompletedTask;
}
```
There are also implemented special events for some types of callbacks that you can listen to:
```csharp
Task OnPlayerConnect(string login, bool isSpectator);
Task OnPlayerDisconnect(string login, string reason);
Task OnPlayerChat(int playerUid, string login, string text, bool isRegisteredCmd);
Task OnEcho(string internalParam, string publicParam);
Task OnBeginMatch(SPlayerRanking[] rankings, int winnerTeam);
Task OnEndMatch(SPlayerRanking[] rankings, int winnerTeam);
Task OnBeginMap(SMapInfo map);
Task OnEndMap(SMapInfo map);
Task OnStatusChanged(int statusCode, string statusName);
Task OnPlayerInfoChanged(SPlayerInfo playerInfo);
```
These are documented and explained in the reference/code.

### Enable Callbacks
Before you can listen to callbacks, you must first tell the server to enable this. GbxRemote.NET provides a easy method that does this for you:
```csharp
await client.EnableCallbackTypeAsync();
```
The method can also enable different types of callbacks if you chose so by providing an argument.

## ModeScript Functions
ModeScript is a feature in GBXRemote to interact with the functionality of game modes and maniascript on the server. These functions have special callbacks and is called through the `TriggerModeScript*` methods. Due to way it is set up, you must call a ModeScript method through one of these methods and wait for the response in a callback. GbxRemote.NET tries to simplify this a bit by providing a method that can call a ModeScript method and wait for a response all in one:
```csharp
Task<JObject> GetModeScriptResponseAsync(string method, params string[] args)
```
ModeScript returns responses in JSON, so you will get a `JObject` back from [Json.NET](https://www.newtonsoft.com/json) which holds the response.
So for example, let's say you want to get a list of callbacks and print them:
```csharp
JObject ret = await client.GetModeScriptResponseAsync("XmlRpc.GetCallbacksList")

foreach (string callback in ret["callbacks"].Values<string>())
                Console.WriteLine($"- {callback}");
```

The complete ModeScript documentation can be found [here](https://wiki.trackmania.io/en/dedicated-server/XML-RPC/Modescript-documentation).

### ModeScript Callbacks
You can listen to any ModeScript callbacks by creating a listener for the `OnModeScriptCallback` event.

# Documentation
Library reference and full documentation is currently being worked on and will be released soon. In the meantime, all methods are documented in the code.

# Contributing
If you have any questions, issues, bugs or suggestions, don't hesitate create open an [Issue](https://github.com/EvoTM/GbxRemote.Net/issues/new)! You can also join our [Discord](https://discord.gg/4PKKesS) for questions.

You may also help with development by creating a pull request, _but please create a new branch first_.
