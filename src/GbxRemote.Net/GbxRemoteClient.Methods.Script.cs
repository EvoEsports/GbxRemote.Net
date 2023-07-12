using System.Threading.Tasks;
using GbxRemoteNet.Structs;
using GbxRemoteNet.XmlRpc;
using GbxRemoteNet.XmlRpc.ExtraTypes;

namespace GbxRemoteNet;

/// <summary>
/// Method Category: ModeScript
/// </summary>
public partial class GbxRemoteClient
{
    public async Task<string> GetModeScriptTextAsync()
    {
        return (string) XmlRpcTypes.ToNativeValue<string>(
            await CallOrFaultAsync("GetModeScriptText")
        );
    }
    
    public async Task<bool> SetModeScriptTextAsync(string script)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("SetModeScriptText", script)
        );
    }
    
    public async Task<TmScriptInfo> GetModeScriptInfoAsync()
    {
        return (TmScriptInfo) XmlRpcTypes.ToNativeValue<TmScriptInfo>(
            await CallOrFaultAsync("GetModeScriptInfo")
        );
    }

    public async Task<GbxDynamicObject> GetModeScriptSettingsAsync()
    {
        return (GbxDynamicObject) XmlRpcTypes.ToNativeValue<GbxDynamicObject>(
            await CallOrFaultAsync("GetModeScriptSettings")
        );
    }

    public async Task<bool> SetModeScriptSettingsAsync(GbxDynamicObject modescriptSettings)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("SetModeScriptSettings", modescriptSettings)
        );
    }

    public async Task<bool> SendModeScriptCommandsAsync(GbxDynamicObject commands)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("SendModeScriptCommands", commands)
        );
    }

    public async Task<bool> SetModeScriptSettingsAndCommandsAsync(GbxDynamicObject settings, GbxDynamicObject modeScript)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("SetModeScriptSettingsAndCommands", settings, modeScript)
        );
    }

    public async Task<GbxDynamicObject> GetModeScriptVariablesAsync()
    {
        return (GbxDynamicObject) XmlRpcTypes.ToNativeValue<GbxDynamicObject>(
            await CallOrFaultAsync("GetModeScriptVariables")
        );
    }

    public async Task<bool> SetModeScriptVariablesAsync(GbxDynamicObject xmlRpcVar)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("SetModeScriptVariables", xmlRpcVar)
        );
    }

    public async Task<bool> TriggerModeScriptEventAsync(string modeScript, string eventName)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("TriggerModeScriptEvent", modeScript, eventName)
        );
    }

    public async Task<bool> TriggerModeScriptEventArrayAsync(string method, params string[] parameters)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("TriggerModeScriptEventArray", method, parameters)
        );
    }

    public async Task<bool> SetServerPluginAsync(bool forceReload, string filename, GbxDynamicObject script)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("SetServerPlugin", forceReload, filename, script)
        );
    }

    public Task<bool> SetServerPluginAsync(bool forceReload) => SetServerPluginAsync(forceReload, null, null);

    public Task<bool> SetServerPluginAsync(bool forceReload, string filename) =>
        SetServerPluginAsync(forceReload, filename, null);

    public async Task<GbxDynamicObject> GetServerPluginAsync()
    {
        return (GbxDynamicObject) XmlRpcTypes.ToNativeValue<GbxDynamicObject>(
            await CallOrFaultAsync("GetServerPlugin")
        );
    }

    public async Task<GbxDynamicObject> GetServerPluginVariablesAsync()
    {
        return (GbxDynamicObject) XmlRpcTypes.ToNativeValue<GbxDynamicObject>(
            await CallOrFaultAsync("GetServerPluginVariables")
        );
    }

    public async Task<bool> TriggerServerPluginEventAsync(string method, string param2)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("TriggerServerPluginEvent", method, param2)
        );
    }

    public async Task<bool> TriggerServerPluginEventArrayAsync(string method, string[] param2)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("TriggerServerPluginEventArray", method, param2)
        );
    }

    public async Task<GbxDynamicObject> GetScriptCloudVariablesAsync(string type, string id)
    {
        return (GbxDynamicObject) XmlRpcTypes.ToNativeValue<GbxDynamicObject>(
            await CallOrFaultAsync("GetScriptCloudVariables")
        );
    }

    public async Task<bool> SetScriptCloudVariablesAsync(string type, string id, GbxDynamicObject variables)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("SetScriptCloudVariables")
        );
    }
}