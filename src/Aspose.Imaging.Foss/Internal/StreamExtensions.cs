using System.IO;

namespace Aspose.Imaging.Foss.Internal;

internal static class StreamExtensions
{
    public static int ReadFully(this Stream stream, byte[] buffer, int offset, int count)
    {
        var totalRead = 0;
        while (totalRead < count)
        {
            var read = stream.Read(buffer, offset + totalRead, count - totalRead);
            if (read == 0)
                break;
            totalRead += read;
        }
        return totalRead;
    }
}
