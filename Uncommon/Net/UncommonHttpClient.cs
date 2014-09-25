using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xciles.Uncommon.Handler;

namespace Xciles.Uncommon.Net
{
    public class UncommonHttpClient : HttpClient
    {
        protected JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings() { PreserveReferencesHandling = PreserveReferencesHandling.Objects };

        public UncommonHttpClient()
            : base(new UncommonHttpClientHandler())
        {
        }

        public UncommonHttpClient(HttpMessageHandler handler)
            : base(handler)
        {

        }

        public UncommonHttpClient(HttpMessageHandler handler, bool disposeHandler)
            : base(handler, disposeHandler)
        {

        }

        public async Task<T> GetJsonAsync<T>(string requestUrl, HttpCompletionOption httpCompletionOption = HttpCompletionOption.ResponseContentRead)
        {
            return await GetJsonAsync<T>(new Uri(requestUrl), httpCompletionOption, CancellationToken.None).ConfigureAwait(false);
        }

        public async Task<T> GetJsonAsync<T>(Uri requestUri, HttpCompletionOption httpCompletionOption = HttpCompletionOption.ResponseContentRead)
        {
            return await GetJsonAsync<T>(requestUri, httpCompletionOption, CancellationToken.None).ConfigureAwait(false);
        }

        // HttpRequestException 
        public async Task<T> GetJsonAsync<T>(Uri requestUri, HttpCompletionOption httpCompletionOption, CancellationToken cancellationToken)
        {
            var httpResponseMessage = await GetAsync(requestUri, httpCompletionOption, cancellationToken).ConfigureAwait(false);
            httpResponseMessage.EnsureSuccessStatusCode();

            var resultAsString = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<T>(resultAsString, JsonSerializerSettings);
        }

        public async Task<HttpResponseMessage> PostContentAsJsonAsync<T>(string requestUrl, T requestContent)
        {
            return await PostContentAsJsonAsync(new Uri(requestUrl), requestContent, CancellationToken.None).ConfigureAwait(false);
        }

        public async Task<HttpResponseMessage> PostContentAsJsonAsync<T>(string requestUrl, T requestContent, CancellationToken cancellationToken)
        {
            return await PostContentAsJsonAsync(new Uri(requestUrl), requestContent, cancellationToken).ConfigureAwait(false);
        }

        public async Task<HttpResponseMessage> PostContentAsJsonAsync<T>(Uri requestUri, T requestContent)
        {
            return await PostContentAsJsonAsync(requestUri, requestContent, CancellationToken.None).ConfigureAwait(false);
        }

        public async Task<HttpResponseMessage> PostContentAsJsonAsync<T>(Uri requestUri, T requestContent, CancellationToken cancellationToken)
        {
            var requestBody = JsonConvert.SerializeObject(requestContent, JsonSerializerSettings);

            HttpContent httpContent = new StringContent(requestBody);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return await PostAsync(requestUri, httpContent, cancellationToken).ConfigureAwait(false);
        }

        public async Task<HttpResponseMessage> PutContentAsJsonAsync<T>(string requestUrl, T requestContent)
        {
            return await PutContentAsJsonAsync(new Uri(requestUrl), requestContent, CancellationToken.None).ConfigureAwait(false);
        }

        public async Task<HttpResponseMessage> PutContentAsJsonAsync<T>(string requestUrl, T requestContent, CancellationToken cancellationToken)
        {
            return await PutContentAsJsonAsync(new Uri(requestUrl), requestContent, cancellationToken).ConfigureAwait(false);
        }

        public async Task<HttpResponseMessage> PutContentAsJsonAsync<T>(Uri requestUri, T requestContent)
        {
            return await PutContentAsJsonAsync(requestUri, requestContent, CancellationToken.None).ConfigureAwait(false);
        }

        public async Task<HttpResponseMessage> PutContentAsJsonAsync<T>(Uri requestUri, T requestContent, CancellationToken cancellationToken)
        {
            var requestBody = JsonConvert.SerializeObject(requestContent, JsonSerializerSettings);

            HttpContent httpContent = new StringContent(requestBody);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return await PutAsync(requestUri, httpContent, cancellationToken).ConfigureAwait(false);
        }

        public async Task<HttpResponseMessage> PatchContentAsJsonAsync<T>(string requestUrl, T requestContent)
        {
            return await PatchContentAsJsonAsync(new Uri(requestUrl), requestContent, CancellationToken.None).ConfigureAwait(false);
        }

        public async Task<HttpResponseMessage> PatchContentAsJsonAsync<T>(string requestUrl, T requestContent, CancellationToken cancellationToken)
        {
            return await PatchContentAsJsonAsync(new Uri(requestUrl), requestContent, cancellationToken).ConfigureAwait(false);
        }

        public async Task<HttpResponseMessage> PatchContentAsJsonAsync<T>(Uri requestUri, T requestContent)
        {
            return await PatchContentAsJsonAsync(requestUri, requestContent, CancellationToken.None).ConfigureAwait(false);
        }

        public async Task<HttpResponseMessage> PatchContentAsJsonAsync<T>(Uri requestUri, T requestContent, CancellationToken cancellationToken)
        {
            var requestBody = JsonConvert.SerializeObject(requestContent, JsonSerializerSettings);

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

            return await SendAsync(request).ConfigureAwait(false);
        }

        public async Task<HttpResponseMessage> PatchAsync(Uri requestUri, HttpContent content)
        {
            var method = new HttpMethod("PATCH");

            var request = new HttpRequestMessage(method, requestUri)
            {
                Content = content
            };

            return await SendAsync(request).ConfigureAwait(false);
        }

        public async Task<HttpResponseMessage> PatchAsync(string requestUri, HttpContent content, CancellationToken cancellationToken)
        {
            var method = new HttpMethod("PATCH");

            var request = new HttpRequestMessage(method, requestUri)
            {
                Content = content
            };

            return await SendAsync(request, cancellationToken).ConfigureAwait(false);
        }

        public async Task<HttpResponseMessage> PatchAsync(Uri requestUri, HttpContent content, CancellationToken cancellationToken)
        {
            var method = new HttpMethod("PATCH");

            var request = new HttpRequestMessage(method, requestUri)
            {
                Content = content
            };

            return await SendAsync(request, cancellationToken).ConfigureAwait(false);
        }

        #endregion
    }
}
