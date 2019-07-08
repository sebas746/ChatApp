using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Domain.Entities.Util
{
    public static class Enumerators
    {
        public enum StockValues
        {
            Symbol = 0,
            Date = 1,
            Time = 2,
            Open = 3,
            High = 4,
            Low = 5,
            Close = 6,
            Volume = 7
        }
    }
}
