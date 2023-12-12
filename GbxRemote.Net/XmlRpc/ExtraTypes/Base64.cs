using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GbxRemoteNet.XmlRpc.ExtraTypes {
    public class Base64(byte[] data) : IEquatable<Base64> {
        private byte[] data = data;

        public byte[] Data => data;

        public Base64(string data) : this(Encoding.UTF8.GetBytes(data))
        {
        }

        public bool Equals(Base64 other) {
            return data.SequenceEqual(other.data);
        }

        public override string ToString() {
            return Convert.ToBase64String(data);
        }

        public static Base64 FromBase64String(string data) {
            return new Base64(Convert.FromBase64String(data));
        }
    }
}
