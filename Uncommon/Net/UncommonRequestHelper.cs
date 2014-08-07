using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xciles.Uncommon.Security;

namespace Xciles.Uncommon.Net
{
    public class UncommonRequestHelper
    {
        public static ISecurityContext SecurityContext { get; set; }

        public static async Task<RestResponse<TResponseType>> ProcessGetRequestAsync<TResponseType>(string restRequestUri, object state = null, UncommonRequestOptions options = null)
        {
            options = SetRestRequestOptions(options);

            using (var client = new UncommonHttpClient())
            {
                SetClientOptions(client, options);
                TResponseType result;
                // check what deserializer to use
                switch (options.ResponseSerializer)
                {
                    case EResponseSerializer.UseJsonNet:
                        {
                            result = await client.GetJsonAsync<TResponseType>(restRequestUri);
                        }
                        break;
                    default:
                        {
                            throw new NotSupportedException();
                        }
                }

                var restResponse = new RestResponse<TResponseType>
                {
                    Result = result,
                    StatusCode = HttpStatusCode.OK
                };

                return restResponse;
            }
        }

        public static async Task<RestResponse<byte[]>> ProcessRawGetRequestAsync(string restRequestUri, object state = null, UncommonRequestOptions options = null)
        {
            options = SetRestRequestOptions(options);

            using (var client = new UncommonHttpClient())
            {
                SetClientOptions(client, options);

                var r = await client.GetAsync(restRequestUri);
                r.EnsureSuccessStatusCode();
                byte[] result = await r.Content.ReadAsByteArrayAsync();

                var restResponse = new RestResponse<byte[]>
                {
                    Result = result,
                    StatusCode = HttpStatusCode.OK
                };

                return restResponse;
            }
        }

        private static void SetClientOptions(UncommonHttpClient client, UncommonRequestOptions options)
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(CreateHttpAcceptHeader(options.ResponseSerializer)));
            if (options.Headers != null)
            {
                foreach (var header in options.Headers)
                {
                    client.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            }

            if (options.Authorized && options.SecurityContext != null)
            {
                client.DefaultRequestHeaders.Add("Authorization", options.SecurityContext.GenerateAuthorizationHeader());
            }

            // Add cookie things...
            //if (Options.CookieContainer != null)
            //{
            //    _request.CookieContainer = Options.CookieContainer;
            //}
        }


        private static string CreateHttpAcceptHeader(EResponseSerializer responseSerializer)
        {
            switch (responseSerializer)
            {
                case EResponseSerializer.UseXmlDataContractSerializer:
                case EResponseSerializer.UseXmlSerializer:
                    return "application/xml";
                case EResponseSerializer.UseJsonNet:
                    return "application/json;charset=UTF-8";
                default:
                    return String.Empty;
            }
        }

        private static UncommonRequestOptions SetRestRequestOptions(UncommonRequestOptions options)
        {
            options = options ?? new UncommonRequestOptions();

            if (SecurityContext != null && options.SecurityContext == null)
            {
                options.SecurityContext = SecurityContext;
            }

            return options;
        }
    }
}
