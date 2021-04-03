using GbxRemoteNet.XmlRpc;
using GbxRemoteNet.XmlRpc.ExtraTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GbxRemoteNet {
    /// <summary>
    /// Method Category: Replays
    /// </summary>
    public partial class GbxRemoteClient {
        public async Task<bool> AutoSaveReplaysAsync(string autoSave) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("AutoSaveReplays", autoSave)
            );

        public async Task<bool> AutoSaveValidationReplaysAsync(string autoSave) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("AutoSaveValidationReplays", autoSave)
            );

        public async Task<bool> IsAutoSaveValidationReplaysEnabledAsync() =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("IsAutoSaveValidationReplaysEnabled")
            );

        public async Task<bool> SaveCurrentReplayAsync(string fileName) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SaveCurrentReplay", fileName)
            );

        public async Task<bool> SaveBestGhostsReplayAsync(string login, string fileName) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SaveBestGhostsReplay", login, fileName)
            );

        public async Task<Base64> GetValidationReplayAsync(string login) =>
            (Base64)XmlRpcTypes.ToNativeValue<Base64>(
                await CallOrFaultAsync("GetValidationReplay", login)
            );
    }
}
