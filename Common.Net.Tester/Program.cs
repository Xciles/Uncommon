using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xciles.Common.Net;

namespace Common.Net.Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync(args).Wait();
        }

        static async Task MainAsync(string[] args)
        {
            var list = await RestRequestHelper.ProcessGetRequest<Rootobject>("http://api.geonames.org/citiesJSON?north=44.1&south=-9.9&east=-22.4&west=55.2&lang=de&username=demo", null);

            Console.ReadKey();
        }

        public class Rootobject
        {
            public IList<Geoname> geonames { get; set; }
        }

        public class Geoname
        {
            public string fcodeName { get; set; }
            public string toponymName { get; set; }
            public string countrycode { get; set; }
            public string fcl { get; set; }
            public string fclName { get; set; }
            public string name { get; set; }
            public string wikipedia { get; set; }
            public float lng { get; set; }
            public string fcode { get; set; }
            public int geonameId { get; set; }
            public float lat { get; set; }
            public int population { get; set; }
        }

    }
}
