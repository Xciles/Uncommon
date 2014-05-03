using System.Net;

namespace Xciles.Common.Net
{
    public class RestResponse<T>
    {
        public object State { get; set; }
        public CookieContainer CookieContainer { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        internal byte[] RawResponseContent { get; set; }
        public T Result { get; set; }
    }
}