namespace Xciles.Uncommon.Net
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