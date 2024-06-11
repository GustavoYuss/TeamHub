using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using TeamHubSessionsServices.DTOs;
using TeamHubSessionsServices.Gateways.Interfaces;

namespace TeamHubServiceUser.Gateways.Providers;

public class LogService : ILogService
{
    public void SaveUserAction(UserActionDTO userAction)
    {
        var factory = new ConnectionFactory { HostName = "172.16.0.11" };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(queue: "Prueba",
                            durable: true,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null);

        string mensaje = JsonSerializer.Serialize(userAction);
        var body = Encoding.UTF8.GetBytes(mensaje);

        channel.BasicPublish(exchange: string.Empty,
                            routingKey: "Prueba",
                            basicProperties: null,
                            body: body);
        
    }
}