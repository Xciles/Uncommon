using System.IO;
using System.Net.Http.Headers;
using Ionic.Zlib;

namespace Xciles.Uncommon.Handler
{
    public class GZipHttpContent : CompressedHttpContent
    {
        public GZipHttpContent(Stream stream)
        {
            Stream = new GZipStream(stream, CompressionMode.Decompress);
        }

        public GZipHttpContent(Stream stream, HttpContentHeaders headers) 
            : this(stream)
        {
            foreach (var pair in headers)
            {
                Headers.TryAddWithoutValidation(pair.Key, pair.Value);
            }
        }
    }
}
