using System.IO;
using Aspose.Imaging.Foss.Internal;

namespace Aspose.Imaging.Foss.Formats;

internal sealed class TiffFormatHandler : IFormatHandler
{
    public ImageFormat Format => ImageFormat.Tiff;
    public int SignatureLength => 4;

    public bool MatchesSignature(byte[] header, int headerLength)
    {
        var littleEndian = header[0] == 'I' && header[1] == 'I' && header[2] == 0x2A && header[3] == 0x00;
        var bigEndian = header[0] == 'M' && header[1] == 'M' && header[2] == 0x00 && header[3] == 0x2A;
        return littleEndian || bigEndian;
    }

    public ImageInfo ReadInfo(Stream stream)
    {
        var header = new byte[8];
        stream.Seek(0, SeekOrigin.Begin);
        stream.ReadFully(header, 0, header.Length);

        var bigEndian = header[0] == 'M';
        var ifdOffset = EndianReader.ReadUInt32(header, 4, bigEndian);

        stream.Seek(ifdOffset, SeekOrigin.Begin);
        var countBuf = new byte[2];
        stream.ReadFully(countBuf, 0, 2);
        var entryCount = EndianReader.ReadUInt16(countBuf, 0, bigEndian);

        int? width = null;
        int? height = null;
        int? bitDepth = null;

        var entry = new byte[12];
        for (var i = 0; i < entryCount; i++)
        {
            stream.ReadFully(entry, 0, entry.Length);
            var tag = EndianReader.ReadUInt16(entry, 0, bigEndian);
            var type = EndianReader.ReadUInt16(entry, 2, bigEndian);

            switch (tag)
            {
                case 256:
                    width = ReadIfdValue(entry, type, bigEndian);
                    break;
                case 257:
                    height = ReadIfdValue(entry, type, bigEndian);
                    break;
                case 258:
                    bitDepth = ReadIfdValue(entry, type, bigEndian);
                    break;
            }

            if (width.HasValue && height.HasValue && bitDepth.HasValue)
                break;
        }

        return new ImageInfo(Format, width, height, bitDepth, frameCount: 1);
    }

    // For SHORT/LONG types with a count of 1, TIFF stores the value left-justified in the entry's 4-byte value field.
    private static int ReadIfdValue(byte[] entry, ushort type, bool bigEndian) =>
        type == 3
            ? EndianReader.ReadUInt16(entry, 8, bigEndian)
            : EndianReader.ReadInt32(entry, 8, bigEndian);
}
