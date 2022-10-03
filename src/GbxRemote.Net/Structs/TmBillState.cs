using System;

namespace GbxRemoteNet.Structs;

[Obsolete]
public class TmBillState
{
    public string State { get; set; }
    public string StateName { get; set; }
    public int TransactionId { get; set; }
}