using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.WebApi.MessageHandlers.Compression;
using Microsoft.AspNet.WebApi.MessageHandlers.Compression.Compressors;
using Newtonsoft.Json;

namespace Uncommon.Tests.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync().Wait();
        }

        private static async Task MainAsync()
        {
            //var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate });

            var client = new HttpClient(new ClientCompressionHandler(new HttpClientHandler(), new GZipCompressor(), new DeflateCompressor()));

            client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));
            client.Timeout = new TimeSpan(0,0,2);
            var t = await client.GetAsync("http://localhost:31146/api/uncommon/testdatas");
            t.EnsureSuccessStatusCode();

            var dataAsString = await client.GetStringAsync("http://localhost:31146/api/uncommon/testdatas");
            var data = JsonConvert.DeserializeObject<IList<UncommonData>>(dataAsString);


            Console.ReadKey();
        }
    }
}
