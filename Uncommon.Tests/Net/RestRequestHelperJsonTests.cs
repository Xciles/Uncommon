//using System;
//using System.IO;
//using System.Linq;
//using System.Net;
//using System.Net.Fakes;
//using System.Text;
//using System.Threading.Tasks;
//using Global.Fakes;
//using Microsoft.QualityTools.Testing.Fakes;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Newtonsoft.Json;
//using Xciles.Uncommon.Net;

//namespace Xciles.Uncommon.Tests.Net
//{
//    public class Person
//    {
//        public string Firstname { get; set; }
//        public string Lastname { get; set; }
//        public string SomeString { get; set; }
//        public DateTime DateOfBirth { get; set; }
//        public string PhoneNumber { get; set; }
//    }

//    [TestClass]
//    public class RestRequestHelperJsonTests
//    {
//        [TestMethod]
//        public void GetTest()
//        {
//            GetTestAsync().Wait();
//        }

//        [TestMethod]
//        public void GetRawTest()
//        {
//            GetRawTestAsync().Wait();
//        }

//        [TestMethod]
//        public void GetRawLongTest()
//        {
//            GetRawLongTestAsync().Wait();
//        }

//        [TestMethod]
//        public void PostTest()
//        {
//            PostTestAsync().Wait();
//        }

//        [TestMethod]
//        public void PostWithResultTest()
//        {
//            PostWithResultTestAsync().Wait();
//        }

//        [TestMethod]
//        public void PutTest()
//        {
//            PutTestAsync().Wait();
//        }

//        [TestMethod]
//        public void PutWithResultTest()
//        {
//            PutWithResultTestAsync().Wait();
//        }

//        [TestMethod]
//        public void PatchTest()
//        {
//            PatchTestAsync().Wait();
//        }

//        [TestMethod]
//        public void PatchWithResultTest()
//        {
//            PatchWithResultTestAsync().Wait();
//        }

//        [TestMethod]
//        public void DeleteTest()
//        {
//            DeleteTestAsync().Wait();
//        }

//        [TestMethod]
//        public void DeleteNoContentTest()
//        {
//            DeleteNoContentTestAsync().Wait();
//        }

//        [TestMethod]
//        public void DeleteWithResultTest()
//        {
//            DeleteWithResultTestAsync().Wait();
//        }

//        private async Task GetTestAsync()
//        {
//            using (ShimsContext.Create())
//            {
//                var person = new Person
//                {
//                    DateOfBirth = DateTime.Now.Subtract(new TimeSpan(800, 1, 1, 1)),
//                    Firstname = "First",
//                    Lastname = "Person",
//                    PhoneNumber = "0123456789",
//                    SomeString = "This is just a string"
//                };


//                ShimHttpWebResponse res = new ShimHttpWebResponse
//                {
//                    StatusCodeGet = () => HttpStatusCode.OK,
//                    GetResponseStream = () =>
//                    {
//                        var bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(person));

//                        var stream = new MemoryStream(bytes);

//                        return stream;
//                    },
//                    CookiesGet = () => new CookieCollection()
//                };

//                ShimAsyncExtensions.GetResponseAsyncWebRequest = request =>
//                {
//                    return Task.FromResult((WebResponse)res.Instance);
//                };

//                var response = await RestRequestHelper.ProcessGetRequestAsync<Person>(String.Format("{0}/{1}", "http://www.example.com", "person"));

//                Assert.AreEqual(person.Firstname, response.Result.Firstname);
//                Assert.AreEqual(person.Lastname, response.Result.Lastname);
//                Assert.AreEqual(person.PhoneNumber, response.Result.PhoneNumber);
//                Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
//            }
//        }

//        private async Task GetRawTestAsync()
//        {
//            using (ShimsContext.Create())
//            {
//                var aString = "Short String just for testing!";
//                var stringAsBytes = Encoding.UTF8.GetBytes(aString);

//                ShimHttpWebResponse res = new ShimHttpWebResponse
//                {
//                    StatusCodeGet = () => HttpStatusCode.OK,
//                    GetResponseStream = () =>
//                    {
//                        var stream = new MemoryStream(stringAsBytes);

//                        return stream;
//                    },
//                    CookiesGet = () => new CookieCollection()
//                };

//                ShimAsyncExtensions.GetResponseAsyncWebRequest = request =>
//                {
//                    return Task.FromResult((WebResponse)res.Instance);
//                };

//                var response = await RestRequestHelper.ProcessRawGetRequestAsync(String.Format("{0}/{1}", "http://www.example.com", "personAsBytes"));

//                var responseString = Encoding.UTF8.GetString(response.Result);

//                Assert.IsTrue(stringAsBytes.Count() == response.Result.Count());
//                Assert.AreEqual(aString, responseString);
//                Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
//            }
//        }


//        private async Task GetRawLongTestAsync()
//        {
//            using (ShimsContext.Create())
//            {
//                var aString = "Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! " +
//                              "Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! Long String just for testing! ";
//                var stringAsBytes = Encoding.UTF8.GetBytes(aString);

//                ShimHttpWebResponse res = new ShimHttpWebResponse
//                {
//                    StatusCodeGet = () => HttpStatusCode.OK,
//                    GetResponseStream = () =>
//                    {
//                        var stream = new MemoryStream(stringAsBytes);

//                        return stream;
//                    },
//                    CookiesGet = () => new CookieCollection()
//                };

//                ShimAsyncExtensions.GetResponseAsyncWebRequest = request =>
//                {
//                    return Task.FromResult((WebResponse)res.Instance);
//                };

//                var response = await RestRequestHelper.ProcessRawGetRequestAsync(String.Format("{0}/{1}", "http://www.example.com", "personAsBytes"));

//                var responseString = Encoding.UTF8.GetString(response.Result);

//                Assert.IsTrue(stringAsBytes.Count() == response.Result.Count());
//                Assert.AreEqual(aString, responseString);
//                Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
//            }
//        }

//        private async Task PostTestAsync()
//        {
//            using (ShimsContext.Create())
//            {
//                var person = new Person
//                {
//                    DateOfBirth = DateTime.Now.Subtract(new TimeSpan(800, 1, 1, 1)),
//                    Firstname = "First",
//                    Lastname = "Person",
//                    PhoneNumber = "0123456789",
//                    SomeString = "This is just a string"
//                };

//                ShimHttpWebResponse res = new ShimHttpWebResponse
//                {
//                    StatusCodeGet = () => HttpStatusCode.OK,
//                    CookiesGet = () => new CookieCollection()
//                };

//                var writeStream = (Stream)new MemoryStream();

//                ShimAsyncExtensions.GetRequestStreamAsyncWebRequest = request =>
//                {
//                    return Task.FromResult(writeStream);
//                };

//                ShimAsyncExtensions.GetResponseAsyncWebRequest = request =>
//                {
//                    return Task.FromResult((WebResponse)res.Instance);
//                };

//                var response = await RestRequestHelper.ProcessPostRequestAsync(String.Format("{0}/{1}", "http://www.example.com", "person"), person);

//                Assert.IsFalse(writeStream.CanWrite); // Since it should be closed...
//                Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
//            }
//        }

//        private async Task PostWithResultTestAsync()
//        {
//            using (ShimsContext.Create())
//            {
//                var person = new Person
//                {
//                    DateOfBirth = DateTime.Now.Subtract(new TimeSpan(800, 1, 1, 1)),
//                    Firstname = "First",
//                    Lastname = "Person",
//                    PhoneNumber = "0123456789",
//                    SomeString = "This is just a string"
//                };

//                ShimHttpWebResponse res = new ShimHttpWebResponse
//                {
//                    StatusCodeGet = () => HttpStatusCode.OK,
//                    GetResponseStream = () =>
//                    {
//                        var bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(person));

//                        var stream = new MemoryStream(bytes);

//                        return stream;
//                    },
//                    CookiesGet = () => new CookieCollection()
//                };

//                var writeStream = (Stream)new MemoryStream();

//                ShimAsyncExtensions.GetRequestStreamAsyncWebRequest = request =>
//                {
//                    return Task.FromResult(writeStream);
//                };

//                ShimAsyncExtensions.GetResponseAsyncWebRequest = request =>
//                {
//                    return Task.FromResult((WebResponse)res.Instance);
//                };

//                var response = await RestRequestHelper.ProcessPostRequestAsync<Person, Person>(String.Format("{0}/{1}", "http://www.example.com", "person"), person);

//                Assert.IsFalse(writeStream.CanWrite); // Since it should be closed...
//                Assert.AreEqual(person.Firstname, response.Result.Firstname);
//                Assert.AreEqual(person.Lastname, response.Result.Lastname);
//                Assert.AreEqual(person.PhoneNumber, response.Result.PhoneNumber);
//                Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
//            }
//        }

//        private async Task PutTestAsync()
//        {
//            using (ShimsContext.Create())
//            {
//                var person = new Person
//                {
//                    DateOfBirth = DateTime.Now.Subtract(new TimeSpan(800, 1, 1, 1)),
//                    Firstname = "First",
//                    Lastname = "Person",
//                    PhoneNumber = "0123456789",
//                    SomeString = "This is just a string"
//                };

//                ShimHttpWebResponse res = new ShimHttpWebResponse
//                {
//                    StatusCodeGet = () => HttpStatusCode.OK,
//                    CookiesGet = () => new CookieCollection()
//                };

//                var writeStream = (Stream)new MemoryStream();

//                ShimAsyncExtensions.GetRequestStreamAsyncWebRequest = request =>
//                {
//                    return Task.FromResult(writeStream);
//                };

//                ShimAsyncExtensions.GetResponseAsyncWebRequest = request =>
//                {
//                    return Task.FromResult((WebResponse)res.Instance);
//                };

//                var response = await RestRequestHelper.ProcessPutRequestAsync(String.Format("{0}/{1}", "http://www.example.com", "person"), person);

//                Assert.IsFalse(writeStream.CanWrite); // Since it should be closed...
//                Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
//            }
//        }

//        private async Task PutWithResultTestAsync()
//        {
//            using (ShimsContext.Create())
//            {
//                var person = new Person
//                {
//                    DateOfBirth = DateTime.Now.Subtract(new TimeSpan(800, 1, 1, 1)),
//                    Firstname = "First",
//                    Lastname = "Person",
//                    PhoneNumber = "0123456789",
//                    SomeString = "This is just a string"
//                };

//                ShimHttpWebResponse res = new ShimHttpWebResponse
//                {
//                    StatusCodeGet = () => HttpStatusCode.OK,
//                    GetResponseStream = () =>
//                    {
//                        var bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(person));

//                        var stream = new MemoryStream(bytes);

//                        return stream;
//                    },
//                    CookiesGet = () => new CookieCollection()
//                };

//                var writeStream = (Stream)new MemoryStream();

//                ShimAsyncExtensions.GetRequestStreamAsyncWebRequest = request =>
//                {
//                    return Task.FromResult(writeStream);
//                };

//                ShimAsyncExtensions.GetResponseAsyncWebRequest = request =>
//                {
//                    return Task.FromResult((WebResponse)res.Instance);
//                };

//                var response = await RestRequestHelper.ProcessPutRequestAsync<Person, Person>(String.Format("{0}/{1}", "http://www.example.com", "person"), person);

//                Assert.IsFalse(writeStream.CanWrite); // Since it should be closed...
//                Assert.AreEqual(person.Firstname, response.Result.Firstname);
//                Assert.AreEqual(person.Lastname, response.Result.Lastname);
//                Assert.AreEqual(person.PhoneNumber, response.Result.PhoneNumber);
//                Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
//            }
//        }

//        private async Task PatchTestAsync()
//        {
//            using (ShimsContext.Create())
//            {
//                var person = new Person
//                {
//                    DateOfBirth = DateTime.Now.Subtract(new TimeSpan(800, 1, 1, 1)),
//                    Firstname = "First",
//                    Lastname = "Person",
//                    PhoneNumber = "0123456789",
//                    SomeString = "This is just a string"
//                };

//                ShimHttpWebResponse res = new ShimHttpWebResponse
//                {
//                    StatusCodeGet = () => HttpStatusCode.OK,
//                    CookiesGet = () => new CookieCollection()
//                };

//                var writeStream = (Stream)new MemoryStream();

//                ShimAsyncExtensions.GetRequestStreamAsyncWebRequest = request =>
//                {
//                    return Task.FromResult(writeStream);
//                };

//                ShimAsyncExtensions.GetResponseAsyncWebRequest = request =>
//                {
//                    return Task.FromResult((WebResponse)res.Instance);
//                };

//                var response = await RestRequestHelper.ProcessPatchRequestAsync(String.Format("{0}/{1}", "http://www.example.com", "person"), person);

//                Assert.IsFalse(writeStream.CanWrite); // Since it should be closed...
//                Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
//            }
//        }

//        private async Task PatchWithResultTestAsync()
//        {
//            using (ShimsContext.Create())
//            {
//                var person = new Person
//                {
//                    DateOfBirth = DateTime.Now.Subtract(new TimeSpan(800, 1, 1, 1)),
//                    Firstname = "First",
//                    Lastname = "Person",
//                    PhoneNumber = "0123456789",
//                    SomeString = "This is just a string"
//                };

//                ShimHttpWebResponse res = new ShimHttpWebResponse
//                {
//                    StatusCodeGet = () => HttpStatusCode.OK,
//                    GetResponseStream = () =>
//                    {
//                        var bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(person));

//                        var stream = new MemoryStream(bytes);

//                        return stream;
//                    },
//                    CookiesGet = () => new CookieCollection()
//                };

//                var writeStream = (Stream)new MemoryStream();

//                ShimAsyncExtensions.GetRequestStreamAsyncWebRequest = request =>
//                {
//                    return Task.FromResult(writeStream);
//                };

//                ShimAsyncExtensions.GetResponseAsyncWebRequest = request =>
//                {
//                    return Task.FromResult((WebResponse)res.Instance);
//                };

//                var response = await RestRequestHelper.ProcessPatchRequestAsync<Person, Person>(String.Format("{0}/{1}", "http://www.example.com", "person"), person);

//                Assert.IsFalse(writeStream.CanWrite); // Since it should be closed...
//                Assert.AreEqual(person.Firstname, response.Result.Firstname);
//                Assert.AreEqual(person.Lastname, response.Result.Lastname);
//                Assert.AreEqual(person.PhoneNumber, response.Result.PhoneNumber);
//                Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
//            }
//        }

//        private async Task DeleteTestAsync()
//        {
//            using (ShimsContext.Create())
//            {
//                var person = new Person
//                {
//                    DateOfBirth = DateTime.Now.Subtract(new TimeSpan(800, 1, 1, 1)),
//                    Firstname = "First",
//                    Lastname = "Person",
//                    PhoneNumber = "0123456789",
//                    SomeString = "This is just a string"
//                };

//                ShimHttpWebResponse res = new ShimHttpWebResponse
//                {
//                    StatusCodeGet = () => HttpStatusCode.OK,
//                    CookiesGet = () => new CookieCollection()
//                };

//                var writeStream = (Stream)new MemoryStream();

//                ShimAsyncExtensions.GetRequestStreamAsyncWebRequest = request =>
//                {
//                    return Task.FromResult(writeStream);
//                };

//                ShimAsyncExtensions.GetResponseAsyncWebRequest = request =>
//                {
//                    return Task.FromResult((WebResponse)res.Instance);
//                };

//                var response = await RestRequestHelper.ProcessDeleteRequestAsync(String.Format("{0}/{1}", "http://www.example.com", "person"), person);

//                Assert.IsFalse(writeStream.CanWrite); // Since it should be closed...
//                Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
//            }
//        }

//        private async Task DeleteNoContentTestAsync()
//        {
//            using (ShimsContext.Create())
//            {
//                ShimHttpWebResponse res = new ShimHttpWebResponse
//                {
//                    StatusCodeGet = () => HttpStatusCode.OK,
//                    CookiesGet = () => new CookieCollection()
//                };

//                var writeStream = (Stream)new MemoryStream();

//                ShimAsyncExtensions.GetRequestStreamAsyncWebRequest = request =>
//                {
//                    return Task.FromResult(writeStream);
//                };

//                ShimAsyncExtensions.GetResponseAsyncWebRequest = request =>
//                {
//                    return Task.FromResult((WebResponse)res.Instance);
//                };

//                var response = await RestRequestHelper.ProcessDeleteRequestAsync(String.Format("{0}/{1}", "http://www.example.com", "person/1"));

//                Assert.IsTrue(writeStream.CanWrite); // Didn't write anything to it, shouldn't be closed...
//                Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
//            }
//        }

//        private async Task DeleteWithResultTestAsync()
//        {
//            using (ShimsContext.Create())
//            {
//                var person = new Person
//                {
//                    DateOfBirth = DateTime.Now.Subtract(new TimeSpan(800, 1, 1, 1)),
//                    Firstname = "First",
//                    Lastname = "Person",
//                    PhoneNumber = "0123456789",
//                    SomeString = "This is just a string"
//                };

//                ShimHttpWebResponse res = new ShimHttpWebResponse
//                {
//                    StatusCodeGet = () => HttpStatusCode.OK,
//                    GetResponseStream = () =>
//                    {
//                        var bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(person));

//                        var stream = new MemoryStream(bytes);

//                        return stream;
//                    },
//                    CookiesGet = () => new CookieCollection()
//                };

//                var writeStream = (Stream)new MemoryStream();

//                ShimAsyncExtensions.GetRequestStreamAsyncWebRequest = request =>
//                {
//                    return Task.FromResult(writeStream);
//                };

//                ShimAsyncExtensions.GetResponseAsyncWebRequest = request =>
//                {
//                    return Task.FromResult((WebResponse)res.Instance);
//                };

//                var response = await RestRequestHelper.ProcessDeleteRequestAsync<Person, Person>(String.Format("{0}/{1}", "http://www.example.com", "person"), person);

//                Assert.IsFalse(writeStream.CanWrite); // Since it should be closed...
//                Assert.AreEqual(person.Firstname, response.Result.Firstname);
//                Assert.AreEqual(person.Lastname, response.Result.Lastname);
//                Assert.AreEqual(person.PhoneNumber, response.Result.PhoneNumber);
//                Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
//            }
//        }
//    }
//}
