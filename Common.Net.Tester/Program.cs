using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xciles.Common.Web.Tester.Domain;

namespace Xciles.Common.Net.Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync(args).Wait();
        }

        static async Task MainAsync(string[] args)
        {
            var serviceUrl = "http://localhost:13580/api";

            try
            {
                var list = await RestRequestHelper.ProcessGetRequest<IList<Person>>(String.Format("{0}/{1}", serviceUrl, "person"), null);
                var single = await RestRequestHelper.ProcessGetRequest<Person>(String.Format("{0}/{1}", serviceUrl, "person/1"), null);

                var willFail = await RestRequestHelper.ProcessGetRequest<Person>(String.Format("{0}/{1}", serviceUrl, "person"), null);
            }
            catch (RestRequestException ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }

        

    }
}
