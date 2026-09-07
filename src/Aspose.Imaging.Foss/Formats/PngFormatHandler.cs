using System.IO;
using Aspose.Imaging.Foss.Internal;

namespace Aspose.Imaging.Foss.Formats;

internal sealed class PngFormatHandler : IFormatHandler
{
    private static readonly byte[] Magic = { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A };

    public ImageFormat Format => ImageFormat.Png;
    public int SignatureLength => Magic.Length;

    public bool MatchesSignature(byte[] header, int headerLength)
    {
        for (var i = 0; i < Magic.Length; i++)
        {
            if (header[i] != Magic[i])
                return false;
        }
        return true;
    }

    public ImageInfo ReadInfo(Stream stream)
    {
        var buffer = new byte[26];
        stream.Seek(0, SeekOrigin.Begin);
        stream.ReadFully(buffer, 0, buffer.Length);

        var width = (int)EndianReader.ReadUInt32(buffer, 16, bigEndian: true);
        var height = (int)EndianReader.ReadUInt32(buffer, 20, bigEndian: true);
        var bitDepth = (int)buffer[24];

        return new ImageInfo(Format, width, height, bitDepth, frameCount: 1);
    }
}
