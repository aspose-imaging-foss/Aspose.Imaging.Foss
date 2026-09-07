using System.IO;

namespace Aspose.Imaging.Foss.Internal;

internal interface IFormatHandler
{
    ImageFormat Format { get; }

    int SignatureLength { get; }

    bool MatchesSignature(byte[] header, int headerLength);

    ImageInfo ReadInfo(Stream stream);
}
