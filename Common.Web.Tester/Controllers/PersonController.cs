using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web.Http;
using Xciles.Common.Web.Tester.Attributes;
using Xciles.Common.Web.Tester.Domain;

namespace Xciles.Common.Web.Tester.Controllers
{
    [RoutePrefix("api")]
    public class PersonController : ApiController
    {
        [HttpGet]
        [Route("person")]
        public IList<Person> GetPersons()
        {
            return new List<Person>
            {
                new Person
                {
                    DateOfBirth = DateTime.Now.Subtract(new TimeSpan(800,1,1,1)),
                    Firstname = "First",
                    Lastname = "Person",
                    PhoneNumber = "0123456789",
                    SomeString = "This is just a string"
                }, 
                new Person
                {
                    DateOfBirth = DateTime.Now.Subtract(new TimeSpan(1800,1,1,1)),
                    Firstname = "Second",
                    Lastname = "Person",
                    PhoneNumber = "0123456789",
                    SomeString = "This is just a string"
                },
                new Person
                {
                    DateOfBirth = DateTime.Now.Subtract(new TimeSpan(2800,1,1,1)),
                    Firstname = "Thrid",
                    Lastname = "Person",
                    PhoneNumber = "0123456789",
                    SomeString = "This is just a string"
                }
            };
        }

        [HttpGet]
        [Route("person/{id}")]
        public Person GetPerson(int id)
        {
            return new Person
            {
                DateOfBirth = DateTime.Now.Subtract(new TimeSpan(800, 1, 1, 1)),
                Firstname = "First",
                Lastname = "Person",
                PhoneNumber = "0123456789",
                SomeString = "This is just a string"
            };
        }

        [HttpGet]
        [Route("personbytes/{id}")]
        public HttpResponseMessage GetPersonAsByteArray(int id)
        {
            var person = new Person
            {
                DateOfBirth = DateTime.Now.Subtract(new TimeSpan(800, 1, 1, 1)),
                Firstname = "First",
                Lastname = "Person",
                PhoneNumber = "0123456789",
                SomeString = "This is just a string"
            };

            var bf = new BinaryFormatter();
            var ms = new MemoryStream();
            bf.Serialize(ms, person);
            ms.Position = 0;

            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new StreamContent(ms);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

            return result;
        }

        [HttpPost]
        [Route("badrequest")]
        [ExceptionHandling]
        public HttpRequestMessage PostBadRequest(Person person)
        {
            throw new ServiceExceptionResult
            {
                Message = "Well something went wrong!...",
                MessageDetail = "Something Wrong!",
                ExceptionResultTypeValue = "PersonError",
                HttpStatusCode = HttpStatusCode.BadRequest,
                StackTrace = "No"
            };
        }

        [HttpPost]
        [Route("person")]
        public HttpResponseMessage PostPerson(Person person)
        {
            // And we do nothing! But return Created (201)
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
            return response;
        }

        [HttpPost]
        [Route("personwr")]
        public Person PostPersonWithResult(Person person)
        {
            return person;
        }

        [HttpPut]
        [Route("person")]
        public HttpResponseMessage PutPerson(Person person)
        {
            // And we do nothing! But return Created (201)
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
            return response;
        }

        [HttpPut]
        [Route("personwr")]
        public Person PutPersonWithResult(Person person)
        {
            return person;
        }

        [HttpPatch]
        [Route("person")]
        public HttpResponseMessage PatchPerson(Person person)
        {
            // And we do nothing! But return Created (201)
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
            return response;
        }

        [HttpPatch]
        [Route("personwr")]
        public Person PatchPersonWithResult(Person person)
        {
            return person;
        }

        [HttpDelete]
        [Route("person")]
        public HttpResponseMessage DeletePerson(Person person)
        {
            // And we do nothing! But return OK (200) !
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }
    }
}