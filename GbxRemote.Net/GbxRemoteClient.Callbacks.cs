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
        public delegate Task CallbackAction<T>(MethodCall call, T[] pars);
        public delegate Task PlayerConnectAction(string login, bool isSpectator);
        public delegate Task PlayerDisconnectAction(string login, string reason);
        public delegate Task PlayerChatAction(int playerUid, string login, string text, bool isRegisteredCmd);
        public delegate Task EchoAction(string internalParam, string publicParam);
        public delegate Task EndMatchAction(SPlayerRanking[] rankings, int winnerTeam);
        public delegate Task BeginEndMapAction(SMapInfo map);
        public delegate Task StatusChangedAction(int statusCode, string statusName);
        public delegate Task PlayerInfoChangedAction(SPlayerInfo playerInfo);

        /// <summary>
        /// Triggered for all possible callbacks.
        /// </summary>
        public event CallbackAction<object> OnAnyCallback;
        /// <summary>
        /// When a player connects to the server.
        /// </summary>
        public event PlayerConnectAction OnPlayerConnect;
        /// <summary>
        /// When a player disconnects from the server
        /// </summary>
        public event PlayerDisconnectAction OnPlayerDisconnect;
        /// <summary>
        /// When a player sends a chat message.
        /// </summary>
        public event PlayerChatAction OnPlayerChat;
        /// <summary>
        /// When a echo message is sent. Can be used for communication with other
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
        /// <param name="message"></param>
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
            }

            OnAnyCallback?.Invoke(call, (object[])XmlRpcTypes.ToNativeValue<object>(
                new XmlRpcArray(call.Arguments)
            ));
        }
    }
}
