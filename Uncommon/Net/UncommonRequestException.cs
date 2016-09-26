using System;
using System.Net;
using Newtonsoft.Json;

namespace Xciles.Uncommon.Net
{
    public class UncommonRequestException : Exception
    {
        public EUncommonRequestExceptionStatus RequestExceptionStatus { get; set; }
        public string Information { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        public new Exception InnerException { get; set; }
        public string ExceptionResponseAsString { get; set; }

        public T ConvertExceptionResponseToObject<T>()
        {
            var jsonSerializerSettings = new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects };

            return JsonConvert.DeserializeObject<T>(ExceptionResponseAsString, jsonSerializerSettings);
        }
    }
}
