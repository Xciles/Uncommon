using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Xciles.Uncommon.Net
{
    public class UncommonRequestException : Exception
    {
        public EUncommonRequestExceptionStatus UncommonRequestExceptionStatus { get; set; }
        public Exception Exception { get; set; }
        public ServiceExceptionResult ServiceExceptionResult { get; set; }
        public string Information { get; set; }
        public WebExceptionStatus WebExceptionStatus { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
