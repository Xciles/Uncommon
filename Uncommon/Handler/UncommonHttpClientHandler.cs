using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Xciles.Uncommon.Handler
{
    public class UncommonHttpClientHandler : HttpClientHandler
    {
        public override bool SupportsAutomaticDecompression
        {
            get { return true; }
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));

            // Todo add gzip when sending.
            var response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);

            if (response.Content.Headers.ContentEncoding.Contains("gzip"))
            {
                var content = new GZipHttpContent(await response.Content.ReadAsStreamAsync().ConfigureAwait(false), response.Content.Headers);
                response.Content = content;
            }
            return response;
        }
    }
}
