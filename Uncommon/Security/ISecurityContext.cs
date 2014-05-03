namespace Xciles.Uncommon.Security
{
    public interface ISecurityContext
    {
        string GenerateAuthorizationHeader();
    }
}
