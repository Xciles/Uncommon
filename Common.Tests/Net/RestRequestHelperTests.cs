using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Fakes;
using System.Text;
using System.Threading.Tasks;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Xciles.Common.Net;

namespace Xciles.Common.Tests.Net
{
    public class Person
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string SomeString { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
    }

    [TestClass]
    public class RestRequestHelperTests
    {
        [TestMethod]
        public async void GetTest()
        {
            using (ShimsContext.Create())
            {
                ShimHttpWebResponse res = new ShimHttpWebResponse();
                res.StatusCodeGet = () => HttpStatusCode.OK;
                res.GetResponseStream = () =>
                {
                    var bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new Person()
                    {
                        DateOfBirth = DateTime.Now.Subtract(new TimeSpan(800, 1, 1, 1)),
                        Firstname = "First",
                        Lastname = "Person",
                        PhoneNumber = "0123456789",
                        SomeString = "This is just a string"
                    }));

                    var stream = new MemoryStream(bytes);

                    return stream;
                };

                ShimHttpWebRequest.AllInstances.GetResponse = request =>
                {
                    return res;
                };


                //ShimHttpWebRequest.AllInstances.async = (request, result) =>
                //{
                //    return res;
                //};

                var list = await RestRequestHelper.ProcessGetRequest<Person>(String.Format("{0}/{1}", "http://www.example.com", "person"), null);


            }
        }
    }
}
