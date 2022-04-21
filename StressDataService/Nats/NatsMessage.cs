public class NatsMessage<T>
{
    public string origin { get; set; } = "Stress Data Service";
    public string target { get; set; }
    public T message { get; set; }
}