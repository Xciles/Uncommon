using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Xciles.Uncommon.Net;

namespace Xciles.Uncommon.Tests.Net
{
    [TestClass]
    public class UncommonHttpClientQuickTests
    {
        [TestMethod]
        public void QuickTests()
        {
            QuickTestsAsync().Wait();
        }

        public async Task QuickTestsAsync()
        {
            var fakeResponseHandler = new FakeResponseHandler();
            fakeResponseHandler.AddFakeResponse(new Uri("http://example.org/test"), new HttpResponseMessage(HttpStatusCode.OK));

            using (var client = new UncommonHttpClient(fakeResponseHandler))
            {
                var response1 = await client.GetAsync("http://example.org/notthere");
                var response2 = await client.GetAsync("http://example.org/test");

                Assert.AreEqual(response1.StatusCode, HttpStatusCode.NotFound);
                Assert.AreEqual(response2.StatusCode, HttpStatusCode.OK);
            }
        }

        [TestMethod]
        public void QuickDisposeTests()
        {
            QuickDisposeTestsAsync().Wait();
        }

        public async Task QuickDisposeTestsAsync()
        {
            var fakeResponseHandler = new FakeResponseHandler();
            fakeResponseHandler.AddFakeResponse(new Uri("http://example.org/test"), new HttpResponseMessage(HttpStatusCode.OK));

            using (var client = new UncommonHttpClient(fakeResponseHandler, false))
            {
                var response1 = await client.GetAsync("http://example.org/notthere");
                var response2 = await client.GetAsync("http://example.org/test");

                Assert.AreEqual(response1.StatusCode, HttpStatusCode.NotFound);
                Assert.AreEqual(response2.StatusCode, HttpStatusCode.OK);
            }
        }

        [TestMethod]
        public void WithObjectTest()
        {
            WithObjectTestAsync().Wait();
        }

        private async Task WithObjectTestAsync()
        {
            var fakeResponseHandler = new FakeResponseHandler();
            var httpResponseMessage = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK
            };

            var person = new Person
            {
                DateOfBirth = DateTime.Now.Subtract(new TimeSpan(800, 1, 1, 1)),
                Firstname = "First",
                Lastname = "Person",
                PhoneNumber = "0123456789",
                SomeString = "This is just a string"
            };

            var stringetje = JsonConvert.SerializeObject(person);

            httpResponseMessage.Content = new StringContent(stringetje);
            fakeResponseHandler.AddFakeResponse(new Uri("http://example.org/test"), httpResponseMessage);

            using (var client = new UncommonHttpClient(fakeResponseHandler, false))
            {
                var response = await client.GetJsonAsync<Person>("http://example.org/test");

                Assert.AreEqual(person.Firstname, response.Firstname);
                Assert.AreEqual(person.Lastname, response.Lastname);
                Assert.AreEqual(person.PhoneNumber, response.PhoneNumber);
            }
        }
    }
}
