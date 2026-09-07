using System.IO;
using System.Text;
using Aspose.Imaging.Foss.Internal;

namespace Aspose.Imaging.Foss.Formats;

/// <summary>
/// Detects standard DICOM files (128-byte preamble + "DICM" magic). Pixel dimensions require
/// walking data elements with transfer-syntax-aware VR parsing, which is not yet implemented.
/// </summary>
internal sealed class DicomFormatHandler : IFormatHandler
{
    public ImageFormat Format => ImageFormat.Dicom;
    public int SignatureLength => 132;

    public bool MatchesSignature(byte[] header, int headerLength) =>
        Encoding.ASCII.GetString(header, 128, 4) == "DICM";

    public ImageInfo ReadInfo(Stream stream) => new(Format);
}
