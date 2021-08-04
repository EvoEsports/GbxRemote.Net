using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GbxRemoteNet.Structs {
    public class CurrentNextValue<T> {
        public T CurrentValue { get; set; }
        public T NextValue { get; set; }
    }
}
