using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Fakes;
using System.Text;
using System.Threading.Tasks;
using Global.Fakes;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Xciles.Uncommon.Net;

namespace Xciles.Uncommon.Tests.Net
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
    public class RestRequestHelperJsonTests
    {
        [TestMethod]
        public void GetTest()
        {
            GetTestAsync().Wait();
        }

        [TestMethod]
        public void GetRawTest()
        {
            GetRawTestAsync().Wait();
        }

        [TestMethod]
        public void GetRawLongTest()
        {
            GetRawLongTestAsync().Wait();
        }

        [TestMethod]
        public void PostTest()
        {
            PostTestAsync().Wait();
        }

        [TestMethod]
        public void PostWithResultTest()
        {
            PostWithResultTestAsync().Wait();
        }

        [TestMethod]
        public void PutTest()
        {
            PutTestAsync().Wait();
        }

        [TestMethod]
        public void PutWithResultTest()
        {
            PutWithResultTestAsync().Wait();
        }

        [TestMethod]
        public void PatchTest()
        {
            PatchTestAsync().Wait();
        }

        [TestMethod]
        public void PatchWithResultTest()
        {
            PatchWithResultTestAsync().Wait();
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

                var response = await RestRequestHelper.ProcessGetRequest<Person>(String.Format("{0}/{1}", "http://www.example.com", "person"));

                Assert.AreEqual(person.Firstname, response.Result.Firstname);
                Assert.AreEqual(person.Lastname, response.Result.Lastname);
                Assert.AreEqual(person.PhoneNumber, response.Result.PhoneNumber);
                Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
            }
        }

        private async Task GetRawTestAsync()
        {
            using (ShimsContext.Create())
            {
                var aString = "Short String just for testing!";
                var stringAsBytes = Encoding.UTF8.GetBytes(aString);

                ShimHttpWebResponse res = new ShimHttpWebResponse
                {
                    StatusCodeGet = () => HttpStatusCode.OK,
                    GetResponseStream = () =>
                    {
                        var stream = new MemoryStream(stringAsBytes);

                        return stream;
                    },
                    CookiesGet = () => new CookieCollection()
                };

                ShimAsyncExtensions.GetResponseAsyncWebRequest = request =>
                {
                    return Task.FromResult((WebResponse)res.Instance);
                };

                var response = await RestRequestHelper.ProcessRawGetRequest(String.Format("{0}/{1}", "http://www.example.com", "personAsBytes"));

                var responseString = Encoding.UTF8.GetString(response.Result);

                Assert.IsTrue(stringAsBytes.Count() == response.Result.Count());
                Assert.AreEqual(aString, responseString);
                Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
            }
        }


        private async Task GetRawLongTestAsync()
        {
            using (ShimsContext.Create())
            {
                var aString = "Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! " +
                              "Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! ";
                var stringAsBytes = Encoding.UTF8.GetBytes(aString);

                ShimHttpWebResponse res = new ShimHttpWebResponse
                {
                    StatusCodeGet = () => HttpStatusCode.OK,
                    GetResponseStream = () =>
                    {
                        var stream = new MemoryStream(stringAsBytes);

                        return stream;
                    },
                    CookiesGet = () => new CookieCollection()
                };

                ShimAsyncExtensions.GetResponseAsyncWebRequest = request =>
                {
                    return Task.FromResult((WebResponse)res.Instance);
                };

                var response = await RestRequestHelper.ProcessRawGetRequest(String.Format("{0}/{1}", "http://www.example.com", "personAsBytes"));

                var responseString = Encoding.UTF8.GetString(response.Result);

                Assert.IsTrue(stringAsBytes.Count() == response.Result.Count());
                Assert.AreEqual(aString, responseString);
                Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
            }
        }

        private async Task PostTestAsync()
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
                    CookiesGet = () => new CookieCollection()
                };

                var writeStream = (Stream)new MemoryStream();

                ShimAsyncExtensions.GetRequestStreamAsyncWebRequest = request =>
                {
                    return Task.FromResult(writeStream);
                };

                ShimAsyncExtensions.GetResponseAsyncWebRequest = request =>
                {
                    return Task.FromResult((WebResponse)res.Instance);
                };

                var response = await RestRequestHelper.ProcessPostRequest(String.Format("{0}/{1}", "http://www.example.com", "person"), person);

                Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
            }
        }

        private async Task PostWithResultTestAsync()
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

                var writeStream = (Stream)new MemoryStream();

                ShimAsyncExtensions.GetRequestStreamAsyncWebRequest = request =>
                {
                    return Task.FromResult(writeStream);
                };

                ShimAsyncExtensions.GetResponseAsyncWebRequest = request =>
                {
                    return Task.FromResult((WebResponse)res.Instance);
                };

                var response = await RestRequestHelper.ProcessPostRequest<Person, Person>(String.Format("{0}/{1}", "http://www.example.com", "person"), person);

                Assert.AreEqual(person.Firstname, response.Result.Firstname);
                Assert.AreEqual(person.Lastname, response.Result.Lastname);
                Assert.AreEqual(person.PhoneNumber, response.Result.PhoneNumber);
                Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
            }
        }

        private async Task PutTestAsync()
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
                    CookiesGet = () => new CookieCollection()
                };

                var writeStream = (Stream)new MemoryStream();

                ShimAsyncExtensions.GetRequestStreamAsyncWebRequest = request =>
                {
                    return Task.FromResult(writeStream);
                };

                ShimAsyncExtensions.GetResponseAsyncWebRequest = request =>
                {
                    return Task.FromResult((WebResponse)res.Instance);
                };

                var response = await RestRequestHelper.ProcessPutRequest(String.Format("{0}/{1}", "http://www.example.com", "person"), person);

                Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
            }
        }

        private async Task PutWithResultTestAsync()
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

                var writeStream = (Stream)new MemoryStream();

                ShimAsyncExtensions.GetRequestStreamAsyncWebRequest = request =>
                {
                    return Task.FromResult(writeStream);
                };

                ShimAsyncExtensions.GetResponseAsyncWebRequest = request =>
                {
                    return Task.FromResult((WebResponse)res.Instance);
                };

                var response = await RestRequestHelper.ProcessPutRequest<Person, Person>(String.Format("{0}/{1}", "http://www.example.com", "person"), person);

                Assert.AreEqual(person.Firstname, response.Result.Firstname);
                Assert.AreEqual(person.Lastname, response.Result.Lastname);
                Assert.AreEqual(person.PhoneNumber, response.Result.PhoneNumber);
                Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
            }
        }

        private async Task PatchTestAsync()
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
                    CookiesGet = () => new CookieCollection()
                };

                var writeStream = (Stream)new MemoryStream();

                ShimAsyncExtensions.GetRequestStreamAsyncWebRequest = request =>
                {
                    return Task.FromResult(writeStream);
                };

                ShimAsyncExtensions.GetResponseAsyncWebRequest = request =>
                {
                    return Task.FromResult((WebResponse)res.Instance);
                };

                var response = await RestRequestHelper.ProcessPatchRequest(String.Format("{0}/{1}", "http://www.example.com", "person"), person);

                Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
            }
        }

        private async Task PatchWithResultTestAsync()
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

                var writeStream = (Stream)new MemoryStream();

                ShimAsyncExtensions.GetRequestStreamAsyncWebRequest = request =>
                {
                    return Task.FromResult(writeStream);
                };

                ShimAsyncExtensions.GetResponseAsyncWebRequest = request =>
                {
                    return Task.FromResult((WebResponse)res.Instance);
                };

                var response = await RestRequestHelper.ProcessPatchRequest<Person, Person>(String.Format("{0}/{1}", "http://www.example.com", "person"), person);

                Assert.AreEqual(person.Firstname, response.Result.Firstname);
                Assert.AreEqual(person.Lastname, response.Result.Lastname);
                Assert.AreEqual(person.PhoneNumber, response.Result.PhoneNumber);
                Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
            }
        }
    }
}
