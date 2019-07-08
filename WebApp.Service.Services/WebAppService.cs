using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebApp.Domain.DataContext.WebApp;
using WebApp.Domain.Entities;
using WebApp.Domain.Interfaces.DAC;
using WebApp.Domain.Interfaces.Service;

namespace WebApp.Service.Services
{
    public class WebAppService : IWebAppService
    {
        public IWebAppDAC _WebAppDAC { get; set; }        
        private static HttpClient client = new HttpClient();
        public ConnectionStrings _ConnectionStrings { get; set; }

        public WebAppService(IWebAppDAC WebAppDAC, ConnectionStrings ConnectionStrings)
        {
            this._WebAppDAC = WebAppDAC;            
            this._ConnectionStrings = ConnectionStrings;
        }

        public Users Login(string email, string password)
        {
            return _WebAppDAC.Login(email, password);
        }

        public List<Users> GetUsers(int userId)
        {
            return _WebAppDAC.GetUsers(userId);
        }

        public bool SendMessage(string message, string sentTo, string sender)
        {
            var stockCode = GetStockCode(message);
            if(stockCode != "")
            {
                
            }

            return true;
        }

        private bool CheckMesaggeStockCode(string msg)
        {
            try
            {
                string stockCommand = ConfigurationManager.AppSettings["stockCommand"].ToString();
                if (msg.Contains(stockCommand))
                {
                    return true;
                }
                return false;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        private string GetStockCode(string msg)
        {
            try
            {
                if (CheckMesaggeStockCode(msg))
                {
                    var botUrl = ConfigurationManager.AppSettings["stockApiUrl"].ToString();                    
                    var stockcode = msg.Substring(msg.LastIndexOf('=') + 1);
                    botUrl = botUrl.Replace("[stockCode]", stockcode);
                    var stockResult = GetStockItemAsync(botUrl);
                    return stockcode;
                }

                return null;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public string GetStockItemAsync(string path)
        {
            var result = String.Empty;
            var response = client.GetAsync(path).Result;
            if (response.IsSuccessStatusCode)
            {
                result = response.Content.ReadAsStringAsync().Result;
            }
            return result;
        }
    }
}
