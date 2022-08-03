using System.Threading.Tasks;
using GbxRemoteNet.Structs;
using GbxRemoteNet.XmlRpc;

namespace GbxRemoteNet;

/// <summary>
///     Method Category: Client
/// </summary>
public partial class GbxRemoteClient
{
    /// <summary>
    ///     Display a notice on all clients. The parameters are the text message to display, and the login of the avatar to
    ///     display next to it (or '' for no avatar), and an optional 'variant' in [0 = normal, 1 = Sad, 2 = Happy]. Only
    ///     available to Admin.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="playerLogin"></param>
    /// <param name="variant"></param>
    /// <returns></returns>
    public async Task<bool> SendNoticeAsync(string message, string playerLogin, int variant)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("SendNotice", message, playerLogin, variant)
        );
    }

    /// <summary>
    ///     Display a notice on the client with the specified UId. The parameters are the Uid of the client to whom the notice
    ///     is sent, the text message to display, and the UId of the avatar to display next to it (or '255' for no avatar), and
    ///     an optional 'variant' in [0 = normal, 1 = Sad, 2 = Happy]. Only available to Admin.
    /// </summary>
    /// <param name="clientUid"></param>
    /// <param name="message"></param>
    /// <param name="avatarUid"></param>
    /// <param name="variant"></param>
    /// <returns></returns>
    public async Task<bool> SendNoticeToIdAsync(int clientUid, string message, int avatarUid, int variant)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("SendNoticeToId", clientUid, message, avatarUid, variant)
        );
    }

    /// <summary>
    ///     Display a notice on the client with the specified login. The parameters are the login of the client to whom the
    ///     notice is sent, the text message to display, and the login of the avatar to display next to it (or '' for no
    ///     avatar), and an optional 'variant' in [0 = normal, 1 = Sad, 2 = Happy]. Login can be a single login or a list of
    ///     comma-separated logins. Only available to Admin.
    /// </summary>
    /// <param name="clientLogin"></param>
    /// <param name="message"></param>
    /// <param name="avatarLogin"></param>
    /// <param name="variant"></param>
    /// <returns></returns>
    public async Task<bool> SendNoticeToLoginAsync(string clientLogin, string message, string avatarLogin, int variant)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("SendNoticeToLogin", clientLogin, message, avatarLogin, variant)
        );
    }

    /// <summary>
    ///     Display a manialink page on all clients. The parameters are the xml description of the page to display, a timeout
    ///     to autohide it (0 = permanent), and a boolean to indicate whether the page must be hidden as soon as the user
    ///     clicks on a page option. Only available to Admin.
    /// </summary>
    /// <param name="xml"></param>
    /// <param name="timeout"></param>
    /// <param name="autohide"></param>
    /// <returns></returns>
    public async Task<bool> SendDisplayManialinkPageAsync(string xml, int timeout, bool autohide)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("SendDisplayManialinkPage", xml, timeout, autohide)
        );
    }

    /// <summary>
    ///     Display a manialink page on the client with the specified UId. The first parameter is the UId of the player, the
    ///     other are identical to 'SendDisplayManialinkPage'. Only available to Admin.
    /// </summary>
    /// <param name="clientId"></param>
    /// <param name="xml"></param>
    /// <param name="timeout"></param>
    /// <param name="autohide"></param>
    /// <returns></returns>
    public async Task<bool> SendDisplayManialinkPageToIdAsync(int clientId, string xml, int timeout, bool autohide)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("SendDisplayManialinkPageToId", clientId, xml, timeout, autohide)
        );
    }

    /// <summary>
    ///     Display a manialink page on the client with the specified login. The first parameter is the login of the player,
    ///     the other are identical to 'SendDisplayManialinkPage'. Login can be a single login or a list of comma-separated
    ///     logins. Only available to Admin.
    /// </summary>
    /// <param name="playerLogin"></param>
    /// <param name="xml"></param>
    /// <param name="timeout"></param>
    /// <param name="autohide"></param>
    /// <returns></returns>
    public async Task<bool> SendDisplayManialinkPageToLoginAsync(string playerLogin, string xml, int timeout,
        bool autohide)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("SendDisplayManialinkPageToLogin", playerLogin, xml, timeout, autohide)
        );
    }

    /// <summary>
    ///     Hide the displayed manialink page on all clients. Only available to Admin.
    /// </summary>
    /// <returns></returns>
    public async Task<bool> SendHideManialinkPageAsync()
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("SendHideManialinkPage")
        );
    }

    /// <summary>
    ///     Hide the displayed manialink page on the client with the specified UId. Only available to Admin.
    /// </summary>
    /// <param name="clientId"></param>
    /// <returns></returns>
    public async Task<bool> SendHideManialinkPageToIdAsync(int clientId)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("SendHideManialinkPageToId", clientId)
        );
    }

    /// <summary>
    ///     Hide the displayed manialink page on the client with the specified login. Login can be a single login or a list of
    ///     comma-separated logins. Only available to Admin.
    /// </summary>
    /// <param name="playerLogin"></param>
    /// <returns></returns>
    public async Task<bool> SendHideManialinkPageToLoginAsync(string playerLogin)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("SendHideManialinkPageToLogin", playerLogin)
        );
    }

    /// <summary>
    ///     Returns the latest results from the current manialink page, as an array of structs {string Login, int PlayerId, int
    ///     Result} Result==0 -> no answer, Result>0.... -> answer from the player.
    /// </summary>
    /// <returns></returns>
    public async Task<ManialinkPageAnswer[]> GetManialinkPageAnswersAsync()
    {
        return (ManialinkPageAnswer[]) XmlRpcTypes.ToNativeValue<ManialinkPageAnswer>(
            await CallOrFaultAsync("GetManialinkPageAnswers")
        );
    }

    /// <summary>
    ///     Opens a link in the client with the specified UId. The parameters are the Uid of the client to whom the link to
    ///     open is sent, the link url, and the 'LinkType' (0 in the external browser, 1 in the internal manialink browser).
    ///     Only available to Admin.
    /// </summary>
    /// <param name="clientId"></param>
    /// <param name="url"></param>
    /// <param name="linkType"></param>
    /// <returns></returns>
    public async Task<bool> SendOpenLinkToIdAsync(int clientId, string url, int linkType)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("SendOpenLinkToId", clientId, url, linkType)
        );
    }

    /// <summary>
    ///     Opens a link in the client with the specified login. The parameters are the login of the client to whom the link to
    ///     open is sent, the link url, and the 'LinkType' (0 in the external browser, 1 in the internal manialink browser).
    ///     Login can be a single login or a list of comma-separated logins. Only available to Admin.
    /// </summary>
    /// <param name="playerLogin"></param>
    /// <param name="url"></param>
    /// <param name="linkType"></param>
    /// <returns></returns>
    public async Task<bool> SendOpenLinkToLoginAsync(string playerLogin, string url, int linkType)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("SendOpenLinkToId", playerLogin, url, linkType)
        );
    }

    /// <summary>
    ///     Sets whether buddy notifications should be sent in the chat. login is the login of the player, or '' for global
    ///     setting, and enabled is the value. Only available to Admin.
    /// </summary>
    /// <param name="login"></param>
    /// <param name="enabled"></param>
    /// <returns></returns>
    public async Task<bool> SetBuddyNotificationAsync(string login, bool enabled)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("SetBuddyNotification", login, enabled)
        );
    }

    /// <summary>
    ///     Gets whether buddy notifications are enabled for login, or '' to get the global setting.
    /// </summary>
    /// <param name="login"></param>
    /// <returns></returns>
    public async Task<bool> GetBuddyNotificationAsync(string login)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("GetBuddyNotification", login)
        );
    }

    /// <summary>
    ///     Customize the clients 'leave server' dialog box. Parameters are: ManialinkPage, SendToServer url
    ///     '#qjoin=login@title', ProposeAddToFavorites and DelayQuitButton (in milliseconds). Only available to Admin.
    /// </summary>
    /// <param name="manialinkPage"></param>
    /// <param name="sendToServerUrl"></param>
    /// <param name="proposeAddToFavorites"></param>
    /// <param name="delayQuiteButton"></param>
    /// <returns></returns>
    public async Task<bool> CustomizeQuitDialogAsync(string manialinkPage, string sendToServerUrl,
        bool proposeAddToFavorites, int delayQuiteButton)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("CustomizeQuitDialog", manialinkPage, sendToServerUrl, proposeAddToFavorites,
                delayQuiteButton)
        );
    }
}