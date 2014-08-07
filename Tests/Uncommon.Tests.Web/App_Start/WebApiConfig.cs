using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.WebApi.MessageHandlers.Compression;
using Microsoft.AspNet.WebApi.MessageHandlers.Compression.Compressors;

namespace Uncommon.Tests.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //var serverCompression = new ServerCompressionHandler(2048, new GZipCompressor(), new DeflateCompressor());
            var serverCompression = new ServerCompressionHandler(new GZipCompressor(), new DeflateCompressor());
            GlobalConfiguration.Configuration.MessageHandlers.Insert(0, serverCompression);
        }
    }
}
