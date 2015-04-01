using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;

namespace TrelloWebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            var json = config.Formatters.JsonFormatter;
            json.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
            config.Formatters.Remove(config.Formatters.XmlFormatter);


            // Web API routes
            config.MapHttpAttributeRoutes();

            /*config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{action}/{id}",
                defaults: new {id = RouteParameter.Optional}
                );*/

            config.Routes.MapHttpRoute(
               name: "GetAllBoards",
               routeTemplate: "api/Boards/",
               defaults: new { controller = "Board", action = "GetAllBoards" }
               );

            config.Routes.MapHttpRoute(
               name: "GetOneBoard",
               routeTemplate: "api/Board/{id}",
               defaults: new { controller = "Board", action = "GetOneBoard", id = RouteParameter.Optional}
               );
        }
    }
}
