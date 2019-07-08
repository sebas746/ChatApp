using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebApp.Domain.DataContext.WebApp;

namespace WebApp.Domain.Interfaces.Service
{
    public interface IBotService
    {
        HttpResponseMessage GetStockItem(string stockCode);
        string GetStockCode(string msg);
        string ParseStockItem(string parsedString);
    }
}
