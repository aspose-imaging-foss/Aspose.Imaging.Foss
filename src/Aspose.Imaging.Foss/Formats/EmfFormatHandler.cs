using System.IO;
using Aspose.Imaging.Foss.Internal;

namespace Aspose.Imaging.Foss.Formats;

internal sealed class EmfFormatHandler : IFormatHandler
{
    private static readonly byte[] SignatureMagic = { 0x20, 0x45, 0x4D, 0x46 };

    public ImageFormat Format => ImageFormat.Emf;
    public int SignatureLength => 44;

    public bool MatchesSignature(byte[] header, int headerLength)
    {
        var iType = EndianReader.ReadUInt32(header, 0, bigEndian: false);
        if (iType != 1)
            return false;

        for (var i = 0; i < SignatureMagic.Length; i++)
        {
            if (header[40 + i] != SignatureMagic[i])
                return false;
        }
        return true;
    }

    public ImageInfo ReadInfo(Stream stream)
    {
        var buffer = new byte[24];
        stream.Seek(0, SeekOrigin.Begin);
        stream.ReadFully(buffer, 0, buffer.Length);

        // EMF has no explicit width/height field; pixel dimensions are derived from the rclBounds device-space rectangle.
        var left = EndianReader.ReadInt32(buffer, 8, bigEndian: false);
        var top = EndianReader.ReadInt32(buffer, 12, bigEndian: false);
        var right = EndianReader.ReadInt32(buffer, 16, bigEndian: false);
        var bottom = EndianReader.ReadInt32(buffer, 20, bigEndian: false);

        return new ImageInfo(Format, right - left, bottom - top, frameCount: 1);
    }
}
