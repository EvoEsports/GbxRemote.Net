using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GbxRemoteNet.Enums {
    [Flags]
    public enum CallbackType {
        Internal,
        ModeScript,
        Checkpoints
    }
}
