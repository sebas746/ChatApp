using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WebApp.Domain.DataContext.WebApp;
using WebApp.Domain.Interfaces.DAC;
using WebApp.Domain.Interfaces.Service;
using static WebApp.Domain.Entities.Util.Enumerators;

namespace Bot.Service.Services
{
    public class BotService : IBotService
    {
        public IBotDAC BotDAC { get; set; }

        public BotService(IBotDAC BotDAC)
        {
            this.BotDAC = BotDAC;
        }

        public bool CheckMesagge(string msg)
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

        public string GetStockCode(string msg)
        {
            try
            {
                if (CheckMesagge(msg))
                {
                    var stockcode = msg.Substring(msg.LastIndexOf('=') + 1);
                    return stockcode;
                }

                return null;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public HttpResponseMessage ReadSentMessage(string message, string sentTo, string sender)
        {
            if(CheckMesagge(message))
            {
                var code = GetStockCode(message);
                if(code != "")
                {
                    var result = GetStockItem(code);
                    return result;
                }
            }

            return null;
        }

        public string ParseStockItem(string parsedString)
        {
            var result = string.Empty;

            if (parsedString != "")
            {
                var splittedString = parsedString.Split('\n');
                if (splittedString.Length > 1)
                {
                    var splittedValues = splittedString[1].Split(',');
                    if (splittedValues.Length > 2)
                    {
                        //Return the 
                        result = splittedValues[(int)StockValues.Open];
                    }
                }
            }
            return result;
        }

        public HttpResponseMessage GetStockItem(string stockCode)
        {
            try
            {
                if (stockCode != null)
                {
                    var resultObject = BotDAC.GetStockItemColumns(stockCode);

                    MemoryStream stream = new MemoryStream();
                    StreamWriter writer = new StreamWriter(stream);

                    //Verify if the column names has values
                    if(resultObject.ColumnNames != null)
                    {
                        writer.Write(resultObject.ColumnNames);
                        writer.Write("\n");
                        writer.Write(resultObject.ColumnValues);
                        writer.Flush();
                    }
                    
                    stream.Position = 0;

                    HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
                    result.Content = new StreamContent(stream);
                    result.Content.Headers.ContentType = new MediaTypeHeaderValue("text/csv");
                    result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = "Export.csv" };

                    return result;

                }

                return null;
            }
            catch(Exception exc)
            {
                throw exc;
            }
        }
    }
}
