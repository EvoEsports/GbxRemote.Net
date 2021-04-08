![GbxRemote.NET](logo.png)
<div align="center">
    
![Nuget](https://img.shields.io/nuget/v/GbxRemote.NET?style=flat-square)
![GitHub](https://img.shields.io/github/license/EvoTM/GbxRemote.NET?style=flat-square)
![Lines of code](https://img.shields.io/tokei/lines/github/EvoTM/GbxRemote.NET?style=flat-square)
[![Discord](https://img.shields.io/discord/384138149686935562?label=Discord&style=flat-square)](https://discord.gg/4PKKesS)
    
</div>

A library for interacting with the [XML-RPC](http://xmlrpc.com/) protocol of [TrackMania](https://www.trackmania.com/) servers and similar titles built with [.NET Core](https://dotnet.microsoft.com/download). It is built using the [async task pattern](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/async/task-asynchronous-programming-model). It comes with pre-made methods for all the [documented XML-RPC methods](https://wiki.trackmania.io/en/dedicated-server/XML-RPC/Methods) provided by the trackmania server and allows you to easily hook into and react on callbacks. Interacting with [ModeScript](https://wiki.trackmania.io/en/dedicated-server/XML-RPC/Modescript-documentation) is also simplified through special features.

# Installation
The client library is available on [Nuget](https://www.nuget.org/packages/GbxRemote.Net).

**Install with Nuget Manager:**
```
Install-Package GbxRemote.Net
```
**Install With dotnet CLI:**
```
dotnet add package GbxRemote.Net
```

# Usage
Here is a simple usage example:
```csharp
using GbxRemoteNet;
using System;
using System.Threading.Tasks;

namespace gbxremotetest {
    class Program {
        static async Task MainAsync() {
            // Create the instance
            GbxRemoteClient client = new("trackmania.test.server", 5001);

            // connect and login
            await client.LoginAsync("SuperAdmin", "SuperAdmin");

            // get the player list
            var players = await client.GetPlayerListAsync();

            Console.WriteLine("Player list:");
            foreach (var player in players) {
                Console.WriteLine($"- {player.NickName}");
            }

            // disconnect and clean up
            await client.DisconnectAsync();
        }

        static void Main(string[] args) {
            MainAsync().GetAwaiter().GetResult();
        }
    }
}
```

More examples can be found in [Examples](Examples/)
