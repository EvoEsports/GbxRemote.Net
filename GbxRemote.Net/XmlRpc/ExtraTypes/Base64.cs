using System;
using System.Linq;
using System.Text;

namespace GbxRemoteNet.XmlRpc.ExtraTypes;

public class Base64 : IEquatable<Base64>
{
    public Base64(byte[] data)
    {
        Data = data;
    }

    public Base64(string data)
    {
        Data = Encoding.UTF8.GetBytes(data);
    }

    public byte[] Data { get; }

    public bool Equals(Base64 other)
    {
        return Data.SequenceEqual(other.Data);
    }

    public override string ToString()
    {
        return Convert.ToBase64String(Data);
    }

    public static Base64 FromBase64String(string data)
    {
        return new Base64(Convert.FromBase64String(data));
    }
}