# ModeScript
ModeScript is a feature in GBXRemote to interact with the functionality of game modes and maniascript on the server. These functions have special callbacks and is called through the `TriggerModeScript*` methods. Due to way it is set up, you must call a ModeScript method through one of these methods and wait for the response in a callback. GbxRemote.NET tries to simplify this a bit by providing a method that can call a ModeScript method and wait for a response all in one:
```csharp
Task<JObject> GetModeScriptResponseAsync(string method, params string[] args)
```
ModeScript returns responses in JSON, so you will get a `JObject` back from [Json.NET](https://www.newtonsoft.com/json) which holds the response.
So for example, let's say you want to get a list of callbacks and print them:
```csharp
JObject (ret, _) = await client.GetModeScriptResponseAsync("XmlRpc.GetCallbacksList")

foreach (string callback in ret["callbacks"].Values<string>())
                Console.WriteLine($"- {callback}");
```

The complete ModeScript documentation can be found [here](https://wiki.trackmania.io/en/dedicated-server/XML-RPC/Modescript-documentation).

## Convert to native type
You can convert the modescript response to any .NET type that supports JSON conversion. When you create a type, we recommend to inherit the `ModeScriptResponse` class.
For example if you want to get all callbacks of the current ModeScript, we can start creating a class called ModeScriptCallbacks:
```csharp
public class CallbacksList : ModeScriptResponse
{
    public string[] Callbacks { get; set; }
}
```

You can now use the an overload of `GetModeScriptResponseAsync` to get the response into an object of `CallbacksList`:
```csharp
var response = await client.GetModeScriptResponseAsync<CallbacksList>("XmlRpc.GetCallbacksList");
Console.WriteLine("ModeScript Callbacks:");
foreach (var callback in response.Callbacks)
{
    Console.WriteLine($"- {callback}");
}
```

## Extra response arguments
Some ModeScript methods returns multiple arguments in their callback. For example the `XmlRpc.GetDocumentation` returns the
argument which contains the response id and in addition a string that contains the documentation itself. The first argument
is always a XMLRPC string which contains JSON encoded data, while the other ones can be any XMLRPC argument type.

So for exampel for the method `XmlRpc.GetDocumentation` we can use an overload of `GetModeScriptResponseAsync` to get both 
arguments and convert them to a native object:
```csharp
var (response, documentation) = await client.GetModeScriptResponseAsync<ModeScriptResponse, string>("XmlRpc.GetDocumentation");
Console.WriteLine(documentation);
```

## ModeScript Callbacks
You can listen to any ModeScript callbacks by creating a listener for the `OnModeScriptCallback` event.

## Next Steps
[Find extensions to the library](extensions.md)
