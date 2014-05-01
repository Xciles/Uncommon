using System.Threading.Tasks;
using Xciles.Common.Security;

namespace Xciles.Common.Net
{
    public class RestRequestHelper
    {
        public static ISecurityContext SecurityContext { get; set; }

        public static async Task<RestResponse<T>> ProcessGetRequest<T>(string restRequestUri, object state = null, RestRequestOptions options = null)
        {
            var restRequest = CreateRestRequest(ERestMethod.GET, restRequestUri, state, options);

            return await restRequest.ProcessRequest<T>().ConfigureAwait(false);
        }

        public static async Task<RestResponse<byte[]>> ProcessRawGetRequest(string restRequestUri, object state = null, RestRequestOptions options = null)
        {
            var restRequest = CreateRestRequest(ERestMethod.GET, restRequestUri, state, options);

            restRequest.Options.ResponseSerializer = EResponseSerializer.UseByteArray;

            var result = await restRequest.ProcessRequest<byte[]>().ConfigureAwait(false);

            result.Result = result.RawResponseContent;

            return result;
        }

        public static async Task<RestResponse<NoResponseContent>> ProcessPostRequest<TRequestType>(string restRequestUri, TRequestType requestContent, object state = null, RestRequestOptions options = null)
        {
            var restRequest = CreateRestRequest(ERestMethod.POST, restRequestUri, state, options);

            return await restRequest.ProcessRequest<TRequestType, NoResponseContent>(requestContent).ConfigureAwait(false);
        }

        public static async Task<RestResponse<TResponseType>> ProcessPostRequest<TResponseType, TRequestType>(string restRequestUri, TRequestType requestContent, object state = null, RestRequestOptions options = null)
        {
            var restRequest = CreateRestRequest(ERestMethod.POST, restRequestUri, state, options);

            return await restRequest.ProcessRequest<TRequestType, TResponseType>(requestContent).ConfigureAwait(false);
        }

        public static async Task<RestResponse<NoResponseContent>> ProcessPutRequest<TRequestType>(string restRequestUri, TRequestType requestContent, object state = null, RestRequestOptions options = null)
        {
            var restRequest = CreateRestRequest(ERestMethod.POST, restRequestUri, state, options);

            return await restRequest.ProcessRequest<TRequestType, NoResponseContent>(requestContent).ConfigureAwait(false);
        }

        public static async Task<RestResponse<TResponseType>> ProcessPutRequest<TResponseType, TRequestType>(string restRequestUri, TRequestType requestContent, object state = null, RestRequestOptions options = null)
        {
            var restRequest = CreateRestRequest(ERestMethod.PUT, restRequestUri, state, options);

            return await restRequest.ProcessRequest<TRequestType, TResponseType>(requestContent).ConfigureAwait(false);
        }

        public static async Task<RestResponse<NoResponseContent>> ProcessPatchRequest<TRequestType>(string restRequestUri, TRequestType requestContent, object state = null, RestRequestOptions options = null)
        {
            var restRequest = CreateRestRequest(ERestMethod.POST, restRequestUri, state, options);

            return await restRequest.ProcessRequest<TRequestType, NoResponseContent>(requestContent).ConfigureAwait(false);
        }

        public static async Task<RestResponse<TResponseType>> ProcessPatchRequest<TResponseType, TRequestType>(string restRequestUri, TRequestType requestContent, object state = null, RestRequestOptions options = null)
        {
            var restRequest = CreateRestRequest(ERestMethod.PATCH, restRequestUri, state, options);

            return await restRequest.ProcessRequest<TRequestType, TResponseType>(requestContent).ConfigureAwait(false);
        }

        public static async Task<RestResponse<TResponseType>> ProcessDeleteRequest<TResponseType, TRequestType>(string restRequestUri, TRequestType requestContent, object state = null, RestRequestOptions options = null)
        {
            var restRequest = CreateRestRequest(ERestMethod.DELETE, restRequestUri, state, options);

            return await restRequest.ProcessRequest<TRequestType, TResponseType>(requestContent).ConfigureAwait(false);
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
