using System;
using System.IO;
using Aspose.Imaging.Foss.Internal;

namespace Aspose.Imaging.Foss.Formats;

internal sealed class BmpFormatHandler : IFormatHandler
{
    public ImageFormat Format => ImageFormat.Bmp;
    public int SignatureLength => 2;

    public bool MatchesSignature(byte[] header, int headerLength) =>
        header[0] == (byte)'B' && header[1] == (byte)'M';

    public ImageInfo ReadInfo(Stream stream)
    {
        var buffer = new byte[30];
        stream.Seek(0, SeekOrigin.Begin);
        stream.ReadFully(buffer, 0, buffer.Length);

        var width = EndianReader.ReadInt32(buffer, 18, bigEndian: false);
        // BMP stores a negative height for top-down (non-flipped) pixel layouts; magnitude is the pixel height.
        var height = Math.Abs(EndianReader.ReadInt32(buffer, 22, bigEndian: false));
        var bitCount = EndianReader.ReadUInt16(buffer, 28, bigEndian: false);

        return new ImageInfo(Format, width, height, bitCount, frameCount: 1);
    }
}
