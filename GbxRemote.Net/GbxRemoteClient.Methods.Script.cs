using GbxRemoteNet.Structs;
using GbxRemoteNet.XmlRpc;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GbxRemoteNet {
    /// <summary>
    /// Method Category: ModeScript
    /// </summary>
    public partial class GbxRemoteClient {
        /// <summary>
        /// Get the current mode script.
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetModeScriptTextAsync() =>
            (string)XmlRpcTypes.ToNativeValue<string>(
                await CallOrFaultAsync("GetModeScriptText")
            );

        /// <summary>
        /// Set the mode script and restart. Only available to Admin.
        /// </summary>
        /// <param name="script"></param>
        /// <returns></returns>
        public async Task<bool> SetModeScriptTextAsync(string script) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetModeScriptText", script)
            );

        /// <summary>
        /// Returns the description of the current mode script, as a structure containing: Name, CompatibleTypes, Description, Version and the settings available.
        /// </summary>
        /// <returns></returns>
        public async Task<ScriptInfo> GetModeScriptInfoAsync() =>
            (ScriptInfo)XmlRpcTypes.ToNativeValue<ScriptInfo>(
                await CallOrFaultAsync("GetModeScriptInfo")
            );

        /// <summary>
        /// Returns the current settings of the mode script.
        /// </summary>
        /// <returns></returns>
        public async Task<DynamicObject> GetModeScriptSettingsAsync() =>
            (DynamicObject)XmlRpcTypes.ToNativeValue<DynamicObject>(
                await CallOrFaultAsync("GetModeScriptSettings")
            );

        /// <summary>
        /// Change the settings of the mode script. Only available to Admin.
        /// </summary>
        /// <param name="modescriptSettings"></param>
        /// <returns></returns>
        public async Task<bool> SetModeScriptSettingsAsync(DynamicObject modescriptSettings) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetModeScriptSettings", modescriptSettings)
            );

        /// <summary>
        /// Send commands to the mode script. Only available to Admin.
        /// </summary>
        /// <param name="commands"></param>
        /// <returns></returns>
        public async Task<bool> SendModeScriptCommandsAsync(DynamicObject commands) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SendModeScriptCommands", commands)
            );

        /// <summary>
        /// Change the settings and send commands to the mode script. Only available to Admin.
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="modeScript"></param>
        /// <returns></returns>
        public async Task<bool> SetModeScriptSettingsAndCommandsAsync(DynamicObject settings, DynamicObject modeScript) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetModeScriptSettingsAndCommands", settings, modeScript)
            );

        /// <summary>
        /// Returns the current xml-rpc variables of the mode script.
        /// </summary>
        /// <returns></returns>
        public async Task<DynamicObject> GetModeScriptVariablesAsync() =>
            (DynamicObject)XmlRpcTypes.ToNativeValue<DynamicObject>(
                await CallOrFaultAsync("GetModeScriptVariables")
            );

        /// <summary>
        /// Set the xml-rpc variables of the mode script. Only available to Admin.
        /// </summary>
        /// <param name="xmlRpcVar"></param>
        /// <returns></returns>
        public async Task<bool> SetModeScriptVariablesAsync(DynamicObject xmlRpcVar) =>
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
        /// <param name="method"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task<bool> TriggerModeScriptEventArrayAsync(string method, params string[] parameters) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("TriggerModeScriptEventArray", method, parameters)
            );

        /// <summary>
        /// Set the ServerPlugin settings. 
        /// </summary>
        /// <param name="forceReload">Whether to reload from disk</param>
        /// <param name="filename">OPTIONAL: Name the filename relative to Scripts/directory</param>
        /// <param name="script">OPTIONAL: The script #Settings to apply.</param>
        /// <returns></returns>
        public async Task<bool> SetServerPluginAsync(bool forceReload, string filename = null, DynamicObject script = null) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetServerPlugin", forceReload, filename, script)
            );

        /// <summary>
        /// Get the ServerPlugin current settings.
        /// </summary>
        /// <returns></returns>
        public async Task<DynamicObject> GetServerPluginAsync() =>
            (DynamicObject)XmlRpcTypes.ToNativeValue<DynamicObject>(
                await CallOrFaultAsync("GetServerPlugin")
            );

        /// <summary>
        /// Send an event to the server script. Only available to Admin.
        /// </summary>
        /// <returns></returns>
        public async Task<DynamicObject> GetServerPluginVariablesAsync() =>
            (DynamicObject)XmlRpcTypes.ToNativeValue<DynamicObject>(
                await CallOrFaultAsync("GetServerPluginVariables")
            );

        /// <summary>
        /// Returns the current xml-rpc variables of the server script.
        /// </summary>
        /// <param name="method"></param>
        /// <param name="param2"></param>
        /// <returns></returns>
        public async Task<bool> TriggerServerPluginEventAsync(string method, string param2) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("TriggerServerPluginEvent", method, param2)
            );

        /// <summary>
        /// Send an event to the server script. Only available to Admin.
        /// </summary>
        /// <param name="method"></param>
        /// <param name="param2"></param>
        /// <returns></returns>
        public async Task<bool> TriggerServerPluginEventArrayAsync(string method, string[] param2) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("TriggerServerPluginEventArray", method, param2)
            );

        /// <summary>
        /// Get the script cloud variables of given object. Only available to Admin.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<DynamicObject> GetScriptCloudVariablesAsync(string type, string id) =>
            (DynamicObject)XmlRpcTypes.ToNativeValue<DynamicObject>(
                await CallOrFaultAsync("GetScriptCloudVariables")
            );

        /// <summary>
        /// Set the script cloud variables of given object. Only available to Admin.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="id"></param>
        /// <param name="variables"></param>
        /// <returns></returns>
        public async Task<bool> SetScriptCloudVariablesAsync(string type, string id, DynamicObject variables) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetScriptCloudVariables")
            );
    }
}
