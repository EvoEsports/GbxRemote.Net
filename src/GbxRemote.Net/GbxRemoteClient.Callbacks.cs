using System;
using System.Threading.Tasks;
using GbxRemoteNet.Enums;
using GbxRemoteNet.Events;
using GbxRemoteNet.Structs;
using GbxRemoteNet.XmlRpc;
using GbxRemoteNet.XmlRpc.ExtraTypes;
using GbxRemoteNet.XmlRpc.Packets;
using GbxRemoteNet.XmlRpc.Types;

namespace GbxRemoteNet;

public partial class GbxRemoteClient
{
    public delegate Task AsyncEventHandler(object sender, EventArgs e);

    public delegate Task AsyncEventHandler<TArgs>(object sender, TArgs e);
    
    /// <summary>
    ///     Triggered for all possible callbacks.
    /// </summary>
    public event AsyncEventHandler<CallbackEventArgs<object>> OnAnyCallback;

    /// <summary>
    ///     When a player connects to the server.
    /// </summary>
    public event AsyncEventHandler<PlayerConnectEventArgs> OnPlayerConnect;

    /// <summary>
    ///     When a player disconnects from the server.
    /// </summary>
    public event AsyncEventHandler<PlayerDisconnectEventArgs> OnPlayerDisconnect;

    /// <summary>
    ///     When a player sends a chat message.
    /// </summary>
    public event AsyncEventHandler<PlayerChatEventArgs> OnPlayerChat;

    /// <summary>
    ///     When a echo message is sent. Can be used for communication with other.
    ///     XMLRPC-clients.
    /// </summary>
    public event AsyncEventHandler<EchoEventArgs> OnEcho;

    /// <summary>
    ///     When the match itself starts, triggered after begin map.
    /// </summary>
    public event AsyncEventHandler OnBeginMatch;

    /// <summary>
    ///     When the match ends, does not give a lot of info in TM2020.
    /// </summary>
    public event AsyncEventHandler<EndMatchEventArgs> OnEndMatch;

    /// <summary>
    ///     When the map has loaded on the server.
    /// </summary>
    public event AsyncEventHandler<MapEventArgs> OnBeginMap;

    /// <summary>
    ///     When the map unloads from the server.
    /// </summary>
    public event AsyncEventHandler<MapEventArgs> OnEndMap;

    /// <summary>
    ///     When the server status changed.
    /// </summary>
    public event AsyncEventHandler<StatusChangedEventArgs> OnStatusChanged;

    /// <summary>
    ///     When data about a player changed, it is usually called when
    ///     a player joins or leaves. Gives you more detailed info about a player.
    /// </summary>
    public event AsyncEventHandler<PlayerInfoChangedEventArgs> OnPlayerInfoChanged;

    /// <summary>
    ///     When a user triggers the page answer callback from a manialink.
    /// </summary>
    public event AsyncEventHandler<ManiaLinkPageActionEventArgs> OnPlayerManialinkPageAnswer;

    /// <summary>
    ///     Triggered when the map list changed.
    /// </summary>
    public event AsyncEventHandler<MapListModifiedEventArgs> OnMapListModified;

    /// <summary>
    ///     When the server is about to start.
    /// </summary>
    public event AsyncEventHandler OnServerStart;

    /// <summary>
    ///     When the server is about to stop.
    /// </summary>
    public event AsyncEventHandler OnServerStop;

    /// <summary>
    ///     Tunnel data received from a player.
    /// </summary>
    public event AsyncEventHandler<TunnelDataEventArgs> OnTunnelDataReceived;

    /// <summary>
    ///     When a current vote has been updated.
    /// </summary>
    public event AsyncEventHandler<VoteUpdatedEventArgs> OnVoteUpdated;

    /// <summary>
    ///     When a player bill is updated.
    /// </summary>
    public event AsyncEventHandler<BillUpdatedEventArgs> OnBillUpdated;

    /// <summary>
    ///     When a player changed allies.
    /// </summary>
    public event AsyncEventHandler<PlayerEventArgs> OnPlayerAlliesChanged;

    /// <summary>
    ///     When a variable from the script cloud is loaded.
    /// </summary>
    public event AsyncEventHandler<ScriptCloudEventArgs> OnScriptCloudLoadData;

    /// <summary>
    ///     When a variable from the script cloud is saved.
    /// </summary>
    public event AsyncEventHandler<ScriptCloudEventArgs> OnScriptCloudSaveData;

    /// <summary>
    ///     Enable callbacks. If no parameter is provided,
    ///     all callbacks are enabled by default.
    /// </summary>
    /// <param name="callbackType"></param>
    /// <returns></returns>
    public async Task EnableCallbackTypeAsync(
        CallbackType callbackType = CallbackType.Internal | CallbackType.ModeScript | CallbackType.Checkpoints)
    {
        if (callbackType.HasFlag(CallbackType.Internal))
            await EnableCallbacksAsync(true);
        if (callbackType.HasFlag(CallbackType.ModeScript))
            await TriggerModeScriptEventArrayAsync("XmlRpc.EnableCallbacks", "true");
        if (callbackType.HasFlag(CallbackType.Checkpoints))
            await TriggerModeScriptEventArrayAsync("Trackmania.Event.SetCurLapCheckpointsMode", "always");
    }

    private async Task InternalInvokeEventsAsync(Delegate[]? invocationList, EventArgs e)
    {
        if (invocationList == null)
        {
            return;
        }
        
        foreach (var del in invocationList)
        {
            await ((Task)del.DynamicInvoke(this, e))!;
        }
    }
    
    /// <summary>
    ///     Main callback handler.
    /// </summary>
    /// <param name="call"></param>
    /// <returns></returns>
    private async Task GbxRemoteClient_OnCallback(MethodCall call)
    {
        switch (call.Method)
        {
            case "ManiaPlanet.PlayerConnect":
                await InternalInvokeEventsAsync(OnPlayerConnect?.GetInvocationList(), new PlayerConnectEventArgs
                {
                    Login = (string) XmlRpcTypes.ToNativeValue<string>(call.Arguments[0]),
                    IsSpectator = (bool) XmlRpcTypes.ToNativeValue<bool>(call.Arguments[1])
                });
                break;
            case "ManiaPlanet.PlayerDisconnect":
                await InternalInvokeEventsAsync(OnPlayerDisconnect?.GetInvocationList(), new PlayerDisconnectEventArgs
                {
                    Login = (string) XmlRpcTypes.ToNativeValue<string>(call.Arguments[0]),
                    Reason = (string) XmlRpcTypes.ToNativeValue<string>(call.Arguments[1])
                });
                break;
            case "ManiaPlanet.PlayerChat":
                await InternalInvokeEventsAsync(OnPlayerChat?.GetInvocationList(), new PlayerChatEventArgs
                {
                    PlayerId = (int) XmlRpcTypes.ToNativeValue<int>(call.Arguments[0]),
                    Login = (string) XmlRpcTypes.ToNativeValue<string>(call.Arguments[1]),
                    Text = (string) XmlRpcTypes.ToNativeValue<string>(call.Arguments[2]),
                    IsRegisteredCmd = (bool) XmlRpcTypes.ToNativeValue<bool>(call.Arguments[3])
                });
                break;
            case "ManiaPlanet.Echo":
                await InternalInvokeEventsAsync(OnEcho?.GetInvocationList(), new EchoEventArgs
                {
                    InternalParam = (string) XmlRpcTypes.ToNativeValue<string>(call.Arguments[0]),
                    PublicParam = (string) XmlRpcTypes.ToNativeValue<string>(call.Arguments[1])
                });
                break;
            case "ManiaPlanet.BeginMatch":
                await InternalInvokeEventsAsync(OnBeginMatch?.GetInvocationList(), new EventArgs());
                break;
            case "ManiaPlanet.EndMatch":
                await InternalInvokeEventsAsync(OnEndMatch?.GetInvocationList(), new EndMatchEventArgs
                {
                    Rankings = (TmSPlayerRanking[]) XmlRpcTypes.ToNativeValue<TmSPlayerRanking>(call.Arguments[0]),
                    WinnerTeam = (int) XmlRpcTypes.ToNativeValue<int>(call.Arguments[1])
                });
                break;
            case "ManiaPlanet.BeginMap":
                await InternalInvokeEventsAsync(OnBeginMap?.GetInvocationList(), new MapEventArgs
                {
                    Map = (TmSMapInfo) XmlRpcTypes.ToNativeValue<TmSMapInfo>(call.Arguments[0])
                });
                break;
            case "ManiaPlanet.EndMap":
                await InternalInvokeEventsAsync(OnEndMap?.GetInvocationList(), new MapEventArgs
                {
                    Map = (TmSMapInfo) XmlRpcTypes.ToNativeValue<TmSMapInfo>(call.Arguments[0])
                });
                break;
            case "ManiaPlanet.StatusChanged":
                await InternalInvokeEventsAsync(OnStatusChanged?.GetInvocationList(), new StatusChangedEventArgs
                {
                    StatusCode = (int) XmlRpcTypes.ToNativeValue<int>(call.Arguments[0]),
                    StatusName = (string) XmlRpcTypes.ToNativeValue<string>(call.Arguments[1])
                });
                break;
            case "ManiaPlanet.PlayerInfoChanged":
                await InternalInvokeEventsAsync(OnPlayerInfoChanged?.GetInvocationList(), new PlayerInfoChangedEventArgs
                {
                    PlayerInfo = (TmSPlayerInfo) XmlRpcTypes.ToNativeValue<TmSPlayerInfo>(call.Arguments[0])
                });
                break;
            case "ManiaPlanet.ModeScriptCallback":
                await HandleModeScriptCallback(call);
                break;
            case "ManiaPlanet.ModeScriptCallbackArray":
                await HandleModeScriptCallback(call);
                break;
            case "ManiaPlanet.PlayerManialinkPageAnswer":
                await InternalInvokeEventsAsync(OnPlayerManialinkPageAnswer?.GetInvocationList(), new ManiaLinkPageActionEventArgs
                {
                    PlayerId = (int) XmlRpcTypes.ToNativeValue<int>(call.Arguments[0]),
                    Login = (string) XmlRpcTypes.ToNativeValue<string>(call.Arguments[1]),
                    Answer = (string) XmlRpcTypes.ToNativeValue<string>(call.Arguments[2]),
                    Entries = (TmSEntryVal[]) XmlRpcTypes.ToNativeValue<TmSEntryVal>(call.Arguments[3])
                });
                break;
            case "ManiaPlanet.MapListModified":
                await InternalInvokeEventsAsync(OnMapListModified?.GetInvocationList(), new MapListModifiedEventArgs
                {
                    CurrentMapIndex = (int) XmlRpcTypes.ToNativeValue<int>(call.Arguments[0]),
                    NextMapIndex = (int) XmlRpcTypes.ToNativeValue<int>(call.Arguments[1]),
                    IsListModified = (bool) XmlRpcTypes.ToNativeValue<bool>(call.Arguments[2])
                });
                break;
            case "ManiaPlanet.ServerStart":
                await InternalInvokeEventsAsync(OnServerStart?.GetInvocationList(), new EventArgs());
                break;
            case "ManiaPlanet.ServerStop":
                await InternalInvokeEventsAsync(OnServerStop?.GetInvocationList(), new EventArgs());
                break;
            case "ManiaPlanet.TunnelDataReceived":
                await InternalInvokeEventsAsync(OnTunnelDataReceived?.GetInvocationList(), new TunnelDataEventArgs
                {
                    PlayerId = (int) XmlRpcTypes.ToNativeValue<int>(call.Arguments[0]),
                    Login = (string) XmlRpcTypes.ToNativeValue<string>(call.Arguments[1]),
                    Data = (Base64) XmlRpcTypes.ToNativeValue<Base64>(call.Arguments[2])
                });
                break;
            case "ManiaPlanet.VoteUpdated":
                await InternalInvokeEventsAsync(OnVoteUpdated?.GetInvocationList(), new VoteUpdatedEventArgs
                {
                    StateName = (string) XmlRpcTypes.ToNativeValue<string>(call.Arguments[0]),
                    Login = (string) XmlRpcTypes.ToNativeValue<string>(call.Arguments[1]),
                    CmdName = (string) XmlRpcTypes.ToNativeValue<string>(call.Arguments[2]),
                    CmdParam = (string) XmlRpcTypes.ToNativeValue<string>(call.Arguments[3])
                });
                break;
            case "ManiaPlanet.BillUpdated":
                await InternalInvokeEventsAsync(OnBillUpdated?.GetInvocationList(), new BillUpdatedEventArgs
                {
                    BillId = (int) XmlRpcTypes.ToNativeValue<int>(call.Arguments[0]),
                    State = (int) XmlRpcTypes.ToNativeValue<int>(call.Arguments[1]),
                    StateName = (string) XmlRpcTypes.ToNativeValue<string>(call.Arguments[2]),
                    TransactionId = (int) XmlRpcTypes.ToNativeValue<int>(call.Arguments[3])
                });
                break;
            case "ManiaPlanet.PlayerAlliesChanged":
                await InternalInvokeEventsAsync(OnPlayerAlliesChanged?.GetInvocationList(), new PlayerEventArgs
                {
                    Login = (string) XmlRpcTypes.ToNativeValue<string>(call.Arguments[0])
                });
                break;
            case "ScriptCloud.LoadData":
                await InternalInvokeEventsAsync(OnScriptCloudLoadData?.GetInvocationList(), new ScriptCloudEventArgs
                {
                    Type = (string) XmlRpcTypes.ToNativeValue<string>(call.Arguments[0]),
                    Id = (string) XmlRpcTypes.ToNativeValue<string>(call.Arguments[1])
                });
                break;
            case "ScriptCloud.SaveData":
                await InternalInvokeEventsAsync(OnScriptCloudSaveData?.GetInvocationList(), new ScriptCloudEventArgs
                {
                    Type = (string) XmlRpcTypes.ToNativeValue<string>(call.Arguments[0]),
                    Id = (string) XmlRpcTypes.ToNativeValue<string>(call.Arguments[1])
                });
                break;
        }

        OnAnyCallback?.Invoke(this, new CallbackEventArgs<object>
        {
            Call = call,
            Parameters = (object[]) XmlRpcTypes.ToNativeValue<object>(new XmlRpcArray(call.Arguments))
        });
    }
}