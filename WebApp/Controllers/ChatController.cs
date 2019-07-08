using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RabbitMQ.Client;
using RabbitMQ.Util;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using WebApp.Models;
using WebApp.Domain.DataContext.WebApp;
using WebApp.Domain.Interfaces.Service;

namespace WebApp.Controllers
{
    public class ChatController : Controller
    {
        public IWebAppService WebAppService { get; set; }       
        public ChatController(IWebAppService WebAppService)
        {
            this.WebAppService = WebAppService;            
        }

        // GET: Home  
        public ActionResult Index()
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("login");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public JsonResult sendmsg(string message, string sentTo, string sender)
        {
            var flag = WebAppService.SendMessage(message, sentTo, sender);
            //RabbitMQBll obj = new RabbitMQBll();
            //IConnection con = obj.GetConnection();
            //bool flag = obj.send(con, message, sentTo, sender);
            return Json(null);
        }
        [HttpPost]
        public JsonResult receive()
        {
            try
            {
                RabbitMQBll obj = new RabbitMQBll();
                IConnection con = obj.GetConnection();
                string userqueue = Session["username"].ToString();
                string message = obj.receive(con, userqueue);
                return Json(message);
            }
            catch (Exception)
            {
                return null;
            }


        }

        public ActionResult login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult login(FormCollection fc)
        {
            string email = fc["txtemail"].ToString();
            string password = fc["txtpassword"].ToString();
            Users user = WebAppService.Login(email, password);
            if (user.UserID > 0)
            {
                ViewData["status"] = 1;
                ViewData["msg"] = "login Successful...";
                Session["username"] = user.Email;
                Session["userid"] = user.UserID.ToString();
                return RedirectToAction("Index");
            }
            else
            {

                ViewData["status"] = 2;
                ViewData["msg"] = "invalid Email or Password...";
                return View();
            }

        }

        [HttpPost]
        public JsonResult friendlist()
        {
            int id = Convert.ToInt32(Session["userid"].ToString());
            List<Users> users = WebAppService.GetUsers(id);
            List<ListItem> userlist = new List<ListItem>();
            foreach (var item in users)
            {
                userlist.Add(new ListItem
                {
                    Value = item.Email.ToString(),
                    Text = item.Email.ToString()

                });
            }
            return Json(userlist);
        }
    }
}