using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using NATS.Client;
using System;
using System.Text;

public class NatsService : INatsService
{
    private readonly IConfiguration _configuration;
    private readonly IConnection? _connection;
    private IAsyncSubscription? _asyncSubscription;

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
        Console.WriteLine("Connected to the NATS Server");
        return cf.CreateConnection(opts);
    }

    public void Publish<T>(string target, T data)
    {
        var message = new NatsMessage<T> { target = target, message = data };
        _connection?.Publish(target, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message)));
    }

    public void Subscribe(string target)
    {
        EventHandler<MsgHandlerEventArgs> h = (sender, args) =>
        {
            // print the message
            string receivedMessage = Encoding.UTF8.GetString(args.Message.Data);
            LogMessage(receivedMessage);
        };

        _asyncSubscription = _connection?.SubscribeAsync(target);
        if (_asyncSubscription != null)
        {
            _asyncSubscription.MessageHandler += h;
            _asyncSubscription.Start();
        }
    }

    private void LogMessage(string message)
    {
        Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fffffff")} - {message}");
    }
}