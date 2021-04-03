using GbxRemoteNet.Structs;
using GbxRemoteNet.XmlRpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GbxRemoteNet {
    /// <summary>
    /// Method Category: Players
    /// </summary>
    public partial class GbxRemoteClient {
        #region Kicking
        public async Task<bool> KickAsync(string login, string message = null) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("Kick", login, message)
            );

        public async Task<bool> KickIdAsync(int id, string message = null) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("KickId", id, message)
            );
        #endregion

        #region Ban List
        public async Task<bool> BanAsync(string login, string message = null) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("Ban", login, message)
            );

        public async Task<bool> BanAndBlackListAsync(string login, string message, string saveToFile = null) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("BanAndBlackList", login, message, saveToFile)
            );

        public async Task<bool> BanIdAsync(int id, string message) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("BanId", id, message)
            );

        public async Task<bool> UnBanAsync(string login) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("UnBan", login)
            );

        public async Task<bool> CleanBanListAsync() =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("CleanBanList")
            );

        public async Task<BanListEntryStruct[]> GetBanListAsync(int maxInfos, int startIndex) =>
            (BanListEntryStruct[])XmlRpcTypes.ToNativeValue<BanListEntryStruct>(
                await CallOrFaultAsync("GetBanList", maxInfos, startIndex)
            );
        #endregion

        #region Black List
        public async Task<bool> BlackListAsync(string login) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("BlackList", login)
            );

        public async Task<bool> BlackListIdAsync(int id) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("BlackListId", id)
            );

        public async Task<bool> UnBlackListAsync(string login) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("UnBlackList", login)
            );

        public async Task<BlackListEntryStruct[]> GetBlackListAsync(int maxInfos, int startIndex) =>
            (BlackListEntryStruct[])XmlRpcTypes.ToNativeValue<BlackListEntryStruct>(
                await CallOrFaultAsync("GetBanList", maxInfos, startIndex)
            );

        public async Task<bool> LoadBlackListAsync(string fileName) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("LoadBlackList", fileName)
            );

        public async Task<bool> SaveBlackListAsync(string fileName) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SaveBlackList", fileName)
            );
        #endregion

        #region Guest List
        public async Task<bool> AddGuestAsync(string login) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("AddGuest", login)
            );

        public async Task<bool> AddGuestIdAsync(int id) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("AddGuestId", id)
            );

        public async Task<bool> RemoveGuestAsync(string login) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("RemoveGuest", login)
            );

        public async Task<bool> RemoveGuestIdAsync(int id) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("RemoveGuestId", id)
            );

        public async Task<bool> CleanGuestListAsync() =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("CleanGuestList")
            );

        public async Task<GuestListEntryStruct[]> GuestListEntryStructAsync(int maxInfos, int startIndex) =>
            (GuestListEntryStruct[])XmlRpcTypes.ToNativeValue<GuestListEntryStruct>(
                await CallOrFaultAsync("GuestListEntryStruct", maxInfos, startIndex)
            );

        public async Task<bool> LoadGuestListAsync(string fileName) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("LoadGuestList", fileName)
            );

        public async Task<bool> SaveGuestListAsync(string fileName) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("SaveGuestList", fileName)
            );

        #endregion

        #region Ignore List
        public async Task<bool> IgnoreAsync(string login) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("Ignore", login)
            );

        public async Task<bool> IgnoreIdAsync(int id) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("IgnoreId", id)
            );

        public async Task<bool> UnIgnoreAsync(string login) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("UnIgnore", login)
            );

        public async Task<bool> UnIgnoreIdAsync(int id) =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("UnIgnoreId", id)
            );

        public async Task<bool> CleanIgnoreListAsync() =>
            (bool)XmlRpcTypes.ToNativeValue<bool>(
                await CallOrFaultAsync("CleanIgnoreList")
            );

        public async Task<IgnoreListEntryStruct[]> GetIgnoreListAsync(int maxInfos, int startIndex) =>
            (IgnoreListEntryStruct[])XmlRpcTypes.ToNativeValue<IgnoreListEntryStruct>(
                await CallOrFaultAsync("CleanIgnoreList", maxInfos, startIndex)
            );
        #endregion

        #region Payments & Bills
        public async Task<int> PayAsync(string login, int planets, string label) =>
            (int)XmlRpcTypes.ToNativeValue<int>(
                await CallOrFaultAsync("Pay", login, planets, label)
            );

        public async Task<int> SendBillAsync(string loginFrom, int planets, string label, string loginTo = null) =>
            (int)XmlRpcTypes.ToNativeValue<int>(
                await CallOrFaultAsync("SendBill", loginFrom, planets, label, loginTo)
            );

        public async Task<BillStateStruct> GetBillStateAsync(int billId) =>
            (BillStateStruct)XmlRpcTypes.ToNativeValue<BillStateStruct>(
                await CallOrFaultAsync("SendBill", billId)
            );
        #endregion
    }
}
