namespace Xciles.Common.Net
{
    public enum ERequestSerializer
    {
        Undefined,
        UseXmlDataContractSerializer,
        UseXmlSerializer,
        UseByteArray,
        UseJsonNet,
        UseStringUrlPost
    }
}