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
        /// <summary>
        /// Set Team names and colors (deprecated). Only available to Admin.
        /// </summary>
        /// <param name="par1"></param>
        /// <param name="par2"></param>
        /// <param name="par3"></param>
        /// <param name="par4"></param>
        /// <param name="par5"></param>
        /// <param name="par6"></param>
        /// <param name="par7"></param>
        /// <param name="par8"></param>
        /// <param name="par9"></param>
        /// <returns></returns>
        [Obsolete("Deprecated and not used in TM2 and later games.")]
        public async Task<string> SetTeamInfoAsync(string par1, double par2, string par3, string par4, double par5, string par6, string par7, double par8, string par9) =>
            (string)XmlRpcTypes.ToNativeValue<string>(
                await CallOrFaultAsync("SetTeamInfo", par1, par2, par3, par4, par5, par6, par7, par8, par9)
            );

        /// <summary>
        /// Return Team info for a given clan (0 = no clan, 1, 2). The structure contains: Name, ZonePath, City, EmblemUrl, HuePrimary, HueSecondary, RGB, ClubLinkUrl. Only available to Admin.
        /// </summary>
        /// <param name="team"></param>
        /// <returns></returns>
        public async Task<TeamInfo> GetTeamInfoAsync(int team) =>
            (TeamInfo)XmlRpcTypes.ToNativeValue<TeamInfo>(
                await CallOrFaultAsync("GetTeamInfo", team)
            );

        /// <summary>
        /// Set the clublinks to use for the two clans. Only available to Admin.
        /// </summary>
        /// <param name="clubLink1"></param>
        /// <param name="clubLink2"></param>
        /// <returns></returns>
        public async Task<bool> SetForcedClubLinksAsync(string clubLink1, string clubLink2) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetForcedClubLinks", clubLink1, clubLink2)
            );

        /// <summary>
        /// Get the forced clublinks.
        /// </summary>
        /// <param name="team"></param>
        /// <returns></returns>
        public async Task<ClubLinks> GetForcedClubLinksAsync(int team) =>
            (ClubLinks)XmlRpcTypes.ToNativeValue<ClubLinks>(
                await CallOrFaultAsync("GetForcedClubLinks", team)
            );

        /// <summary>
        /// Set whether the players can choose their side or if the teams are forced by the server (using ForcePlayerTeam()). Only available to Admin.
        /// </summary>
        /// <param name="forced"></param>
        /// <returns></returns>
        public async Task<bool> SetForcedTeamsAsync(bool forced) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetForcedTeams", forced)
            );

        /// <summary>
        /// Returns whether the players can choose their side or if the teams are forced by the server.
        /// </summary>
        /// <returns></returns>
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
        public async Task<bool> ForcePlayerTeamAsync(string playerLogin, int cameraType) =>
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
