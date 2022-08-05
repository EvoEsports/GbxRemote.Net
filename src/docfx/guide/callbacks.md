# Callbacks
You can also subscribe to callbacks from the server. To subscribe to any callback you can create a listener to the `OnAnyCallback` event:
```csharp
client.OnAnyCallback += Client_OnAnyCallback;

// ...

private static Task Client_OnAnyCallback(object sender, CallbackEventArgs<object> e) {
    // code to react on callback ...
    
    return Task.CompletedTask;
}
```

## Enable Callbacks
Before you can listen to callbacks, you must first tell the server to enable this. GbxRemote.NET provides a easy method that does this for you:
```csharp
await client.EnableCallbackTypeAsync();
```
The method can also enable different types of callbacks if you chose so by providing an argument.

## Listen to specific callbacks
Most callbacks are implemented as an event that you can listen to. This means you do not need to use `OnAnyCallback` to listen for specific callbacks. The library will handle the callback and parser the parameters automatically for you.

All the callbacks will follow the general event handler convention.

You can find them all [here](../api/GbxRemoteNet.GbxRemoteClient.html#events).

## Next Steps
[Learn to interact with the gamemode](modescript.md)
