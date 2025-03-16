using RabbitMQ.Client;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RabbitMQ
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            await Call();
        }

        static async Task Call() {
            var factory = new ConnectionFactory { HostName = "localhost" };
            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(queue: "hello", durable: false, exclusive: false, autoDelete: false,
                arguments: null);

            const string message = "Hello World!";
            var body = Encoding.UTF8.GetBytes(message);

            await channel.BasicPublishAsync(exchange: string.Empty, routingKey: "hello", body: body);
            Console.WriteLine($" [x] Sent {message}");

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}