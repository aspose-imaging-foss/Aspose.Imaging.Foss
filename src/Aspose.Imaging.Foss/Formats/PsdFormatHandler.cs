using System.IO;
using System.Text;
using Aspose.Imaging.Foss.Internal;

namespace Aspose.Imaging.Foss.Formats;

internal sealed class PsdFormatHandler : IFormatHandler
{
    public ImageFormat Format => ImageFormat.Psd;
    public int SignatureLength => 4;

    public bool MatchesSignature(byte[] header, int headerLength) =>
        Encoding.ASCII.GetString(header, 0, 4) == "8BPS";

    public ImageInfo ReadInfo(Stream stream)
    {
        var buffer = new byte[26];
        stream.Seek(0, SeekOrigin.Begin);
        stream.ReadFully(buffer, 0, buffer.Length);

        var height = (int)EndianReader.ReadUInt32(buffer, 14, bigEndian: true);
        var width = (int)EndianReader.ReadUInt32(buffer, 18, bigEndian: true);
        var depth = EndianReader.ReadUInt16(buffer, 22, bigEndian: true);

        return new ImageInfo(Format, width, height, depth, frameCount: 1);
    }
}
