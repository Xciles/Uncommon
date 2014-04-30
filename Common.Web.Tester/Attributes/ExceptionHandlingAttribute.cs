using System.Net;
using System.Net.Http;
using System.Security.Authentication;
using System.Web.Http;
using System.Web.Http.Filters;
using Xciles.Common.Web.Tester.Domain;
using Xciles.Common.Web.Tester.Utils;

namespace Xciles.Common.Web.Tester.Attributes
{
    public class ExceptionHandlingAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            var statusCode = HttpStatusCode.InternalServerError;

            var result = context.Exception as ServiceExceptionResult;
            if (result != null)
            {
                statusCode = HttpStatusCode.BadRequest;
                throw new HttpResponseException(context.Request.CreateErrorResponse(statusCode, HttpErrorHelper.Create(result)));
            }

            throw new HttpResponseException(context.Request.CreateErrorResponse(statusCode, context.Exception.Message));
        }
    }
}