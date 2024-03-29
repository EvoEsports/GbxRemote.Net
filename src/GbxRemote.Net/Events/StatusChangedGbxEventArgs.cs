﻿using System;

namespace GbxRemoteNet.Events;

public class StatusChangedGbxEventArgs : EventArgs
{
    /// <summary>
    /// Code/ID of the status.
    /// </summary>
    public int StatusCode { get; set; }
    /// <summary>
    /// A friendly string that represents the status.
    /// </summary>
    public string StatusName { get; set; }
}