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

        public static async Task<RestResponse<byte[]>> ProcessRawGetRequest(string restRequestUri, object state, RestRequestOptions options = null)
        {
            var restRequest = new RestRequest
            {
                State = state,
                Options = SetRestRequestOptions(options),
                RestRequestUri = restRequestUri,
                RestMethod = ERestMethod.GET,
            };

            restRequest.Options.ResponseSerializer = EResponseSerializer.UseByteArray;

            var result = await restRequest.ProcessRequest<byte[]>();

            result.Result = result.RawResponseContent;

            return result;
        }

        public static async Task<RestResponse<NoResponseContent>> ProcessPostRequest<TR>(string restRequestUri, object state, TR requestContent, RestRequestOptions options = null)
        {
            var restRequest = new RestRequest
            {
                State = state,
                Options = SetRestRequestOptions(options),
                RestRequestUri = restRequestUri,
                RestMethod = ERestMethod.POST
            };

            return await restRequest.ProcessRequest<NoResponseContent, TR>(requestContent);
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
