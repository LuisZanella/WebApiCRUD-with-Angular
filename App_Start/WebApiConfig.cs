using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace MiWebObj
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "User",
                routeTemplate: "api/User/{id}",
                defaults: new {controller = "User", id = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
                name: "OtherUser",
                routeTemplate: "api/OtherUser/{id}",
                defaults: new { controller = "OtherUser", id = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
                name:"Diary",
                routeTemplate:"api/Diary/{id}",
                defaults: new { controller = "Diary", id = RouteParameter.Optional}
                );
        }
    }
}
