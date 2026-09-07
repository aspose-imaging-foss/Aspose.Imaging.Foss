using System.IO;
using Aspose.Imaging.Foss.Internal;

namespace Aspose.Imaging.Foss.Formats;

internal sealed class IcoFormatHandler : IFormatHandler
{
    private static readonly byte[] Magic = { 0x00, 0x00, 0x01, 0x00 };

    public ImageFormat Format => ImageFormat.Ico;
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
        var buffer = new byte[22];
        stream.Seek(0, SeekOrigin.Begin);
        stream.ReadFully(buffer, 0, buffer.Length);

        var imageCount = EndianReader.ReadUInt16(buffer, 4, bigEndian: false);
        // A dimension byte of 0 means 256px, per the ICO directory entry spec.
        var width = buffer[6] == 0 ? 256 : buffer[6];
        var height = buffer[7] == 0 ? 256 : buffer[7];
        var bitCount = EndianReader.ReadUInt16(buffer, 12, bigEndian: false);

        return new ImageInfo(Format, width, height, bitCount, frameCount: imageCount);
    }
}
