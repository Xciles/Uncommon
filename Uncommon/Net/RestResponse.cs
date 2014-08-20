using System;
using System.Net;

namespace Xciles.Uncommon.Net
{
    [Obsolete("This will be removed in a future version because this lib will start to use the HttpClient instead of just webrequests.")]
    public class RestResponse<T>
    {
        [Obsolete("This will be removed in a future version.")]
        public object State { get; set; }
        [Obsolete("This will be removed in a future version.")]
        public CookieContainer CookieContainer { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        internal byte[] RawResponseContent { get; set; }
        public T Result { get; set; }
    }
}