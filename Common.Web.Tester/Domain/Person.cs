using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Common.Web.Tester.Domain
{
    public class Person
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string SomeString { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
    }
}