using System.Threading.Tasks;
using GbxRemoteNet.Structs;
using GbxRemoteNet.XmlRpc;

namespace GbxRemoteNet;

/// <summary>
/// Method Category: Client
/// </summary>
public partial class GbxRemoteClient
{
    public async Task<bool> SendNoticeAsync(string message, string playerLogin, int variant)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("SendNotice", message, playerLogin, variant)
        );
    }

    public async Task<bool> SendNoticeToIdAsync(int clientUid, string message, int avatarUid, int variant)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("SendNoticeToId", clientUid, message, avatarUid, variant)
        );
    }

    public async Task<bool> SendNoticeToLoginAsync(string clientLogin, string message, string avatarLogin, int variant)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("SendNoticeToLogin", clientLogin, message, avatarLogin, variant)
        );
    }

    public async Task<bool> SendDisplayManialinkPageAsync(string xml, int timeout, bool autohide)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("SendDisplayManialinkPage", xml, timeout, autohide)
        );
    }

    public async Task<bool> SendDisplayManialinkPageToIdAsync(int clientId, string xml, int timeout, bool autohide)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("SendDisplayManialinkPageToId", clientId, xml, timeout, autohide)
        );
    }

    public async Task<bool> SendDisplayManialinkPageToLoginAsync(string playerLogin, string xml, int timeout,
        bool autohide)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("SendDisplayManialinkPageToLogin", playerLogin, xml, timeout, autohide)
        );
    }
    
    public async Task<bool> SendHideManialinkPageAsync()
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("SendHideManialinkPage")
        );
    }

    public async Task<bool> SendHideManialinkPageToIdAsync(int clientId)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("SendHideManialinkPageToId", clientId)
        );
    }

    public async Task<bool> SendHideManialinkPageToLoginAsync(string playerLogin)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("SendHideManialinkPageToLogin", playerLogin)
        );
    }

    public async Task<TmManialinkPageAnswer[]> GetManialinkPageAnswersAsync()
    {
        return (TmManialinkPageAnswer[]) XmlRpcTypes.ToNativeValue<TmManialinkPageAnswer>(
            await CallOrFaultAsync("GetManialinkPageAnswers")
        );
    }

    public async Task<bool> SendOpenLinkToIdAsync(int clientId, string url, int linkType)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("SendOpenLinkToId", clientId, url, linkType)
        );
    }

    public async Task<bool> SendOpenLinkToLoginAsync(string playerLogin, string url, int linkType)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("SendOpenLinkToLogin", playerLogin, url, linkType)
        );
    }

    public async Task<bool> SetBuddyNotificationAsync(string login, bool enabled)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("SetBuddyNotification", login, enabled)
        );
    }
    
    public async Task<bool> GetBuddyNotificationAsync(string login)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("GetBuddyNotification", login)
        );
    }
    
    public async Task<bool> CustomizeQuitDialogAsync(string manialinkPage, string sendToServerUrl,
        bool proposeAddToFavorites, int delayQuiteButton)
    {
        return (bool) XmlRpcTypes.ToNativeValue<bool>(
            await CallOrFaultAsync("CustomizeQuitDialog", manialinkPage, sendToServerUrl, proposeAddToFavorites,
                delayQuiteButton)
        );
    }
}