using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApp.Domain.Interfaces.Service;

namespace WebApp.Controllers
{
    public class BotController : ApiController
    {
        public IBotService BotService { get; set; }

        public BotController(IBotService BotService)
        {
            this.BotService = BotService;
        }

        [HttpGet]
        [Route("api/Bot/GetStockDetails/")]
        public HttpResponseMessage GetStockDetails(string stockCode)
        {            
            return BotService.GetStockItem(stockCode);
        }
    }
}