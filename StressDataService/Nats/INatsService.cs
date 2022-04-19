using NATS.Client;

public interface INatsService
{
    public IConnection Connect();
    public void Publish<T>(string target, T data);
    public void Subscribe(string target);
}