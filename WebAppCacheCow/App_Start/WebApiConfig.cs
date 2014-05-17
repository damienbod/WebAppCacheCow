using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using CacheCow.Common;
using CacheCow.Server;
using WebAppCacheCow.Controllers;

namespace WebAppCacheCow
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

            // In memory
            //var cacheCowCacheHandler = new CachingHandler(config);
            //config.MessageHandlers.Add(cacheCowCacheHandler);

            //Configure HTTP Caching using Elasticsearch
            IEntityTagStore eTagStore = new CacheCow.Server.EntityTagStore.Elasticsearch.NestEntityTagStore();
            var cacheCowCacheHandler = new CachingHandler(config, eTagStore)
            {
                AddLastModifiedHeader = false
            };
            config.MessageHandlers.Add(cacheCowCacheHandler);

        }
    }
}
