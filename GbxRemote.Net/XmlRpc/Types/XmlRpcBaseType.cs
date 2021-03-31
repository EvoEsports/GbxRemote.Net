using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GbxRemoteNet.XmlRpc.Types {
    public abstract class XmlRpcBaseType {
        public XmlRpcBaseType(XElement element) { }

        public abstract XElement GetXml();
    }
}
