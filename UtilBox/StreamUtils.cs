
using System.Text;

namespace UtilBox
{
    public static class StreamUtils
    {
        // Convert stream to byte array
        public static byte[] ToByteArray(Stream stream)
        {
            using var ms = new MemoryStream();
            stream.CopyTo(ms);
            return ms.ToArray();
        }

        public static async Task<byte[]> ToByteArrayAsync(Stream stream)
        {
            using var ms = new MemoryStream();
            await stream.CopyToAsync(ms);
            return ms.ToArray();
        }

        // Create stream from string
        public static Stream GetStreamFromString(string content, Encoding encoding = null)
        {
            encoding ??= Encoding.UTF8;
            return new MemoryStream(encoding.GetBytes(content));
        }

        // Create stream from byte array
        public static Stream GetStreamFromBytes(byte[] data)
        {
            return new MemoryStream(data);
        }

        // Copy one stream to another
        // bufferSize = 81920 -> 4096 × 20 ( 4KB *20 = 80KB )
        public static void Copy(Stream source, Stream destination, int bufferSize = 81920)
        {
            source.CopyTo(destination, bufferSize);
        }

        public static async Task CopyAsync(Stream source, Stream destination, int bufferSize = 81920)
        {
            await source.CopyToAsync(destination, bufferSize);
        }


        // Read stream into string
        public static string ReadToString(Stream stream, Encoding encoding = null)
        {
            encoding ??= Encoding.UTF8;
            using var reader = new StreamReader(stream, encoding, detectEncodingFromByteOrderMarks: true, leaveOpen: true);
            return reader.ReadToEnd();
        }

        public static async Task<string> ReadToStringAsync(Stream stream, Encoding encoding = null)
        {
            encoding ??= Encoding.UTF8;
            using var reader = new StreamReader(stream, encoding, detectEncodingFromByteOrderMarks: true, leaveOpen: true);
            return await reader.ReadToEndAsync();
        }
    }
}
