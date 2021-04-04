using GbxRemoteNet.Structs;
using GbxRemoteNet.XmlRpc;
using GbxRemoteNet.XmlRpc.ExtraTypes;
using GbxRemoteNet.XmlRpc.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GbxRemoteNet {
    public partial class GbxRemoteClient {
        /*
         * Methods Reference: https://wiki.trackmania.io/en/dedicated-server/XML-RPC/Methods
         */


        #region Replays

        /// <summary>
        /// Change the settings of the mode script. Only available to Admin.
        /// </summary>
        /// <param name="modescriptSettings"></param>
        /// <returns></returns>
        public async Task<bool> SetModeScriptSettingsAsync(Object modescriptSettings) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetModeScriptSettings", modescriptSettings)
            );

        /// <summary>
        /// Send commands to the mode script. Only available to Admin.
        /// </summary>
        /// <param name="commands"></param>
        /// <returns></returns>
        public async Task<bool> SendModeScriptCommandsAsync(Object commands) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SendModeScriptCommands", commands)
            );

        /// <summary>
        /// Change the settings and send commands to the mode script. Only available to Admin.
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="modeScript"></param>
        /// <returns></returns>
        public async Task<bool> SetModeScriptSettingsAndCommandsAsync(Object settings, Object modeScript) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetModeScriptSettingsAndCommands", settings, modeScript)
            );

        /// <summary>
        /// Returns the current xml-rpc variables of the mode script.
        /// </summary>
        /// <returns></returns>
        public async Task<Object> GetModeScriptVariablesAsync() =>
            (Object)XmlRpcTypes.ToNativeValue<Object>(
                await CallOrFaultAsync("GetModeScriptVariables")
            );

        /// <summary>
        /// Set the xml-rpc variables of the mode script. Only available to Admin.
        /// </summary>
        /// <param name="xmlRpcVar"></param>
        /// <returns></returns>
        public async Task<bool> SetModeScriptVariablesAsync(Object xmlRpcVar) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetModeScriptVariables", xmlRpcVar)
            );

        /// <summary>
        /// Send an event to the mode script. Only available to Admin. 
        /// </summary>
        /// <param name="modeScript"></param>
        /// <param name="eventName"></param>
        /// <returns></returns>
        public async Task<bool> TriggerModeScriptEventAsync(string modeScript, string eventName) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("TriggerModeScriptEvent", modeScript, eventName)
            );

        /// <summary>
        /// Send an event to the mode script. Only available to Admin. 
        /// </summary>
        /// <param name="modeScript"></param>
        /// <param name="events"></param>
        /// <returns></returns>
        public async Task<bool> TriggerModeScriptEventArrayAsync(string modeScript, Array events) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("TriggerModeScriptEventArray", modeScript, events)
            );

        /// <summary>
        /// Set the ServerPlugin settings. 
        /// </summary>
        /// <param name="forceReload">Whether to reload from disk</param>
        /// <param name="filename">OPTIONAL: Name the filename relative to Scripts/directory</param>
        /// <param name="script">OPTIONAL: The script #Settings to apply.</param>
        /// <returns></returns>
        public async Task<bool> SetServerPluginAsync(bool forceReload, string filename = null, Object script = null) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetServerPlugin", forceReload, filename, script)
            );

        /// <summary>
        /// Get the ServerPlugin current settings.
        /// </summary>
        /// <returns></returns>
        public async Task<Object> GetServerPluginAsync() =>
            (Object)XmlRpcTypes.ToNativeValue<Object>(
                await CallOrFaultAsync("GetServerPlugin")
            );

        /// <summary>
        /// Send an event to the server script. Only available to Admin.
        /// </summary>
        /// <returns></returns>
        public async Task<Object> GetServerPluginVariablesAsync() =>
            (Object)XmlRpcTypes.ToNativeValue<Object>(
                await CallOrFaultAsync("Returns the current xml-rpc variables of the server script.")
            );

        /// <summary>
        /// Send an event to the server script. Only available to Admin.
        /// </summary>
        /// <returns></returns>

        // TODO : Missing implementation due to lacking documentation on method.
        public async Task<bool> TriggerServerPluginEventAsync() =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("TriggerServerPluginEvent")
            );

        /// <summary>
        /// Send an event to the server script. Only available to Admin.
        /// </summary>
        /// <returns></returns>

        // TODO : Missing implementation due to lacking documentation on method.
        public async Task<bool> TriggerServerPluginEventArrayAsync() =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("TriggerServerPluginEventArray")
            );

        /// <summary>
        /// Get the script cloud variables of given object. Only available to Admin.
        /// </summary>
        /// <returns></returns>

        // TODO : Missing implementation due to lacking documentation on method.
        public async Task<bool> GetScriptCloudVariablesAsync() =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("GetScriptCloudVariables")
            );

        /// <summary>
        /// Set the script cloud variables of given object. Only available to Admin.
        /// </summary>
        /// <returns></returns>

        // TODO : Missing implementation due to lacking documentation on method.
        public async Task<bool> SetScriptCloudVariablesAsync() =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetScriptCloudVariables")
            );

        /// <summary>
        /// Restarts the map, with an optional boolean parameter DontClearCupScores (only available in cup mode). Only available to Admin.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> RestartMapAsync() =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("RestartMap")
            );

        /// <summary>
        /// Switch to next map, with an optional boolean parameter DontClearCupScores (only available in cup mode). Only available to Admin.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> NextMapAsync() =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("NextMap")
            );

        /// <summary>
        /// Attempt to balance teams. Only available to Admin.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> AutoTeamBalanceAsync() =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("AutoTeamBalance")
            );

        /// <summary>
        /// Stop the server. Only available to SuperAdmin.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> StopServerAsync() =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("StopServer")
            );

        /// <summary>
        /// In Rounds or Laps mode, force the end of round without waiting for all players to giveup/finish. Only available to Admin.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> ForceEndRoundAsync() =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("ForceEndRound")
            );

        /// <summary>
        /// Set new game settings using the struct passed as parameters. This struct must contain the following fields :
        /// GameMode, ChatTime, RoundsPointsLimit, RoundsUseNewRules, RoundsForcedLaps, TimeAttackLimit, TimeAttackSynchStartPeriod, TeamPointsLimit, TeamMaxPoints, TeamUseNewRules, LapsNbLaps, LapsTimeLimit, FinishTimeout,
        /// and optionally: AllWarmUpDuration, DisableRespawn, ForceShowAllOpponents, RoundsPointsLimitNewRules, TeamPointsLimitNewRules, CupPointsLimit, CupRoundsPerMap, CupNbWinners, CupWarmUpDuration.
        /// Only available to Admin. Requires a map restart to be taken into account.
        /// </summary>
        /// <param name="gameInfo"></param>
        /// <returns></returns>
        public async Task<bool> SetGameInfos(GameInfoStruct gameInfo) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetGameInfos", gameInfo)
            );

        /// <summary>
        /// Returns a struct containing the current game settings.
        /// </summary>
        /// <returns></returns>
        public async Task<GameInfoStruct> GetCurrentGameInfoAsync() =>
            (GameInfoStruct)XmlRpcTypes.ToNativeValue<GameInfoStruct>(
                await CallOrFaultAsync("GetCurrentGameInfo")
            );

        /// <summary>
        /// Returns a struct containing the game settings for the next map.
        /// </summary>
        /// <returns></returns>
        public async Task<GameInfoStruct> GetNextGameInfoAsync() =>
            (GameInfoStruct)XmlRpcTypes.ToNativeValue<GameInfoStruct>(
                await CallOrFaultAsync("GetNextGameInfo")
            );
        
        /// <summary>
        /// Returns a struct containing two other structures, the first containing the current game settings and the second the game settings for next map. The first structure is named CurrentGameInfos and the second NextGameInfos.
        /// </summary>
        /// <returns></returns>
        public async Task<CurrentNextValueStruct<GameInfoStruct>> GetGameInfosAsync() =>
            (CurrentNextValueStruct<GameInfoStruct>)XmlRpcTypes.ToNativeValue<CurrentNextValueStruct<GameInfoStruct>>(
                await CallOrFaultAsync("GetGameInfos")
            );

        /// <summary>
        /// Set a new game mode between Script (0), Rounds (1), TimeAttack (2), Team (3), Laps (4), Cup (5) and Stunts (6). Only available to Admin. Requires a map restart to be taken into account.
        /// </summary>
        /// <param name="gamemode">Script (0), Rounds (1), TimeAttack (2), Team (3), Laps (4), Cup (5) and Stunts (6).</param>
        /// <returns></returns>
        public async Task<bool> SetGameModeAsync(int gamemode) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetGameMode", gamemode)
            );
        
        /// <summary>
        /// Get the current game mode.
        /// </summary>
        /// <returns></returns>
        public async Task<int> GetGamemodeAsync() =>
            (int)XmlRpcTypes.ToNativeValue<int>(
                await CallOrFaultAsync("GetGameMode")
            );

        /// <summary>
        /// Set a new chat time value in milliseconds (actually 'chat time' is the duration of the end race podium). Only available to Admin.
        /// </summary>
        /// <param name="chatTime">0 means no podium displayed.</param>
        /// <returns></returns>
        public async Task<bool> SetChatTimeAsync(int chatTime) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetChatTime", chatTime)
            );
        
        /// <summary>
        /// Get the current and next chat time. The struct returned contains two fields CurrentValue and NextValue.
        /// </summary>
        /// <returns></returns>
        public async Task<CurrentNextValueStruct<int>> GetChatTimeAsync() =>
            (CurrentNextValueStruct<int>)XmlRpcTypes.ToNativeValue<CurrentNextValueStruct<int>>(
                await CallOrFaultAsync("GetChatTime")
            );

        /// <summary>
        /// Set a new finish timeout (for rounds/laps mode) value in milliseconds. Only available to Admin. Requires a map restart to be taken into account.
        /// </summary>
        /// <param name="finishTimeout">0 means default. 1 means adaptative to the duration of the map.</param>
        /// <returns></returns>
        public async Task<bool> SetFinishTimeoutAsync(int finishTimeout) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetFinishTimeout", finishTimeout)
            );
        
        /// <summary>
        /// Get the current and next FinishTimeout. The struct returned contains two fields CurrentValue and NextValue.
        /// </summary>
        /// <returns></returns>
        public async Task<CurrentNextValueStruct<int>> GetFinishTimeoutAsync() =>
            (CurrentNextValueStruct<int>)XmlRpcTypes.ToNativeValue<CurrentNextValueStruct<int>>(
                await CallOrFaultAsync("GetFinishTimeout")
            );

        /// <summary>
        /// Set whether to enable the automatic warm-up phase in all modes. Only available to Admin. Requires a map restart to be taken into account.
        /// </summary>
        /// <param name="warmupDuration">0 = no, otherwise it's the duration of the phase, expressed in number of rounds (in rounds/team mode), or in number of times the gold medal time (other modes).</param>
        /// <returns></returns>
        public async Task<bool> SetAllWarmUpDurationAsync(int warmupDuration) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetAllWarmUpDuration", warmupDuration)
            );
        
        /// <summary>
        /// Get whether the automatic warm-up phase is enabled in all modes. The struct returned contains two fields CurrentValue and NextValue.
        /// </summary>
        /// <returns></returns>
        public async Task<CurrentNextValueStruct<int>> GetAllWarmUpDurationAsync() =>
            (CurrentNextValueStruct<int>)XmlRpcTypes.ToNativeValue<CurrentNextValueStruct<int>>(
                await CallOrFaultAsync("GetAllWarmUpDuration")
            );

        /// <summary>
        /// Set whether to disallow players to respawn. Only available to Admin. Requires a map restart to be taken into account.
        /// </summary>
        /// <param name="disableRespawn"></param>
        /// <returns></returns>
        public async Task<bool> SetDisableRespawnAsync(bool disableRespawn) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetDisableRespawn", disableRespawn)
            );
        
        /// <summary>
        /// Get whether players are disallowed to respawn. The struct returned contains two fields CurrentValue and NextValue.
        /// </summary>
        /// <returns></returns>
        public async Task<CurrentNextValueStruct<bool>> GetDisableRespawnAsync() =>
            (CurrentNextValueStruct<bool>)XmlRpcTypes.ToNativeValue<CurrentNextValueStruct<bool>>(
                await CallOrFaultAsync("GetDisableRespawn")
            );

        /// <summary>
        /// Set whether to override the players preferences and always display all opponents). Only available to Admin. Requires a map restart to be taken into account.
        /// </summary>
        /// <param name="playersPreferenceOverride">0=no override, 1=show all, other value=minimum number of opponents</param>
        /// <returns></returns>
        public async Task<bool> SetForceShowAllOpponentsAsync(int playersPreferenceOverride) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetForceShowAllOpponents", playersPreferenceOverride)
            );
        
        /// <summary>
        /// Get whether players are forced to show all opponents. The struct returned contains two fields CurrentValue and NextValue.
        /// </summary>
        /// <returns></returns>
        public async Task<CurrentNextValueStruct<int>> GetForceShowAllOpponentsAsync() =>
            (CurrentNextValueStruct<int>)XmlRpcTypes.ToNativeValue<CurrentNextValueStruct<int>>(
                await CallOrFaultAsync("GetTimeAttackLimit")
            );

        /// <summary>
        /// Set a new mode script name for script mode. Only available to Admin. Requires a map restart to be taken into account.
        /// </summary>
        /// <param name="scriptName"></param>
        /// <returns></returns>
        public async Task<bool> SetScriptNameAsync(string scriptName) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetScriptName", scriptName)
            );
        
        /// <summary>
        /// Get the current and next mode script name for script mode. The struct returned contains two fields CurrentValue and NextValue.
        /// </summary>
        /// <returns></returns>
        public async Task<CurrentNextValueStruct<string>> GetScriptNameAsync() =>
            (CurrentNextValueStruct<string>)XmlRpcTypes.ToNativeValue<CurrentNextValueStruct<string>>(
                await CallOrFaultAsync("GetScriptName")
            );

        /// <summary>
        /// Set a new time limit for laps mode. Only available to Admin. Requires a map restart to be taken into account.
        /// </summary>
        /// <param name="timelimit"></param>
        /// <returns></returns>
        public async Task<bool> SetTimeAttackLimitAsync(int timelimit) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetTimeAttackLimit", timelimit)
            );
        
        /// <summary>
        /// Get the current and next time limit for time attack mode. The struct returned contains two fields CurrentValue and NextValue.
        /// </summary>
        /// <returns></returns>
        public async Task<CurrentNextValueStruct<int>> GetTimeAttackLimitAsync() =>
            (CurrentNextValueStruct<int>)XmlRpcTypes.ToNativeValue<CurrentNextValueStruct<int>>(
                await CallOrFaultAsync("GetTimeAttackLimit")
            );

        /// <summary>
        /// Set a new synchronized start period for time attack mode. Only available to Admin. Requires a map restart to be taken into account.
        /// </summary>
        /// <param name="syncStartPeriod"></param>
        /// <returns></returns>
        public async Task<bool> SetTimeAttackSynchStartPeriodAsync(int syncStartPeriod) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetTimeAttackSynchStartPeriod", syncStartPeriod)
            );
        
        /// <summary>
        /// Get the current and synchronized start period for time attack mode. The struct returned contains two fields CurrentValue and NextValue.
        /// </summary>
        /// <returns></returns>
        public async Task<CurrentNextValueStruct<int>> GetTimeAttackSynchStartPeriodAsync() =>
            (CurrentNextValueStruct<int>)XmlRpcTypes.ToNativeValue<CurrentNextValueStruct<int>>(
                await CallOrFaultAsync("GetTimeAttackSynchStartPeriod")
            );

        /// <summary>
        /// Set a new time limit for laps mode. Only available to Admin. Requires a map restart to be taken into account.
        /// </summary>
        /// <param name="timelimit"></param>
        /// <returns></returns>
        public async Task<bool> SetLapsTimeLimitAsync(int timelimit) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetLapsTimeLimit", timelimit)
            );
        
        /// <summary>
        /// Get the current and next number of laps for rounds mode. The struct returned contains two fields CurrentValue and NextValue.
        /// </summary>
        /// <returns></returns>
        public async Task<CurrentNextValueStruct<int>> GetLapsTimeLimitAsync() =>
            (CurrentNextValueStruct<int>)XmlRpcTypes.ToNativeValue<CurrentNextValueStruct<int>>(
                await CallOrFaultAsync("GetLapsTimeLimit")
            );

        /// <summary>
        /// Set a new number of laps for laps mode. Only available to Admin. Requires a map restart to be taken into account.
        /// </summary>
        /// <param name="nbrLaps"></param>
        /// <returns></returns>
        public async Task<bool> SetNbLapsAsync(int nbrLaps) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetNbLaps", nbrLaps)
            );
        
        /// <summary>
        /// Get the current and next number of laps for laps mode. The struct returned contains two fields CurrentValue and NextValue.
        /// </summary>
        /// <returns></returns>
        public async Task<CurrentNextValueStruct<int>> GetNbLapsAsync() =>
            (CurrentNextValueStruct<int>)XmlRpcTypes.ToNativeValue<CurrentNextValueStruct<int>>(
                await CallOrFaultAsync("GetNbLaps")
            );

        /// <summary>
        /// Set a new number of laps for rounds mode. Only available to Admin. Requires a map restart to be taken into account.
        /// </summary>
        /// <param name="nbrLaps">0 = default, use the number of laps from the maps, otherwise forces the number of rounds for multilaps maps</param>
        /// <returns></returns>
        public async Task<bool> SetRoundForcedLapsAsync(int nbrLaps) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetRoundForcedLaps", nbrLaps)
            );
        
        /// <summary>
        /// 	Get the current and next number of laps for rounds mode. The struct returned contains two fields CurrentValue and NextValue.
        /// </summary>
        /// <returns></returns>
        public async Task<CurrentNextValueStruct<int>> GetRoundForcedLapsAsync() =>
            (CurrentNextValueStruct<int>)XmlRpcTypes.ToNativeValue<CurrentNextValueStruct<int>>(
                await CallOrFaultAsync("GetRoundForcedLaps")
            );

        /// <summary>
        /// Set a new points limit for rounds mode (value set depends on UseNewRulesRound). Only available to Admin. Requires a map restart to be taken into account.
        /// </summary>
        /// <param name="limit"></param>
        /// <returns></returns>
        public async Task<bool> SetRoundPointsLimitAsync(int limit) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetRoundPointsLimit", limit)
            );
        
        /// <summary>
        /// Get the current and next points limit for rounds mode (values returned depend on UseNewRulesRound). The struct returned contains two fields CurrentValue and NextValue.
        /// </summary>
        /// <returns></returns>
        public async Task<CurrentNextValueStruct<string>> GetRoundPointsLimitAsync() =>
            (CurrentNextValueStruct<string>)XmlRpcTypes.ToNativeValue<CurrentNextValueStruct<string>>(
                await CallOrFaultAsync("GetRoundPointsLimit")
            );

        /// <summary>
        /// Set the points used for the scores in rounds mode. Points is an array of decreasing integers for the players from the first to last.
        /// And you can add an optional boolean to relax the constraint checking on the scores. Only available to Admin.
        /// </summary>
        /// <param name="points">An array of points which should be sorted in a descending order from first to last</param>
        /// <param name="scoreConstraintCheck">Optional boolean to relax the constraint checking on the scores. </param>
        /// <returns></returns>
        public async Task<bool> SetRoundCustomPointsAsync(int[] points, bool scoreConstraintCheck = false) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetRoundCustomPoints", points, scoreConstraintCheck)
            );
        
        /// <summary>
        /// Gets the points used for the scores in rounds mode.
        /// </summary>
        /// <returns></returns>
        public async Task<int[]> GetRoundCustomPointsAsync() =>
            (int[])XmlRpcTypes.ToNativeValue<int[]>(
                await CallOrFaultAsync("GetRoundCustomPoints")
            );

        /// <summary>
        /// Set if new rules are used for rounds mode. Only available to Admin. Requires a map restart to be taken into account.
        /// </summary>
        /// <param name="newRules"></param>
        /// <returns></returns>
        public async Task<bool> SetUseNewRulesRoundAsync(bool newRules) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetUseNewRulesRound", newRules)
            );
        
        /// <summary>
        /// Get if the new rules are used for rounds mode (Current and next values). The struct returned contains two fields CurrentValue and NextValue.
        /// </summary>
        /// <returns></returns>
        public async Task<CurrentNextValueStruct<string>> GetUseNewRulesRoundAsync() =>
            (CurrentNextValueStruct<string>)XmlRpcTypes.ToNativeValue<CurrentNextValueStruct<string>>(
                await CallOrFaultAsync("GetUseNewRulesRound")
            );

        /// <summary>
        /// Set a new number of maximum points per round for team mode. Only available to Admin. Requires a map restart to be taken into account.
        /// </summary>
        /// <param name="maxPoints"></param>
        /// <returns></returns>
        public async Task<bool> SetMaxPointsTeamAsync(int maxPoints) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetMaxPointsTeam", maxPoints)
            );
        
        /// <summary>
        /// Get the current and next number of maximum points per round for team mode. The struct returned contains two fields CurrentValue and NextValue.
        /// </summary>
        /// <returns></returns>
        public async Task<CurrentNextValueStruct<int>> GetMaxPointsTeamAsync() =>
            (CurrentNextValueStruct<int>)XmlRpcTypes.ToNativeValue<CurrentNextValueStruct<int>>(
                await CallOrFaultAsync("GetMaxPointsTeam")
            );
        
        /// <summary>
        /// Set if new rules are used for team mode. Only available to Admin. Requires a map restart to be taken into account.
        /// </summary>
        /// <param name="newRules"></param>
        /// <returns></returns>
        public async Task<bool> SetUseNewRulesTeamAsync(bool newRules) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetUseNewRulesTeam", newRules)
            );
        
        /// <summary>
        /// Get if the new rules are used for team mode (Current and next values). The struct returned contains two fields CurrentValue and NextValue.
        /// </summary>
        /// <returns></returns>
        public async Task<CurrentNextValueStruct<string>> GetUseNewRulesTeamAsync() =>
            (CurrentNextValueStruct<string>)XmlRpcTypes.ToNativeValue<CurrentNextValueStruct<string>>(
                await CallOrFaultAsync("GetUseNewRulesTeam")
            );

        /// <summary>
        /// Set the points needed for victory in Cup mode. Only available to Admin. Requires a map restart to be taken into account.
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public async Task<bool> SetCupPointsLimitAsync(int points) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetCupPointsLimit", points)
            );

        /// <summary>
        /// Get the points needed for victory in Cup mode. The struct returned contains two fields CurrentValue and NextValue.
        /// </summary>
        /// <returns></returns>
        public async Task<CurrentNextValueStruct<int>> GetCupPointsLimitAsync() =>
            (CurrentNextValueStruct<int>)XmlRpcTypes.ToNativeValue<CurrentNextValueStruct<int>>(
                await CallOrFaultAsync("GetCupPointsLimit")
            );

        /// <summary>
        /// Sets the number of rounds before going to next map in Cup mode. Only available to Admin. Requires a map restart to be taken into account.
        /// </summary>
        /// <param name="nbrRounds"></param>
        /// <returns></returns>
        public async Task<bool> SetCupRoundsPerMapAsync(int nbrRounds) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetCupRoundsPerMap", nbrRounds)
            );
        
        /// <summary>
        /// Get the number of rounds before going to next map in Cup mode. The struct returned contains two fields CurrentValue and NextValue.
        /// </summary>
        /// <returns></returns>
        public async Task<CurrentNextValueStruct<int>> GetCupRoundsPerMapAsync() =>
            (CurrentNextValueStruct<int>)XmlRpcTypes.ToNativeValue<CurrentNextValueStruct<int>>(
                await CallOrFaultAsync("GetCupRoundsPerMap")
            );

        /// <summary>
        /// Set whether to enable the automatic warm-up phase in Cup mode. 0 = no, otherwise it's the duration of the phase, expressed in number of rounds. Only available to Admin. Requires a map restart to be taken into account.
        /// </summary>
        /// <param name="duration">0 = no, otherwise it's the duration of the phase, expressed in number of rounds.</param>
        /// <returns></returns>
        public async Task<bool> SetCupWarmUpDurationAsync(int duration) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetCupWarmUpDuration", duration)
            );

        /// <summary>
        /// Get whether the automatic warm-up phase is enabled in Cup mode. The struct returned contains two fields CurrentValue and NextValue.
        /// </summary>
        /// <returns></returns>
        public async Task<CurrentNextValueStruct<int>> GetCupWarmUpDurationAsync() =>
            (CurrentNextValueStruct<int>)XmlRpcTypes.ToNativeValue<CurrentNextValueStruct<int>>(
                await CallOrFaultAsync("GetCupWarmUpDuration")
            );

        /// <summary>
        /// Set the number of winners to determine before the match is considered over. Only available to Admin. Requires a map restart to be taken into account.
        /// </summary>
        /// <param name="nbWinners">Number of winners</param>
        /// <returns></returns>
        public async Task<bool> SetCupNbWinnersAsync(int nbWinners) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetCupNbWinners", nbWinners)
            );

        /// <summary>
        /// Get the number of winners to determine before the match is considered over. The struct returned contains two fields CurrentValue and NextValue.
        /// </summary>
        /// <returns></returns>
        public async Task<CurrentNextValueStruct<int>> GetCupNbWinnersAsync() =>
            (CurrentNextValueStruct<int>)XmlRpcTypes.ToNativeValue<CurrentNextValueStruct<int>>(
                await CallOrFaultAsync("GetCupNbWinners")
            );

        /// <summary>
        /// Returns the current map index in the selection, or -1 if the map is no longer in the selection.
        /// </summary>
        /// <returns></returns>
        public async Task<int> GetCurrentMapIndexAsync() =>
            (int)XmlRpcTypes.ToNativeValue<int>(
                await CallOrFaultAsync("GetCurrentMapIndex")
            );
        
        /// <summary>
        /// Returns the map index in the selection that will be played next (unless the current one is restarted...)
        /// </summary>
        /// <returns></returns>
        public async Task<int> GetNextMapIndexAsync() =>
            (int)XmlRpcTypes.ToNativeValue<int>(
                await CallOrFaultAsync("GetNextMapIndex")
            );

        /// <summary>
        /// Sets the map index in the selection that will be played next (unless the current one is restarted...)
        /// </summary>
        /// <param name="mapIndex"></param>
        /// <returns></returns>
        public async Task<bool> SetNextMapIndexAsync(int mapIndex) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetNextMapIndex")
            );
        
        /// <summary>
        /// Immediately jumps to the map designated by its identifier (it must be in the selection).
        /// </summary>
        /// <param name="mapId"></param>
        /// <returns></returns>
        public async Task<bool> SetNextMapIdentAsync(string mapId) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetNextMapIdent", mapId)
            );

        /// <summary>
        /// Immediately jumps to the map designated by the index in the selection.
        /// </summary>
        /// <param name="mapIndex"></param>
        /// <returns></returns>
        public async Task<bool> JumpToMapIndexAsync(int mapIndex) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("JumpToMapIndex")
            );
        
        /// <summary>
        /// Immediately jumps to the map designated by its identifier (it must be in the selection).
        /// </summary>
        /// <param name="mapId"></param>
        /// <returns></returns>
        public async Task<bool> JumpToMapIdentAsync(string mapId) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("JumpToMapIdent", mapId)
            );

        /// <summary>
        /// Returns a struct containing the infos for the current map. The struct contains the following fields : Name, UId, FileName, Author, Environnement, Mood, BronzeTime, SilverTime, GoldTime, AuthorTime, CopperPrice, LapRace, MapType, MapStyle.
        /// (NbLaps and NbCheckpoints are also present but always set to -1)
        /// </summary>
        /// <returns></returns>
        public async Task<MapStruct> GetCurrentMapInfoAsync() =>
            (MapStruct)XmlRpcTypes.ToNativeValue<MapStruct>(
                await CallOrFaultAsync("GetCurrentMapInfo")
            );

        /// <summary>
        /// Returns a struct containing the infos for the next map. The struct contains the following fields : Name, UId, FileName, Author, Environnement, Mood, BronzeTime, SilverTime, GoldTime, AuthorTime, CopperPrice, LapRace, MapType, MapStyle.
        /// (NbLaps and NbCheckpoints are also present but always set to -1)
        /// </summary>
        /// <returns></returns>
        public async Task<MapStruct> GetNextMapInfoAsync() =>
            (MapStruct)XmlRpcTypes.ToNativeValue<MapStruct>(
                await CallOrFaultAsync("GetNextMapInfo")
            );

        /// <summary>
        /// Returns a struct containing the infos for the map with the specified filename. The struct contains the following fields : Name, UId, FileName, Author, Environnement, Mood, BronzeTime, SilverTime, GoldTime, AuthorTime, CopperPrice, LapRace, MapType, MapStyle.
        /// (NbLaps and NbCheckpoints are also present but always set to -1)
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public async Task<MapStruct> GetMapInfoAsync(string filename) =>
            (MapStruct)XmlRpcTypes.ToNativeValue<MapStruct>(
                await CallOrFaultAsync("GetMapInfo", filename)
            );
        
        /// <summary>
        /// Returns a boolean if the map with the specified filename matches the current server settings.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public async Task<bool> CheckMapForCurrentServerParamsAsync(string filename) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("CheckMapForCurrentServerParams", filename)
            );

        /// <summary>
        /// Returns a list of maps among the current selection of the server. This method take two parameters. The first parameter specifies the maximum number of infos to be returned, and the second one the starting index in the selection.
        /// The list is an array of structures. Each structure contains the following fields : Name, UId, FileName, Environnement, Author, GoldTime, CopperPrice, MapType, MapStyle.
        /// </summary>
        /// <param name="maxInfos"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public async Task<MapStruct[]> GetMapListAsync(int maxInfos, int startIndex) =>
            (MapStruct[])XmlRpcTypes.ToNativeValue<MapStruct[]>(
                await CallOrFaultAsync("GetMapList", maxInfos, startIndex)
            );

        /// <summary>
        /// Add the map with the specified filename at the end of the current selection. Only available to Admin.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public async Task<bool> AddMapAsync(string filename) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("AddMap", filename)
            );

        /// <summary>
        /// Add the list of maps with the specified filenames at the end of the current selection. The list of maps to add is an array of strings. Only available to Admin.
        /// </summary>
        /// <param name="filenames"></param>
        /// <returns></returns>
        public async Task<int> AddMapListAsync(Array filenames) =>
            (int)XmlRpcTypes.ToNativeValue<int>(
                await CallOrFaultAsync("AddMapList", filenames)
            );

        /// <summary>
        /// Remove the map with the specified filename from the current selection. Only available to Admin.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public async Task<bool> RemoveMapAsync(string filename) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("RemoveMap", filename)
            );

        /// <summary>
        /// Remove the list of maps with the specified filenames from the current selection. The list of maps to remove is an array of strings. Only available to Admin.
        /// </summary>
        /// <param name="filenames"></param>
        /// <returns></returns>
        public async Task<int> RemoveMapListAsync(Array filenames) =>
            (int)XmlRpcTypes.ToNativeValue<int>(
                await CallOrFaultAsync("RemoveMapList", filenames)
            );

        /// <summary>
        /// Insert the map with the specified filename after the current map. Only available to Admin.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public async Task<bool> InsertMapAsync(string filename) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("InsertMap", filename)
            );
        
        /// <summary>
        /// Insert the list of maps with the specified filenames after the current map. The list of maps to insert is an array of strings. Only available to Admin.
        /// </summary>
        /// <param name="filenames"></param>
        /// <returns></returns>
        public async Task<int> InsertMapListAsync(Array filenames) =>
            (int)XmlRpcTypes.ToNativeValue<int>(
                await CallOrFaultAsync("InsertMapList", filenames)
            );

        /// <summary>
        /// Set as next map the one with the specified filename, if it is present in the selection. Only available to Admin.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public async Task<bool> ChooseNextMapAsync(string filename) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("ChooseNextMap", filename)
            );

        /// <summary>
        /// Set as next maps the list of maps with the specified filenames, if they are present in the selection. The list of maps to choose is an array of strings. Only available to Admin.
        /// </summary>
        /// <param name="filenames"></param>
        /// <returns></returns>
        public async Task<int> ChooseNextMapListAsync(Array filenames) =>
            (int)XmlRpcTypes.ToNativeValue<int>(
                await CallOrFaultAsync("ChooseNextMapList", filenames)
            );

        /// <summary>
        /// Set a list of maps defined in the playlist with the specified filename as the current selection of the server, and load the gameinfos from the same file. Only available to Admin.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public async Task<int> LoadMatchSettingsAsync(string filename) =>
            (int)XmlRpcTypes.ToNativeValue<int>(
                await CallOrFaultAsync("LoadMatchSettings", filename)
            );

        /// <summary>
        /// Add a list of maps defined in the playlist with the specified filename at the end of the current selection. Only available to Admin.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public async Task<int> AppendPlaylistFromMatchSettingsAsync(string filename) =>
            (int)XmlRpcTypes.ToNativeValue<int>(
                await CallOrFaultAsync("AppendPlaylistFromMatchSettings", filename)
            );

        /// <summary>
        /// Save the current selection of map in the playlist with the specified filename, as well as the current gameinfos. Only available to Admin.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public async Task<int> SaveMatchSettingsAsync(string filename) =>
            (int)XmlRpcTypes.ToNativeValue<int>(
                await CallOrFaultAsync("SaveMatchSettings", filename)
            );

        /// <summary>
        /// Returns the list of players on the server. This method take two parameters. The first parameter specifies the maximum number of infos to be returned, and the second one the starting index in the list, an optional 3rd parameter is used for compatibility: struct version (0 = united, 1 = forever, 2 = forever, including the servers).
        /// The list is an array of PlayerInfo structures. Forever PlayerInfo struct is: Login, NickName, PlayerId, TeamId, SpectatorStatus, LadderRanking, and Flags.
        /// LadderRanking is 0 when not in official mode,
        /// Flags = ForceSpectator(0,1,2) + IsReferee * 10 + IsPodiumReady * 100 + StereoDisplayMode * 1000 + IsManagedByAnOtherServer * 10000 + IsServer * 100000 + HasPlayerSlot * 1000000 + IsBroadcasting * 10000000 + HasJoinedGame * 100000000
        /// SpectatorStatus = Spectator + TemporarySpectator * 10 + PureSpectator * 100 + AutoTarget * 1000 + CurrentTargetId * 10000
        /// </summary>
        /// <param name="maxInfos"></param>
        /// <param name="startIndex"></param>
        /// <param name="serverType">OPTIONAL: Used for compatibility: struct version (0 = united, 1 = forever, 2 = forever, including the servers)</param>
        /// <returns></returns>
        public async Task<PlayerInfoStruct[]> GetPlayerListAsync(int maxInfos, int startIndex, int? serverType = -1) =>
            (PlayerInfoStruct[])XmlRpcTypes.ToNativeValue<PlayerInfoStruct[]>(
                await CallOrFaultAsync("GetPlayerList", maxInfos, startIndex, serverType)
            );

        /// <summary>
        /// Returns a struct containing the infos on the player with the specified login, with an optional parameter for compatibility: struct version (0 = united, 1 = forever). The structure is identical to the ones from GetPlayerList. Forever PlayerInfo struct is: Login, NickName, PlayerId, TeamId, SpectatorStatus, LadderRanking, and Flags.
        /// LadderRanking is 0 when not in official mode,
        /// Flags = ForceSpectator(0,1,2) + IsReferee * 10 + IsPodiumReady * 100 + StereoDisplayMode * 1000 + IsManagedByAnOtherServer * 10000 + IsServer * 100000 + HasPlayerSlot * 1000000 + IsBroadcasting * 10000000 + HasJoinedGame * 100000000
        /// SpectatorStatus = Spectator + TemporarySpectator * 10 + PureSpectator * 100 + AutoTarget * 1000 + CurrentTargetId * 10000
        /// Each structure of the array Skins contains two fields Environnement and a struct PackDesc. Each structure PackDesc, as well as the struct Avatar, contains two fields FileName and Checksum.
        /// </summary>
        /// <param name="playerLogin"></param>
        /// <param name="serverType"></param>
        /// <returns></returns>
        public async Task<PlayerInfoStruct> GetPlayerInfoAsync(string playerLogin, int serverType) =>
            (PlayerInfoStruct)XmlRpcTypes.ToNativeValue<PlayerInfoStruct>(
                await CallOrFaultAsync("GetPlayerInfo", playerLogin, serverType)
            );

        /// <summary>
        /// Returns a struct containing the infos on the player with the specified login. The structure contains the following fields : Login, NickName, PlayerId, TeamId, IPAddress, DownloadRate, UploadRate, Language, IsSpectator, IsInOfficialMode, a structure named Avatar, an array of structures named Skins, a structure named LadderStats, HoursSinceZoneInscription and OnlineRights (0: nations account, 3: united account).
        /// Each structure of the array Skins contains two fields Environnement and a struct PackDesc. Each structure PackDesc, as well as the struct Avatar, contains two fields FileName and Checksum.
        /// </summary>
        /// <param name="playerLogin"></param>
        /// <returns></returns>
        public async Task<PlayerDetailedInfoStruct> GetDetailedPlayerInfoAsync(string playerLogin) =>
            (PlayerDetailedInfoStruct)XmlRpcTypes.ToNativeValue<PlayerDetailedInfoStruct>(
                await CallOrFaultAsync("GetDetailedPlayerInfo", playerLogin)
            );
        
        /// <summary>
        /// Returns a struct containing the player infos of the game server (ie: in case of a basic server, itself; in case of a relay server, the main server), with an optional parameter for compatibility: struct version (0 = united, 1 = forever).
        /// The structure is identical to the ones from GetPlayerList. Forever PlayerInfo struct is: Login, NickName, PlayerId, TeamId, SpectatorStatus, LadderRanking, and Flags.
        /// LadderRanking is 0 when not in official mode,
        /// Flags = ForceSpectator(0,1,2) + IsReferee * 10 + IsPodiumReady * 100 + StereoDisplayMode * 1000 + IsManagedByAnOtherServer * 10000 + IsServer * 100000 + HasPlayerSlot * 1000000 + IsBroadcasting * 10000000 + HasJoinedGame * 100000000
        /// SpectatorStatus = Spectator + TemporarySpectator * 10 + PureSpectator * 100 + AutoTarget * 1000 + CurrentTargetId * 10000
        /// Each structure contains the following fields : Login, NickName, PlayerId and Rank. In addition, for legacy trackmania modes it also contains BestTime, Score, NbrLapsFinished, LadderScore, and an array BestCheckpoints that contains the checkpoint times for the best race. 
        /// </summary>
        /// <param name="serverType"></param>
        /// <returns></returns>
        public async Task<PlayerInfoStruct> GetMainServerPlayerInfoAsync(int serverType) =>
            (PlayerInfoStruct)XmlRpcTypes.ToNativeValue<PlayerInfoStruct>(
                await CallOrFaultAsync("GetMainServerPlayerInfo", serverType)
            );

        /// <summary>
        /// Returns the current rankings for the race in progress. (In trackmania legacy team modes, the scores for the two teams are returned. In other modes, it's the individual players' scores) This method take two parameters. The first parameter specifies the maximum number of infos to be returned, and the second one the starting index in the ranking.  The ranking returned is a list of structures.
        /// Each structure contains the following fields : Login, NickName, PlayerId and Rank. In addition, for legacy trackmania modes it also contains BestTime, Score, NbrLapsFinished, LadderScore, and an array BestCheckpoints that contains the checkpoint times for the best race. 
        /// </summary>
        /// <param name="playerLogin"></param>
        /// <returns></returns>
        public async Task<PlayerRankingStruct[]> GetCurrentRankingAsync(int maxInfos, int startRatingIndex) =>
            (PlayerRankingStruct[])XmlRpcTypes.ToNativeValue<PlayerRankingStruct[]>(
                await CallOrFaultAsync("GetCurrentRanking")
            );

        /// <summary>
        /// Returns the current ranking for the race in progressof the player with the specified login (or list of comma-separated logins). The ranking returned is a list of structures.
        /// Each structure contains the following fields : Login, NickName, PlayerId and Rank. In addition, for legacy trackmania modes it also contains BestTime, Score, NbrLapsFinished, LadderScore, and an array BestCheckpoints that contains the checkpoint times for the best race. 
        /// </summary>
        /// <param name="playerLogin"></param>
        /// <returns></returns>
        public async Task<PlayerRankingStruct[]> GetCurrentRankingForLoginAsync(string playerLogin) =>
            (PlayerRankingStruct[])XmlRpcTypes.ToNativeValue<PlayerRankingStruct[]>(
                await CallOrFaultAsync("GetCurrentRankingForLogin")
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
        /// Force the scores of the current game. Only available in rounds and team mode. You have to pass an array of structs {int PlayerId, int Score}.
        /// And a boolean SilentMode - if true, the scores are silently updated (only available for SuperAdmin), allowing an external controller to do its custom counting... Only available to Admin/SuperAdmin.
        /// </summary>
        /// <param name="playerScores"></param>
        /// <param name="silentMode"></param>
        /// <returns></returns>
        public async Task<bool> ForceScoresAsync(PlayerScoreStruct[] playerScores, bool silentMode) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("ForceScores", playerScores, silentMode)
            );

        /// <summary>
        /// Force the team of the player. Only available in team mode. You have to pass the login and the team number (0 or 1). Only available to Admin.
        /// </summary>
        /// <param name="playerLogin"></param>
        /// <param name="cameraType"></param>
        /// <returns></returns>
        public async Task<bool> ForcePlayerTeamAsync(int playerLogin, int cameraType) =>
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

        /// <summary>
        /// Force the spectating status of the player. You have to pass the login and the spectator mode (0: user selectable, 1: spectator, 2: player, 3: spectator but keep selectable). Only available to Admin.
        /// </summary>
        /// <param name="playerLogin"></param>
        /// <param name="cameraType"></param>
        /// <returns></returns>
        public async Task<bool> ForceSpectatorAsync(int playerLogin, int cameraType) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("ForceSpectator", playerLogin, cameraType)
            );

        /// <summary>
        /// Force the spectating status of the player. You have to pass the playerid and the spectator mode (0: user selectable, 1: spectator, 2: player, 3: spectator but keep selectable). Only available to Admin.
        /// </summary>
        /// <param name="playerId"></param>
        /// <param name="cameraType"></param>
        /// <returns></returns>
        public async Task<bool> ForceSpectatorIdAsync(int playerId, int cameraType) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("ForceSpectatorId", playerId, cameraType)
            );

        /// <summary>
        /// Force spectators to look at a specific player. You have to pass the login of the spectator (or '' for all) and the login of the target (or '' for automatic),
        /// and an integer for the camera type to use (-1 = leave unchanged, 0 = replay, 1 = follow, 2 = free). Only available to Admin.
        /// </summary>
        /// <param name="playerLogin"></param>
        /// <param name="targetLogin"></param>
        /// <param name="cameraType"></param>
        /// <returns></returns>
        public async Task<bool> ForceSpectatorTargetAsync(string playerLogin, string targetLogin, int cameraType) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("ForceSpectatorTarget", playerLogin, targetLogin, cameraType)
            );
        
        /// <summary>
        /// Force spectators to look at a specific player. You have to pass the id of the spectator (or -1 for all) and the id of the target (or -1 for automatic),
        /// and an integer for the camera type to use (-1 = leave unchanged, 0 = replay, 1 = follow, 2 = free). Only available to Admin.
        /// </summary>
        /// <param name="playerId"></param>
        /// <param name="targetId"></param>
        /// <param name="cameraType"></param>
        /// <returns></returns>
        public async Task<bool> ForceSpectatorTargetIdAsync(int playerId, int targetId, int cameraType) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("ForceSpectatorTargetId", playerId, targetId, cameraType)
            );

        /// <summary>
        /// Pass the login of the spectator. A spectator that once was a player keeps his player slot, so that he can go back to race mode.
        /// Calling this function frees this slot for another player to connect. Only available to Admin.
        /// </summary>
        /// <param name="playerLogin"></param>
        /// <returns></returns>
        public async Task<bool> SpectatorReleasePlayerSlotAsync(string playerLogin) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SpectatorReleasePlayerSlot", playerLogin)
            );

        /// <summary>
        /// Pass the playerid of the spectator. A spectator that once was a player keeps his player slot, so that he can go back to race mode.
        /// Calling this function frees this slot for another player to connect. Only available to Admin.
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns>
        public async Task<bool> SpectatorReleasePlayerSlotIdAsync(int playerId) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SpectatorReleasePlayerSlotId", playerId)
            );
        
        /// <summary>
        /// Enable control of the game flow: the game will wait for the caller to validate state transitions. Only available to Admin.
        /// </summary>
        /// <param name="enabled"></param>
        /// <returns></returns>
        public async Task<bool> ManualFlowControlEnableAsync(bool enabled) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("ManualFlowControlEnable", enabled)
            );

        /// <summary>
        /// Allows the game to proceed. Only available to Admin.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> ManualFlowControlProceedAsync() =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("ManualFlowControlProceed")
            );

        /// <summary>
        /// Returns whether the manual control of the game flow is enabled. 0 = no, 1 = yes by the xml-rpc client making the call, 2 = yes, by some other xml-rpc client. Only available to Admin.
        /// </summary>
        /// <returns></returns>
        public async Task<int> ManualFlowControlIsEnabledAsync() =>
            (int)XmlRpcTypes.ToNativeValue<int>(
                await CallOrFaultAsync("ManualFlowControlIsEnabled")
            );

        /// <summary>
        /// Returns the transition that is currently blocked, or '' if none. (That's exactly the value last received by the callback.) Only available to Admin.
        /// </summary>
        /// <returns></returns>
        public async Task<string> ManualFlowControlGetCurTransitionAsync() =>
            (string)XmlRpcTypes.ToNativeValue<string>(
                await CallOrFaultAsync("ManualFlowControlGetCurTransition")
            );
        
        /// <summary>
        /// Returns the current match ending condition. Return values are: 'Playing', 'ChangeMap' or 'Finished'.
        /// </summary>
        /// <returns></returns>
        public async Task<string> CheckEndMatchConditionAsync() =>
            (string)XmlRpcTypes.ToNativeValue<string>(
                await CallOrFaultAsync("CheckEndMatchCondition")
            );

        /// <summary>
        /// Returns a struct containing the networks stats of the server. The structure contains the following fields : Uptime, NbrConnection, MeanConnectionTime, MeanNbrPlayer, RecvNetRate, SendNetRate, TotalReceivingSize, TotalSendingSize and an array of structures named PlayerNetInfos.
        /// Each structure of the array PlayerNetInfos contains the following fields : Login, IPAddress, LastTransferTime, DeltaBetweenTwoLastNetState, PacketLossRate. Only available to SuperAdmin.
        /// </summary>
        /// <returns></returns>
        public async Task<NetworkStatsStruct> GetNetworkStatsAsync() =>
            (NetworkStatsStruct)XmlRpcTypes.ToNativeValue<NetworkStatsStruct>(
                await CallOrFaultAsync("GetNetworkStats")
            );

        /// <summary>
        /// Start a server on lan, using the current configuration. Only available to SuperAdmin.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> StartServerLanAsync() =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("StartServerLan")
            );
        
        /// <summary>
        /// Start a server on internet, using the current configuration. Only available to SuperAdmin.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> StartServerInternetAsync() =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("StartServerInternet")
            );
        #endregion
    }
}
