# GbxRemote.Net
Trackmania dedicated server XML-RPC library for .NET Core

# Installation
The client is available on [Nuget](https://www.nuget.org/packages/GbxRemote.Net).
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