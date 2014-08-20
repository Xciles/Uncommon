using System;

namespace Xciles.Uncommon.Net
{
    [Obsolete("This will be removed in a future version because this lib will start to use the HttpClient instead of just webrequests.")]
    public enum ERestRequestExceptionStatus
    {
        UnknownError = -1,

        Undefined = 0,
        Ok = 1,
        Timeout = 2,
        NoConnection = 3,
        Failed = 4,
        ServiceError = 5,
        ProtocolError = 6,
        SerializationError = 7
    }
}