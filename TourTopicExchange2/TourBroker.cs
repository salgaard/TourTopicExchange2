using Microsoft.AspNetCore.Connections;
using RabbitMQ.Client;
using System.Text;

public class TourBroker
{
    public async Task Send(string action, string city, string name, string email)
    {
        var factory = new ConnectionFactory { HostName = "localhost" };
        using var connection = await factory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();

        string exchange = "tours.exchange";
        await channel.ExchangeDeclareAsync(exchange: exchange, type: ExchangeType.Topic);

        string routingKey = $"tour.{action}";

        string payloadExample = $"{action} {city} {name} {email}";

        var body = Encoding.UTF8.GetBytes(payloadExample);

        await channel.BasicPublishAsync(exchange: exchange, routingKey: routingKey, body: body);
    }
}