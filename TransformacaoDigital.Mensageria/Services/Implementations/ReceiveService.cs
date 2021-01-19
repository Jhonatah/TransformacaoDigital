using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading.Tasks;

namespace TransformacaoDigital.Mensageria.Services.Implementations
{
    public class ReceiveService : IReceiveService
    {
        private string _queueName;

        public void Subscribe(IEventHandler handler)
        {
            _queueName = handler.QueueToProcess.GetName();

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
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, eventArgs) =>
                {
                    var body = eventArgs.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    var response = handler.HandlerMessage(message);

                    //if (response.Ok)
                    //    channel.BasicAck(eventArgs.DeliveryTag, false);

                    //channel.BasicNack(eventArgs.DeliveryTag, false, false);
                };

                channel.BasicConsume(queue: _queueName,
                                     autoAck: true,
                                     consumer: consumer);
            }
        }
    }
}