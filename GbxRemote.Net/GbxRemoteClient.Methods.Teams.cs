using GbxRemoteNet.Structs;
using GbxRemoteNet.XmlRpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GbxRemoteNet {
    /// <summary>
    /// Method Category: Teams
    /// </summary>
    public partial class GbxRemoteClient {
        [Obsolete("Deprecated and not used in TM2 and later games.")]
        public async Task<string> SetTeamInfo(string par1, double par2, string par3, string par4, double par5, string par6, string par7, double par8, string par9) =>
            (string)XmlRpcTypes.ToNativeValue<string>(
                await CallOrFaultAsync("SetTeamInfo", par1, par2, par3, par4, par5, par6, par7, par8, par9)
            );

        public async Task<TeamInfoStruct> GetTeamInfoAsync(int team) =>
            (TeamInfoStruct)XmlRpcTypes.ToNativeValue<TeamInfoStruct>(
                await CallOrFaultAsync("GetTeamInfo", team)
            );

        public async Task<bool> SetForcedClubLinksAsync(string clubLink1, string clubLink2) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetForcedClubLinks", clubLink1, clubLink2)
            );

        public async Task<ClubLinksStruct> GetForcedClubLinksAsync(int team) =>
            (ClubLinksStruct)XmlRpcTypes.ToNativeValue<ClubLinksStruct>(
                await CallOrFaultAsync("GetForcedClubLinks", team)
            );

        public async Task<bool> SetForcedTeamsAsync(bool forced) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetForcedTeams", forced)
            );

        public async Task<bool> GetForcedTeamsAsync() =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("GetForcedTeams")
            );

        /// <summary>
        /// Returns the current winning team for the race in progress. (-1: if not in team mode, or draw match)
        /// </summary>
        /// <returns></returns>
        public async Task<int> GetCurrentWinnerTeamAsync() =>
            (int)XmlRpcTypes.ToNativeValue<int>(
                await CallOrFaultAsync("GetCurrentWinnerTeam")
            );

        /// <summary>
        /// Force the team of the player. Only available in team mode. You have to pass the login and the team number (0 or 1). Only available to Admin.
        /// </summary>
        /// <param name="playerLogin"></param>
        /// <param name="cameraType"></param>
        /// <returns></returns>
        public async Task<bool> ForcePlayerTeamAsync(int playerLogin, int cameraType) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("ForcePlayerTeam", playerLogin, cameraType)
            );

        /// <summary>
        /// Force the team of the player. Only available in team mode. You have to pass the playerid and the team number (0 or 1). Only available to Admin.
        /// </summary>
        /// <param name="playerId"></param>
        /// <param name="cameraType"></param>
        /// <returns></returns>
        public async Task<bool> ForcePlayerTeamIdAsync(int playerId, int cameraType) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("ForcePlayerTeamId", playerId, cameraType)
            );
    }
}
