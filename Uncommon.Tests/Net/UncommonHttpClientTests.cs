using System;
using System.Net.Http.Fakes;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
    public class UncommonHttpClientTests
    {
        [TestMethod]
        public void TestTest()
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

                ShimHttpClient client = new ShimHttpClient();


            }
        }
    }
}
