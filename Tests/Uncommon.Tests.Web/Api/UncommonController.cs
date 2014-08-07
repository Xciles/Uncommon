using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Uncommon.Tests.Web.Domain;

namespace Uncommon.Tests.Web.Api
{
    [RoutePrefix("api/uncommon")]
    public class UncommonController : ApiController
    {
        [Route("testdata")]
        public async Task<UncommonData> GetData()
        {
            return await Task.FromResult(new UncommonData()
            {
                ADateTime = DateTime.Now,
                AString = "This is a test string",
                AnInt = 1234
            });
        }

        [Route("testdatas")]
        public async Task<IList<UncommonData>> GetDatas()
        {
            return await Task.FromResult(new List<UncommonData>()
            {
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string1",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string2",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string3",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string4",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string5",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string6",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string7",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string8",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string9",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string10",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string11",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string12",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string13",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string14",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string15",
                    AnInt = 1234
                }
            });
        }

        [Route("testdatasbig")]
        public async Task<IList<UncommonData>> GetDatasBig()
        {
            return await Task.FromResult(new List<UncommonData>()
            {
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string1",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string2",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string3",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string4",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string5",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string6",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string7",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string8",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string9",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string10",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string11",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string12",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string13",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string14",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string15",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string1",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string2",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string3",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string4",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string5",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string6",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string7",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string8",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string9",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string10",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string11",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string12",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string13",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string14",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string15",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string1",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string2",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string3",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string4",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string5",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string6",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string7",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string8",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string9",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string10",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string11",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string12",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string13",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string14",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string15",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string1",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string2",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string3",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string4",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string5",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string6",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string7",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string8",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string9",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string10",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string11",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string12",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string13",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string14",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string15",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string1",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string2",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string3",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string4",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string5",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string6",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string7",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string8",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string9",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string10",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string11",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string12",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string13",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string14",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string15",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string1",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string2",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string3",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string4",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string5",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string6",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string7",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string8",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string9",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string10",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string11",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string12",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string13",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string14",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string15",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string1",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string2",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string3",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string4",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string5",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string6",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string7",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string8",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string9",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string10",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string11",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string12",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string13",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string14",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string15",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string1",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string2",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string3",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string4",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string5",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string6",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string7",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string8",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string9",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string10",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string11",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string12",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string13",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string14",
                    AnInt = 1234
                },
                new UncommonData()
                {
                    ADateTime = DateTime.Now,
                    AString = "This is a test string15",
                    AnInt = 1234
                }
            });
        }
    }
}
