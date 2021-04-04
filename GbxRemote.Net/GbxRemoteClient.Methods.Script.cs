using GbxRemoteNet.Structs;
using GbxRemoteNet.XmlRpc;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GbxRemoteNet {
    public partial class GbxRemoteClient {
        public async Task<string> GetModeScriptText() =>
            (string)XmlRpcTypes.ToNativeValue<string>(
                await CallOrFaultAsync("GetModeScriptText")
            );

        public async Task<bool> SetModeScriptTextAsync(string script) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SetModeScriptText", script)
            );

        public async Task<ScriptInfoStruct> GetModeScriptInfoAsync() =>
            (ScriptInfoStruct)XmlRpcTypes.ToNativeValue<ScriptInfoStruct>(
                await CallOrFaultAsync("GetModeScriptInfo")
            );

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
    }
}
