using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Domain.DataContext.WebApp;
using WebApp.Domain.Entities;
using WebApp.Domain.Interfaces.DAC;

namespace WebApp.DAC
{
    public class WebAppDAC : IWebAppDAC
    {
        public ConnectionStrings ConnectionStrings { get; set; }

        public WebAppDAC(ConnectionStrings ConnectionStrings)
        {
            this.ConnectionStrings = ConnectionStrings;
        }

        public Users Login(string email, string password)
        {
            using (WebAppDataContext db = new WebAppDataContext())
            {
                var response = db.Users.Where(x => x.Email == email && x.Password == password).FirstOrDefault();

                if(response != null)
                {
                    return response;
                }
                else
                {
                    return new Users()
                    {
                        Email = "",
                        FullName = "",
                        Password = "",
                        Mobile = "",
                        UserID = 0
                    };
                }
                
            }
        }

        public List<Users> GetUsers(int userId)
        {
            using (WebAppDataContext db = new WebAppDataContext())
            {
                var response = db.Users.Where(x => x.UserID != userId);

                return response.ToList();
            }
        }
    }
}
