using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using System.Web.Http.Cors;

namespace T5PWebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));
            var corsAttr = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(corsAttr);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "ParametersApi",
                routeTemplate: "api/parameters/{type}/{code}",
                defaults: new { controller="Parameter", type = RouteParameter.Optional, code=RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
                name: "CitiesApi",
                routeTemplate: "api/city/{id}",
                defaults: new { controller = "Parameter", id = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
                name: "ProvinciesApi",
                routeTemplate: "api/province/{id}",
                defaults: new { controller = "Parameter", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "HRStatusesApi",
                routeTemplate: "api/hrstatuses/{id}",
                defaults: new { controller = "Parameter", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "InFromApi",
                routeTemplate: "api/infrom/{id}",
                defaults: new { controller = "Parameter", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "empleavedetail_ongoing",
                routeTemplate:
                    "api/empleavedetail_ongoing/{forminstanceid}/{rowindex}/{empid}/{leavecode}/{leavefromdate}/{leavefromtime}",
                defaults: new
                {
                    controller = "Empleavedetail_ongoing",
                    forminstanceid = RouteParameter.Optional,
                    rowindex = RouteParameter.Optional,
                    empid = RouteParameter.Optional,
                    leavecode = RouteParameter.Optional,
                    leavefromdate = RouteParameter.Optional,
                    leavefromtime = RouteParameter.Optional
                });

            config.Routes.MapHttpRoute(
                name: "SalutationFromApi",
                routeTemplate: "api/salutation/{id}",
                defaults: new { controller = "Parameter", id = RouteParameter.Optional }
            );

            //config.Routes.MapHttpRoute(
            //    name: "CancelLeaveApplication",
            //    routeTemplate: "api/empleavedata/{autoid}/cancel",
            //    defaults: new { controller = "Empleavedata" }
            //); 

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );        
        }
    }
}
