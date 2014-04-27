namespace Xciles.Common.Security
{
    public interface ISecurityContext
    {
        string GenerateAuthorizationHeader();
    }
}
