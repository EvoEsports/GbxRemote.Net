using System.Collections.Generic;
using System.Threading.Tasks;
using GbxRemoteNet.Structs;
using GbxRemoteNet.XmlRpc;

namespace GbxRemoteNet;

/// <summary>
/// Method Category: Chat
/// </summary>
public partial class GbxRemoteClient
{
    public async Task<bool> ChatSendServerMessageAsync(string message)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("ChatSendServerMessage", message)
        );
    }

    public async Task<bool> ChatSendServerMessageToLanguageAsync(TmLanguage[] lang, string message)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("ChatSendServerMessage", lang, message)
        );
    }

    public async Task<bool> ChatSendServerMessageToIdAsync(string message, int loginId)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("ChatSendServerMessageToId", message, loginId)
        );
    }

    public async Task<bool> ChatSendServerMessageToLoginAsync(string message, string playerLogins)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("ChatSendServerMessageToLogin", message, playerLogins)
        );
    }

    public Task<bool> ChatSendServerMessageToLoginAsync(string message, IEnumerable<string> playerLogins) =>
        ChatSendServerMessageToLoginAsync(message, string.Join(',', playerLogins));

    public async Task<bool> ChatSendAsync(string message)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("ChatSend", message)
        );
    }

    public async Task<bool> ChatSendToLanguageAsync(TmLanguage[] lang, string message)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("ChatSendToLanguage", lang, message)
        );
    }

    public async Task<bool> ChatSendToLoginAsync(string message, string playerLogins)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("ChatSendToLogin", message, playerLogins)
        );
    }

    public Task<bool> ChatSendToLoginAsync(string message, IEnumerable<string> playerLogins) =>
        ChatSendToLoginAsync(message, string.Join(',', playerLogins));

    public async Task<bool> ChatSendToIdAsync(string message, int playerId)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("ChatSendToId", message, playerId)
        );
    }

    public async Task<string[]> GetChatLinesAsync()
    {
        return (string[]) XmlRpcTypes.ToNativeValue<string>(
            await CallOrFaultAsync("GetChatLines")
        );
    }

    public async Task<bool> ChatEnableManualRoutingAsync(bool enable, bool forward)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("ChatEnableManualRouting", enable, forward)
        );
    }

    public Task<bool> ChatEnableManualRoutingAsync() =>
        ChatEnableManualRoutingAsync(true, false);

    public async Task<bool> ChatForwardToLoginAsync(string text, string senderLogin, string destinationLogin)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("ChatForwardToLogin", text, senderLogin, destinationLogin)
        );
    }
}