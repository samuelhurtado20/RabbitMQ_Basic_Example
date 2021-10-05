using System;
using RabbitMQ.Client;
using System.Text;

namespace Publisher
{
    class Publisher
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

                string message = @$"Hello World from RabbitMQ... ({DateTime.Now})";

                channel.BasicPublish(exchange: "",
                                    routingKey: @"queue1",
                                    basicProperties: null,
                                    body: Encoding.UTF8.GetBytes(message));
                Console.WriteLine($" [>>>] Sent {message}");
            }

            Console.WriteLine(" Press any key to exit.");
            Console.ReadLine();
        }
    }
}
