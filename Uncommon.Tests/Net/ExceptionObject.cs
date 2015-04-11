using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xciles.Uncommon.Tests.Net
{
    public class ExceptionObject
    {
        public string Message { get; set; }
        public string Description { get; set; }
        public EType Type { get; set; }
    }

    public enum EType
    {
        WrongCredentials,
        WrongHeaders, 
        WrongContent
    }
}
