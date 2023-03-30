using System;

namespace GbxRemoteNet.Enums;

[Flags]
public enum GbxCallbackType
{
    Internal,
    ModeScript,
    Checkpoints
}