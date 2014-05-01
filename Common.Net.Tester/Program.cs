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

                var bytes = await RestRequestHelper.ProcessRawGetRequest(String.Format("{0}/{1}", serviceUrl, "personbytes/1"), null);

                var post = await RestRequestHelper.ProcessPostRequest(String.Format("{0}/{1}", serviceUrl, "person"), single.Result);
                var postResult = await RestRequestHelper.ProcessPostRequest<Person>(String.Format("{0}/{1}", serviceUrl, "person"), single.Result);

                var put = await RestRequestHelper.ProcessPutRequest(String.Format("{0}/{1}", serviceUrl, "person"), single.Result);
                var putResult = await RestRequestHelper.ProcessPutRequest<Person>(String.Format("{0}/{1}", serviceUrl, "person"), single.Result);

                var patch = await RestRequestHelper.ProcessPatchRequest(String.Format("{0}/{1}", serviceUrl, "person"), single.Result);
                var patchResult = await RestRequestHelper.ProcessPatchRequest<Person>(String.Format("{0}/{1}", serviceUrl, "person"), single.Result);

                var badRequest = await RestRequestHelper.ProcessPostRequest<Person>(String.Format("{0}/{1}", serviceUrl, "badrequest"), single.Result);

                var willFailAlsoFail = await RestRequestHelper.ProcessGetRequest<Person>(String.Format("{0}/{1}", serviceUrl, "person"));
            }
            catch (RestRequestException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("---------");
                Console.WriteLine(ex.ServiceExceptionResult.Message);
                Console.WriteLine(ex.ServiceExceptionResult.MessageDetail);
                Console.WriteLine(ex.ServiceExceptionResult.ExceptionResultTypeValue);
                Console.WriteLine(ex.ServiceExceptionResult.StackTrace);
                Console.WriteLine("---------");
            }

            Console.ReadKey();
        }

        

    }
}
