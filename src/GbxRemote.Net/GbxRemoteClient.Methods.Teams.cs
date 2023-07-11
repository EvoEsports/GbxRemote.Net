using System;
using System.Threading.Tasks;
using GbxRemoteNet.Structs;
using GbxRemoteNet.XmlRpc;

namespace GbxRemoteNet;

/// <summary>
/// Method Category: Teams
/// </summary>
public partial class GbxRemoteClient
{
    public async Task<string> SetTeamInfoAsync(string par1, double par2, string par3, string par4, double par5,
        string par6, string par7, double par8, string par9)
    {
        return (string) XmlRpcTypes.ToNativeValue<string>(
            await CallOrFaultAsync("SetTeamInfo", par1, par2, par3, par4, par5, par6, par7, par8, par9)
        );
    }

    public async Task<TmTeamInfo> GetTeamInfoAsync(int team)
    {
        return (TmTeamInfo) XmlRpcTypes.ToNativeValue<TmTeamInfo>(
            await CallOrFaultAsync("GetTeamInfo", team)
        );
    }

    public async Task<bool> SetForcedClubLinksAsync(string clubLink1, string clubLink2)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("SetForcedClubLinks", clubLink1, clubLink2)
        );
    }

    public async Task<TmClubLinks> GetForcedClubLinksAsync(int team)
    {
        return (TmClubLinks) XmlRpcTypes.ToNativeValue<TmClubLinks>(
            await CallOrFaultAsync("GetForcedClubLinks", team)
        );
    }

    public async Task<bool> SetForcedTeamsAsync(bool forced)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("SetForcedTeams", forced)
        );
    }

    public async Task<bool> GetForcedTeamsAsync()
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("GetForcedTeams")
        );
    }

    public async Task<int> GetCurrentWinnerTeamAsync()
    {
        return (int) XmlRpcTypes.ToNativeValue<int>(
            await CallOrFaultAsync("GetCurrentWinnerTeam")
        );
    }

    public async Task<bool> ForcePlayerTeamAsync(string playerLogin, int cameraType)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("ForcePlayerTeam", playerLogin, cameraType)
        );
    }

    public async Task<bool> ForcePlayerTeamIdAsync(int playerId, int cameraType)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("ForcePlayerTeamId", playerId, cameraType)
        );
    }
}