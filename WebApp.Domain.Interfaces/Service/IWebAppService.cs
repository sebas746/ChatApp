using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Domain.DataContext.WebApp;

namespace WebApp.Domain.Interfaces.Service
{
    public interface IWebAppService
    {
        User Login(string email, string password);
        List<User> GetUsers(int userId);
        bool SendMessage(string message, string sentTo, string sender);
        string GetStockItemAsync(string stockCode, string sentTo, string sender);
    }
}
