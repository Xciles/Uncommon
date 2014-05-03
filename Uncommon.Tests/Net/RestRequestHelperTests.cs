using System;
using System.IO;
using System.Net;
using System.Net.Fakes;
using System.Text;
using System.Threading.Tasks;
using Global.Fakes;
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
        public void GetTest()
        {
            GetTestAsync().Wait();
        }

        private async Task GetTestAsync()
        {
            using (ShimsContext.Create())
            {
                var person = new Person
                {
                    DateOfBirth = DateTime.Now.Subtract(new TimeSpan(800, 1, 1, 1)),
                    Firstname = "First",
                    Lastname = "Person",
                    PhoneNumber = "0123456789",
                    SomeString = "This is just a string"
                };


                ShimHttpWebResponse res = new ShimHttpWebResponse
                {
                    StatusCodeGet = () => HttpStatusCode.OK,
                    GetResponseStream = () =>
                    {
                        var bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(person));

                        var stream = new MemoryStream(bytes);

                        return stream;
                    },
                    CookiesGet = () => new CookieCollection()
                };

                ShimAsyncExtensions.GetResponseAsyncWebRequest = request =>
                {
                    return Task.FromResult((WebResponse)res.Instance);
                };

                var single = await RestRequestHelper.ProcessGetRequest<Person>(String.Format("{0}/{1}", "http://www.example.com", "person"), null);

                Assert.AreEqual(person.Firstname, single.Result.Firstname);
                Assert.AreEqual(person.Lastname, single.Result.Lastname);
                Assert.AreEqual(person.PhoneNumber, single.Result.PhoneNumber);
                Assert.IsTrue(single.StatusCode == HttpStatusCode.OK);
            }
        }

    }
}
