using System;
using System.Net;

namespace Xciles.Common.Net
{
    public enum ECommunicationResult
    {
        Undefined = 0,
        Ok = 1,
        Timeout = 2,
        NoConnection = 3,
        Failed = 4,
        ServiceException = 5,
        ProtocolError = 6,
        SerializationError = 7,
        UnknownError = -1
    }

    public class RestRequestException : Exception
    {
        public ECommunicationResult CommunicationResult { get; set; }
        public Exception Exception { get; set; }
        public ServiceExceptionResult ServiceExceptionResult { get; set; }
        public string Information { get; set; }
        public WebExceptionStatus WebExceptionStatus { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
