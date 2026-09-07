using System.Collections.Generic;
using System.Text;

namespace Aspose.Imaging.Foss.Tests;

internal sealed class ByteBuilder
{
    private readonly List<byte> _bytes = new();

    public ByteBuilder Bytes(params byte[] values)
    {
        _bytes.AddRange(values);
        return this;
    }

    public ByteBuilder Ascii(string value)
    {
        _bytes.AddRange(Encoding.ASCII.GetBytes(value));
        return this;
    }

    public ByteBuilder Zeros(int count)
    {
        _bytes.AddRange(new byte[count]);
        return this;
    }

    public ByteBuilder UInt16LE(int value)
    {
        _bytes.Add((byte)(value & 0xFF));
        _bytes.Add((byte)((value >> 8) & 0xFF));
        return this;
    }

    public ByteBuilder UInt16BE(int value)
    {
        _bytes.Add((byte)((value >> 8) & 0xFF));
        _bytes.Add((byte)(value & 0xFF));
        return this;
    }

    public ByteBuilder UInt32LE(uint value)
    {
        _bytes.Add((byte)(value & 0xFF));
        _bytes.Add((byte)((value >> 8) & 0xFF));
        _bytes.Add((byte)((value >> 16) & 0xFF));
        _bytes.Add((byte)((value >> 24) & 0xFF));
        return this;
    }

    public ByteBuilder UInt32BE(uint value)
    {
        _bytes.Add((byte)((value >> 24) & 0xFF));
        _bytes.Add((byte)((value >> 16) & 0xFF));
        _bytes.Add((byte)((value >> 8) & 0xFF));
        _bytes.Add((byte)(value & 0xFF));
        return this;
    }

    public ByteBuilder Int32LE(int value) => UInt32LE(unchecked((uint)value));

    public ByteBuilder UInt24LE(int value)
    {
        _bytes.Add((byte)(value & 0xFF));
        _bytes.Add((byte)((value >> 8) & 0xFF));
        _bytes.Add((byte)((value >> 16) & 0xFF));
        return this;
    }

    public byte[] ToArray() => _bytes.ToArray();
}
