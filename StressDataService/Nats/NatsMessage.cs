namespace StressDataService.Nats;

public class NatsMessage<T>
{
    public string Origin { get; set; } = "stress_data_service";
    public string Target { get; set; }
    public T Message { get; set; }
}