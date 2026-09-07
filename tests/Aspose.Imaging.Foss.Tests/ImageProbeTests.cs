using System.IO;

namespace Aspose.Imaging.Foss.Tests;

public class ImageProbeTests
{
    [Fact]
    public void Png_ReportsFormatAndDimensions()
    {
        var info = ImageProbe.Probe(SampleImages.Png(640, 480, 8));

        Assert.Equal(ImageFormat.Png, info.Format);
        Assert.Equal(640, info.Width);
        Assert.Equal(480, info.Height);
        Assert.Equal(8, info.BitDepth);
        Assert.Equal(1, info.FrameCount);
    }

    [Fact]
    public void Jpeg_ReportsFormatAndDimensions()
    {
        var info = ImageProbe.Probe(SampleImages.Jpeg(1024, 768));

        Assert.Equal(ImageFormat.Jpeg, info.Format);
        Assert.Equal(1024, info.Width);
        Assert.Equal(768, info.Height);
        Assert.Equal(8, info.BitDepth);
    }

    [Fact]
    public void Jpeg_TruncatedStream_DegradesToFormatOnlyInsteadOfThrowing()
    {
        var info = ImageProbe.Probe(SampleImages.TruncatedJpeg());

        Assert.Equal(ImageFormat.Jpeg, info.Format);
        Assert.Null(info.Width);
        Assert.Null(info.Height);
    }

    [Fact]
    public void Gif_ReportsFormatAndDimensions()
    {
        var info = ImageProbe.Probe(SampleImages.Gif(320, 240));

        Assert.Equal(ImageFormat.Gif, info.Format);
        Assert.Equal(320, info.Width);
        Assert.Equal(240, info.Height);
    }

    [Fact]
    public void Bmp_ReportsFormatAndDimensions()
    {
        var info = ImageProbe.Probe(SampleImages.Bmp(200, 100, 24));

        Assert.Equal(ImageFormat.Bmp, info.Format);
        Assert.Equal(200, info.Width);
        Assert.Equal(100, info.Height);
        Assert.Equal(24, info.BitDepth);
    }

    [Fact]
    public void Bmp_NegativeHeight_ReportsPositiveMagnitude()
    {
        var info = ImageProbe.Probe(SampleImages.Bmp(200, -100, 24));

        Assert.Equal(100, info.Height);
    }

    [Theory]
    [InlineData("lossy")]
    [InlineData("lossless")]
    [InlineData("extended")]
    public void WebP_ReportsFormatAndDimensions(string variant)
    {
        var bytes = variant switch
        {
            "lossy" => SampleImages.WebPLossy(400, 300),
            "lossless" => SampleImages.WebPLossless(400, 300),
            _ => SampleImages.WebPExtended(400, 300),
        };

        var info = ImageProbe.Probe(bytes);

        Assert.Equal(ImageFormat.WebP, info.Format);
        Assert.Equal(400, info.Width);
        Assert.Equal(300, info.Height);
    }

    [Fact]
    public void Ico_ReportsFormatDimensionsAndFrameCount()
    {
        var info = ImageProbe.Probe(SampleImages.Ico(32, 32, 32, imageCount: 3));

        Assert.Equal(ImageFormat.Ico, info.Format);
        Assert.Equal(32, info.Width);
        Assert.Equal(32, info.Height);
        Assert.Equal(3, info.FrameCount);
    }

    [Fact]
    public void Ico_ZeroDimensionByte_MeansTwoFiftySixPixels()
    {
        var info = ImageProbe.Probe(SampleImages.Ico(0, 0, 32));

        Assert.Equal(256, info.Width);
        Assert.Equal(256, info.Height);
    }

    [Fact]
    public void Tiff_LittleEndian_ReportsFormatAndDimensions()
    {
        var info = ImageProbe.Probe(SampleImages.Tiff(1600, 1200, 8));

        Assert.Equal(ImageFormat.Tiff, info.Format);
        Assert.Equal(1600, info.Width);
        Assert.Equal(1200, info.Height);
        Assert.Equal(8, info.BitDepth);
    }

    [Fact]
    public void Psd_ReportsFormatAndDimensions()
    {
        var info = ImageProbe.Probe(SampleImages.Psd(500, 400, 8));

        Assert.Equal(ImageFormat.Psd, info.Format);
        Assert.Equal(500, info.Width);
        Assert.Equal(400, info.Height);
        Assert.Equal(8, info.BitDepth);
    }

    [Fact]
    public void Emf_DerivesDimensionsFromBounds()
    {
        var info = ImageProbe.Probe(SampleImages.Emf(800, 600));

        Assert.Equal(ImageFormat.Emf, info.Format);
        Assert.Equal(800, info.Width);
        Assert.Equal(600, info.Height);
    }

    [Fact]
    public void Wmf_Placeable_ReportsDimensionsAt96Dpi()
    {
        var info = ImageProbe.Probe(SampleImages.Wmf(200, 100));

        Assert.Equal(ImageFormat.Wmf, info.Format);
        Assert.Equal(200, info.Width);
        Assert.Equal(100, info.Height);
    }

    [Fact]
    public void Dicom_DetectsFormatButDimensionsAreNotYetSupported()
    {
        var info = ImageProbe.Probe(SampleImages.Dicom());

        Assert.Equal(ImageFormat.Dicom, info.Format);
        Assert.Null(info.Width);
        Assert.Null(info.Height);
    }

    [Fact]
    public void UnknownData_ReportsUnknownFormat()
    {
        var info = ImageProbe.Probe(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 });

        Assert.Equal(ImageFormat.Unknown, info.Format);
        Assert.Null(info.Width);
        Assert.Null(info.Height);
    }

    [Fact]
    public void DetectFormat_MatchesProbeFormat()
    {
        var format = ImageProbe.DetectFormat(SampleImages.Png(10, 10));

        Assert.Equal(ImageFormat.Png, format);
    }

    [Fact]
    public void ProbeFile_ReadsFromDisk()
    {
        var path = Path.GetTempFileName();
        try
        {
            File.WriteAllBytes(path, SampleImages.Png(50, 60));
            var info = ImageProbe.ProbeFile(path);

            Assert.Equal(ImageFormat.Png, info.Format);
            Assert.Equal(50, info.Width);
            Assert.Equal(60, info.Height);
        }
        finally
        {
            File.Delete(path);
        }
    }

    [Fact]
    public void Probe_NonSeekableStream_StillWorks()
    {
        var info = ImageProbe.Probe(new NonSeekableStream(SampleImages.Png(30, 20)));

        Assert.Equal(ImageFormat.Png, info.Format);
        Assert.Equal(30, info.Width);
        Assert.Equal(20, info.Height);
    }
}
