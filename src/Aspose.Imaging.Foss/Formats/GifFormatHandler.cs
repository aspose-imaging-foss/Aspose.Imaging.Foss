using System.IO;
using System.Text;
using Aspose.Imaging.Foss.Internal;

namespace Aspose.Imaging.Foss.Formats;

internal sealed class GifFormatHandler : IFormatHandler
{
    public ImageFormat Format => ImageFormat.Gif;
    public int SignatureLength => 6;

    public bool MatchesSignature(byte[] header, int headerLength)
    {
        var signature = Encoding.ASCII.GetString(header, 0, 6);
        return signature == "GIF87a" || signature == "GIF89a";
    }

    public ImageInfo ReadInfo(Stream stream)
    {
        var buffer = new byte[4];
        stream.Seek(6, SeekOrigin.Begin);
        stream.ReadFully(buffer, 0, 4);

        var width = EndianReader.ReadUInt16(buffer, 0, bigEndian: false);
        var height = EndianReader.ReadUInt16(buffer, 2, bigEndian: false);

        return new ImageInfo(Format, width, height);
    }
}
