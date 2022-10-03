namespace GbxRemoteNet.Events;

public class VoteUpdatedEventArgs : PlayerEventArgs
{
    /// <summary>
    /// Name of the state, can be: NewVote, VoteCancelled, VotePassed or VoteFailed
    /// </summary>
    public string StateName { get; set; }
    /// <summary>
    /// Command name.
    /// </summary>
    public string CmdName { get; set; }
    /// <summary>
    /// Command parameter.
    /// </summary>
    public string CmdParam { get; set; }
}