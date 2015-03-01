using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Fakes;
using System.Threading;
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

        private async Task GetJsonAsyncStringTestAsync()
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

        private async Task GetJsonAsyncUriTestAsync()
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

        [TestMethod]
        public void PostContentAsJsonStringTest()
        {
            PostContentAsJsonStringTestAsync().Wait();
        }

        private async Task PostContentAsJsonStringTestAsync()
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

                ShimHttpClient.AllInstances.PostAsyncUriHttpContentCancellationToken = (client, uri, arg3, arg4) =>
                {
                    return Task.FromResult(new HttpResponseMessage()
                    {
                        StatusCode = HttpStatusCode.OK
                    });
                };

                using (var client = new UncommonHttpClient())
                {
                    var result = await client.PostContentAsJsonAsync("http://www.xciles.com/", person);

                    Assert.IsTrue(result.StatusCode == HttpStatusCode.OK);
                }
            }
        }

        [TestMethod]
        public void PostContentAsJsonStringCancelTest()
        {
            PostContentAsJsonStringCancelTestAsync().Wait();
        }

        private async Task PostContentAsJsonStringCancelTestAsync()
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

                ShimHttpClient.AllInstances.PostAsyncUriHttpContentCancellationToken = (client, uri, arg3, arg4) =>
                {
                    return Task.FromResult(new HttpResponseMessage()
                    {
                        StatusCode = HttpStatusCode.OK
                    });
                };

                using (var client = new UncommonHttpClient())
                {
                    var result = await client.PostContentAsJsonAsync("http://www.xciles.com/", person, CancellationToken.None);

                    Assert.IsTrue(result.StatusCode == HttpStatusCode.OK);
                }
            }
        }

        [TestMethod]
        public void PostContentAsJsonUriTest()
        {
            PostContentAsJsonUriTestAsync().Wait();
        }

        private async Task PostContentAsJsonUriTestAsync()
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

                ShimHttpClient.AllInstances.PostAsyncUriHttpContentCancellationToken = (client, uri, arg3, arg4) =>
                {
                    return Task.FromResult(new HttpResponseMessage()
                    {
                        StatusCode = HttpStatusCode.OK
                    });
                };

                using (var client = new UncommonHttpClient())
                {
                    var result = await client.PostContentAsJsonAsync(new Uri("http://www.xciles.com/"), person);

                    Assert.IsTrue(result.StatusCode == HttpStatusCode.OK);
                }
            }
        }
    }
}
