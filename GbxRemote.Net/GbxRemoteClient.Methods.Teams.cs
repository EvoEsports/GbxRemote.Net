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
    }
}
