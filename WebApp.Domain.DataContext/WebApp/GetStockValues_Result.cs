using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Domain.DataContext.WebApp
{
    public partial class GetStockValues_Result
    {
        public string ColumnNames { get; set; }
        public string ColumnValues { get; set; }
    }
}
