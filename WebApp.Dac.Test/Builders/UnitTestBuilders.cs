using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Domain.DataContext.WebApp;
using WebApp.Domain.Entities;

namespace WebApp.Dac.Test.Builders
{
    public static class UnitTestBuilders
    {
        static string useremail = "sebas746@hotmail.com";
        static string userpassword = "sebastian";
        static string userfullName = "Juan Sebastian Ortegon";
        static int userid = 1;

        public static User BuildUser()
        {
            return new User
            {
                Email = useremail,                
                Password = userpassword,
                UserId = userid
            };
        }

        public static ConnectionStrings BuildConnectionStrings()
        {
            return new ConnectionStrings
            {
                BotApiUrl = ConfigurationManager.AppSettings["stockApiUrl"].ToString(),
                ChatDB = ConfigurationManager.ConnectionStrings["WebAppDataContext"].ToString()
            };
        }

        public static Stock BuildStock()
        {
            return new Stock
            {
                ID = 1,
                Close = 204,
                Date = null,
                High = 205,
                Low = 202,
                Open = 203,
                Symbol = "APPL.US",
                Time = "22:00:01",
                Volume = 17265518
            };
        }
    }
}
