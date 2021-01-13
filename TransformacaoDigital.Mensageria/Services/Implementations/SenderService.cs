using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Text;

namespace TransformacaoDigital.Mensageria.Services.Implementations
{
    internal class SenderService : ISenderService
    {
        public void Send(QueueEnum queue, object objMessage)
        {
            var factory = new ConnectionFactory()
            {
                HostName = ConfigurationServerRabbitMQ.Instancia.HostName,
                VirtualHost = ConfigurationServerRabbitMQ.Instancia.UserName,
                Port = ConfigurationServerRabbitMQ.Instancia.Port,
                UserName = ConfigurationServerRabbitMQ.Instancia.UserName,
                Password = ConfigurationServerRabbitMQ.Instancia.Password
            };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                //channel.QueueDeclare(queue: queue.GetName(),
                //                     durable: false,
                //                     exclusive: false,
                //                     autoDelete: false,
                //                     arguments: null);

                string message = JsonConvert.SerializeObject(objMessage);
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: queue.GetName(),
                                     basicProperties: null,
                                     body: body);
            }

        }
    }
}
