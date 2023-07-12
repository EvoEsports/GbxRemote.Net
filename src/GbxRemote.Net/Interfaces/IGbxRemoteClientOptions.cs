namespace GbxRemoteNet.Interfaces;

public interface IGbxRemoteClientOptions
{
    /// <summary>
    ///     Number of times to re-try connection if it fails.
    /// </summary>
    public int ConnectionRetries { get; set; }

    /// <summary>
    ///     Milliseconds to wait before re-trying connection.
    /// </summary>
    public int ConnectionRetryTimeout { get; set; }

    /// <summary>
    ///     If true, OnModeScriptCallback is triggered in addition when the callback
    ///     is a response for a ModeScript method call.
    /// </summary>
    public bool InvokeEventOnModeScriptMethodResponse { get; set; }
}