using System;

namespace GbxRemoteNet.Enums;

[Flags]
public enum CallbackType
{
    Internal,
    ModeScript,
    Checkpoints
}