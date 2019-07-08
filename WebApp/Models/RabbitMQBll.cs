using Bot.Service.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WebApp.Domain.DataContext.WebApp;
using WebApp.Domain.Interfaces.Service;

namespace WebApp.Models
{
    public class RabbitMQBll
    {
        public IConnection GetConnection()
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.UserName = "guest";
            factory.Password = "guest";
            factory.Port = 5672;
            factory.HostName = "localhost";
            factory.VirtualHost = "/";
           // factory.Uri = "http://192.168.7.140:15672/";
            return factory.CreateConnection();
        }
        public bool send(IConnection con,string message,string friendqueue, string sender)
        {
            try
            {
                IModel channel = con.CreateModel();
                channel.ExchangeDeclare("messageexchange", ExchangeType.Direct);
                channel.QueueDeclare(friendqueue, true, false, false, null);                
                channel.QueueBind(friendqueue, "messageexchange", friendqueue, null);
                channel.QueueBind(friendqueue, "messageexchange", "all", null);
                var msg = Encoding.UTF8.GetBytes(sender + ": " + message);
                channel.BasicPublish("messageexchange", "all", null, msg);                
            }
            catch (Exception)
            {

            }
            return true;

        }
        public string receive(IConnection con,string myqueue)
        {
            try
            {                
                string queue = myqueue;
                var stock = new Stock();
                IModel channel = con.CreateModel();
                channel.QueueDeclare(queue: queue, durable: true, exclusive: false, autoDelete: false, arguments: null);
                var consumer = new EventingBasicConsumer(channel);
                BasicGetResult result = channel.BasicGet(queue: queue, autoAck: true);
                if (result != null)
                {   
                    var msg = Encoding.UTF8.GetString(result.Body);
                    //var stockCode = BotService.GetStockCode(msg);                   

                    return msg;
                }   
                else
                    return null;
            }
            catch (Exception)
            {
                return null;
                
            }
           
        }
    }
}