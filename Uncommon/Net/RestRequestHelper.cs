using System.Threading.Tasks;
using Xciles.Uncommon.Security;

namespace Xciles.Uncommon.Net
{
    public class RestRequestHelper
    {
        public static ISecurityContext SecurityContext { get; set; }

        public static async Task<RestResponse<TResponseType>> ProcessGetRequest<TResponseType>(string restRequestUri, object state = null, RestRequestOptions options = null)
        {
            var restRequest = CreateRestRequest(ERestMethod.GET, restRequestUri, state, options);

            return await restRequest.ProcessRequestAsync<TResponseType>().ConfigureAwait(false);
        }

        public static async Task<RestResponse<byte[]>> ProcessRawGetRequest(string restRequestUri, object state = null, RestRequestOptions options = null)
        {
            var restRequest = CreateRestRequest(ERestMethod.GET, restRequestUri, state, options);

            restRequest.Options.ResponseSerializer = EResponseSerializer.UseByteArray;

            var result = await restRequest.ProcessRequestAsync<byte[]>().ConfigureAwait(false);

            result.Result = result.RawResponseContent;

            return result;
        }

        public static async Task<RestResponse<NoResponseContent>> ProcessPostRequest<TRequestType>(string restRequestUri, TRequestType requestContent, object state = null, RestRequestOptions options = null)
        {
            var restRequest = CreateRestRequest(ERestMethod.POST, restRequestUri, state, options);

            return await restRequest.ProcessRequestAsync<TRequestType, NoResponseContent>(requestContent).ConfigureAwait(false);
        }

        public static async Task<RestResponse<TResponseType>> ProcessPostRequest<TRequestType, TResponseType>(string restRequestUri, TRequestType requestContent, object state = null, RestRequestOptions options = null)
        {
            var restRequest = CreateRestRequest(ERestMethod.POST, restRequestUri, state, options);

            return await restRequest.ProcessRequestAsync<TRequestType, TResponseType>(requestContent).ConfigureAwait(false);
        }

        public static async Task<RestResponse<NoResponseContent>> ProcessPutRequest<TRequestType>(string restRequestUri, TRequestType requestContent, object state = null, RestRequestOptions options = null)
        {
            var restRequest = CreateRestRequest(ERestMethod.PUT, restRequestUri, state, options);

            return await restRequest.ProcessRequestAsync<TRequestType, NoResponseContent>(requestContent).ConfigureAwait(false);
        }

        public static async Task<RestResponse<TResponseType>> ProcessPutRequest<TRequestType, TResponseType>(string restRequestUri, TRequestType requestContent, object state = null, RestRequestOptions options = null)
        {
            var restRequest = CreateRestRequest(ERestMethod.PUT, restRequestUri, state, options);

            return await restRequest.ProcessRequestAsync<TRequestType, TResponseType>(requestContent).ConfigureAwait(false);
        }

        public static async Task<RestResponse<NoResponseContent>> ProcessPatchRequest<TRequestType>(string restRequestUri, TRequestType requestContent, object state = null, RestRequestOptions options = null)
        {
            var restRequest = CreateRestRequest(ERestMethod.PATCH, restRequestUri, state, options);

            return await restRequest.ProcessRequestAsync<TRequestType, NoResponseContent>(requestContent).ConfigureAwait(false);
        }

        public static async Task<RestResponse<TResponseType>> ProcessPatchRequest<TRequestType, TResponseType>(string restRequestUri, TRequestType requestContent, object state = null, RestRequestOptions options = null)
        {
            var restRequest = CreateRestRequest(ERestMethod.PATCH, restRequestUri, state, options);

            return await restRequest.ProcessRequestAsync<TRequestType, TResponseType>(requestContent).ConfigureAwait(false);
        }

        public static async Task<RestResponse<NoResponseContent>> ProcessDeleteRequest(string restRequestUri, object state = null, RestRequestOptions options = null)
        {
            var restRequest = CreateRestRequest(ERestMethod.DELETE, restRequestUri, state, options);

            return await restRequest.ProcessRequestAsync<NoRequestContent, NoResponseContent>(null).ConfigureAwait(false);
        }

        public static async Task<RestResponse<NoResponseContent>> ProcessDeleteRequest<TRequestType>(string restRequestUri, TRequestType requestContent, object state = null, RestRequestOptions options = null)
        {
            var restRequest = CreateRestRequest(ERestMethod.DELETE, restRequestUri, state, options);

            return await restRequest.ProcessRequestAsync<TRequestType, NoResponseContent>(requestContent).ConfigureAwait(false);
        }

        public static async Task<RestResponse<TResponseType>> ProcessDeleteRequest<TRequestType, TResponseType>(string restRequestUri, TRequestType requestContent, object state = null, RestRequestOptions options = null)
        {
            var restRequest = CreateRestRequest(ERestMethod.DELETE, restRequestUri, state, options);

            return await restRequest.ProcessRequestAsync<TRequestType, TResponseType>(requestContent).ConfigureAwait(false);
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
            }

            return options;
        }
    }
}
