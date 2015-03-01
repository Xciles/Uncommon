using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Fakes;
using System.Threading.Tasks;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Xciles.Uncommon.Net;

namespace Xciles.Uncommon.Tests.Net
{
    [TestClass]
    public class UncommonHttpClientTests
    {
        [TestMethod]
        public void GetJsonAsyncStringTest()
        {
            GetJsonAsyncStringTestAsync().Wait();
        }

        public async Task GetJsonAsyncStringTestAsync()
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

                ShimHttpClient.AllInstances.GetAsyncUriHttpCompletionOptionCancellationToken = (httpClient, uri, arg3, arg4) =>
                {
                    return Task.FromResult(new HttpResponseMessage()
                    {
                        Content = new StringContent(JsonConvert.SerializeObject(person)),
                        StatusCode = HttpStatusCode.OK
                    });
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
        public void GetJsonAsyncUriTest()
        {
            GetJsonAsyncUriTestAsync().Wait();
        }

        public async Task GetJsonAsyncUriTestAsync()
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

                ShimHttpClient.AllInstances.GetAsyncUriHttpCompletionOptionCancellationToken = (httpClient, uri, arg3, arg4) =>
                {
                    return Task.FromResult(new HttpResponseMessage()
                    {
                        Content = new StringContent(JsonConvert.SerializeObject(person)),
                        StatusCode = HttpStatusCode.OK
                    });
                };

                using (var client = new UncommonHttpClient())
                {
                    var result = await client.GetJsonAsync<Person>(new Uri("http://www.xciles.com/"));

                    Assert.AreEqual(person.Firstname, result.Firstname);
                    Assert.AreEqual(person.Lastname, result.Lastname);
                    Assert.AreEqual(person.PhoneNumber, result.PhoneNumber);
                }
            }
        }


    }
}
