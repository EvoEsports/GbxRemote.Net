using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GbxRemoteNet.Structs {
    public class ScriptInfoStruct {
        public string Name;
        public string CompatibleMapTypes;
        public string Description;
        public string Version;
        public ScriptParamDescStruct[] ParamDescs;
        public ScriptCommandDescs[] CommandDescs;
    }
}
