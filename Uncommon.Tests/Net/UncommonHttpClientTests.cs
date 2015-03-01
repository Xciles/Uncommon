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
        #region Get Tests

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

        #endregion

        #region Post Tests

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

        #endregion

        #region Put Tests

        [TestMethod]
        public void PutContentAsJsonStringTest()
        {
            PutContentAsJsonStringTestAsync().Wait();
        }

        private async Task PutContentAsJsonStringTestAsync()
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

                ShimHttpClient.AllInstances.PutAsyncUriHttpContentCancellationToken = (client, uri, arg3, arg4) =>
                {
                    return Task.FromResult(new HttpResponseMessage()
                    {
                        StatusCode = HttpStatusCode.OK
                    });
                };

                using (var client = new UncommonHttpClient())
                {
                    var result = await client.PutContentAsJsonAsync("http://www.xciles.com/", person);

                    Assert.IsTrue(result.StatusCode == HttpStatusCode.OK);
                }
            }
        }

        [TestMethod]
        public void PutContentAsJsonStringCancelTest()
        {
            PutContentAsJsonStringCancelTestAsync().Wait();
        }

        private async Task PutContentAsJsonStringCancelTestAsync()
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

                ShimHttpClient.AllInstances.PutAsyncUriHttpContentCancellationToken = (client, uri, arg3, arg4) =>
                {
                    return Task.FromResult(new HttpResponseMessage()
                    {
                        StatusCode = HttpStatusCode.OK
                    });
                };

                using (var client = new UncommonHttpClient())
                {
                    var result = await client.PutContentAsJsonAsync("http://www.xciles.com/", person, CancellationToken.None);

                    Assert.IsTrue(result.StatusCode == HttpStatusCode.OK);
                }
            }
        }

        [TestMethod]
        public void PutContentAsJsonUriTest()
        {
            PutContentAsJsonUriTestAsync().Wait();
        }

        private async Task PutContentAsJsonUriTestAsync()
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

                ShimHttpClient.AllInstances.PutAsyncUriHttpContentCancellationToken = (client, uri, arg3, arg4) =>
                {
                    return Task.FromResult(new HttpResponseMessage()
                    {
                        StatusCode = HttpStatusCode.OK
                    });
                };

                using (var client = new UncommonHttpClient())
                {
                    var result = await client.PutContentAsJsonAsync(new Uri("http://www.xciles.com/"), person);

                    Assert.IsTrue(result.StatusCode == HttpStatusCode.OK);
                }
            }
        }

        #endregion

        #region Patch Implementation Tests

        [TestMethod]
        public void PatchAsyncStringTest()
        {
            PatchAsyncStringTestAsync().Wait();
        }

        private async Task PatchAsyncStringTestAsync()
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

                ShimHttpClient.AllInstances.SendAsyncHttpRequestMessage = (client, message) =>
                {
                    return Task.FromResult(new HttpResponseMessage()
                    {
                        StatusCode = HttpStatusCode.OK
                    });
                };

                using (var client = new UncommonHttpClient())
                {
                    var content = new StringContent(JsonConvert.SerializeObject(person));
                    var result = await client.PatchAsync("http://www.xciles.com/", content);

                    Assert.IsTrue(result.StatusCode == HttpStatusCode.OK);
                }
            }
        }

        [TestMethod]
        public void PatchAsyncStringCancelTest()
        {
            PatchAsyncStringCancelTestAsync().Wait();
        }

        private async Task PatchAsyncStringCancelTestAsync()
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

                ShimHttpClient.AllInstances.SendAsyncHttpRequestMessageCancellationToken = (client, message, token) =>
                {
                    return Task.FromResult(new HttpResponseMessage()
                    {
                        StatusCode = HttpStatusCode.OK
                    });
                };

                using (var client = new UncommonHttpClient())
                {
                    var content = new StringContent(JsonConvert.SerializeObject(person));
                    var result = await client.PatchAsync("http://www.xciles.com/", content, CancellationToken.None);

                    Assert.IsTrue(result.StatusCode == HttpStatusCode.OK);
                }
            }
        }

        [TestMethod]
        public void PatchAsyncUriTest()
        {
            PatchAsyncUriTestAsync().Wait();
        }

        private async Task PatchAsyncUriTestAsync()
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

                ShimHttpClient.AllInstances.SendAsyncHttpRequestMessage = (client, message) =>
                {
                    return Task.FromResult(new HttpResponseMessage()
                    {
                        StatusCode = HttpStatusCode.OK
                    });
                };

                using (var client = new UncommonHttpClient())
                {
                    var content = new StringContent(JsonConvert.SerializeObject(person));
                    var result = await client.PatchAsync(new Uri("http://www.xciles.com/"), content);

                    Assert.IsTrue(result.StatusCode == HttpStatusCode.OK);
                }
            }
        }


        #endregion

        #region Patch Tests

        [TestMethod]
        public void PatchContentAsJsonStringTest()
        {
            PatchContentAsJsonStringTestAsync().Wait();
        }

        private async Task PatchContentAsJsonStringTestAsync()
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

                ShimHttpClient.AllInstances.SendAsyncHttpRequestMessageCancellationToken = (client, message, token) =>
                {
                    return Task.FromResult(new HttpResponseMessage()
                    {
                        StatusCode = HttpStatusCode.OK
                    });
                };

                using (var client = new UncommonHttpClient())
                {
                    var result = await client.PatchContentAsJsonAsync("http://www.xciles.com/", person);

                    Assert.IsTrue(result.StatusCode == HttpStatusCode.OK);
                }
            }
        }

        [TestMethod]
        public void PatchContentAsJsonStringCancelTest()
        {
            PatchContentAsJsonStringCancelTestAsync().Wait();
        }

        private async Task PatchContentAsJsonStringCancelTestAsync()
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

                ShimHttpClient.AllInstances.SendAsyncHttpRequestMessageCancellationToken = (client, message, token) =>
                {
                    return Task.FromResult(new HttpResponseMessage()
                    {
                        StatusCode = HttpStatusCode.OK
                    });
                };

                using (var client = new UncommonHttpClient())
                {
                    var result = await client.PatchContentAsJsonAsync("http://www.xciles.com/", person, CancellationToken.None);

                    Assert.IsTrue(result.StatusCode == HttpStatusCode.OK);
                }
            }
        }

        [TestMethod]
        public void PatchContentAsJsonUriTest()
        {
            PatchContentAsJsonUriTestAsync().Wait();
        }

        private async Task PatchContentAsJsonUriTestAsync()
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

                ShimHttpClient.AllInstances.SendAsyncHttpRequestMessageCancellationToken = (client, message, token) =>
                {
                    return Task.FromResult(new HttpResponseMessage()
                    {
                        StatusCode = HttpStatusCode.OK
                    });
                };

                using (var client = new UncommonHttpClient())
                {
                    var result = await client.PatchContentAsJsonAsync(new Uri("http://www.xciles.com/"), person);

                    Assert.IsTrue(result.StatusCode == HttpStatusCode.OK);
                }
            }
        }

        #endregion
    }
}
