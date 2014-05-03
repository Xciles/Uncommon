using System.Net;

namespace Xciles.Common.Net
{
    public class ServiceExceptionResult
    {
        public string ExceptionResultTypeValue { get; set; }
        public string Message { get; set; }
        public string MessageDetail { get; set; }
        public string StackTrace { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }
    }
}
