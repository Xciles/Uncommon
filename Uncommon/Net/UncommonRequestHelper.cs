using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xciles.Uncommon.Security;

namespace Xciles.Uncommon.Net
{
    public class UncommonRequestHelper
    {
        public static ISecurityContext SecurityContext { get; set; }

        public static async Task<RestResponse<TResponseType>> ProcessGetRequestAsync<TResponseType>(string restRequestUri, object state = null, RestRequestOptions options = null)
        {
            options = SetRestRequestOptions(options);

            using (var client = new UncommonHttpClient())
            {
                SetClientOptions(client, options);
                // check what deserializer to use
                switch (options.ResponseSerializer)
                {
                    case EResponseSerializer.UseJsonNet:
                        {

                        }
                        break;
                    default:
                        {
                            throw new NotSupportedException();
                        }
                }
            }

            var restRequest = CreateRestRequest(ERestMethod.GET, restRequestUri, state, options);

            return await restRequest.ProcessRequestAsync<TResponseType>().ConfigureAwait(false);
        }

        private static void SetClientOptions(UncommonHttpClient client, RestRequestOptions options)
        {
            _request.Accept = CreateHttpAcceptHeader(Options.ResponseSerializer);

            if (RestMethod != ERestMethod.GET)
            {
                _request.ContentType = CreateHttpContentTypeHeader(Options.RequestSerializer);
            }

            if (Options.Headers != null)
            {
                _request.Headers = Options.Headers;
            }

            if (Options.Authorized && Options.SecurityContext != null)
            {
                _request.Headers[HttpRequestHeader.Authorization] = Options.SecurityContext.GenerateAuthorizationHeader();
            }

            if (Options.CookieContainer != null)
            {
                _request.CookieContainer = Options.CookieContainer;
            }

            

        //private static string CreateHttpAcceptHeader(EResponseSerializer responseSerializer)
        //{
        //    switch (responseSerializer)
        //    {
        //        case EResponseSerializer.UseXmlDataContractSerializer:
        //        case EResponseSerializer.UseXmlSerializer:
        //            return "application/xml";
        //        case EResponseSerializer.UseJsonNet:
        //            return "application/json;charset=UTF-8";
        //        default:
        //            return String.Empty;
        //    }
        //}

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
