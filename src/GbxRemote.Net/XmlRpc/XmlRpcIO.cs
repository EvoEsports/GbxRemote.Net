using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace GbxRemoteNet.XmlRpc;

/// <summary>
///     Handles reading and writing to the XML-RPC server.
/// </summary>
public class XmlRpcIO
{
    private readonly Stream _stream;

    /// <summary>
    ///     Create a new instance using the provided TCP connection.
    /// </summary>
    /// <param name="tcpClient">Instance to an active TCP connection.</param>
    public XmlRpcIO(TcpClient tcpClient)
    {
        _stream = tcpClient.GetStream();
    }

    /// <summary>
    ///     Creates a new instance using a stream.
    /// </summary>
    /// <param name="stream">This should be a stream that implements the XML-RPC protocol.</param>
    public XmlRpcIO(Stream stream)
    {
        this._stream = stream;
    }

    /// <summary>
    ///     Read a specific number of bytes from the connection.
    ///     This method will always read exactly n amount of bytes, so
    ///     if there is nothing to read until the number of bytes to read
    ///     is fulfilled, it will block.
    /// </summary>
    /// <param name="n">Number of bytes to read.</param>
    /// <returns>The bytes that was read from the connection.</returns>
    public async Task<byte[]> ReadBytesAsync(int n)
    {
        var data = new byte[n];
        var count = 0;
        while (n - count > 0) count += await _stream.ReadAsync(data.AsMemory(count, n - count));
        return data;
    }

    /// <summary>
    ///     Read a specific number of bytes from the connection.
    ///     This method will always read exactly n amount of bytes, so
    ///     if there is nothing to read until the number of bytes to read
    ///     is fulfilled, it will block.
    /// </summary>
    /// <param name="n">Number of bytes to read.</param>
    /// <param name="token">
    ///     The token to monitor for cancellation requests. The default value is
    ///     <see cref="P:System.Threading.CancellationToken.None" />.
    /// </param>
    /// <returns>The bytes that was read from the connection.</returns>
    public async Task<byte[]> ReadBytesAsync(int n, CancellationToken token)
    {
        var data = new byte[n];
        var count = 0;
        while (n - count > 0) count += await _stream.ReadAsync(data.AsMemory(count, n - count), token);
        return data;
    }

    /// <summary>
    ///     Send and write some bytes to the connection.
    /// </summary>
    /// <param name="bytes">The bytes to write.</param>
    /// <returns></returns>
    public async Task WriteBytesAsync(byte[] bytes)
    {
        await _stream.WriteAsync(bytes.AsMemory(0, bytes.Length));
        await _stream.FlushAsync();
    }
}