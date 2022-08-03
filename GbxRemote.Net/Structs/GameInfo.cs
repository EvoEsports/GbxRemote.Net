using System;

namespace GbxRemoteNet.Structs;

[Obsolete]
public class GameInfo
{
    /**
        * Game Modes
        */
    private const int GAMEMODE_SCRIPT = 0;

    private const int GAMEMODE_ROUNDS = 1;
    private const int GAMEMODE_TIMEATTACK = 2;
    private const int GAMEMODE_TEAM = 3;
    private const int GAMEMODE_LAPS = 4;
    private const int GAMEMODE_CUP = 5;
    private const int GAMEMODE_STUNTS = 6;

    public int GameMode { get; set; }
    public string ScriptName { get; set; }
    public int NbMaps { get; set; }
    public int ChatTime { get; set; }
    public int FinishTimeout { get; set; }
    public int AllWarmUpDuration { get; set; }
    public bool DisableRespawn { get; set; }
    public int ForceShowAllOpponents { get; set; }
    public int RoundsPointsLimit { get; set; }
    public int RoundsForcedLaps { get; set; }
    public bool RoundsUseNewRules { get; set; }
    public int RoundsPointsLimitNewRules { get; set; }
    public int TeamPointsLimit { get; set; }
    public int TeamMaxPoints { get; set; }
    public bool TeamUseNewRules { get; set; }
    public int TeamPointsLimitNewRules { get; set; }
    public int TimeAttackLimit { get; set; }
    public int TimeAttackSynchStartPeriod { get; set; }
    public int LapsNbLaps { get; set; }
    public int LapsTimeLimit { get; set; }
    public int CupPointsLimit { get; set; }
    public int CupRoundsPerMap { get; set; }
    public int CupNbWinners { get; set; }
    public int CupWarmUpDuration { get; set; }
}