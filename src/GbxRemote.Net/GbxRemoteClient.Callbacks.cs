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
    public event AsyncEventHandler<CallbackGbxEventArgs<object>> OnAnyCallback;

    /// <summary>
    ///     When a player connects to the server.
    /// </summary>
    public event AsyncEventHandler<PlayerConnectGbxEventArgs> OnPlayerConnect;

    /// <summary>
    ///     When a player disconnects from the server.
    /// </summary>
    public event AsyncEventHandler<PlayerDisconnectGbxEventArgs> OnPlayerDisconnect;

    /// <summary>
    ///     When a player sends a chat message.
    /// </summary>
    public event AsyncEventHandler<PlayerChatGbxEventArgs> OnPlayerChat;

    /// <summary>
    ///     When a echo message is sent. Can be used for communication with other.
    ///     XMLRPC-clients.
    /// </summary>
    public event AsyncEventHandler<EchoGbxEventArgs> OnEcho;

    /// <summary>
    ///     When the match itself starts, triggered after begin map.
    /// </summary>
    public event AsyncEventHandler OnBeginMatch;

    /// <summary>
    ///     When the match ends, does not give a lot of info in TM2020.
    /// </summary>
    public event AsyncEventHandler<EndMatchGbxEventArgs> OnEndMatch;

    /// <summary>
    ///     When the map has loaded on the server.
    /// </summary>
    public event AsyncEventHandler<MapGbxEventArgs> OnBeginMap;

    /// <summary>
    ///     When the map unloads from the server.
    /// </summary>
    public event AsyncEventHandler<MapGbxEventArgs> OnEndMap;

    /// <summary>
    ///     When the server status changed.
    /// </summary>
    public event AsyncEventHandler<StatusChangedGbxEventArgs> OnStatusChanged;

    /// <summary>
    ///     When data about a player changed, it is usually called when
    ///     a player joins or leaves. Gives you more detailed info about a player.
    /// </summary>
    public event AsyncEventHandler<PlayerInfoChangedGbxEventArgs> OnPlayerInfoChanged;

    /// <summary>
    ///     When a user triggers the page answer callback from a manialink.
    /// </summary>
    public event AsyncEventHandler<ManiaLinkPageActionGbxEventArgs> OnPlayerManialinkPageAnswer;

    /// <summary>
    ///     Triggered when the map list changed.
    /// </summary>
    public event AsyncEventHandler<MapListModifiedGbxEventArgs> OnMapListModified;

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
    public event AsyncEventHandler<TunnelDataGbxEventArgs> OnTunnelDataReceived;

    /// <summary>
    ///     When a current vote has been updated.
    /// </summary>
    public event AsyncEventHandler<VoteUpdatedGbxEventArgs> OnVoteUpdated;

    /// <summary>
    ///     When a player bill is updated.
    /// </summary>
    public event AsyncEventHandler<BillUpdatedGbxEventArgs> OnBillUpdated;

    /// <summary>
    ///     When a player changed allies.
    /// </summary>
    public event AsyncEventHandler<PlayerGbxEventArgs> OnPlayerAlliesChanged;

    /// <summary>
    ///     When a variable from the script cloud is loaded.
    /// </summary>
    public event AsyncEventHandler<ScriptCloudGbxEventArgs> OnScriptCloudLoadData;

    /// <summary>
    ///     When a variable from the script cloud is saved.
    /// </summary>
    public event AsyncEventHandler<ScriptCloudGbxEventArgs> OnScriptCloudSaveData;

    /// <summary>
    ///     Enable callbacks. If no parameter is provided,
    ///     all callbacks are enabled by default.
    /// </summary>
    /// <param name="gbxCallbackType"></param>
    /// <returns></returns>
    public async Task EnableCallbackTypeAsync(
        GbxCallbackType gbxCallbackType = GbxCallbackType.Internal | GbxCallbackType.ModeScript | GbxCallbackType.Checkpoints)
    {
        if (gbxCallbackType.HasFlag(GbxCallbackType.Internal))
            await EnableCallbacksAsync(true);
        if (gbxCallbackType.HasFlag(GbxCallbackType.ModeScript))
            await TriggerModeScriptEventArrayAsync("XmlRpc.EnableCallbacks", "true");
        if (gbxCallbackType.HasFlag(GbxCallbackType.Checkpoints))
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
                await InternalInvokeEventsAsync(OnPlayerConnect?.GetInvocationList(), new PlayerConnectGbxEventArgs
                {
                    Login = (string) XmlRpcTypes.ToNativeValue<string>(call.Arguments[0]),
                    IsSpectator = (bool) XmlRpcTypes.ToNativeValue<bool>(call.Arguments[1])
                });
                break;
            case "ManiaPlanet.PlayerDisconnect":
                await InternalInvokeEventsAsync(OnPlayerDisconnect?.GetInvocationList(), new PlayerDisconnectGbxEventArgs
                {
                    Login = (string) XmlRpcTypes.ToNativeValue<string>(call.Arguments[0]),
                    Reason = (string) XmlRpcTypes.ToNativeValue<string>(call.Arguments[1])
                });
                break;
            case "ManiaPlanet.PlayerChat":
                await InternalInvokeEventsAsync(OnPlayerChat?.GetInvocationList(), new PlayerChatGbxEventArgs
                {
                    PlayerId = (int) XmlRpcTypes.ToNativeValue<int>(call.Arguments[0]),
                    Login = (string) XmlRpcTypes.ToNativeValue<string>(call.Arguments[1]),
                    Text = (string) XmlRpcTypes.ToNativeValue<string>(call.Arguments[2]),
                    IsRegisteredCmd = (bool) XmlRpcTypes.ToNativeValue<bool>(call.Arguments[3]),
                    Options = (int) XmlRpcTypes.ToNativeValue<int>(call.Arguments[4])
                });
                break;
            case "ManiaPlanet.Echo":
                await InternalInvokeEventsAsync(OnEcho?.GetInvocationList(), new EchoGbxEventArgs
                {
                    InternalParam = (string) XmlRpcTypes.ToNativeValue<string>(call.Arguments[0]),
                    PublicParam = (string) XmlRpcTypes.ToNativeValue<string>(call.Arguments[1])
                });
                break;
            case "ManiaPlanet.BeginMatch":
                await InternalInvokeEventsAsync(OnBeginMatch?.GetInvocationList(), new EventArgs());
                break;
            case "ManiaPlanet.EndMatch":
                await InternalInvokeEventsAsync(OnEndMatch?.GetInvocationList(), new EndMatchGbxEventArgs
                {
                    Rankings = (TmSPlayerRanking[]) XmlRpcTypes.ToNativeValue<TmSPlayerRanking>(call.Arguments[0]),
                    WinnerTeam = (int) XmlRpcTypes.ToNativeValue<int>(call.Arguments[1])
                });
                break;
            case "ManiaPlanet.BeginMap":
                await InternalInvokeEventsAsync(OnBeginMap?.GetInvocationList(), new MapGbxEventArgs
                {
                    Map = (TmSMapInfo) XmlRpcTypes.ToNativeValue<TmSMapInfo>(call.Arguments[0])
                });
                break;
            case "ManiaPlanet.EndMap":
                await InternalInvokeEventsAsync(OnEndMap?.GetInvocationList(), new MapGbxEventArgs
                {
                    Map = (TmSMapInfo) XmlRpcTypes.ToNativeValue<TmSMapInfo>(call.Arguments[0])
                });
                break;
            case "ManiaPlanet.StatusChanged":
                await InternalInvokeEventsAsync(OnStatusChanged?.GetInvocationList(), new StatusChangedGbxEventArgs
                {
                    StatusCode = (int) XmlRpcTypes.ToNativeValue<int>(call.Arguments[0]),
                    StatusName = (string) XmlRpcTypes.ToNativeValue<string>(call.Arguments[1])
                });
                break;
            case "ManiaPlanet.PlayerInfoChanged":
                await InternalInvokeEventsAsync(OnPlayerInfoChanged?.GetInvocationList(), new PlayerInfoChangedGbxEventArgs
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
                await InternalInvokeEventsAsync(OnPlayerManialinkPageAnswer?.GetInvocationList(), new ManiaLinkPageActionGbxEventArgs
                {
                    PlayerId = (int) XmlRpcTypes.ToNativeValue<int>(call.Arguments[0]),
                    Login = (string) XmlRpcTypes.ToNativeValue<string>(call.Arguments[1]),
                    Answer = (string) XmlRpcTypes.ToNativeValue<string>(call.Arguments[2]),
                    Entries = (TmSEntryVal[]) XmlRpcTypes.ToNativeValue<TmSEntryVal>(call.Arguments[3])
                });
                break;
            case "ManiaPlanet.MapListModified":
                await InternalInvokeEventsAsync(OnMapListModified?.GetInvocationList(), new MapListModifiedGbxEventArgs
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
                await InternalInvokeEventsAsync(OnTunnelDataReceived?.GetInvocationList(), new TunnelDataGbxEventArgs
                {
                    PlayerId = (int) XmlRpcTypes.ToNativeValue<int>(call.Arguments[0]),
                    Login = (string) XmlRpcTypes.ToNativeValue<string>(call.Arguments[1]),
                    Data = (GbxBase64) XmlRpcTypes.ToNativeValue<GbxBase64>(call.Arguments[2])
                });
                break;
            case "ManiaPlanet.VoteUpdated":
                await InternalInvokeEventsAsync(OnVoteUpdated?.GetInvocationList(), new VoteUpdatedGbxEventArgs
                {
                    StateName = (string) XmlRpcTypes.ToNativeValue<string>(call.Arguments[0]),
                    Login = (string) XmlRpcTypes.ToNativeValue<string>(call.Arguments[1]),
                    CmdName = (string) XmlRpcTypes.ToNativeValue<string>(call.Arguments[2]),
                    CmdParam = (string) XmlRpcTypes.ToNativeValue<string>(call.Arguments[3])
                });
                break;
            case "ManiaPlanet.BillUpdated":
                await InternalInvokeEventsAsync(OnBillUpdated?.GetInvocationList(), new BillUpdatedGbxEventArgs
                {
                    BillId = (int) XmlRpcTypes.ToNativeValue<int>(call.Arguments[0]),
                    State = (int) XmlRpcTypes.ToNativeValue<int>(call.Arguments[1]),
                    StateName = (string) XmlRpcTypes.ToNativeValue<string>(call.Arguments[2]),
                    TransactionId = (int) XmlRpcTypes.ToNativeValue<int>(call.Arguments[3])
                });
                break;
            case "ManiaPlanet.PlayerAlliesChanged":
                await InternalInvokeEventsAsync(OnPlayerAlliesChanged?.GetInvocationList(), new PlayerGbxEventArgs
                {
                    Login = (string) XmlRpcTypes.ToNativeValue<string>(call.Arguments[0])
                });
                break;
            case "ScriptCloud.LoadData":
                await InternalInvokeEventsAsync(OnScriptCloudLoadData?.GetInvocationList(), new ScriptCloudGbxEventArgs
                {
                    Type = (string) XmlRpcTypes.ToNativeValue<string>(call.Arguments[0]),
                    Id = (string) XmlRpcTypes.ToNativeValue<string>(call.Arguments[1])
                });
                break;
            case "ScriptCloud.SaveData":
                await InternalInvokeEventsAsync(OnScriptCloudSaveData?.GetInvocationList(), new ScriptCloudGbxEventArgs
                {
                    Type = (string) XmlRpcTypes.ToNativeValue<string>(call.Arguments[0]),
                    Id = (string) XmlRpcTypes.ToNativeValue<string>(call.Arguments[1])
                });
                break;
        }

        OnAnyCallback?.Invoke(this, new CallbackGbxEventArgs<object>
        {
            Call = call,
            Parameters = (object[]) XmlRpcTypes.ToNativeValue<object>(new XmlRpcArray(call.Arguments))
        });
    }
}