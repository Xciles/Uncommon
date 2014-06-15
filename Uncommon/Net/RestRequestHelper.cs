using System.Threading.Tasks;
using Xciles.Uncommon.Security;

namespace Xciles.Uncommon.Net
{
    public class RestRequestHelper
    {
        public static ISecurityContext SecurityContext { get; set; }

        public static async Task<RestResponse<TResponseType>> ProcessGetRequestAsync<TResponseType>(string restRequestUri, object state = null, RestRequestOptions options = null)
        {
            var restRequest = CreateRestRequest(ERestMethod.GET, restRequestUri, state, options);

            return await restRequest.ProcessRequestAsync<TResponseType>().ConfigureAwait(false);
        }

        public static async Task<RestResponse<byte[]>> ProcessRawGetRequestAsync(string restRequestUri, object state = null, RestRequestOptions options = null)
        {
            var restRequest = CreateRestRequest(ERestMethod.GET, restRequestUri, state, options);

            restRequest.Options.ResponseSerializer = EResponseSerializer.UseByteArray;

            var result = await restRequest.ProcessRequestAsync<byte[]>().ConfigureAwait(false);

            result.Result = result.RawResponseContent;

            return result;
        }

        public static async Task<RestResponse<NoResponseContent>> ProcessPostRequestAsync<TRequestType>(string restRequestUri, TRequestType requestContent, object state = null, RestRequestOptions options = null)
        {
            var restRequest = CreateRestRequest(ERestMethod.POST, restRequestUri, state, options);

            return await restRequest.ProcessRequestAsync<TRequestType, NoResponseContent>(requestContent).ConfigureAwait(false);
        }

        public static async Task<RestResponse<TResponseType>> ProcessPostRequestAsync<TRequestType, TResponseType>(string restRequestUri, TRequestType requestContent, object state = null, RestRequestOptions options = null)
        {
            var restRequest = CreateRestRequest(ERestMethod.POST, restRequestUri, state, options);

            return await restRequest.ProcessRequestAsync<TRequestType, TResponseType>(requestContent).ConfigureAwait(false);
        }

        public static async Task<RestResponse<NoResponseContent>> ProcessPutRequestAsync<TRequestType>(string restRequestUri, TRequestType requestContent, object state = null, RestRequestOptions options = null)
        {
            var restRequest = CreateRestRequest(ERestMethod.PUT, restRequestUri, state, options);

            return await restRequest.ProcessRequestAsync<TRequestType, NoResponseContent>(requestContent).ConfigureAwait(false);
        }

        public static async Task<RestResponse<TResponseType>> ProcessPutRequestAsync<TRequestType, TResponseType>(string restRequestUri, TRequestType requestContent, object state = null, RestRequestOptions options = null)
        {
            var restRequest = CreateRestRequest(ERestMethod.PUT, restRequestUri, state, options);

            return await restRequest.ProcessRequestAsync<TRequestType, TResponseType>(requestContent).ConfigureAwait(false);
        }

        public static async Task<RestResponse<NoResponseContent>> ProcessPatchRequestAsync<TRequestType>(string restRequestUri, TRequestType requestContent, object state = null, RestRequestOptions options = null)
        {
            var restRequest = CreateRestRequest(ERestMethod.PATCH, restRequestUri, state, options);

            return await restRequest.ProcessRequestAsync<TRequestType, NoResponseContent>(requestContent).ConfigureAwait(false);
        }

        public static async Task<RestResponse<TResponseType>> ProcessPatchRequestAsync<TRequestType, TResponseType>(string restRequestUri, TRequestType requestContent, object state = null, RestRequestOptions options = null)
        {
            var restRequest = CreateRestRequest(ERestMethod.PATCH, restRequestUri, state, options);

            return await restRequest.ProcessRequestAsync<TRequestType, TResponseType>(requestContent).ConfigureAwait(false);
        }

        public static async Task<RestResponse<NoResponseContent>> ProcessDeleteRequestAsync(string restRequestUri, object state = null, RestRequestOptions options = null)
        {
            var restRequest = CreateRestRequest(ERestMethod.DELETE, restRequestUri, state, options);

            return await restRequest.ProcessRequestAsync<NoRequestContent, NoResponseContent>(null).ConfigureAwait(false);
        }

        public static async Task<RestResponse<NoResponseContent>> ProcessDeleteRequestAsync<TRequestType>(string restRequestUri, TRequestType requestContent, object state = null, RestRequestOptions options = null)
        {
            var restRequest = CreateRestRequest(ERestMethod.DELETE, restRequestUri, state, options);

            return await restRequest.ProcessRequestAsync<TRequestType, NoResponseContent>(requestContent).ConfigureAwait(false);
        }

        public static async Task<RestResponse<TResponseType>> ProcessDeleteRequestAsync<TRequestType, TResponseType>(string restRequestUri, TRequestType requestContent, object state = null, RestRequestOptions options = null)
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
