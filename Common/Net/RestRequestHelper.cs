using System.Threading.Tasks;
using Xciles.Common.Security;

namespace Xciles.Common.Net
{
    public class RestRequestHelper
    {
        public static ISecurityContext SecurityContext { get; set; }

        public static async Task<RestResponse<T>> ProcessGetRequest<T>(string restRequestUri, object state, RestRequestOptions options = null)
        {
            var restRequest = new RestRequest
            {
                State = state,
                Options = SetRestRequestOptions(options),
                RestRequestUri = restRequestUri,
                RestMethod = ERestMethod.GET
            };

            return await restRequest.ProcessRequest<T>();
        }




        private static RestRequestOptions SetRestRequestOptions(RestRequestOptions options)
        {
            options = options ?? new RestRequestOptions();

            if (SecurityContext != null && options.SecurityContext == null)
            {
                options.SecurityContext = SecurityContext;
                options.Authorized = true;
            }

            return options;
        }
    }
}
