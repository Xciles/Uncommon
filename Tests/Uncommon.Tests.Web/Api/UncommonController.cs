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
                    AString = "This is a test string",
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
                    AString = "This is a test string",
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
                    AString = "This is a test string",
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
                    AString = "This is a test string",
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
                    AString = "This is a test string",
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
                    AString = "This is a test string",
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
                    AString = "This is a test string",
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
                    AString = "This is a test string",
                    AnInt = 1234
                }
            });
        }
    }
}
