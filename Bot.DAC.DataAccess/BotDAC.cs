using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Domain.DataContext.WebApp;
using WebApp.Domain.Entities;
using WebApp.Domain.Interfaces.DAC;

namespace Bot.DAC.DataAccess
{
    public class BotDAC : IBotDAC
    {
        public ConnectionStrings ConnectionStrings { get; set; }

        public BotDAC(ConnectionStrings ConnectionStrings)
        {
            this.ConnectionStrings = ConnectionStrings;
        }

        public Stock GetStockItem(string stockCode)
        {
            using (WebAppDataContext db = new WebAppDataContext())
            {
                var response = db.Stock.Where(x => x.Symbol == stockCode).FirstOrDefault();

                return response;
            }
        }

        public GetStockValues_Result GetStockItemColumns(string stockCode)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionStrings.ChatDB))
            {
                conn.Open();

                // 1.  create a command object identifying the stored procedure
                SqlCommand cmd = new SqlCommand("GetStockValues", conn);

                // 2. set the command object so it knows to execute a stored procedure
                cmd.CommandType = CommandType.StoredProcedure;

                // 3. add parameter to command, which will be passed to the stored procedure
                cmd.Parameters.Add(new SqlParameter("@stockCode", stockCode));

                var result = new GetStockValues_Result();

                // execute the command
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    
                    // iterate through results, printing each to console
                    while (rdr.Read())
                    {
                        result.ColumnNames = rdr["ColumnNames"].ToString();
                        result.ColumnValues = rdr["ColumnValues"].ToString();
                    }
                }

                return result;
            }
        }
    }
}
