using System;
using System.Net;

namespace Xciles.Uncommon.Net
{
    [Obsolete("This will be removed in a future version because this lib will start to use the HttpClient instead of just webrequests.")]
    public class RestRequestException : Exception
    {
        public ERestRequestExceptionStatus RestRequestExceptionStatus { get; set; }
        public Exception Exception { get; set; }
        public ServiceExceptionResult ServiceExceptionResult { get; set; }
        public string Information { get; set; }
        public WebExceptionStatus WebExceptionStatus { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
