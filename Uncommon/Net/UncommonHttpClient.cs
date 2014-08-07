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

        public async Task<T> GetJsonAsync<T>(Uri requestUri, HttpCompletionOption httpCompletionOption = HttpCompletionOption.ResponseContentRead)
        {
            return await GetJsonAsync<T>(requestUri, httpCompletionOption, CancellationToken.None);
        }

        // HttpRequestException 
        public async Task<T> GetJsonAsync<T>(Uri requestUri, HttpCompletionOption httpCompletionOption, CancellationToken cancellationToken)
        {
            var httpResponseMessage = await GetAsync(requestUri, httpCompletionOption, cancellationToken);
            httpResponseMessage.EnsureSuccessStatusCode();

            var resultAsString = await httpResponseMessage.Content.ReadAsStringAsync();
            return await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<T>(resultAsString, JsonSerializerSettings), cancellationToken).ConfigureAwait(false);
        }

        public async Task<HttpResponseMessage> PostContentAsJsonAsync<T>(string requestUrl, T requestContent)
        {
            return await PostContentAsJsonAsync(new Uri(requestUrl), requestContent, CancellationToken.None);
        }

        public async Task<HttpResponseMessage> PostContentAsJsonAsync<T>(string requestUrl, T requestContent, CancellationToken cancellationToken)
        {
            return await PostContentAsJsonAsync(new Uri(requestUrl), requestContent, cancellationToken);
        }

        public async Task<HttpResponseMessage> PostContentAsJsonAsync<T>(Uri requestUri, T requestContent)
        {
            return await PostContentAsJsonAsync(requestUri, requestContent, CancellationToken.None);
        }

        public async Task<HttpResponseMessage> PostContentAsJsonAsync<T>(Uri requestUri, T requestContent, CancellationToken cancellationToken)
        {
            var requestBody = await Task.Factory.StartNew(() => JsonConvert.SerializeObject(requestContent, JsonSerializerSettings), cancellationToken).ConfigureAwait(false);

            HttpContent httpContent = new StringContent(requestBody);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return await PostAsync(requestUri, httpContent, cancellationToken).ConfigureAwait(false);
        }

        public async Task<HttpResponseMessage> PutContentAsJsonAsync<T>(string requestUrl, T requestContent)
        {
            return await PutContentAsJsonAsync(new Uri(requestUrl), requestContent, CancellationToken.None);
        }

        public async Task<HttpResponseMessage> PutContentAsJsonAsync<T>(string requestUrl, T requestContent, CancellationToken cancellationToken)
        {
            return await PutContentAsJsonAsync(new Uri(requestUrl), requestContent, cancellationToken);
        }

        public async Task<HttpResponseMessage> PutContentAsJsonAsync<T>(Uri requestUri, T requestContent)
        {
            return await PutContentAsJsonAsync(requestUri, requestContent, CancellationToken.None);
        }

        public async Task<HttpResponseMessage> PutContentAsJsonAsync<T>(Uri requestUri, T requestContent, CancellationToken cancellationToken)
        {
            var requestBody = await Task.Factory.StartNew(() => JsonConvert.SerializeObject(requestContent, JsonSerializerSettings), cancellationToken).ConfigureAwait(false);

            HttpContent httpContent = new StringContent(requestBody);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return await PutAsync(requestUri, httpContent, cancellationToken).ConfigureAwait(false);
        }

        public async Task<HttpResponseMessage> PatchContentAsJsonAsync<T>(string requestUrl, T requestContent)
        {
            return await PatchContentAsJsonAsync(new Uri(requestUrl), requestContent, CancellationToken.None);
        }

        public async Task<HttpResponseMessage> PatchContentAsJsonAsync<T>(string requestUrl, T requestContent, CancellationToken cancellationToken)
        {
            return await PatchContentAsJsonAsync(new Uri(requestUrl), requestContent, cancellationToken);
        }

        public async Task<HttpResponseMessage> PatchContentAsJsonAsync<T>(Uri requestUri, T requestContent)
        {
            return await PatchContentAsJsonAsync(requestUri, requestContent, CancellationToken.None);
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
