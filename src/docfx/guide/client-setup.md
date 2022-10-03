# Client Setup & Connection

In order to connect to the server, a small setup is needed as you will need to create an async context from which you interact with the client.

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

## Next Steps
[Learn to call the XMLRPC methods](calling-methods.md)
