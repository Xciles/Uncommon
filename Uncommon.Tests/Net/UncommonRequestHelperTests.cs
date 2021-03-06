﻿using System;
using System.Net;
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
    public class UncommonRequestHelperTests
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
        public void ProcessGetRequestTest()
        {
            ProcessGetRequestTestAsync().Wait();
        }

        public async Task ProcessGetRequestTestAsync()
        {
            //await UncommonRequestHelper.ProcessGetRequestAsync()
            
            using (ShimsContext.Create())
            {
                ShimHttpClient.AllInstances.SendAsyncHttpRequestMessageCancellationToken = (client, message, arg3) =>
                {
                    return Task.FromResult(new HttpResponseMessage()
                    {
                        Content = new StringContent(JsonConvert.SerializeObject(_person)),
                        StatusCode = HttpStatusCode.OK
                    });
                };

                var result = await UncommonRequestHelper.ProcessGetRequestAsync<Person>("http://www.xciles.com/");

                Assert.AreEqual(_person.Firstname, result.Result.Firstname);
                Assert.AreEqual(_person.Lastname, result.Result.Lastname);
                Assert.AreEqual(_person.PhoneNumber, result.Result.PhoneNumber);
            }
        }
    }
}
