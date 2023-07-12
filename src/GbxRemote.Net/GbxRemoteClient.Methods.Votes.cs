using System.Threading.Tasks;
using GbxRemoteNet.Structs;
using GbxRemoteNet.XmlRpc;

namespace GbxRemoteNet;

/// <summary>
/// Method Category: Votes
/// </summary>
public partial class GbxRemoteClient
{

    public async Task<bool> CallVoteAsync(string cmd)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("CallVote", cmd)
        );
    }

    public async Task<bool> CallVoteExAsync(string cmd, double ratio, int timeout, int who)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("CallVoteEx", cmd, ratio, timeout, who)
        );
    }

    public async Task<bool> InternalCallVoteAsync()
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("InternalCallVote")
        );
    }

    public async Task<bool> CancelVoteVoteAsync()
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("CancelVoteVote")
        );
    }

    public async Task<TmCurrentCallVote> GetCurrentCallVoteAsync()
    {
        return (TmCurrentCallVote) XmlRpcTypes.ToNativeValue<TmCurrentCallVote>(
            await CallOrFaultAsync("GetCurrentCallVote")
        );
    }

    public async Task<bool> SetCallVoteTimeOutAsync(int timeout)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("SetCallVoteTimeOut", timeout)
        );
    }

    public async Task<TmCurrentNextValue<int>> GetCallVoteTimeOutAsync()
    {
        return (TmCurrentNextValue<int>) XmlRpcTypes.ToNativeValue<TmCurrentNextValue<int>>(
            await CallOrFaultAsync("GetCallVoteTimeOut")
        );
    }

    public async Task<bool> SetCallVoteRatioAsync(double ratio)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("SetCallVoteRatio", ratio)
        );
    }

    public async Task<double> GetCallVoteRatioAsync()
    {
        return (double) XmlRpcTypes.ToNativeValue<double>(
            await CallOrFaultAsync("GetCallVoteRatio")
        );
    }

    public async Task<bool> SetCallVoteRatiosAsync(TmCallVoteRatio[] ratios)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("SetCallVoteRatios", ratios)
        );
    }

    public async Task<TmCallVoteRatio[]> GetCallVoteRatiosAsync()
    {
        return (TmCallVoteRatio[]) XmlRpcTypes.ToNativeValue<TmCallVoteRatio>(
            await CallOrFaultAsync("GetCallVoteRatios")
        );
    }
}