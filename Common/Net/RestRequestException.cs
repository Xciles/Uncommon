using System;
using System.Net;

namespace Xciles.Common.Net
{
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
