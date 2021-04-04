using GbxRemoteNet.Enums;
using GbxRemoteNet.Structs;
using GbxRemoteNet.XmlRpc;
using GbxRemoteNet.XmlRpc.ExtraTypes;
using GbxRemoteNet.XmlRpc.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GbxRemoteNet {
    public partial class GbxRemoteClient {
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
                await TriggerModeScriptEventAsync("Trackmania.Event.SetCurLapCheckpointsMode", "always");
        }

        /// <summary>
        /// Main callback handler.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private Task GbxRemoteClient_OnCallback(MethodCall call) {
            //Console.WriteLine(call.Method);

            return Task.CompletedTask;
        }
    }
}
