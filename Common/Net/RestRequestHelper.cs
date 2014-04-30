using System.Threading.Tasks;
using Xciles.Common.Security;

namespace Xciles.Common.Net
{
    public class RestRequestHelper
    {
        private static ISecurityContext _sc;


        

        public static async Task<RestResponse<T>> ProcessGetRequest<T>(string restRequestUri, object state, RestRequestOptions options = null)
        {
            var restRequest = new RestRequest
            {
                State = state,
                Options = options ?? new RestRequestOptions(),
                RestRequestUri = restRequestUri,
                RestMethod = ERestMethod.GET
            };

            return await restRequest.ProcessRequest<T>();
        }
    }
}
