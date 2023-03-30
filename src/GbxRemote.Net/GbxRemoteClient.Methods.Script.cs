using System.Threading.Tasks;
using GbxRemoteNet.Structs;
using GbxRemoteNet.XmlRpc;
using GbxRemoteNet.XmlRpc.ExtraTypes;

namespace GbxRemoteNet;

/// <summary>
///     Method Category: ModeScript
/// </summary>
public partial class GbxRemoteClient
{
    /// <summary>
    ///     Get the current mode script.
    /// </summary>
    /// <returns></returns>
    public async Task<string> GetModeScriptTextAsync()
    {
        return (string) XmlRpcTypes.ToNativeValue<string>(
            await CallOrFaultAsync("GetModeScriptText")
        );
    }

    /// <summary>
    ///     Set the mode script and restart. Only available to Admin.
    /// </summary>
    /// <param name="script"></param>
    /// <returns></returns>
    public async Task<bool> SetModeScriptTextAsync(string script)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("SetModeScriptText", script)
        );
    }

    /// <summary>
    ///     Returns the description of the current mode script, as a structure containing: Name, CompatibleTypes, Description,
    ///     Version and the settings available.
    /// </summary>
    /// <returns></returns>
    public async Task<TmScriptInfo> GetModeScriptInfoAsync()
    {
        return (TmScriptInfo) XmlRpcTypes.ToNativeValue<TmScriptInfo>(
            await CallOrFaultAsync("GetModeScriptInfo")
        );
    }

    /// <summary>
    ///     Returns the current settings of the mode script.
    /// </summary>
    /// <returns></returns>
    public async Task<GbxDynamicObject> GetModeScriptSettingsAsync()
    {
        return (GbxDynamicObject) XmlRpcTypes.ToNativeValue<GbxDynamicObject>(
            await CallOrFaultAsync("GetModeScriptSettings")
        );
    }

    /// <summary>
    ///     Change the settings of the mode script. Only available to Admin.
    /// </summary>
    /// <param name="modescriptSettings"></param>
    /// <returns></returns>
    public async Task<bool> SetModeScriptSettingsAsync(GbxDynamicObject modescriptSettings)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("SetModeScriptSettings", modescriptSettings)
        );
    }

    /// <summary>
    ///     Send commands to the mode script. Only available to Admin.
    /// </summary>
    /// <param name="commands"></param>
    /// <returns></returns>
    public async Task<bool> SendModeScriptCommandsAsync(GbxDynamicObject commands)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("SendModeScriptCommands", commands)
        );
    }

    /// <summary>
    ///     Change the settings and send commands to the mode script. Only available to Admin.
    /// </summary>
    /// <param name="settings"></param>
    /// <param name="modeScript"></param>
    /// <returns></returns>
    public async Task<bool> SetModeScriptSettingsAndCommandsAsync(GbxDynamicObject settings, GbxDynamicObject modeScript)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("SetModeScriptSettingsAndCommands", settings, modeScript)
        );
    }

    /// <summary>
    ///     Returns the current xml-rpc variables of the mode script.
    /// </summary>
    /// <returns></returns>
    public async Task<GbxDynamicObject> GetModeScriptVariablesAsync()
    {
        return (GbxDynamicObject) XmlRpcTypes.ToNativeValue<GbxDynamicObject>(
            await CallOrFaultAsync("GetModeScriptVariables")
        );
    }

    /// <summary>
    ///     Set the xml-rpc variables of the mode script. Only available to Admin.
    /// </summary>
    /// <param name="xmlRpcVar"></param>
    /// <returns></returns>
    public async Task<bool> SetModeScriptVariablesAsync(GbxDynamicObject xmlRpcVar)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("SetModeScriptVariables", xmlRpcVar)
        );
    }

    /// <summary>
    ///     Send an event to the mode script. Only available to Admin.
    /// </summary>
    /// <param name="modeScript"></param>
    /// <param name="eventName"></param>
    /// <returns></returns>
    public async Task<bool> TriggerModeScriptEventAsync(string modeScript, string eventName)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("TriggerModeScriptEvent", modeScript, eventName)
        );
    }

    /// <summary>
    ///     Send an event to the mode script. Only available to Admin.
    /// </summary>
    /// <param name="method"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public async Task<bool> TriggerModeScriptEventArrayAsync(string method, params string[] parameters)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("TriggerModeScriptEventArray", method, parameters)
        );
    }

    /// <summary>
    ///     Set the ServerPlugin settings.
    /// </summary>
    /// <param name="forceReload">Whether to reload from disk</param>
    /// <param name="filename">OPTIONAL: Name the filename relative to Scripts/directory</param>
    /// <param name="script">OPTIONAL: The script #Settings to apply.</param>
    /// <returns></returns>
    public async Task<bool> SetServerPluginAsync(bool forceReload, string filename = null, GbxDynamicObject script = null)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("SetServerPlugin", forceReload, filename, script)
        );
    }

    /// <summary>
    ///     Get the ServerPlugin current settings.
    /// </summary>
    /// <returns></returns>
    public async Task<GbxDynamicObject> GetServerPluginAsync()
    {
        return (GbxDynamicObject) XmlRpcTypes.ToNativeValue<GbxDynamicObject>(
            await CallOrFaultAsync("GetServerPlugin")
        );
    }

    /// <summary>
    ///     Send an event to the server script. Only available to Admin.
    /// </summary>
    /// <returns></returns>
    public async Task<GbxDynamicObject> GetServerPluginVariablesAsync()
    {
        return (GbxDynamicObject) XmlRpcTypes.ToNativeValue<GbxDynamicObject>(
            await CallOrFaultAsync("GetServerPluginVariables")
        );
    }

    /// <summary>
    ///     Returns the current xml-rpc variables of the server script.
    /// </summary>
    /// <param name="method"></param>
    /// <param name="param2"></param>
    /// <returns></returns>
    public async Task<bool> TriggerServerPluginEventAsync(string method, string param2)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("TriggerServerPluginEvent", method, param2)
        );
    }

    /// <summary>
    ///     Send an event to the server script. Only available to Admin.
    /// </summary>
    /// <param name="method"></param>
    /// <param name="param2"></param>
    /// <returns></returns>
    public async Task<bool> TriggerServerPluginEventArrayAsync(string method, string[] param2)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("TriggerServerPluginEventArray", method, param2)
        );
    }

    /// <summary>
    ///     Get the script cloud variables of given object. Only available to Admin.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<GbxDynamicObject> GetScriptCloudVariablesAsync(string type, string id)
    {
        return (GbxDynamicObject) XmlRpcTypes.ToNativeValue<GbxDynamicObject>(
            await CallOrFaultAsync("GetScriptCloudVariables")
        );
    }

    /// <summary>
    ///     Set the script cloud variables of given object. Only available to Admin.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="id"></param>
    /// <param name="variables"></param>
    /// <returns></returns>
    public async Task<bool> SetScriptCloudVariablesAsync(string type, string id, GbxDynamicObject variables)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("SetScriptCloudVariables")
        );
    }
}