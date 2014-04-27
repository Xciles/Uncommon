using System;
using System.Collections.Generic;
using System.Web.Http;
using Common.Web.Tester.Domain;

namespace Common.Web.Tester.Controllers
{
    [RoutePrefix("api/person")]
    public class PersonController : ApiController
    {
        public IList<Person> GetPersons()
        {
            return new List<Person>()
            {
                new Person()
                {
                    DateOfBirth = DateTime.Now.Subtract(new TimeSpan(800,1,1,1)),
                    Firstname = "First",
                    Lastname = "Person",
                    PhoneNumber = "0123456789",
                    SomeString = "This is just a string"
                }, 
                new Person()
                {
                    DateOfBirth = DateTime.Now.Subtract(new TimeSpan(1800,1,1,1)),
                    Firstname = "Second",
                    Lastname = "Person",
                    PhoneNumber = "0123456789",
                    SomeString = "This is just a string"
                },
                new Person()
                {
                    DateOfBirth = DateTime.Now.Subtract(new TimeSpan(2800,1,1,1)),
                    Firstname = "Thrid",
                    Lastname = "Person",
                    PhoneNumber = "0123456789",
                    SomeString = "This is just a string"
                }
            };
        }

        public Person GetPerson(int id)
        {
            return new Person()
            {
                DateOfBirth = DateTime.Now.Subtract(new TimeSpan(800, 1, 1, 1)),
                Firstname = "First",
                Lastname = "Person",
                PhoneNumber = "0123456789",
                SomeString = "This is just a string"
            };
        }
    }
}