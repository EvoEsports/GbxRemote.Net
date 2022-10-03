# Calling Methods
The client implements automatic parsing for most of the documented methods of GBXRemote. Therefore for most functions, you just need to call the class method implemented for a specific XML-RPC method.

## Calling the main XMLRPC Methods
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

## Method Documentation
There are a few hundreds of methods available through the XMLRPC protocol of the server. The library implements each and every single one for your convenience.

There are a few places where you can find a list of all the methods and documentation on how to use them and what they do:

| Place | Link |
|-------|------|
| **GbxRemote.Net Reference** | [Link](../api/GbxRemoteNet.GbxRemoteClient.html#methods) |
| **Trackmania Wiki on XMLRPC Methods** | [Link (external)](https://wiki.trackmania.io/en/dedicated-server/XML-RPC/Methods) |
| **GbxRemote.Net Source code** | [Link (external)](https://github.com/EvoTM/GbxRemote.Net) |

## Next Steps
[Optimize your calls by calling multiple methods at once](multicall.md)

[Handle the server calling you back](callbacks.md)
