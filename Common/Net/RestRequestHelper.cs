using System.Threading.Tasks;
using Xciles.Common.Security;

namespace Xciles.Common.Net
{
    public class RestRequestHelper
    {
        public static ISecurityContext SecurityContext { get; set; }

        public static async Task<RestResponse<T>> ProcessGetRequest<T>(string restRequestUri, object state, RestRequestOptions options = null)
        {
            var restRequest = CreateRestRequest(ERestMethod.GET, restRequestUri, state, options);

            return await restRequest.ProcessRequest<T>();
        }

        public static async Task<RestResponse<byte[]>> ProcessRawGetRequest(string restRequestUri, object state, RestRequestOptions options = null)
        {
            var restRequest = CreateRestRequest(ERestMethod.GET, restRequestUri, state, options);

            restRequest.Options.ResponseSerializer = EResponseSerializer.UseByteArray;

            var result = await restRequest.ProcessRequest<byte[]>();

            result.Result = result.RawResponseContent;

            return result;
        }

        public static async Task<RestResponse<NoResponseContent>> ProcessPostRequest<TR>(string restRequestUri, object state, TR requestContent, RestRequestOptions options = null)
        {
            var restRequest = CreateRestRequest(ERestMethod.POST, restRequestUri, state, options);

            return await restRequest.ProcessRequest<NoResponseContent, TR>(requestContent);
        }

        public static async Task<RestResponse<T>> ProcessPostRequest<T,TR>(string restRequestUri, object state, TR requestContent, RestRequestOptions options = null)
        {
            var restRequest = CreateRestRequest(ERestMethod.POST, restRequestUri, state, options);

            return await restRequest.ProcessRequest<T, TR>(requestContent);
        }

        public static async Task<RestResponse<T>> ProcessPutRequest<T, TR>(string restRequestUri, object state, TR requestContent, RestRequestOptions options = null)
        {
            var restRequest = CreateRestRequest(ERestMethod.PUT, restRequestUri, state, options);

            return await restRequest.ProcessRequest<T, TR>(requestContent);
        }

        public static async Task<RestResponse<T>> ProcessPatchRequest<T, TR>(string restRequestUri, object state, TR requestContent, RestRequestOptions options = null)
        {
            var restRequest = CreateRestRequest(ERestMethod.PATCH, restRequestUri, state, options);

            return await restRequest.ProcessRequest<T, TR>(requestContent);
        }

        public static async Task<RestResponse<T>> ProcessDeleteRequest<T, TR>(string restRequestUri, object state, TR requestContent, RestRequestOptions options = null)
        {
            var restRequest = CreateRestRequest(ERestMethod.DELETE, restRequestUri, state, options);

            return await restRequest.ProcessRequest<T, TR>(requestContent);
        }


        private static RestRequest CreateRestRequest(ERestMethod restMethod, string restRequestUri, object state, RestRequestOptions options)
        {
            return new RestRequest
            {
                State = state,
                Options = SetRestRequestOptions(options),
                RestRequestUri = restRequestUri,
                RestMethod = restMethod
            };
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
