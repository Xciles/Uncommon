using System.Reflection;
using System.Web.Http;
using Xciles.Common.Web.Tester.Domain;

namespace Xciles.Common.Web.Tester.Utils
{
    public class HttpErrorHelper
    {
        public static HttpError Create(ServiceExceptionResult exception)
        {
            var properties = exception.GetType().GetProperties(BindingFlags.Instance
                                                               | BindingFlags.Public
                                                               | BindingFlags.DeclaredOnly);
            var error = new HttpError();
            foreach (var propertyInfo in properties)
            {
                error.Add(propertyInfo.Name, propertyInfo.GetValue(exception, null));
            }
            return error;
        }
    }
}