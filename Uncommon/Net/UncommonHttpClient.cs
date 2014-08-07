using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Xciles.Uncommon.Net
{
    public class UncommonHttpClient : HttpClient
    {
        protected JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings() { PreserveReferencesHandling = PreserveReferencesHandling.Objects };

        // HttpRequestException 
        public async Task<T> GetJsonAsync<T>(Uri requestUri, HttpCompletionOption httpCompletionOption, CancellationToken cancellationToken)
        {
            var httpResponseMessage = await GetAsync(requestUri, httpCompletionOption, cancellationToken);
            httpResponseMessage.EnsureSuccessStatusCode();

            var resultAsString = await httpResponseMessage.Content.ReadAsStringAsync();
            return await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<T>(resultAsString, JsonSerializerSettings), cancellationToken).ConfigureAwait(false);
        }

        public async Task<HttpResponseMessage> PostContentAsJsonAsync<T>(Uri requestUri, T requestContent, CancellationToken cancellationToken)
        {
            var requestBody = await Task.Factory.StartNew(() => JsonConvert.SerializeObject(requestContent, JsonSerializerSettings), cancellationToken).ConfigureAwait(false);

            HttpContent httpContent = new StringContent(requestBody);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return await PostAsync(requestUri, httpContent, cancellationToken).ConfigureAwait(false);
        }

        public async Task<HttpResponseMessage> PutContentAsJsonAsync<T>(Uri requestUri, T requestContent, CancellationToken cancellationToken)
        {
            var requestBody = await Task.Factory.StartNew(() => JsonConvert.SerializeObject(requestContent, JsonSerializerSettings), cancellationToken).ConfigureAwait(false);

            HttpContent httpContent = new StringContent(requestBody);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return await PutAsync(requestUri, httpContent, cancellationToken).ConfigureAwait(false);
        }

        public async Task<HttpResponseMessage> PatchContentAsJsonAsync<T>(Uri requestUri, T requestContent, CancellationToken cancellationToken)
        {
            var requestBody = await Task.Factory.StartNew(() => JsonConvert.SerializeObject(requestContent, JsonSerializerSettings), cancellationToken).ConfigureAwait(false);

            HttpContent httpContent = new StringContent(requestBody);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return await PatchAsync(requestUri, httpContent, cancellationToken).ConfigureAwait(false);
        }


        #region Patch support

        public async Task<HttpResponseMessage> PatchAsync(string requestUri, HttpContent content)
        {
            var method = new HttpMethod("PATCH");

            var request = new HttpRequestMessage(method, requestUri)
            {
                Content = content
            };

            return await SendAsync(request);
        }

        public async Task<HttpResponseMessage> PatchAsync(Uri requestUri, HttpContent content)
        {
            var method = new HttpMethod("PATCH");

            var request = new HttpRequestMessage(method, requestUri)
            {
                Content = content
            };

            return await SendAsync(request);
        }

        public async Task<HttpResponseMessage> PatchAsync(string requestUri, HttpContent content, CancellationToken cancellationToken)
        {
            var method = new HttpMethod("PATCH");

            var request = new HttpRequestMessage(method, requestUri)
            {
                Content = content
            };

            return await SendAsync(request, cancellationToken);
        }

        public async Task<HttpResponseMessage> PatchAsync(Uri requestUri, HttpContent content, CancellationToken cancellationToken)
        {
            var method = new HttpMethod("PATCH");

            var request = new HttpRequestMessage(method, requestUri)
            {
                Content = content
            };

            return await SendAsync(request, cancellationToken);
        }

        #endregion
    }
}
