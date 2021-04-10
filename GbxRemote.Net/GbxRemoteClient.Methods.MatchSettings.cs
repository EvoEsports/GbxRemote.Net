using GbxRemoteNet.XmlRpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GbxRemoteNet {
    /// <summary>
    /// Method Category: Match Settings
    /// </summary>
    public partial class GbxRemoteClient {
        /// <summary>
        /// Set a list of maps defined in the playlist with the specified filename as the current selection of the server, and load the gameinfos from the same file. Only available to Admin.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public async Task<int> LoadMatchSettingsAsync(string filename) =>
            (int)XmlRpcTypes.ToNativeValue<int>(
                await CallOrFaultAsync("LoadMatchSettings", filename)
            );

        /// <summary>
        /// Add a list of maps defined in the playlist with the specified filename at the end of the current selection. Only available to Admin.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public async Task<int> AppendPlaylistFromMatchSettingsAsync(string filename) =>
            (int)XmlRpcTypes.ToNativeValue<int>(
                await CallOrFaultAsync("AppendPlaylistFromMatchSettings", filename)
            );

        /// <summary>
        /// Save the current selection of map in the playlist with the specified filename, as well as the current gameinfos. Only available to Admin.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public async Task<int> SaveMatchSettingsAsync(string filename) =>
            (int)XmlRpcTypes.ToNativeValue<int>(
                await CallOrFaultAsync("SaveMatchSettings", filename)
            );
    }
}
