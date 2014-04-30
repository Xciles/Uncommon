﻿using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Xciles.Common.Security;

namespace Xciles.Common.Net
{
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

        #region Timer things...

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

        public async Task<RestResponse<T>> ProcessRequest<T>()
        {
            var restResponse = new RestResponse<T>();

            var requestUri = new Uri(RestRequestUri);

            _request = (HttpWebRequest)WebRequest.Create(requestUri);
            _request.Method = RestMethod.ToString("G");
            _request.Accept = CreateAcceptHeader(Options.ResponseSerializer);

            if (RestMethod != ERestMethod.GET)
                _request.ContentType = CreateContentTypeHeader(Options.RequestSerializer);

            if (Options.Headers != null)
            {
                _request.Headers = Options.Headers;
            }

            if (Options.Authorized)
            {
                ISecurityContext securityContext = Options.SecurityContext;
                _request.Headers[HttpRequestHeader.Authorization] = securityContext.GenerateAuthorizationHeader();
            }

            if (Options.CookieContainer != null)
            {
                _request.CookieContainer = Options.CookieContainer;
            }

            StartTimer();

            try
            {
                using (var response = await _request.GetResponseAsync() as HttpWebResponse)
                {
                    StopTimer();

                    restResponse = await HandleResponseContent<T>(response);
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

        private async Task<RestResponse<T>> HandleResponseContent<T>(HttpWebResponse response)
        {
            var restResponse = new RestResponse<T>();

            switch (Options.ResponseSerializer)
            {
                case EResponseSerializer.UseXmlDataContractSerializer:
                    restResponse.Result = ConvertResponseToModelObjectFromDataContract<T>(response);
                    break;
                case EResponseSerializer.UseXmlSerializer:
                    restResponse.Result = ConvertResponseToModelObjectFromXml<T>(response);
                    break;
                case EResponseSerializer.UseJsonNet:
                    {
                        using (var responseStream = response.GetResponseStream())
                        {
                            using (var reader = new StreamReader(responseStream))
                            {
                                var objectAsString = await reader.ReadToEndAsync();
                                restResponse.Result = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<T>(objectAsString, _jsonSerializerSettings));
                            }
                        }
                    }
                    break;
                case EResponseSerializer.UseByteArray:
                    {
                        using (Stream stream = response.GetResponseStream())
                        {
                            // http://www.yoda.arachsys.com/csharp/readbinary.html
                            restResponse.RawResponseContent = await ReadFullyAsync(stream, response.ContentLength);
                        }
                    }
                    break;
                default:
                    // Wrong ResponseSerializer settings are used: response is not set.
                    // Possibly set an error ;)
                    break;
            }

            CookieContainer cookies = null;
            if (response.Cookies.Count > 0)
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

        private static TContentType ConvertResponseToModelObjectFromDataContract<TContentType>(HttpWebResponse response)
        {
            TContentType result;

            using (var responseStream = response.GetResponseStream())
            {
                var serializer = new DataContractSerializer(typeof(TContentType));
                result = (TContentType)serializer.ReadObject(responseStream);
            }

            return result;
        }

        private static TContentType ConvertResponseToModelObjectFromXml<TContentType>(HttpWebResponse response)
        {
            TContentType result;

            using (var responseStream = response.GetResponseStream())
            {
                var serializer = new XmlSerializer(typeof(TContentType));
                result = (TContentType)serializer.Deserialize(responseStream);
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

        private static string CreateContentTypeHeader(ERequestSerializer eRequestSerializer)
        {
            switch (eRequestSerializer)
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

        private static string CreateAcceptHeader(EResponseSerializer eResponseSerializer)
        {
            switch (eResponseSerializer)
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
    }
}
