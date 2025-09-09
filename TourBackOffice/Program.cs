using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory { HostName = "localhost" };

using var connection = await factory.CreateConnectionAsync();
using var channel = await connection.CreateChannelAsync();

await channel.ExchangeDeclareAsync(exchange: "tours.exchange", type: ExchangeType.Topic);

QueueDeclareOk queueDeclareResult = await channel.QueueDeclareAsync();
string queueName = queueDeclareResult.QueueName;

await channel.QueueBindAsync(queue: queueName, exchange: "tours.exchange", routingKey: "tour.*");

Console.WriteLine(" [*] Waiting for messages. To exit press CTRL+C");

var consumer = new AsyncEventingBasicConsumer(channel);
consumer.ReceivedAsync += (model, ea) =>
{
    var body = ea.Body.ToArray();
    var payLoad = Encoding.UTF8.GetString(body);
    var routingKey = ea.RoutingKey;

    HandleRequest(routingKey, payLoad);

    Console.WriteLine($" [x] Received '{routingKey}':'{payLoad}'");
    return Task.CompletedTask;
};

await channel.BasicConsumeAsync(queueName, autoAck: true, consumer: consumer);

Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();



void HandleRequest(string routingKey, string payLoad)
{
    switch (routingKey)
    {
        case "tour.booked":
            TourBackOffice.BackOfficeService.CreateBooking(payLoad);
            break;
        case "tour.cancelled":
            TourBackOffice.BackOfficeService.CancelBooking(payLoad);
            break;
        default:
            return;
    }
}