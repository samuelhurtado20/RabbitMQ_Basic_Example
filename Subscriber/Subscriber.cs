using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Subscriber
{
    class Subscriber
    {
        static void Main(string[] args)
        {
            string queueName = @"queue1";
            string hostName = @"localhost";

            var factory = new ConnectionFactory() { HostName = hostName };
            using(var connection = factory.CreateConnection())
            using(var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: queueName,
                                    durable: false,
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var message = Encoding.UTF8.GetString(ea.Body.ToArray());
                    Console.WriteLine($" [>>>] Msg Received: {message}");
                    Console.WriteLine($"[>>>>>>>>>>>>>>>>>>>>>>>>>>>]");
                };
                channel.BasicConsume(queue: queueName,
                                    autoAck: true,
                                    consumer: consumer);

                Console.WriteLine(@" Press any key to exit.");
                Console.ReadLine();
            }
        }
    }
}
