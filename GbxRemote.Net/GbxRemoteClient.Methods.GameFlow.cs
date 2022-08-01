using GbxRemoteNet.Structs;
using GbxRemoteNet.XmlRpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GbxRemoteNet {
    /// <summary>
    /// Method Category: Game Flow
    /// </summary>
    public partial class GbxRemoteClient {
        /// <summary>
        /// Restarts the map, with an optional boolean parameter DontClearCupScores (only available in cup mode). Only available to Admin.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> RestartMapAsync() =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("RestartMap")
            );

        /// <summary>
        /// Switch to next map, with an optional boolean parameter DontClearCupScores (only available in cup mode). Only available to Admin.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> NextMapAsync() =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("NextMap")
            );

        /// <summary>
        /// Attempt to balance teams. Only available to Admin.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> AutoTeamBalanceAsync() =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("AutoTeamBalance")
            );

        /// <summary>
        /// Returns a struct containing the current game settings.
        /// </summary>
        /// <returns></returns>
        [Obsolete]
        public async Task<GameInfo> GetCurrentGameInfoAsync() =>
            (GameInfo)XmlRpcTypes.ToNativeValue<GameInfo>(
                await CallOrFaultAsync("GetCurrentGameInfo")
            );

        /// <summary>
        /// Returns a struct containing the game settings for the next map.
        /// </summary>
        /// <returns></returns>
        [Obsolete]
        public async Task<GameInfo> GetNextGameInfoAsync() =>
            (GameInfo)XmlRpcTypes.ToNativeValue<GameInfo>(
                await CallOrFaultAsync("GetNextGameInfo")
            );

        /// <summary>
        /// Returns a struct containing two other structures, the first containing the current game settings and the second the game settings for next map. The first structure is named CurrentGameInfos and the second NextGameInfos.
        /// </summary>
        /// <returns></returns>
        [Obsolete]
        public async Task<CurrentNextValue<GameInfo>> GetGameInfosAsync() =>
            (CurrentNextValue<GameInfo>)XmlRpcTypes.ToNativeValue<CurrentNextValue<GameInfo>>(
                await CallOrFaultAsync("GetGameInfos")
            );


        /// <summary>
        /// Set whether to override the players preferences and always display all opponents). Only available to Admin. Requires a map restart to be taken into account.
        /// </summary>
        /// <param name="playersPreferenceOverride">0=no override, 1=show all, other value=minimum number of opponents</param>
        /// <returns></returns>
        public async Task<bool> SetForceShowAllOpponentsAsync(int playersPreferenceOverride) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetForceShowAllOpponents", playersPreferenceOverride)
            );
        

        /// <summary>
        /// Set a new mode script name for script mode. Only available to Admin. Requires a map restart to be taken into account.
        /// </summary>
        /// <param name="scriptName"></param>
        /// <returns></returns>
        public async Task<bool> SetScriptNameAsync(string scriptName) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetScriptName", scriptName)
            );

        /// <summary>
        /// Get the current and next mode script name for script mode. The struct returned contains two fields CurrentValue and NextValue.
        /// </summary>
        /// <returns></returns>
        public async Task<CurrentNextValue<string>> GetScriptNameAsync() =>
            (CurrentNextValue<string>)XmlRpcTypes.ToNativeValue<CurrentNextValue<string>>(
                await CallOrFaultAsync("GetScriptName")
            );
        

        /// <summary>
        /// Get the current and next time limit for time attack mode. The struct returned contains two fields CurrentValue and NextValue.
        /// </summary>
        /// <returns></returns>
        public async Task<CurrentNextValue<int>> GetTimeAttackLimitAsync() =>
            (CurrentNextValue<int>)XmlRpcTypes.ToNativeValue<CurrentNextValue<int>>(
                await CallOrFaultAsync("GetTimeAttackLimit")
            );

    }
}
