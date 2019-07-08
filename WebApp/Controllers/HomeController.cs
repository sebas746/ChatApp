using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Domain.Interfaces.Service;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        public IWebAppService WebAppService { get; set; }

        public HomeController(IWebAppService WebAppService)
        {
            this.WebAppService = WebAppService;
        }

        public ActionResult Index()
        {
            return RedirectToAction("../Chat");
        }
    }
}
