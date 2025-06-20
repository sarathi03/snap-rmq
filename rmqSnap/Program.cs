using System;
using System.Text;
using RabbitMQ.Client;

class Program
{
    static void Main(string[] args)
    {
        var factory = new ConnectionFactory()
        {
            HostName = "localhost", 
            Port = 5672
        };

        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(queue: "snap-test",
                             durable: false,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

        string message = "Hello from Snap RabbitMQ Producer!";
        var body = Encoding.UTF8.GetBytes(message);

        channel.BasicPublish(exchange: "",
                             routingKey: "snap-test",
                             basicProperties: null,
                             body: body);

        Console.WriteLine($"[x] Sent: {message}");
    }
}
