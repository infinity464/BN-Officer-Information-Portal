using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace Infinity.Bnois.Api.Web
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'WebApiConfig'
    public static class WebApiConfig
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'WebApiConfig'
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'WebApiConfig.Register(HttpConfiguration)'
        public static void Register(HttpConfiguration config)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'WebApiConfig.Register(HttpConfiguration)'
        {
            // Web API configuration and services
            //config.EnableCors();
            //var cors = new EnableCorsAttribute("*", "*", "*");
            //config.EnableCors(cors);


            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling
               = ReferenceLoopHandling.Serialize;

            config.Formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling
                 = PreserveReferencesHandling.Objects;

            //config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("text/html"));

            var formatters = GlobalConfiguration.Configuration.Formatters;
            var jsonFormatter = formatters.JsonFormatter;
            var settings = jsonFormatter.SerializerSettings;
            settings.Formatting = Formatting.Indented;
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            config.Formatters.Remove(config.Formatters.XmlFormatter);
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));
            //DateTimeZone 
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Local;
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}