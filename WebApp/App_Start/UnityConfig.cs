using Bot.DAC.DataAccess;
using Bot.Service.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity;
using Unity.AspNet.Mvc;
using WebApp.DAC;
using WebApp.Domain.Entities;
using WebApp.Domain.Interfaces.DAC;
using WebApp.Domain.Interfaces.Service;
using WebApp.Service.Services;

namespace WebApp.App_Start
{
    public class UnityConfig
    {
        // Web API configuration and services
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            //Register WebApp Dependencies
            container.RegisterType<IWebAppService, WebAppService>();
            container.RegisterType<IWebAppDAC, WebAppDAC>();

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

            //Register RabbitMQ Dependencies
            container.RegisterType<IRabbitMQService, RabbitMQService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));            
        }
    }
}