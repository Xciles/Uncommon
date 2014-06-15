using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Xciles.Uncommon.Net
{
    internal class NoRequestContent { }
    public class NoResponseContent { }

    public class RestRequest
    {
        private Timer _timer;
        public object State { get; set; }
        public ERestMethod RestMethod { get; set; }
        public string RestRequestUri { get; set; }

        private static JsonSerializerSettings _jsonSerializerSettings;
        private HttpWebRequest _request;
        public RestRequestOptions Options { get; set; }

        public RestRequest(JsonSerializerSettings jsonSettings = null)
        {
            _jsonSerializerSettings = jsonSettings ?? new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            };
        }

        #region Timer/timeout things...

        private void StartTimer()
        {
            if (Options.Timeout != 0)
            {
                _timer = new Timer(TimeoutCallback, null, TimeSpan.FromMilliseconds(Options.Timeout), TimeSpan.FromMilliseconds(-1));
            }
        }

        private void StopTimer()
        {
            if (_timer != null)
            {
                _timer.Dispose();
                _timer = null;
            }
        }

        private void TimeoutCallback(object timer)
        {
            if (_request != null)
            {
                _request.Abort();
            }
        }

        #endregion

        public async Task<RestResponse<TResponseType>> ProcessRequestAsync<TRequestType, TResponseType>(TRequestType requestContent)
        {
            var restResponse = new RestResponse<TResponseType>();

            var requestUri = new Uri(RestRequestUri);

            _request = (HttpWebRequest)WebRequest.Create(requestUri);
            _request.Method = RestMethod.ToString("G");
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

            StartTimer();

            try
            {
                if (!EqualityComparer<TRequestType>.Default.Equals(requestContent, default(TRequestType)))
                {
                    using (var requestStream = await _request.GetRequestStreamAsync().ConfigureAwait(false))
                    {
                        byte[] requestBody = await GetRequestBodyAsync(requestContent).ConfigureAwait(false);
                        await requestStream.WriteAsync(requestBody, 0, requestBody.Length).ConfigureAwait(false);
                    }
                }

                using (var response = await _request.GetResponseAsync().ConfigureAwait(false) as HttpWebResponse)
                {
                    StopTimer();

                    restResponse = await HandleResponseAsync<TResponseType>(response).ConfigureAwait(false);
                    restResponse.State = State;
                    restResponse.StatusCode = response.StatusCode;
                }
            }
            catch (JsonSerializationException ex)
            {
                StopTimer();

                throw new RestRequestException
                {
                    Information = "JsonSerializationException",
                    StatusCode = HttpStatusCode.OK,
                    WebExceptionStatus = WebExceptionStatus.UnknownError,
                    Exception = ex,
                    RestRequestExceptionStatus = ERestRequestExceptionStatus.SerializationError
                };

            }
            catch (WebException webException)
            {
                StopTimer();

                HandleWebException(webException);
            }

            return restResponse;
        }

        public async Task<RestResponse<TResponseType>> ProcessRequestAsync<TResponseType>()
        {
            return await ProcessRequestAsync<NoRequestContent, TResponseType>(null).ConfigureAwait(false);
        }

        #region Request methods

        private async Task<byte[]> GetRequestBodyAsync<TRequestType>(TRequestType requestContent)
        {
            byte[] requestBody = null;

            switch (Options.RequestSerializer)
            {
                case ERequestSerializer.UseXmlDataContractSerializer:
                    {
                        requestBody = await Task.Factory.StartNew(() => ConvertModelObjectByDataContactXmlToByteArray(requestContent)).ConfigureAwait(false);
                    }
                    break;
                case ERequestSerializer.UseXmlSerializer:
                    {
                        requestBody = await Task.Factory.StartNew(() => ConvertModelObjectByXmlToByteArray(requestContent)).ConfigureAwait(false);
                    }
                    break;
                case ERequestSerializer.UseByteArray:
                    {
                        requestBody = requestContent as byte[];
                    }
                    break;
                case ERequestSerializer.UseJsonNet:
                    {
                        requestBody = await Task.Factory.StartNew(() => Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(requestContent))).ConfigureAwait(false);
                    }
                    break;
                case ERequestSerializer.UseStringUrlPost:
                    {
                        requestBody = Encoding.UTF8.GetBytes(requestContent.ToString());
                    }
                    break;
                default:
                    // Return value null indicates that wrong RequestSerializer settings are used.
                    break;
            }

            return requestBody;
        }

        private static string CreateHttpContentTypeHeader(ERequestSerializer requestSerializer)
        {
            switch (requestSerializer)
            {
                case ERequestSerializer.UseXmlDataContractSerializer:
                case ERequestSerializer.UseXmlSerializer:
                    return "application/xml";
                case ERequestSerializer.UseJsonNet:
                    return "application/json;charset=UTF-8";
                case ERequestSerializer.UseStringUrlPost:
                    return "application/x-www-form-urlencoded;charset=UTF-8";
                default:
                    return String.Empty;
            }
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

        private static byte[] ConvertModelObjectByDataContactXmlToByteArray<TRequestType>(TRequestType modelObject)
        {
            using (var memoryStream = new MemoryStream())
            {
                var serializer = new DataContractSerializer(typeof(TRequestType));
                serializer.WriteObject(memoryStream, modelObject);

                return memoryStream.ToArray();
            }
        }

        private static byte[] ConvertModelObjectByXmlToByteArray<TRequestType>(TRequestType modelObject)
        {
            using (var memoryStream = new MemoryStream())
            {
                var serializer = new XmlSerializer(typeof(TRequestType));
                serializer.Serialize(memoryStream, modelObject);

                return memoryStream.ToArray();
            }
        }

        #endregion



        #region Response methods

        private async Task<RestResponse<TResponseType>> HandleResponseAsync<TResponseType>(HttpWebResponse response)
        {
            var restResponse = new RestResponse<TResponseType>();

            if (typeof (TResponseType) != typeof (NoResponseContent))
            {
                switch (Options.ResponseSerializer)
                {
                    case EResponseSerializer.UseXmlDataContractSerializer:
                        restResponse.Result = await Task.Factory.StartNew(() => ConvertResponseToModelObjectFromDataContractXml<TResponseType>(response)).ConfigureAwait(false);
                        break;
                    case EResponseSerializer.UseXmlSerializer:
                        restResponse.Result = await Task.Factory.StartNew(() => ConvertResponseToModelObjectFromXml<TResponseType>(response)).ConfigureAwait(false);
                        break;
                    case EResponseSerializer.UseJsonNet:
                    {
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
                    case EResponseSerializer.UseByteArray:
                    {
                        using (Stream stream = response.GetResponseStream())
                        {
                            // http://www.yoda.arachsys.com/csharp/readbinary.html
                            restResponse.RawResponseContent = await ReadFullyAsync(stream, response.ContentLength).ConfigureAwait(false);
                        }
                    }
                        break;
                    default:
                        // Wrong ResponseSerializer settings are used: response is not set.
                        // Possibly set an error ;)
                        break;
                }
            }

            CookieContainer cookies = null;
            if (response.Cookies != null && response.Cookies.Count > 0)
            {
                cookies = new CookieContainer();
                foreach (Cookie c in response.Cookies)
                {
                    if (c.Domain[0] == '.' && c.Domain.Substring(1) == response.ResponseUri.Host)
                    {
                        c.Domain = c.Domain.TrimStart(new[] { '.' });
                    }
                    cookies.Add(new Uri(response.ResponseUri.Scheme + "://" + response.ResponseUri.Host), c);
                }
            }

            restResponse.CookieContainer = cookies;
            restResponse.StatusCode = response.StatusCode;

            return restResponse;
        }

        private static void HandleWebException(WebException webException)
        {
            // WebExceptionStatus does (or did) not contain all posible statusses returned by the webRequest on certain platforms (Xamarin based).
            // Therefor we have to check by string...............................................
            // ¯\(°_o)/¯

            switch (webException.Status.ToString("G"))
            {
                case "RequestCanceled":
                    {
                        // Request is cancelled because of timeout.
                        throw new RestRequestException
                        {
                            RestRequestExceptionStatus = ERestRequestExceptionStatus.Timeout
                        };
                    }
                case "ConnectFailure":
                case "NameResolutionFailure":
                    {
                        throw new RestRequestException
                        {
                            RestRequestExceptionStatus = ERestRequestExceptionStatus.NoConnection
                        };
                    }
                case "SendFailure":
                    {
                        throw new RestRequestException
                        {
                            RestRequestExceptionStatus = ERestRequestExceptionStatus.Failed
                        };
                    }
                default:
                    var restRequestException = new RestRequestException();
                    if (webException.Response != null)
                    {
                        var response = (HttpWebResponse)webException.Response;
                        using (var responseStream = response.GetResponseStream())
                        {
                            // Moved objectAsString outside of try because of NotSupportedException occurs when reading the stream twice. (Seek to begin throws)
                            string objectAsString = String.Empty;
                            try
                            {
                                using (var reader = new StreamReader(responseStream))
                                {
                                    objectAsString = reader.ReadToEnd();
                                    var exceptionResult = JsonConvert.DeserializeObject<ServiceExceptionResult>(objectAsString, _jsonSerializerSettings);

                                    restRequestException.ServiceExceptionResult = exceptionResult;
                                    restRequestException.RestRequestExceptionStatus = ERestRequestExceptionStatus.ServiceError;
                                }
                            }
                            catch (JsonSerializationException ex)
                            {
                                restRequestException.Information = objectAsString;
                                restRequestException.Exception = ex;
                                restRequestException.RestRequestExceptionStatus = ERestRequestExceptionStatus.SerializationError;
                            }
                            finally
                            {
                                restRequestException.RestRequestExceptionStatus = ERestRequestExceptionStatus.ServiceError;
                            }
                        }
                        restRequestException.StatusCode = response.StatusCode;
                    }
                    else
                    {
                        restRequestException.RestRequestExceptionStatus = ERestRequestExceptionStatus.UnknownError;
                    }

                    restRequestException.WebExceptionStatus = webException.Status;

                    throw restRequestException;
            }
        }

        private static TResponseType ConvertResponseToModelObjectFromDataContractXml<TResponseType>(HttpWebResponse response)
        {
            TResponseType result;

            using (var responseStream = response.GetResponseStream())
            {
                var serializer = new DataContractSerializer(typeof(TResponseType));
                result = (TResponseType)serializer.ReadObject(responseStream);
            }

            return result;
        }

        private static TResponseType ConvertResponseToModelObjectFromXml<TResponseType>(HttpWebResponse response)
        {
            TResponseType result;

            using (var responseStream = response.GetResponseStream())
            {
                var serializer = new XmlSerializer(typeof(TResponseType));
                result = (TResponseType)serializer.Deserialize(responseStream);
            }

            return result;
        }

        /// <summary>
        /// Reads data from a stream until the end is reached. The
        /// data is returned as a byte array. An IOException is
        /// thrown if any of the underlying IO calls fail.
        /// </summary>
        /// <param name="stream">The stream to read data from</param>
        /// <param name="initialLength">The initial buffer length</param>
        private static async Task<byte[]> ReadFullyAsync(Stream stream, long initialLength)
        {
            // If we've been passed an unhelpful initial length, just
            // use 32K.
            if (initialLength < 1)
            {
                initialLength = 32768;
            }

            var buffer = new byte[initialLength];
            int read = 0;

            int chunk;
            while ((chunk = await stream.ReadAsync(buffer, read, buffer.Length - read).ConfigureAwait(false)) > 0)
            {
                read += chunk;

                // If we've reached the end of our buffer, check to see if there's
                // any more information
                if (read == buffer.Length)
                {
                    int nextByte = stream.ReadByte();

                    // End of stream? If so, we're done
                    if (nextByte == -1)
                    {
                        return buffer;
                    }

                    // Nope. Resize the buffer, put in the byte we've just
                    // read, and continue
                    var newBuffer = new byte[buffer.Length * 2];
                    Array.Copy(buffer, newBuffer, buffer.Length);
                    newBuffer[read] = (byte)nextByte;
                    buffer = newBuffer;
                    read++;
                }
            }
            // Buffer is now too big. Shrink it.
            var ret = new byte[read];
            Array.Copy(buffer, ret, read);
            return ret;
        }

        #endregion

    }
}
