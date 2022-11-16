using System;
using NATS.Client;

namespace StressDataService.Interfaces;

public interface INatsService
{
    public IConnection Connect();
    public void Publish<T>(string target, T data);
    public void Subscribe<T>(string target, Action<T> handler);
}