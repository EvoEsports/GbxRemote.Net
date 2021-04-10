namespace GbxRemoteNet.Structs {
    public class GameInfo {
        /**
        * Game Modes
        */
        const int GAMEMODE_SCRIPT = 0;
        const int GAMEMODE_ROUNDS = 1;
        const int GAMEMODE_TIMEATTACK = 2;
        const int GAMEMODE_TEAM = 3;
        const int GAMEMODE_LAPS = 4;
        const int GAMEMODE_CUP = 5;
        const int GAMEMODE_STUNTS = 6;

        public int GameMode;
        public string ScriptName;
        public int NbMaps;
        public int ChatTime;
        public int FinishTimeout;
        public int AllWarmUpDuration;
        public bool DisableRespawn;
        public int ForceShowAllOpponents;
        public int RoundsPointsLimit;
        public int RoundsForcedLaps;
        public bool RoundsUseNewRules;
        public int RoundsPointsLimitNewRules;
        public int TeamPointsLimit;
        public int TeamMaxPoints;
        public bool TeamUseNewRules;
        public int TeamPointsLimitNewRules;
        public int TimeAttackLimit;
        public int TimeAttackSynchStartPeriod;
        public int LapsNbLaps;
        public int LapsTimeLimit;
        public int CupPointsLimit;
        public int CupRoundsPerMap;
        public int CupNbWinners;
        public int CupWarmUpDuration;
    }
}