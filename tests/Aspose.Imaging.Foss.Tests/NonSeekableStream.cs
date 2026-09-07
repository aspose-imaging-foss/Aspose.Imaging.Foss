using System.IO;

namespace Aspose.Imaging.Foss.Tests;

internal sealed class NonSeekableStream : MemoryStream
{
    public NonSeekableStream(byte[] data) : base(data)
    {
    }

    public override bool CanSeek => false;
}
