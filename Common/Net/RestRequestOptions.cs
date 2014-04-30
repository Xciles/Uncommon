using System.Net;
using Xciles.Common.Security;

namespace Xciles.Common.Net
{
    public class RestRequestOptions
    {
        public bool Authorized { get; set; }
        public WebHeaderCollection Headers { get; set; }
        public int Timeout { get; set; }
        public CookieContainer CookieContainer { get; set; }
        public ERequestSerializer RequestSerializer { get; set; }
        public EResponseSerializer ResponseSerializer { get; set; }
        public ISecurityContext SecurityContext { get; set; }

        public RestRequestOptions()
        {
            Authorized = false;
            Timeout = 30000;
            RequestSerializer = ERequestSerializer.UseJsonNet;
            ResponseSerializer = EResponseSerializer.UseJsonNet;
        }
    }
}
