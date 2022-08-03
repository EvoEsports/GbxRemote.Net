using GbxRemoteNet.Enums;
using GbxRemoteNet.Structs;
using GbxRemoteNet.XmlRpc;
using GbxRemoteNet.XmlRpc.ExtraTypes;
using GbxRemoteNet.XmlRpc.Packets;
using GbxRemoteNet.XmlRpc.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GbxRemoteNet {
    public partial class GbxRemoteClient {
        /// <summary>
        /// Action for the OnAnyCallback event.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="call">Information about the callback.</param>
        /// <param name="pars">Parameters of the callback.</param>
        /// <returns></returns>
        public delegate Task CallbackAction<T>(MethodCall call, T[] pars);
        /// <summary>
        /// Action for the OnPlayerConnect event.
        /// </summary>
        /// <param name="login">Player's login/user name.</param>
        /// <param name="isSpectator">Whether the player is in spectator mode.</param>
        /// <returns></returns>
        public delegate Task PlayerConnectAction(string login, bool isSpectator);
        /// <summary>
        /// Action for the OnPlayerDisconnect event.
        /// </summary>
        /// <param name="login">Player's login/user name.</param>
        /// <param name="reason">The reason the player disconnected.</param>
        /// <returns></returns>
        public delegate Task PlayerDisconnectAction(string login, string reason);
        /// <summary>
        /// Action for the OnPlayerChat event.
        /// </summary>
        /// <param name="playerUid">The ID of the player on the server.</param>
        /// <param name="login">Login/user name of the player.</param>
        /// <param name="text">The message the player sent to the chat.</param>
        /// <param name="isRegisteredCmd">Whether the message is a command.</param>
        /// <returns></returns>
        public delegate Task PlayerChatAction(int playerUid, string login, string text, bool isRegisteredCmd);
        /// <summary>
        /// Action for the OnEcho event.
        /// </summary>
        /// <param name="internalParam">The internal parameter, or simply "parameter 1".</param>
        /// <param name="publicParam">The public parameter, or simply "parameter 2".</param>
        /// <returns></returns>
        public delegate Task EchoAction(string internalParam, string publicParam);
        /// <summary>
        /// Action for the OnEndMatch event.
        /// </summary>
        /// <param name="rankings">Array containing the ranking results of the match.</param>
        /// <param name="winnerTeam">The ID of the team that won the match if the Teams gamemode is played.</param>
        /// <returns></returns>
        public delegate Task EndMatchAction(SPlayerRanking[] rankings, int winnerTeam);
        /// <summary>
        /// Action for the OnBeginMap and OnEndMap events.
        /// </summary>
        /// <param name="map">Information about the map that will be/was played.</param>
        /// <returns></returns>
        public delegate Task BeginEndMapAction(SMapInfo map);
        /// <summary>
        /// Action for the OnStatusChanged event.
        /// </summary>
        /// <param name="statusCode">Code/ID of the status.</param>
        /// <param name="statusName">A friendly string that represents the status.</param>
        /// <returns></returns>
        public delegate Task StatusChangedAction(int statusCode, string statusName);
        /// <summary>
        /// Action for when player info changed.
        /// </summary>
        /// <param name="playerUIid">New information about the player.</param>
        public delegate Task PlayerInfoChangedAction(SPlayerInfo playerInfo);
        /// <summary>
        /// Action for the OnStatusChanged event.
        /// </summary>
        /// <param name="playerUid">Player's server ID</param>
        /// <param name="login">Login of the player.</param>
        /// <param name="answer">String representing the answer.</param>
        /// <param name="entries">Key/Value of entries.</param>
        /// <returns></returns>
        public delegate Task PlayerManialinkPageAnswerAction(int playerUid, string login, string answer, SEntryVal[] entries);
        /// <summary>
        /// Action for the MapListModified event.
        /// </summary>
        public delegate Task MapListModifiedAction(int curMapIndex, int nextMapIndex, bool isListModified);
        /// <summary>
        /// Action for the tunnel data received event.
        /// </summary>
        public delegate Task TunnelDataReceivedAction(int playerUid, string login, Base64 data);
        /// <summary>
        /// Action for vote updated event.
        /// </summary>
        public delegate Task VoteUpdatedAction(string stateName, string login, string cmdName, string cmdParam);
        
        /// <summary>
        /// Triggered for all possible callbacks.
        /// </summary>
        public event CallbackAction<object> OnAnyCallback;
        /// <summary>
        /// When a player connects to the server.
        /// </summary>
        public event PlayerConnectAction OnPlayerConnect;
        /// <summary>
        /// When a player disconnects from the server.
        /// </summary>
        public event PlayerDisconnectAction OnPlayerDisconnect;
        /// <summary>
        /// When a player sends a chat message.
        /// </summary>
        public event PlayerChatAction OnPlayerChat;
        /// <summary>
        /// When a echo message is sent. Can be used for communication with other.
        /// XMLRPC-clients.
        /// </summary>
        public event EchoAction OnEcho;
        /// <summary>
        /// When the match itself starts, triggered after begin map.
        /// </summary>
        public event TaskAction OnBeginMatch;
        /// <summary>
        /// When the match ends, does not give a lot of info in TM2020.
        /// </summary>
        public event EndMatchAction OnEndMatch;
        /// <summary>
        /// When the map has loaded on the server.
        /// </summary>
        public event BeginEndMapAction OnBeginMap;
        /// <summary>
        /// When the map unloads from the server.
        /// </summary>
        public event BeginEndMapAction OnEndMap;
        /// <summary>
        /// When the server status changed.
        /// </summary>
        public event StatusChangedAction OnStatusChanged;
        /// <summary>
        /// When data about a player changed, it is usually called when
        /// a player joins or leaves. Gives you more detailed info about a player.
        /// </summary>
        public event PlayerInfoChangedAction OnPlayerInfoChanged;
        /// <summary>
        /// When a user triggers the page answer callback from a manialink.
        /// </summary>
        public event PlayerManialinkPageAnswerAction OnPlayerManialinkPageAnswer;
        /// <summary>
        /// Triggered when the map list changed.
        /// </summary>
        public event MapListModifiedAction OnMapListModified;
        public event TaskAction OnServerStart;
        public event TaskAction OnServerStop;
        public event TunnelDataReceivedAction OnTunnelDataReceived;
        public event VoteUpdatedAction OnVoteUpdated;

        /// <summary>
        /// Enable callbacks. If no parameter is provided,
        /// all callbacks are enabled by default.
        /// </summary>
        /// <param name="callbackType"></param>
        /// <returns></returns>
        public async Task EnableCallbackTypeAsync(CallbackType callbackType=CallbackType.Internal | CallbackType.ModeScript | CallbackType.Checkpoints) {
            if (callbackType.HasFlag(CallbackType.Internal))
                await EnableCallbacksAsync(true);
            if (callbackType.HasFlag(CallbackType.ModeScript))
                await TriggerModeScriptEventArrayAsync("XmlRpc.EnableCallbacks", "true");
            if (callbackType.HasFlag(CallbackType.Checkpoints))
                await TriggerModeScriptEventArrayAsync("Trackmania.Event.SetCurLapCheckpointsMode", "always");
        }

        /// <summary>
        /// Main callback handler.
        /// </summary>
        /// <param name="call"></param>
        /// <returns></returns>
        private async Task GbxRemoteClient_OnCallback(MethodCall call) {
            switch (call.Method) {
                case "ManiaPlanet.PlayerConnect":
                case "TrackMania.PlayerConnect":
                    OnPlayerConnect?.Invoke(
                        (string)XmlRpcTypes.ToNativeValue<string>(call.Arguments[0]),
                        (bool)XmlRpcTypes.ToNativeValue<bool>(call.Arguments[1])
                    );
                    break;
                case "ManiaPlanet.PlayerDisconnect":
                case "TrackMania.PlayerDisconnect":
                    OnPlayerDisconnect?.Invoke(
                        (string)XmlRpcTypes.ToNativeValue<string>(call.Arguments[0]),
                        (string)XmlRpcTypes.ToNativeValue<string>(call.Arguments[1])
                    );
                    break;
                case "ManiaPlanet.PlayerChat":
                case "TrackMania.PlayerChat":
                    OnPlayerChat?.Invoke(
                        (int)XmlRpcTypes.ToNativeValue<int>(call.Arguments[0]),
                        (string)XmlRpcTypes.ToNativeValue<string>(call.Arguments[1]),
                        (string)XmlRpcTypes.ToNativeValue<string>(call.Arguments[2]),
                        (bool)XmlRpcTypes.ToNativeValue<bool>(call.Arguments[3])
                    );
                    break;
                case "ManiaPlanet.Echo":
                case "TrackMania.Echo":
                    OnEcho?.Invoke(
                        (string)XmlRpcTypes.ToNativeValue<string>(call.Arguments[0]),
                        (string)XmlRpcTypes.ToNativeValue<string>(call.Arguments[1])
                    );
                    break;
                case "ManiaPlanet.BeginMatch":
                case "TrackMania.BeginMatch":
                    OnBeginMatch?.Invoke();
                    break;
                case "ManiaPlanet.EndMatch":
                case "TrackMania.EndMatch":
                    OnEndMatch?.Invoke(
                        (SPlayerRanking[])XmlRpcTypes.ToNativeValue<SPlayerRanking>(call.Arguments[0]),
                        (int)XmlRpcTypes.ToNativeValue<int>(call.Arguments[1])
                    );
                    break;
                case "ManiaPlanet.BeginMap":
                case "TrackMania.BeginMap":
                    OnBeginMap?.Invoke(
                        (SMapInfo)XmlRpcTypes.ToNativeValue<SMapInfo>(call.Arguments[0])
                    );
                    break;
                case "ManiaPlanet.EndMap":
                case "TrackMania.EndMap":
                    OnEndMap?.Invoke(
                        (SMapInfo)XmlRpcTypes.ToNativeValue<SMapInfo>(call.Arguments[0])
                    );
                    break;
                case "ManiaPlanet.StatusChanged":
                case "TrackMania.StatusChanged":
                    OnStatusChanged?.Invoke(
                        (int)XmlRpcTypes.ToNativeValue<int>(call.Arguments[0]),
                        (string)XmlRpcTypes.ToNativeValue<string>(call.Arguments[1])
                    );
                    break;
                case "ManiaPlanet.PlayerInfoChanged":
                case "TrackMania.PlayerInfoChanged":
                    OnPlayerInfoChanged?.Invoke(
                        (SPlayerInfo)XmlRpcTypes.ToNativeValue<SPlayerInfo>(call.Arguments[0])
                    );
                    break;
                case "ManiaPlanet.ModeScriptCallback":
                case "TrackMania.ModeScriptCallback":
                    await HandleModeScriptCallback(call);
                    break;
                case "ManiaPlanet.ModeScriptCallbackArray":
                case "TrackMania.ModeScriptCallbackArray":
                    await HandleModeScriptCallback(call);
                    break;
                case "ManiaPlanet.PlayerManialinkPageAnswer":
                case "TrackMania.PlayerManialinkPageAnswer":
                    OnPlayerManialinkPageAnswer?.Invoke(
                        (int)XmlRpcTypes.ToNativeValue<int>(call.Arguments[0]),
                        (string)XmlRpcTypes.ToNativeValue<string>(call.Arguments[1]),
                        (string)XmlRpcTypes.ToNativeValue<string>(call.Arguments[2]),
                        (SEntryVal[])XmlRpcTypes.ToNativeValue<SEntryVal>(call.Arguments[3])
                    );
                    break;
                case "ManiaPlanet.MapListModified":
                    OnMapListModified?.Invoke(
                        (int)XmlRpcTypes.ToNativeValue<int>(call.Arguments[0]),
                        (int)XmlRpcTypes.ToNativeValue<int>(call.Arguments[1]),
                        (bool)XmlRpcTypes.ToNativeValue<bool>(call.Arguments[2])
                    );
                    break;
                case "ManiaPlanet.ServerStart":
                case "TrackMania.ServerStart":
                    OnServerStart?.Invoke();
                    break;
                case "ManiaPlanet.ServerStop":
                case "TrackMania.ServerStop":
                    OnServerStop?.Invoke();
                    break;
                case "ManiaPlanet.TunnelDataReceived":
                case "TrackMania.TunnelDataReceived":
                    OnTunnelDataReceived?.Invoke(
                        (int)XmlRpcTypes.ToNativeValue<int>(call.Arguments[0]),
                        (string)XmlRpcTypes.ToNativeValue<string>(call.Arguments[1]),
                        (Base64)XmlRpcTypes.ToNativeValue<Base64>(call.Arguments[2])
                    );
                    break;
                case "ManiaPlanet.VoteUpdated":
                case "TrackMania.VoteUpdated":
                    OnVoteUpdated?.Invoke(
                        (string)XmlRpcTypes.ToNativeValue<string>(call.Arguments[0]),
                        (string)XmlRpcTypes.ToNativeValue<string>(call.Arguments[1]),
                        (string)XmlRpcTypes.ToNativeValue<string>(call.Arguments[2]),
                        (string)XmlRpcTypes.ToNativeValue<string>(call.Arguments[3])
                    );
                    break;
            }

            // always invoke the OnAnyCallback event
            OnAnyCallback?.Invoke(call, (object[])XmlRpcTypes.ToNativeValue<object>(
                new XmlRpcArray(call.Arguments)
            ));
        }
    }
}
