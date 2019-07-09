using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Domain.DataContext.WebApp;
using WebApp.Domain.Entities;

namespace WebApp.Domain.Interfaces.DAC
{
    public interface IWebAppDAC
    {
        User Login(string email, string password);
        List<User> GetUsers(int userId);
    }
}
