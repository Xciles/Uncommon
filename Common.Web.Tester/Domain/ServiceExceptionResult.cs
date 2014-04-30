using System;
using System.Net;

namespace Xciles.Common.Web.Tester.Domain
{
    public class ServiceExceptionResult : Exception
    {
        public string ExceptionResultTypeValue { get; set; }
        public new string Message { get; set; }
        public string MessageDetail { get; set; }
        public new string StackTrace { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }
    }
}
