using System;
using System.Net;
using System.Net.Fakes;
using System.Net.Http;
using System.Net.Http.Fakes;
using System.Threading.Tasks;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Xciles.Uncommon.Net;

namespace Xciles.Uncommon.Tests.Net
{
    [TestClass]
    public class UncommonRequestHelperExceptionTests
    {
        private readonly Person _person = new Person
        {
            DateOfBirth = DateTime.Now.Subtract(new TimeSpan(800, 1, 1, 1)),
            Firstname = "First",
            Lastname = "Person",
            PhoneNumber = "0123456789",
            SomeString = "This is just a string"
        };

        [TestMethod]
        public void ProcessGetRequestWebExceptionTest()
        {
            ProcessGetRequestWebExceptionTestAsync().Wait();
        }

        private async Task ProcessGetRequestWebExceptionTestAsync()
        {
            var exceptionObject = new ExceptionObject()
            {
                Description = "This is a test Exception Description",
                Message = "This is a test Exception Message",
                Type = EType.WrongHeaders
            };

            using (ShimsContext.Create())
            {

                ShimHttpClient.AllInstances.SendAsyncHttpRequestMessageCancellationToken = (client, message, arg3) =>
                {
                    return Task.FromResult(new HttpResponseMessage()
                    {
                        Content = new StringContent(JsonConvert.SerializeObject(exceptionObject)),
                        StatusCode = HttpStatusCode.BadRequest
                    });
                };

                try
                {
                    var result = await UncommonRequestHelper.ProcessGetRequestAsync<Person>("http://www.xciles.com/");
                    Assert.Fail("Should not be able to be here...");
                }
                catch (UncommonRequestException ex)
                {
                    Assert.IsTrue(ex.RequestExceptionStatus == EUncommonRequestExceptionStatus.ServiceError);
                    Assert.IsTrue(ex.ServiceExceptionResult == null);
                    Assert.IsTrue(ex.StatusCode == HttpStatusCode.BadRequest);

                    var responseResult = ex.ConvertExceptionResponseToObject<ExceptionObject>();
                    Assert.IsTrue(responseResult != null);
                    Assert.IsTrue(responseResult.Description == exceptionObject.Description);
                    Assert.IsTrue(responseResult.Message == exceptionObject.Message);
                    Assert.IsTrue(responseResult.Type == exceptionObject.Type);
                }
            }
        }



        [TestMethod]
        public void ProcessGetRequestHttpRequestExceptionTest()
        {
            ProcessGetRequestHttpRequestExceptionTestAsync().Wait();
        }

        private async Task ProcessGetRequestHttpRequestExceptionTestAsync()
        {
            var exceptionObject = new ExceptionObject()
            {
                Description = "This is a test Exception Description",
                Message = "This is a test Exception Message",
                Type = EType.WrongHeaders
            };

            using (ShimsContext.Create())
            {

                ShimHttpClient.AllInstances.SendAsyncHttpRequestMessageCancellationToken = (client, message, arg3) =>
                {
                    //var webEx = new WebException("", WebExceptionStatus.UnknownError, )
                    throw new HttpRequestException();
                };

                ShimHttpWebResponse.AllInstances.ResponseStreamGet = (response) =>
                {
                    return ShimsContext.ExecuteWithoutShims(() => response.GetResponseStream());
                };

                try
                {
                    var result = await UncommonRequestHelper.ProcessGetRequestAsync<Person>("http://www.xciles.com/");
                    Assert.Fail("Should not be able to be here...");
                }
                catch (UncommonRequestException ex)
                {
                    Assert.IsTrue(ex.RequestExceptionStatus == EUncommonRequestExceptionStatus.ServiceError);
                    Assert.IsTrue(ex.ServiceExceptionResult == null);
                    Assert.IsTrue(ex.StatusCode == HttpStatusCode.BadRequest);

                    //var responseResult = ex.ConvertExceptionResponseToObject<ExceptionObject>();
                    //Assert.IsTrue(responseResult != null);
                    //Assert.IsTrue(responseResult.Description == exceptionObject.Description);
                    //Assert.IsTrue(responseResult.Message == exceptionObject.Message);
                    //Assert.IsTrue(responseResult.Type == exceptionObject.Type);
                }
            }
        }
    }
}
