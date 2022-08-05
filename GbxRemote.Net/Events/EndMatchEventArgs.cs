using System;
using GbxRemoteNet.Structs;

namespace GbxRemoteNet.Events;

public class EndMatchEventArgs : EventArgs
{
    /// <summary>
    /// Array containing the ranking results of the match.
    /// </summary>
    public TmSPlayerRanking[] Rankings { get; set; }
    /// <summary>
    /// The ID of the team that won the match if the Teams game-mode is played.
    /// </summary>
    public int WinnerTeam { get; set; }
}