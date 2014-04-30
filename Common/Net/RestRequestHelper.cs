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
                RestMethod = ERestMethod.GET
            };

            return await restRequest.ProcessRequest<NoResponseContent>(requestContent);
        }


        //public static void ProcessPostRequest<TRequestContent>(string restRequestUri, object state, TRequestContent requestContent, Action<object> restSuccessAsyncCallback, Action<object, ECommunicationResult, ExceptionResult> restErrorAsyncCallback, RestRequestOptions options = null)
        //{
        //    var restRequest = new RestRequest<TRequestContent, NoContentType>
        //    {
        //        State = state,
        //        Options = options ?? new RestRequestOptions(),
        //        RequestContent = requestContent,
        //        RestSuccessAsyncCallback = restSuccessAsyncCallback,
        //        RestErrorAsyncCallback = restErrorAsyncCallback
        //    };

        //    BeginRestRequest("POST", restRequestUri, restRequest);
        //}


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
