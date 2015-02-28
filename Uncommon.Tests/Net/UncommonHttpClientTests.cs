using System;
using System.IO;
using System.Net;
using System.Net.Fakes;
using System.Net.Http;
using System.Net.Http.Fakes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Global.Fakes;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Xciles.Uncommon.Net;

namespace Xciles.Uncommon.Tests.Net
{
    public class Person
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string SomeString { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
    }

    [TestClass]
    public class UncommonHttpClientTests
    {
        [TestMethod]
        public void TestTest()
        {
            GetAsyncTestAsync().Wait();
        }

        public async Task GetAsyncTestAsync()
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

                ShimHttpClient client = new ShimHttpClient();
                client.GetAsyncUriHttpCompletionOptionCancellationToken = (uri, option, arg3) =>
                {
                    return Task.FromResult(new HttpResponseMessage()
                    {
                        Content = new StringContent(JsonConvert.SerializeObject(person)),
                        StatusCode = HttpStatusCode.OK
                    });
                };
                client.GetAsyncUri = uri =>
                {
                    return Task.FromResult(new HttpResponseMessage());
                };

                client.GetAsyncUriCancellationToken = (uri, token) =>
                {
                    return Task.FromResult(new HttpResponseMessage());
                };

                client.GetAsyncUriHttpCompletionOption = (uri, option) =>
                {
                    return Task.FromResult(new HttpResponseMessage());
                };

                ShimHttpClient.AllInstances.GetAsyncUriHttpCompletionOptionCancellationToken = (httpClient, uri, arg3, arg4) => 
                {
                    //return ShimsContext.ExecuteWithoutShims(() => httpClient.GetAsync(uri, arg3, arg4));
                    return Task.FromResult(new HttpResponseMessage()
                    {
                        Content = new StringContent(JsonConvert.SerializeObject(person)),
                        StatusCode = HttpStatusCode.OK
                    });
                };

                ShimAsyncExtensions.GetResponseAsyncWebRequest = request =>
                {
                    return ShimsContext.ExecuteWithoutShims(() => request.GetResponseAsync());
                };

                //ShimHttpWebRequest.AllInstances.BeginGetResponseAsyncCallbackObject = (request, callback, arg3) =>
                //{
                //    return ShimsContext.ExecuteWithoutShims(() => request.BeginGetResponse(callback, arg3));
                //};

                //ShimHttpWebResponse.AllInstances.StatusCodeGet = response =>
                //{
                //    return ShimsContext.ExecuteWithoutShims(() => response.StatusCode);
                //};

                //ShimHttpWebResponse.AllInstances.GetResponseStream = response =>
                //{
                //    var bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(person));

                //    var stream = new MemoryStream(bytes);

                //    return stream;
                //};

                //ShimHttpWebResponse.AllInstances.HeadersGet = response =>
                //{
                //    //return ShimsContext.ExecuteWithoutShims(() => response.Headers);

                //    var headers = new WebHeaderCollection();
                //    headers.Add(HttpRequestHeader.ContentType, "text/html; charset=utf-8");

                //    return headers;
                //    //base = {Content-Encoding: gzip Content-Type: text/html; charset=utf-8 Content-Length: 155}
                //};

                HttpClient aa = new HttpClient();
                var a = await aa.GetAsync(new Uri("http://www.xciles.com/"), HttpCompletionOption.ResponseContentRead, CancellationToken.None);

                UncommonHttpClient bla = new UncommonHttpClient();

                var t = await bla.GetJsonAsync<Person>("http://www.xciles.com/");
            }
        }
    }
}
