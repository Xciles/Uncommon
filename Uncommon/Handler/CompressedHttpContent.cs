using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Xciles.Uncommon.Handler
{
    public class CompressedHttpContent : HttpContent
    {
        protected Stream Stream;

        protected override Task SerializeToStreamAsync(Stream stream, TransportContext context)
        {
            return Stream.CopyToAsync(stream);
        }

        protected override bool TryComputeLength(out long length)
        {
            length = 0;
            return false;
        }

        protected override void Dispose(bool disposing)
        {
            Stream.Dispose();
            base.Dispose(disposing);
        }
    }
}
