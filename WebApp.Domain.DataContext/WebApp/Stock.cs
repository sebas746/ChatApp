using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Domain.DataContext.WebApp
{
    public class Stock
    {
        [Key]
        public int ID { get; set; }
        
        public string Symbol { get; set; }
        
        public DateTime? Date { get; set; }
        
        public string Time { get; set; }

        public int? Open { get; set; }

        public int? High { get; set; }

        public int? Low { get; set; }

        public int? Close { get; set; }

        public long? Volume { get; set; }        
    }
}
