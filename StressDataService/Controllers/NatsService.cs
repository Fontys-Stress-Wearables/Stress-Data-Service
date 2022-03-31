using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using NATS.Client;
using System;
using System.Text;

public class NatsService : INatsService
{
    private readonly IConfiguration _configuration;
    private readonly IConnection? _connection;

    public NatsService(IConfiguration configuration)
    {
        _configuration = configuration;
        _connection = Connect();
        Subscribe("technical_health");
    }

    public IConnection Connect()
    {
        ConnectionFactory cf = new ConnectionFactory();
        Options opts = ConnectionFactory.GetDefaultOptions();

        opts.Url = "nats://localhost:4222";
        Console.WriteLine("Connected to Nats Server");
        return cf.CreateConnection(opts);
    }

    public void Publish<T>(string target, T data)
    {
        var message = new NatsMessage<T>{target = target, message = data};
        _connection?.Publish(target, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message)));
    }

    public void Subscribe(string target)
    {
        ISyncSubscription sub = _connection.SubscribeSync(target);
        var message = sub.NextMessage();
        if (message != null)
        {
            string data = Encoding.UTF8.GetString(message.Data);
            LogMessage(data);
        }
    }

    private void LogMessage(string message)
    {
        Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fffffff")} - {message}");
    }
}
