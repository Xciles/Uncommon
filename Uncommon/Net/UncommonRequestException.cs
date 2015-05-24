using System;
using System.Net;
using Newtonsoft.Json;

namespace Xciles.Uncommon.Net
{
    public class UncommonRequestException : Exception
    {
        public EUncommonRequestExceptionStatus RequestExceptionStatus { get; set; }
        [Obsolete ("What was in here has been moved to innerexception")]
        public Exception Exception { get; set; }
        [Obsolete("This is no longer being used. Please use ConvertExceptionResponseToObject<T> for parsing to a <T> of your choice.")]
        public ServiceExceptionResult ServiceExceptionResult { get; set; }
        public string Information { get; set; }
        public WebExceptionStatus WebExceptionStatus { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        public new Exception InnerException { get; set; }
        internal string ExceptionResponseAsString { get; set; }

        public T ConvertExceptionResponseToObject<T>()
        {
            var jsonSerializerSettings = new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects };

            return JsonConvert.DeserializeObject<T>(ExceptionResponseAsString, jsonSerializerSettings);
        }
    }
}
