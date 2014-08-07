using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
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

        public static async Task<RestResponse<NoResponseContent>> ProcessPostRequestAsync<TRequestType>(string restRequestUri, TRequestType requestContent, UncommonRequestOptions options = null)
        {
            options = SetRestRequestOptions(options);

            using (var client = new UncommonHttpClient())
            {
                SetClientOptions(client, options);

                HttpResponseMessage response;
                // check what deserializer to use
                switch (options.ResponseSerializer)
                {
                    case EResponseSerializer.UseJsonNet:
                        {
                            response = await client.PostContentAsJsonAsync(restRequestUri, requestContent);
                            response.EnsureSuccessStatusCode();
                        }
                        break;
                    default:
                        {
                            throw new NotSupportedException();
                        }
                }

                var restResponse = new RestResponse<NoResponseContent>
                {
                    StatusCode = response.StatusCode
                };

                return restResponse;
            }

        }


        private static async Task<RestResponse<TResponseType>> ProcessRequest<TRequestType, TResponseType>(UncommonHttpClient client, string requestUrl, TRequestType requestContent, UncommonRequestOptions options)
        {
            HttpResponseMessage response;
            // check what deserializer to use
            switch (options.ResponseSerializer)
            {
                case EResponseSerializer.UseJsonNet:
                    {
                        response = await client.PostContentAsJsonAsync(requestUrl, requestContent);
                        response.EnsureSuccessStatusCode();
                    }
                    break;
                default:
                    {
                        throw new NotSupportedException();
                    }
            }

            var restResponse = new RestResponse<TResponseType>();

            if (typeof (TResponseType) != typeof (NoResponseContent))
            {
                switch (options.ResponseSerializer)
                {
                    case EResponseSerializer.UseJsonNet:
                    {
                        var resultAsString = response.Content.ReadAsStringAsync();

                            using (var responseStream = response.GetResponseStream())
                            {
                                using (var reader = new StreamReader(responseStream))
                                {
                                    var objectAsString = await reader.ReadToEndAsync().ConfigureAwait(false);
                                    restResponse.Result = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<TResponseType>(objectAsString, _jsonSerializerSettings)).ConfigureAwait(false);
                                }
                            }
                        }
                        break;
                    default:
                        // Wrong ResponseSerializer settings are used: response is not set.
                        // Possibly set an error ;)
                        break;
                }
                }
            }

            //if (typeof(TResponseType) != typeof(NoResponseContent))
            //{
            //    switch (Options.ResponseSerializer)
            //    {
            //        case EResponseSerializer.UseXmlDataContractSerializer:
            //            restResponse.Result = await Task.Factory.StartNew(() => ConvertResponseToModelObjectFromDataContractXml<TResponseType>(response)).ConfigureAwait(false);
            //            break;
            //        case EResponseSerializer.UseXmlSerializer:
            //            restResponse.Result = await Task.Factory.StartNew(() => ConvertResponseToModelObjectFromXml<TResponseType>(response)).ConfigureAwait(false);
            //            break;
            //        case EResponseSerializer.UseJsonNet:
            //            {
            //                using (var responseStream = response.GetResponseStream())
            //                {
            //                    using (var reader = new StreamReader(responseStream))
            //                    {
            //                        var objectAsString = await reader.ReadToEndAsync().ConfigureAwait(false);
            //                        restResponse.Result = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<TResponseType>(objectAsString, _jsonSerializerSettings)).ConfigureAwait(false);
            //                    }
            //                }
            //            }
            //            break;
            //        case EResponseSerializer.UseByteArray:
            //            {
            //                using (Stream stream = response.GetResponseStream())
            //                {
            //                    // http://www.yoda.arachsys.com/csharp/readbinary.html
            //                    restResponse.RawResponseContent = await ReadFullyAsync(stream, response.ContentLength).ConfigureAwait(false);
            //                }
            //            }
            //            break;
            //        default:
            //            // Wrong ResponseSerializer settings are used: response is not set.
            //            // Possibly set an error ;)
            //            break;
            //    }
            //}

            //CookieContainer cookies = null;
            //if (response.Cookies != null && response.Cookies.Count > 0)
            //{
            //    cookies = new CookieContainer();
            //    foreach (Cookie c in response.Cookies)
            //    {
            //        if (c.Domain[0] == '.' && c.Domain.Substring(1) == response.ResponseUri.Host)
            //        {
            //            c.Domain = c.Domain.TrimStart(new[] { '.' });
            //        }
            //        cookies.Add(new Uri(response.ResponseUri.Scheme + "://" + response.ResponseUri.Host), c);
            //    }
            //}

            //restResponse.CookieContainer = cookies;
            //restResponse.StatusCode = response.StatusCode;


            //var restResponse = new RestResponse<NoResponseContent>
            //{
            //    StatusCode = response.StatusCode
            //};
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
