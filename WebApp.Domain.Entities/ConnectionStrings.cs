using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Domain.Entities
{
    public class ConnectionStrings
    {
        public string ChatDB { get; set; }
        public string BotApiUrl { get; set; }

        public ConnectionStrings() { }

        public ConnectionStrings(string ChatDB, string BotApiUrl)
        {
            this.ChatDB = ChatDB;
            this.BotApiUrl = BotApiUrl;
        }
    }
}
