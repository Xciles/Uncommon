using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Xciles.Uncommon.Extensions;
using Xciles.Uncommon.Security;

namespace Xciles.Uncommon.Net
{
    internal class NoRequestContent { }
    public class NoResponseContent { }

    // todo change to correct cancellationtoken
    public class UncommonRequestHelper
    {
        public static ISecurityContext SecurityContext { get; set; }
        protected static JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects };

        public static async Task<UncommonResponse<TResponseType>> ProcessGetRequestAsync<TResponseType>(string requestUri, UncommonRequestOptions options = null)
        {
            return await ProcessRequest<NoRequestContent, TResponseType>(EUncommonRequestMethod.GET, requestUri, null, options).ConfigureAwait(false);
        }

        public static async Task<UncommonResponse<byte[]>> ProcessRawGetRequestAsync(string requestUri, UncommonRequestOptions options = null)
        {
            // Make sure the options are set and set the responseSerializer to use ByteArray
            options = SetRestRequestOptions(options);

            options.ResponseSerializer = EUncommonResponseSerializer.UseByteArray;

            var result = await ProcessRequest<NoRequestContent, byte[]>(EUncommonRequestMethod.GET, requestUri, null, options).ConfigureAwait(false);
            result.Result = result.RawResponseContent;
            result.RawResponseContent = null;

            return result;
        }

        public static async Task<UncommonResponse<NoResponseContent>> ProcessPostRequestAsync<TRequestType>(string requestUri, TRequestType requestContent, UncommonRequestOptions options = null)
        {
            return await ProcessRequest<TRequestType, NoResponseContent>(EUncommonRequestMethod.POST, requestUri, requestContent, options).ConfigureAwait(false);
        }

        public static async Task<UncommonResponse<TResponseType>> ProcessPostRequestAsync<TRequestType, TResponseType>(string requestUri, TRequestType requestContent, UncommonRequestOptions options = null)
        {
            return await ProcessRequest<TRequestType, TResponseType>(EUncommonRequestMethod.POST, requestUri, requestContent, options).ConfigureAwait(false);
        }

        public static async Task<UncommonResponse<NoResponseContent>> ProcessPutRequestAsync<TRequestType>(string requestUri, TRequestType requestContent, UncommonRequestOptions options = null)
        {
            return await ProcessRequest<TRequestType, NoResponseContent>(EUncommonRequestMethod.PUT, requestUri, requestContent, options).ConfigureAwait(false);
        }

        public static async Task<UncommonResponse<TResponseType>> ProcessPutRequestAsync<TRequestType, TResponseType>(string requestUri, TRequestType requestContent, UncommonRequestOptions options = null)
        {
            return await ProcessRequest<TRequestType, TResponseType>(EUncommonRequestMethod.PUT, requestUri, requestContent, options).ConfigureAwait(false);
        }

        public static async Task<UncommonResponse<NoResponseContent>> ProcessPatchRequestAsync<TRequestType>(string requestUri, TRequestType requestContent, UncommonRequestOptions options = null)
        {
            return await ProcessRequest<TRequestType, NoResponseContent>(EUncommonRequestMethod.PATCH, requestUri, requestContent, options).ConfigureAwait(false);
        }

        public static async Task<UncommonResponse<TResponseType>> ProcessPatchRequestAsync<TRequestType, TResponseType>(string requestUri, TRequestType requestContent, UncommonRequestOptions options = null)
        {
            return await ProcessRequest<TRequestType, TResponseType>(EUncommonRequestMethod.PATCH, requestUri, requestContent, options).ConfigureAwait(false);
        }

        public static async Task<UncommonResponse<NoResponseContent>> ProcessDeleteRequestAsync(string requestUri, UncommonRequestOptions options = null)
        {
            return await ProcessRequest<NoRequestContent, NoResponseContent>(EUncommonRequestMethod.DELETE, requestUri, null, options).ConfigureAwait(false);
        }

        public static async Task<UncommonResponse<NoResponseContent>> ProcessDeleteRequestAsync<TRequestType>(string requestUri, TRequestType requestContent, UncommonRequestOptions options = null)
        {
            return await ProcessRequest<TRequestType, NoResponseContent>(EUncommonRequestMethod.DELETE, requestUri, requestContent, options).ConfigureAwait(false);
        }

        public static async Task<UncommonResponse<TResponseType>> ProcessDeleteRequestAsync<TRequestType, TResponseType>(string requestUri, TRequestType requestContent, UncommonRequestOptions options = null)
        {
            return await ProcessRequest<TRequestType, TResponseType>(EUncommonRequestMethod.DELETE, requestUri, requestContent, options).ConfigureAwait(false);
        }


        private static async Task<UncommonResponse<TResponseType>> ProcessRequest<TRequestType, TResponseType>(EUncommonRequestMethod method, string requestUri, TRequestType requestContent, UncommonRequestOptions options)
        {
            HttpResponseMessage response = null;
            try
            {
                options = SetRestRequestOptions(options);

                using (var client = new UncommonHttpClient())
                {
                    client.Timeout = new TimeSpan(0, 0, 0, 0, options.Timeout);

                    HttpContent httpContent = null;
                    if (typeof(TRequestType) != typeof(NoRequestContent))
                    {
                        httpContent = await GenerateRequestContent(requestContent, options).ConfigureAwait(false);
                    }

                    var request = CreateRequestMessage(method, requestUri, httpContent, options);
                    response = await client.SendAsync(request, CancellationToken.None).ConfigureAwait(false);

                    if (response.IsSuccessStatusCode)
                    {
                        return await ProcessReponseContent<TResponseType>(response, options).ConfigureAwait(false);
                    }

                    var requestException = new UncommonRequestException
                    {
                        Information = "RequestException",
                        RequestExceptionStatus = EUncommonRequestExceptionStatus.ServiceError,
                        StatusCode = response.StatusCode,
                    };

                    var resultAsString = response.Content.ReadAsStringAsync().Result;
                    requestException.ExceptionResponseAsString = resultAsString;

                    throw requestException;
                }
            }
            catch (UncommonRequestException)
            {
                throw;
            }
            catch (JsonSerializationException ex)
            {
                throw new UncommonRequestException
                {
                    Information = "JsonSerializationException",
                    InnerException = ex,
                    RequestExceptionStatus = EUncommonRequestExceptionStatus.SerializationError,
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (HttpRequestException ex)
            {
                var requestException = new UncommonRequestException
                {
                    Information = "HttpRequestException",
                    InnerException = ex.InnerException ?? ex,
                    RequestExceptionStatus = EUncommonRequestExceptionStatus.ServiceError,
                    StatusCode = response != null ? response.StatusCode : HttpStatusCode.BadRequest
                };

                throw requestException;
            }
            catch (TaskCanceledException ex)
            {
                // most likely a timeout
                throw new UncommonRequestException
                {
                    Information = "TaskCanceledException",
                    InnerException = ex,
                    RequestExceptionStatus = EUncommonRequestExceptionStatus.Timeout
                };
            }
            catch (Exception ex)
            {
                throw new UncommonRequestException
                {
                    Information = ex.Message,
                    InnerException = ex,
                    RequestExceptionStatus = EUncommonRequestExceptionStatus.Undefined,
                    StatusCode = HttpStatusCode.NotFound
                };
            }
        }

        private static HttpRequestMessage CreateRequestMessage(EUncommonRequestMethod restMethod, string requestUri, HttpContent content, UncommonRequestOptions options)
        {
            var method = new HttpMethod(restMethod.ToString("F"));
            var request = new HttpRequestMessage(method, requestUri)
            {
                Content = content
            };

            SetHttpAcceptHeader(request, options.ResponseSerializer);
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

            return request;
        }

        private static async Task<HttpContent> GenerateRequestContent<TRequestType>(TRequestType requestContent, UncommonRequestOptions options)
        {
            HttpContent httpContent;

            switch (options.RequestSerializer)
            {
                case EUncommonRequestSerializer.UseXmlSerializer:
                    {
                        var requestBody = await ConvertModelObjectByXmlToString(requestContent).ConfigureAwait(false);

                        httpContent = new StringContent(requestBody);
                        httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/xml");
                    }
                    break;
                case EUncommonRequestSerializer.UseByteArray:
                    {
                        httpContent = new ByteArrayContent(requestContent as byte[]);
                    }
                    break;
                case EUncommonRequestSerializer.UseJsonNet:
                    {
                        var requestBody = JsonConvert.SerializeObject(requestContent, JsonSerializerSettings);

                        httpContent = new StringContent(requestBody);
                        httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    }
                    break;
                case EUncommonRequestSerializer.UseFormUrlEncoded:
                    {
                        var pairs = requestContent as IEnumerable<KeyValuePair<string, string>>;

                        var content = pairs ?? requestContent.GetProperties().AsEnumerable();

                        httpContent = new FormUrlEncodedContent(content);
                    }
                    break;
                default:
                    // Return value null indicates that wrong RequestSerializer settings are used.
                    throw new NotSupportedException();
            }

            return httpContent;
        }

        private static async Task<UncommonResponse<TResponseType>> ProcessReponseContent<TResponseType>(HttpResponseMessage response, UncommonRequestOptions options)
        {
            var restResponse = new UncommonResponse<TResponseType>
            {
                StatusCode = response.StatusCode
            };

            if (typeof(TResponseType) != typeof(NoResponseContent))
            {
                switch (options.ResponseSerializer)
                {
                    case EUncommonResponseSerializer.UseXmlSerializer:
                        {
                            var resultAsStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                            restResponse.Result = ConvertResponseToModelObjectFromXml<TResponseType>(resultAsStream);
                        }
                        break;
                    case EUncommonResponseSerializer.UseJsonNet:
                        {
                            var resultAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                            restResponse.Result = JsonConvert.DeserializeObject<TResponseType>(resultAsString, JsonSerializerSettings);
                        }
                        break;
                    case EUncommonResponseSerializer.UseByteArray:
                        {
                            var resultAsBytes = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
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
        }

        private static void SetHttpAcceptHeader(HttpRequestMessage request, EUncommonResponseSerializer responseSerializer)
        {
            string acceptHeader;
            switch (responseSerializer)
            {
                case EUncommonResponseSerializer.UseXmlSerializer:
                    acceptHeader = "application/xml";
                    break;
                case EUncommonResponseSerializer.UseJsonNet:
                    acceptHeader = "application/json";
                    break;
                default:
                    acceptHeader = String.Empty;
                    break;
            }

            if (!String.IsNullOrWhiteSpace(acceptHeader))
            {
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(acceptHeader));
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

        private static async Task<string> ConvertModelObjectByXmlToString<TRequestType>(TRequestType modelObject)
        {
            using (var memoryStream = new MemoryStream())
            {
                var serializer = new XmlSerializer(typeof(TRequestType));
                serializer.Serialize(memoryStream, modelObject);

                memoryStream.Position = 0;
                return await new StreamReader(memoryStream).ReadToEndAsync().ConfigureAwait(false);
            }
        }

        private static TResponseType ConvertResponseToModelObjectFromXml<TResponseType>(Stream resultAsStream)
        {
            var serializer = new XmlSerializer(typeof(TResponseType));
            var result = (TResponseType)serializer.Deserialize(resultAsStream);

            return result;
        }
    }
}