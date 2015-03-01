﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xciles.Uncommon.Net;

namespace Xciles.Uncommon.Tests.Net
{
    public class FakeResponseHandler : DelegatingHandler
    {
        private readonly Dictionary<Uri, HttpResponseMessage> _fakeResponses = new Dictionary<Uri, HttpResponseMessage>();

        public void AddFakeResponse(Uri uri, HttpResponseMessage responseMessage)
        {
            _fakeResponses.Add(uri, responseMessage);
        }

        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            if (_fakeResponses.ContainsKey(request.RequestUri))
            {
                return _fakeResponses[request.RequestUri];
            }
            return new HttpResponseMessage(HttpStatusCode.NotFound) { RequestMessage = request };
        }
    }

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
