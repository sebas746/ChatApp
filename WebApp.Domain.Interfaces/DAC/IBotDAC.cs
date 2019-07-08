using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Domain.DataContext.WebApp;
using WebApp.Domain.Entities;

namespace WebApp.Domain.Interfaces.DAC
{
    public interface IBotDAC
    {
        Stock GetStockItem(string stockCode);
        GetStockValues_Result GetStockItemColumns(string stockCode);
    }
}
