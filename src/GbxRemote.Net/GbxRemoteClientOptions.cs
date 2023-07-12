using GbxRemoteNet.Interfaces;

namespace GbxRemoteNet;

public class GbxRemoteClientOptions : IGbxRemoteClientOptions
{
    public int ConnectionRetries { get; set; } = 0;
    public int ConnectionRetryTimeout { get; set; } = 1000;
    public bool InvokeEventOnModeScriptMethodResponse { get; set; } = false;
}