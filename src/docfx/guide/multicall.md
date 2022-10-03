# Multicall
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

## Next Steps
[Handle the server calling you back](callbacks.md)