#nullable enable
using System;
using System.Text;
using NATS.Client;
using Newtonsoft.Json;
using StressDataService.Interfaces;

namespace StressDataService.Nats;

public class NatsService : INatsService
{
    private readonly IConnection? _connection;
    private IAsyncSubscription? _asyncSubscription;
    private TechnicalHealthManager _technicalHealthManager;

    public NatsService()
    {
        _connection = Connect();
        _technicalHealthManager = new TechnicalHealthManager(this);
    }

    public IConnection? Connect()
    {
        ConnectionFactory cf = new ConnectionFactory();
        Options opts = ConnectionFactory.GetDefaultOptions();

        opts.Url = "nats://host.docker.internal:4222";
        Console.WriteLine("Trying to connect to the NATS Server");

        try
        {
            IConnection? connection = cf.CreateConnection(opts);
            Console.WriteLine("Succesfully connected to the NATS server");
            return connection;  
        }
        catch
        {
            Console.WriteLine("Failed to connect to the NATS server");
            return null;
        }
    }

    public void Publish<T>(string target, T data)
    {
        var message = new NatsMessage<T> { Target = target, Message = data };
        _connection?.Publish(target, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message)));
    }

    public void Subscribe<T>(string target, Action<T> handler)
    {
        void EventHandler(object? sender, MsgHandlerEventArgs args)
        {
            string receivedMessage = Encoding.UTF8.GetString(args.Message.Data);
            var message = JsonConvert.DeserializeObject<T>(receivedMessage);

            if (message != null) handler(message);
        }

        _asyncSubscription = _connection?.SubscribeAsync(target);
        if (_asyncSubscription != null)
        {
            _asyncSubscription.MessageHandler += EventHandler;
            _asyncSubscription.Start();
        }
    }
}