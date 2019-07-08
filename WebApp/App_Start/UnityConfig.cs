using Bot.DAC.DataAccess;
using Bot.Service.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity;
using Unity.AspNet.Mvc;
using WebApp.DAC;
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

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));            
        }
    }
}