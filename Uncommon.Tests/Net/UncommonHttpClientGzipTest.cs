using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Fakes;
using System.Text;
using System.Threading.Tasks;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Xciles.Uncommon.Handler;
using Xciles.Uncommon.Net;

namespace Xciles.Uncommon.Tests.Net
{
    [TestClass]
    public class UncommonHttpClientGzipTests
    {
        [TestMethod]
        public void GetJsonAsyncNoGzipTest()
        {
            GetJsonAsyncNoGzipTestAsync().Wait();
        }

        public async Task GetJsonAsyncNoGzipTestAsync()
        {
            using (ShimsContext.Create())
            {
                var person = new Person
                {
                    DateOfBirth = DateTime.Now.Subtract(new TimeSpan(800, 1, 1, 1)),
                    Firstname = "First",
                    Lastname = "Person",
                    PhoneNumber = "0123456789",
                    SomeString = "This is just a string"
                };

                ShimHttpWebRequest.AllInstances.BeginGetResponseAsyncCallbackObject = (request, callback, arg3) =>
                {
                    return ShimsContext.ExecuteWithoutShims(() => request.BeginGetResponse(callback, arg3));
                };

                ShimHttpWebResponse.AllInstances.StatusCodeGet = response =>
                {
                    return ShimsContext.ExecuteWithoutShims(() => response.StatusCode);
                };

                ShimHttpWebResponse.AllInstances.GetResponseStream = response =>
                {
                    var bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(person));

                    var stream = new MemoryStream(bytes);

                    return stream;
                };

                ShimHttpWebResponse.AllInstances.HeadersGet = response =>
                {
                    var headers = new WebHeaderCollection();
                    headers.Add(HttpRequestHeader.ContentType, "text/html; charset=utf-8");

                    //base = {Content-Encoding: gzip Content-Type: text/html; charset=utf-8 Content-Length: 155}

                    return headers;
                };

                using (var client = new UncommonHttpClient())
                {
                    var result = await client.GetJsonAsync<Person>("http://www.xciles.com/");

                    Assert.AreEqual(person.Firstname, result.Firstname);
                    Assert.AreEqual(person.Lastname, result.Lastname);
                    Assert.AreEqual(person.PhoneNumber, result.PhoneNumber);
                }
            }
        }
        
        [TestMethod]
        public void GetJsonAsyncGzipTest()
        {
            GetJsonAsyncGzipTestAsync().Wait();
        }

        public async Task GetJsonAsyncGzipTestAsync()
        {
            using (ShimsContext.Create())
            {
                var person = new Person
                {
                    DateOfBirth = DateTime.Now.Subtract(new TimeSpan(800, 1, 1, 1)),
                    Firstname = "First",
                    Lastname = "Person",
                    PhoneNumber = "0123456789",
                    SomeString = "This is just a string"
                };

                ShimHttpWebRequest.AllInstances.BeginGetResponseAsyncCallbackObject = (request, callback, arg3) =>
                {
                    return ShimsContext.ExecuteWithoutShims(() => request.BeginGetResponse(callback, arg3));
                };

                ShimHttpWebResponse.AllInstances.StatusCodeGet = response =>
                {
                    return ShimsContext.ExecuteWithoutShims(() => response.StatusCode);
                };

                ShimHttpWebResponse.AllInstances.GetResponseStream = response =>
                {
                    var bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(person));

                    var stream = new MemoryStream(bytes);

                    var s = new GZipStream(stream, CompressionMode.Compress);

                    return s;
                };

                ShimHttpWebResponse.AllInstances.HeadersGet = response =>
                {
                    var headers = new WebHeaderCollection();
                    headers.Add(HttpRequestHeader.ContentType, "text/html; charset=utf-8");
                    headers.Add(HttpRequestHeader.ContentEncoding, "gzip");
                    return headers;
                };

                using (var client = new UncommonHttpClient())
                {
                    var result = await client.GetJsonAsync<Person>("http://www.xciles.com/");

                    Assert.AreEqual(person.Firstname, result.Firstname);
                    Assert.AreEqual(person.Lastname, result.Lastname);
                    Assert.AreEqual(person.PhoneNumber, result.PhoneNumber);
                }
            }
        }
    }
}
