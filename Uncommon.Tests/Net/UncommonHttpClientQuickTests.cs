using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    }
}
