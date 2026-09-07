namespace Aspose.Imaging.Foss;

public sealed class ImageInfo
{
    public ImageFormat Format { get; }
    public int? Width { get; }
    public int? Height { get; }
    public int? BitDepth { get; }
    public int? FrameCount { get; }

    public ImageInfo(
        ImageFormat format,
        int? width = null,
        int? height = null,
        int? bitDepth = null,
        int? frameCount = null)
    {
        Format = format;
        Width = width;
        Height = height;
        BitDepth = bitDepth;
        FrameCount = frameCount;
    }
}
