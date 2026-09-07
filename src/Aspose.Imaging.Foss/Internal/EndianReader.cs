namespace Aspose.Imaging.Foss.Internal;

internal static class EndianReader
{
    public static ushort ReadUInt16(byte[] buffer, int offset, bool bigEndian) =>
        bigEndian
            ? (ushort)((buffer[offset] << 8) | buffer[offset + 1])
            : (ushort)(buffer[offset] | (buffer[offset + 1] << 8));

    public static uint ReadUInt32(byte[] buffer, int offset, bool bigEndian) =>
        bigEndian
            ? ((uint)buffer[offset] << 24) | ((uint)buffer[offset + 1] << 16) | ((uint)buffer[offset + 2] << 8) | buffer[offset + 3]
            : ((uint)buffer[offset + 3] << 24) | ((uint)buffer[offset + 2] << 16) | ((uint)buffer[offset + 1] << 8) | buffer[offset];

    public static int ReadInt32(byte[] buffer, int offset, bool bigEndian) =>
        unchecked((int)ReadUInt32(buffer, offset, bigEndian));

    public static int ReadUInt24LittleEndian(byte[] buffer, int offset) =>
        buffer[offset] | (buffer[offset + 1] << 8) | (buffer[offset + 2] << 16);
}
