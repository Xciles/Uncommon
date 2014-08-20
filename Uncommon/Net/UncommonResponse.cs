using System.Net;

namespace Xciles.Uncommon.Net
{
    public class UncommonResponse<T>
    {
        public HttpStatusCode StatusCode { get; set; }

        internal byte[] RawResponseContent { get; set; }
        public T Result { get; set; }
    }
}