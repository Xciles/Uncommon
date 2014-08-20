namespace Xciles.Uncommon.Net
{
    public enum EUncommonRequestSerializer
    {
        Undefined,
        UseXmlDataContractSerializer,
        UseXmlSerializer,
        UseByteArray,
        UseJsonNet,
        UseStringUrlPost
    }
}