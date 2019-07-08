using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Unity;
using Unity.Lifetime;
using WebApp.App_Start;
using WebApp.DAC;
using WebApp.Domain.Interfaces.DAC;
using WebApp.Domain.Interfaces.Service;
using WebApp.Service.Services;
using System.Web.Http.Cors;
using Bot.Service.Services;
using Bot.DAC.DataAccess;
using WebApp.Domain.Entities;
using System.Configuration;

namespace WebApp
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Configuración y servicios de API web
            config.EnableCors(new EnableCorsAttribute("*", "*", "*"));

            var container = new UnityContainer();

            //Register Bot Dependencies
            container.RegisterType<IBotService, BotService>();
            container.RegisterType<IBotDAC, BotDAC>();
            
            //Connection string
            ConnectionStrings connectionStrings = new ConnectionStrings()
            {
                ChatDB = ConfigurationManager.ConnectionStrings["WebAppDataContext"].ToString(),
                BotApiUrl = ConfigurationManager.AppSettings["stockApiUrl"].ToString()
            };

            container.RegisterInstance<ConnectionStrings>(connectionStrings);

            config.DependencyResolver = new UnityResolver(container);

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
