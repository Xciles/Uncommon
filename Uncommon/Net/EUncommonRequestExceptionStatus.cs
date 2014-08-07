namespace Xciles.Uncommon.Net
{
    public enum EUncommonRequestExceptionStatus
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