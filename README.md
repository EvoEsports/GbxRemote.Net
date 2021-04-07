![GbxRemote.NET](logo.png)
![GitHub release (latest by date)](https://img.shields.io/github/v/release/EvoTM/GbxRemote.NET?style=flat-square)
![GitHub](https://img.shields.io/github/license/EvoTM/GbxRemote.NET?style=flat-square)
![Lines of code](https://img.shields.io/tokei/lines/github/EvoTM/GbxRemote.NET?style=flat-square)
![Discord](https://img.shields.io/discord/384138149686935562?style=flat-square)

A library for interacting with the XML-RPC protocol of TrackMania servers and similar titles built with .NET Core. It is built using the async task pattern. It comes with pre-made methods for all the documented XML-RPC methods provided by the trackmania server and allows you to easily hook into and react on callbacks. Interacting with ModeScript is also simplified through special features.

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