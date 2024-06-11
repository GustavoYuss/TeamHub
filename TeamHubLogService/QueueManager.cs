using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using TeamHubLogService.DTOs;

namespace TeamHubLogService;

class QueueManager(ILogger<QueueManager> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var factory = new ConnectionFactory { HostName = "172.16.0.11" };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(queue: "Prueba",
                            durable: true,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null);
        var consumer = new EventingBasicConsumer(channel);
        consumer.Received   += (model, ea) => 
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            var action =  JsonSerializer.Deserialize<UserActionDTO>(message);
            logger.LogInformation(message);
        };

        while(true)
        {
            channel.BasicConsume(queue: "Prueba",
                                autoAck: true,
                                consumer: consumer);
            await Task.Delay(1000, stoppingToken);
        }
    }
}