using RabbitMQ.Client;
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
    public interface IRabbitMQService
    {
        IConnection GetConnection();
        bool send(IConnection con, string message, string sentTo, string sender);
        string receive(IConnection con, string myqueue);
    }
}
