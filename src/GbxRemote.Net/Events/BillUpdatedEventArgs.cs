using System;

namespace GbxRemoteNet.Events;

public class BillUpdatedEventArgs : EventArgs
{
    /// <summary>
    /// ID of the bill.
    /// </summary>
    public int BillId { get; set; }
    /// <summary>
    /// State of the bill.
    /// </summary>
    public int State { get; set; }
    /// <summary>
    /// State name of the bill.
    /// </summary>
    public string StateName { get; set; }
    /// <summary>
    /// ID of the bill transaction.
    /// </summary>
    public int TransactionId { get; set; }
}