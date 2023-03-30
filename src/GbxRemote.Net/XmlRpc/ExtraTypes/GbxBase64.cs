using System;
using System.Linq;
using System.Text;

namespace GbxRemoteNet.XmlRpc.ExtraTypes;

public class GbxBase64 : IEquatable<GbxBase64>
{
    public GbxBase64(byte[] data)
    {
        Data = data;
    }

    public GbxBase64(string data)
    {
        Data = Encoding.UTF8.GetBytes(data);
    }

    public byte[] Data { get; }

    public bool Equals(GbxBase64 other)
    {
        if (other == null)
        {
            return false;
        }
        
        return Data.SequenceEqual(other.Data);
    }

    public override string ToString()
    {
        return Convert.ToBase64String(Data);
    }

    public static GbxBase64 FromBase64String(string data)
    {
        return new GbxBase64(Convert.FromBase64String(data));
    }
}