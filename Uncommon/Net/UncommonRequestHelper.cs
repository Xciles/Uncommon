using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Xciles.Uncommon.Security;

namespace Xciles.Uncommon.Net
{
    // todo change to correct cancellationtoken
    // Todo change methods so that the existing contract does not break
    public class UncommonRequestHelper
    {
        public static ISecurityContext SecurityContext { get; set; }
        protected static JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings() { PreserveReferencesHandling = PreserveReferencesHandling.Objects };

        public static async Task<RestResponse<TResponseType>> ProcessGetRequestAsync<TResponseType>(string requestUri, UncommonRequestOptions options = null)
        {
            return await ProcessRequest<NoRequestContent, TResponseType>(ERestMethod.GET, requestUri, null, options);
        }

        public static async Task<RestResponse<byte[]>> ProcessRawGetRequestAsync(string requestUri, UncommonRequestOptions options = null)
        {
            var result = await ProcessRequest<NoRequestContent, byte[]>(ERestMethod.GET, requestUri, null, options);
            result.Result = result.RawResponseContent;
            result.RawResponseContent = null;
            // will this work??

            return result;
        }

        public static async Task<RestResponse<NoResponseContent>> ProcessPostRequestAsync<TRequestType>(string requestUri, TRequestType requestContent, UncommonRequestOptions options = null)
        {
            return await ProcessRequest<TRequestType, NoResponseContent>(ERestMethod.POST, requestUri, requestContent, options);
        }

        public static async Task<RestResponse<TResponseType>> ProcessPostRequestAsync<TRequestType, TResponseType>(string requestUri, TRequestType requestContent, UncommonRequestOptions options = null)
        {
            return await ProcessRequest<TRequestType, TResponseType>(ERestMethod.POST, requestUri, requestContent, options);
        }

        public static async Task<RestResponse<NoResponseContent>> ProcessPutRequestAsync<TRequestType>(string requestUri, TRequestType requestContent, UncommonRequestOptions options = null)
        {
            return await ProcessRequest<TRequestType, NoResponseContent>(ERestMethod.PUT, requestUri, requestContent, options);
        }

        public static async Task<RestResponse<TResponseType>> ProcessPutRequestAsync<TRequestType, TResponseType>(string requestUri, TRequestType requestContent, UncommonRequestOptions options = null)
        {
            return await ProcessRequest<TRequestType, TResponseType>(ERestMethod.PUT, requestUri, requestContent, options);
        }

        public static async Task<RestResponse<NoResponseContent>> ProcessPatchRequestAsync<TRequestType>(string requestUri, TRequestType requestContent, UncommonRequestOptions options = null)
        {
            return await ProcessRequest<TRequestType, NoResponseContent>(ERestMethod.PATCH, requestUri, requestContent, options);
        }

        public static async Task<RestResponse<TResponseType>> ProcessPatchRequestAsync<TRequestType, TResponseType>(string requestUri, TRequestType requestContent, UncommonRequestOptions options = null)
        {
            return await ProcessRequest<TRequestType, TResponseType>(ERestMethod.PATCH, requestUri, requestContent, options);
        }

        public static async Task<RestResponse<NoResponseContent>> ProcessDeleteRequestAsync(string requestUri, UncommonRequestOptions options = null)
        {
            return await ProcessRequest<NoRequestContent, NoResponseContent>(ERestMethod.DELETE, requestUri, null, options);
        }

        public static async Task<RestResponse<NoResponseContent>> ProcessDeleteRequestAsync<TRequestType>(string requestUri, TRequestType requestContent, UncommonRequestOptions options = null)
        {
            return await ProcessRequest<TRequestType, NoResponseContent>(ERestMethod.DELETE, requestUri, requestContent, options);
        }

        public static async Task<RestResponse<TResponseType>> ProcessDeleteRequestAsync<TRequestType, TResponseType>(string requestUri, TRequestType requestContent, UncommonRequestOptions options = null)
        {
            return await ProcessRequest<TRequestType, TResponseType>(ERestMethod.DELETE, requestUri, requestContent, options);
        }


        private static async Task<RestResponse<TResponseType>> ProcessRequest<TRequestType, TResponseType>(ERestMethod method, string requestUri, TRequestType requestContent, UncommonRequestOptions options)
        {
            try
            {
                options = SetRestRequestOptions(options);

                //var client = new HttpClient(new ClientCompressionHandler(new HttpClientHandler(), new GZipCompressor(), new DeflateCompressor()));

                //client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
                //client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));

                using (var client = new UncommonHttpClient())
                {
                    client.Timeout = new TimeSpan(0, 0, 0, 0, options.Timeout);

                    var httpContent = await GenerateRequestContent(requestContent, options);
                    var request = CreateRequestMessage(method, requestUri, httpContent, options);

                    var response = await client.SendAsync(request, CancellationToken.None);
                    response.EnsureSuccessStatusCode();

                    return await ProcessReponseContent<TResponseType>(response, options);
                }
            }
            catch (JsonSerializationException ex)
            {
                throw new RestRequestException
                {
                    Information = "JsonSerializationException",
                    StatusCode = HttpStatusCode.OK,
                    WebExceptionStatus = WebExceptionStatus.UnknownError,
                    Exception = ex,
                    RestRequestExceptionStatus = ERestRequestExceptionStatus.SerializationError
                };
            }
            catch (HttpRequestException ex)
            {
                throw; // change to properly handle exceptions
            }
            catch (TaskCanceledException ex)
            {
                // mostlikely an timeout
                throw new RestRequestException
                {
                    RestRequestExceptionStatus = ERestRequestExceptionStatus.Timeout
                };
            }
            catch (Exception ex)
            {
                throw new RestRequestException
                {
                    RestRequestExceptionStatus = ERestRequestExceptionStatus.Undefined,
                    Information = "TheStrangeMonoNullException",
                    StatusCode = HttpStatusCode.NotFound
                };
            }
        }

        private static HttpRequestMessage CreateRequestMessage(ERestMethod restMethod, string requestUri, HttpContent content, UncommonRequestOptions options)
        {
            var method = new HttpMethod(restMethod.ToString("F"));
            var request = new HttpRequestMessage(method, requestUri)
            {
                Content = content
            };

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(CreateHttpAcceptHeader(options.ResponseSerializer)));
            if (options.Headers != null)
            {
                foreach (var header in options.Headers)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }

            if (options.Authorized && options.SecurityContext != null)
            {
                request.Headers.Add("Authorization", options.SecurityContext.GenerateAuthorizationHeader());
            }

            // Add cookie things...
            //if (Options.CookieContainer != null)
            //{
            //    _request.CookieContainer = Options.CookieContainer;
            //}

            return request;
        }

        private static async Task<HttpContent> GenerateRequestContent<TRequestType>(TRequestType requestContent, UncommonRequestOptions options)
        {
            HttpContent httpContent = null;

            if (typeof(TRequestType) != typeof(NoResponseContent))
            {
                switch (options.RequestSerializer)
                {
                    case ERequestSerializer.UseXmlDataContractSerializer:
                        {
                            throw new NotSupportedException();
                        }
                        break;
                    case ERequestSerializer.UseXmlSerializer:
                        {
                            throw new NotSupportedException();
                        }
                        break;
                    case ERequestSerializer.UseByteArray:
                        {
                            httpContent = new ByteArrayContent(requestContent as byte[]);
                        }
                        break;
                    case ERequestSerializer.UseJsonNet:
                        {
                            var requestBody = await Task.Factory.StartNew(() => JsonConvert.SerializeObject(requestContent, JsonSerializerSettings)).ConfigureAwait(false);

                            httpContent = new StringContent(requestBody);
                            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json;charset=UTF-8");
                        }
                        break;
                    case ERequestSerializer.UseStringUrlPost:
                        {
                            throw new NotSupportedException();
                        }
                        break;
                    default:
                        // Return value null indicates that wrong RequestSerializer settings are used.
                        throw new NotSupportedException();
                        break;
                }
            }

            return httpContent;
        }

        private static async Task<RestResponse<TResponseType>> ProcessReponseContent<TResponseType>(HttpResponseMessage response, UncommonRequestOptions options)
        {
            var restResponse = new RestResponse<TResponseType>
            {
                StatusCode = response.StatusCode
            };

            if (typeof(TResponseType) != typeof(NoResponseContent))
            {
                switch (options.ResponseSerializer)
                {
                    case EResponseSerializer.UseXmlDataContractSerializer:
                        {
                            var resultAsStream = await response.Content.ReadAsStreamAsync();
                            restResponse.Result = await Task.Factory.StartNew(() => ConvertResponseToModelObjectFromDataContractXml<TResponseType>(resultAsStream)).ConfigureAwait(false);
                        }
                        break;
                    case EResponseSerializer.UseXmlSerializer:
                        {
                            var resultAsStream = await response.Content.ReadAsStreamAsync();
                            restResponse.Result = await Task.Factory.StartNew(() => ConvertResponseToModelObjectFromXml<TResponseType>(resultAsStream)).ConfigureAwait(false);
                        }
                        break;
                    case EResponseSerializer.UseJsonNet:
                        {
                            var resultAsString = await response.Content.ReadAsStringAsync();
                            restResponse.Result = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<TResponseType>(resultAsString, JsonSerializerSettings));
                        }
                        break;
                    case EResponseSerializer.UseByteArray:
                        {
                            var resultAsBytes = await response.Content.ReadAsByteArrayAsync();
                            restResponse.RawResponseContent = resultAsBytes;
                        }
                        break;
                    default:
                        // Wrong ResponseSerializer settings are used: response is not set.
                        // Possibly set an error ;)
                        break;
                }
            }

            return restResponse;

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

        private static TResponseType ConvertResponseToModelObjectFromDataContractXml<TResponseType>(Stream resultAsStream)
        {
            var serializer = new DataContractSerializer(typeof(TResponseType));
            var result = (TResponseType)serializer.ReadObject(resultAsStream);

            return result;
        }

        private static TResponseType ConvertResponseToModelObjectFromXml<TResponseType>(Stream resultAsStream)
        {
            var serializer = new XmlSerializer(typeof(TResponseType));
            var result = (TResponseType)serializer.Deserialize(resultAsStream);

            return result;
        }
    }
}