using System.Threading.Tasks;
using GbxRemoteNet.Structs;
using GbxRemoteNet.XmlRpc;

namespace GbxRemoteNet;

/// <summary>
///     Method Category: Chat
/// </summary>
public partial class GbxRemoteClient
{
    /// <summary>
    ///     Send a text message to all clients without the server login. Only available to Admin.
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public async Task<bool> ChatSendServerMessageAsync(string message)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("ChatSendServerMessage", message)
        );
    }

    /// <summary>
    ///     Send a localised text message to all clients without the server login, or optionally to a Login (which can be a
    ///     single login or a list of comma-separated logins). The parameter is an array of structures {Lang='xx', Text='...'}.
    ///     If no matching language is found, the last text in the array is used. Only available to Admin.
    /// </summary>
    /// <param name="lang"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    public async Task<bool> ChatSendServerMessageToLanguageAsync(Language[] lang, string message)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("ChatSendServerMessage", lang, message)
        );
    }

    /// <summary>
    ///     Send a text message without the server login to the client with the specified PlayerId. Only available to Admin.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="loginId"></param>
    /// <returns></returns>
    public async Task<bool> ChatSendServerMessageToIdAsync(string message, int loginId)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("ChatSendServerMessageToId", message, loginId)
        );
    }

    /// <summary>
    ///     Send a text message without the server login to the client with the specified login. Login can be a single login or
    ///     a list of comma-separated logins. Only available to Admin.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="playerLogins"></param>
    /// <returns></returns>
    public async Task<bool> ChatSendServerMessageToLoginAsync(string message, string playerLogins)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("ChatSendServerMessageToLogin", message, playerLogins)
        );
    }

    /// <summary>
    ///     Send a text message to all clients. Only available to Admin.
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public async Task<bool> ChatSendAsync(string message)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("ChatSend", message)
        );
    }

    /// <summary>
    ///     Send a localised text message to all clients, or optionally to a Login (which can be a single login or a list of
    ///     comma-separated logins). The parameter is an array of structures {Lang='xx', Text='...'}. If no matching language
    ///     is found, the last text in the array is used. Only available to Admin.
    /// </summary>
    /// <param name="lang"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    public async Task<bool> ChatSendToLanguageAsync(Language[] lang, string message)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("ChatSendToLanguage", lang, message)
        );
    }

    /// <summary>
    ///     Send a text message to the client with the specified login. Login can be a single login or a list of
    ///     comma-separated logins. Only available to Admin.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="playerLogins"></param>
    /// <returns></returns>
    public async Task<bool> ChatSendToLoginAsync(string message, string playerLogins)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("ChatSendToLogin", message, playerLogins)
        );
    }

    /// <summary>
    ///     Send a text message to the client with the specified PlayerId. Only available to Admin.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="playerId"></param>
    /// <returns></returns>
    public async Task<bool> ChatSendToIdAsync(string message, int playerId)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("ChatSendToId", message, playerId)
        );
    }

    /// <summary>
    ///     Returns the last chat lines. Maximum of 40 lines. Only available to Admin.
    /// </summary>
    /// <returns></returns>
    public async Task<string[]> GetChatLinesAsync()
    {
        return (string[]) XmlRpcTypes.ToNativeValue<string>(
            await CallOrFaultAsync("GetChatLines")
        );
    }

    /// <summary>
    ///     The chat messages are no longer dispatched to the players, they only go to the rpc callback and the controller has
    ///     to manually forward them. The second (optional) parameter allows all messages from the server to be automatically
    ///     forwarded. Only available to Admin.
    /// </summary>
    /// <param name="enable"></param>
    /// <param name="forward"></param>
    /// <returns></returns>
    public async Task<bool> ChatEnableManualRoutingAsync(bool enable, bool forward)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("ChatSendToId", enable, forward)
        );
    }

    /// <summary>
    ///     (Text, SenderLogin, DestLogin) Send a text message to the specified DestLogin (or everybody if empty) on behalf of
    ///     SenderLogin. DestLogin can be a single login or a list of comma-separated logins. Only available if manual routing
    ///     is enabled. Only available to Admin.
    /// </summary>
    /// <param name="text"></param>
    /// <param name="senderLogin"></param>
    /// <param name="destinationLogin"></param>
    /// <returns></returns>
    public async Task<bool> ChatForwardToLoginAsync(string text, string senderLogin, string destinationLogin)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("ChatForwardToLogin", text, senderLogin, destinationLogin)
        );
    }
}