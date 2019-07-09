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
        public IRabbitMQService _RabbitMQService { get; set; }
        public ConnectionStrings _ConnectionStrings { get; set; }
        public IBotService _BotService { get; set; }

        public WebAppService(IWebAppDAC WebAppDAC, ConnectionStrings ConnectionStrings, RabbitMQService RabbitMQService, IBotService BotService)
        {
            this._WebAppDAC = WebAppDAC;            
            this._ConnectionStrings = ConnectionStrings;
            this._RabbitMQService = RabbitMQService;
            this._BotService = BotService;
        }

        public User Login(string email, string password)
        {
            return _WebAppDAC.Login(email, password);
        }

        public List<User> GetUsers(int userId)
        {
            return _WebAppDAC.GetUsers(userId);
        }

        public bool SendMessage(string message, string sentTo, string sender)
        {
            var conn = _RabbitMQService.GetConnection();
            var sendResult = _RabbitMQService.send(conn, message, sentTo, sender);
            var stockCode = _BotService.GetStockCode(message);
            if(stockCode != "")
            {
                var stockItemResult = GetStockItemAsync(stockCode, sentTo, sender);
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

        public string GetStockItemAsync(string stockCode, string sentTo, string sender)
        {
            var path = _ConnectionStrings.BotApiUrl.Replace("[stockCode]", stockCode);

            var result = String.Empty;
            var response = client.GetAsync(path).Result;
            if (response.IsSuccessStatusCode)
            {
                var apiResult = response.Content.ReadAsStringAsync().Result;
                result = _BotService.ParseStockItem(apiResult);
            }

            if(result != "")
            {
                var stockCommand = ConfigurationManager.AppSettings["BotResponseMsg"].ToString();
                stockCommand = stockCommand.Replace("[stockCode]", stockCode).Replace("[open]", result);                
                SendMessage(stockCommand, sender, "System BOT");
            }

            return result;
        }
    }
}
