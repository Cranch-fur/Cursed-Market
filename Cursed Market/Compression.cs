using System;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace Cursed_Market
{
    public static class Compression
    {
        public static class ZLIB
        {
            public static string Compress(string input)
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (var gzipStream = new GZipStream(memoryStream, CompressionMode.Compress))
                    {
                        gzipStream.Write(bytes, 0, bytes.Length);
                    }

                    byte[] compressedBytes = memoryStream.ToArray();
                    return Convert.ToBase64String(compressedBytes);
                }
            }


            public static string Decompress(string base64Input)
            {
                byte[] compressedBytes = Convert.FromBase64String(base64Input);

                using (MemoryStream memoryStream = new MemoryStream(compressedBytes))
                {
                    using (var gzipStream = new GZipStream(memoryStream, CompressionMode.Decompress))
                    {
                        using (var resultStream = new MemoryStream())
                        {
                            gzipStream.CopyTo(resultStream);
                            byte[] decompressedBytes = resultStream.ToArray();
                            return Encoding.UTF8.GetString(decompressedBytes);
                        }
                    }
                }
            }
        }
    }
}
