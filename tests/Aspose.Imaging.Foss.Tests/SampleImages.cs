namespace Aspose.Imaging.Foss.Tests;

internal static class SampleImages
{
    public static byte[] Png(int width, int height, int bitDepth = 8) =>
        new ByteBuilder()
            .Bytes(0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A)
            .UInt32BE(13)
            .Ascii("IHDR")
            .UInt32BE((uint)width)
            .UInt32BE((uint)height)
            .Bytes((byte)bitDepth, 2, 0, 0, 0)
            .Zeros(4)
            .ToArray();

    public static byte[] Jpeg(int width, int height, int precision = 8) =>
        new ByteBuilder()
            .Bytes(0xFF, 0xD8, 0xFF, 0xC0)
            .UInt16BE(7)
            .Bytes((byte)precision)
            .UInt16BE(height)
            .UInt16BE(width)
            .ToArray();

    public static byte[] TruncatedJpeg() => new byte[] { 0xFF, 0xD8, 0xFF };

    public static byte[] Gif(int width, int height) =>
        new ByteBuilder()
            .Ascii("GIF89a")
            .UInt16LE(width)
            .UInt16LE(height)
            .Bytes(0, 0, 0)
            .ToArray();

    public static byte[] Bmp(int width, int height, int bitCount = 24) =>
        new ByteBuilder()
            .Ascii("BM")
            .UInt32LE(0)
            .UInt32LE(0)
            .UInt32LE(54)
            .UInt32LE(40)
            .Int32LE(width)
            .Int32LE(height)
            .UInt16LE(1)
            .UInt16LE(bitCount)
            .ToArray();

    public static byte[] WebPLossy(int width, int height) =>
        new ByteBuilder()
            .Ascii("RIFF")
            .UInt32LE(0)
            .Ascii("WEBP")
            .Ascii("VP8 ")
            .UInt32LE(10)
            .Bytes(0, 0, 0)
            .Bytes(0x9d, 0x01, 0x2a)
            .UInt16LE(width & 0x3FFF)
            .UInt16LE(height & 0x3FFF)
            .ToArray();

    public static byte[] WebPLossless(int width, int height)
    {
        var packed = (uint)((width - 1) & 0x3FFF) | (uint)(((height - 1) & 0x3FFF) << 14);
        return new ByteBuilder()
            .Ascii("RIFF")
            .UInt32LE(0)
            .Ascii("WEBP")
            .Ascii("VP8L")
            .UInt32LE(5)
            .Bytes(0x2F)
            .UInt32LE(packed)
            .ToArray();
    }

    public static byte[] WebPExtended(int width, int height) =>
        new ByteBuilder()
            .Ascii("RIFF")
            .UInt32LE(0)
            .Ascii("WEBP")
            .Ascii("VP8X")
            .UInt32LE(10)
            .Bytes(0)
            .Zeros(3)
            .UInt24LE(width - 1)
            .UInt24LE(height - 1)
            .ToArray();

    public static byte[] Ico(int width, int height, int bitCount = 32, int imageCount = 1) =>
        new ByteBuilder()
            .Bytes(0, 0, 1, 0)
            .UInt16LE(imageCount)
            .Bytes((byte)width, (byte)height, 0, 0)
            .UInt16LE(1)
            .UInt16LE(bitCount)
            .UInt32LE(0)
            .UInt32LE(22)
            .ToArray();

    public static byte[] Tiff(int width, int height, int bitDepth = 8)
    {
        var builder = new ByteBuilder()
            .Ascii("II")
            .Bytes(0x2A, 0x00)
            .UInt32LE(8)
            .UInt16LE(3);

        AddShortEntry(builder, 256, width);
        AddShortEntry(builder, 257, height);
        AddShortEntry(builder, 258, bitDepth);

        return builder.ToArray();
    }

    private static void AddShortEntry(ByteBuilder builder, int tag, int value)
    {
        builder.UInt16LE(tag);
        builder.UInt16LE(3);
        builder.UInt32LE(1);
        builder.UInt16LE(value);
        builder.UInt16LE(0);
    }

    public static byte[] Psd(int width, int height, int depth = 8) =>
        new ByteBuilder()
            .Ascii("8BPS")
            .UInt16BE(1)
            .Zeros(6)
            .UInt16BE(3)
            .UInt32BE((uint)height)
            .UInt32BE((uint)width)
            .UInt16BE(depth)
            .UInt16BE(3)
            .ToArray();

    public static byte[] Emf(int width, int height) =>
        new ByteBuilder()
            .UInt32LE(1)
            .UInt32LE(88)
            .Int32LE(0)
            .Int32LE(0)
            .Int32LE(width)
            .Int32LE(height)
            .Zeros(16)
            .Bytes(0x20, 0x45, 0x4D, 0x46)
            .ToArray();

    public static byte[] Wmf(int width, int height)
    {
        const int inch = 1440;
        var right = width * inch / 96;
        var bottom = height * inch / 96;

        return new ByteBuilder()
            .Bytes(0x9A, 0xC6, 0xCD, 0xD7)
            .UInt16LE(0)
            .UInt16LE(0)
            .UInt16LE(0)
            .UInt16LE(right)
            .UInt16LE(bottom)
            .UInt16LE(inch)
            .UInt32LE(0)
            .UInt16LE(0)
            .ToArray();
    }

    public static byte[] Dicom() =>
        new ByteBuilder()
            .Zeros(128)
            .Ascii("DICM")
            .ToArray();
}
