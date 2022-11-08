using System.Threading;

namespace StressDataService.Nats
{
    // ToDo Clean up
    public class TechnicalHealthManager
    {
        static INatsService _natsService;

        public TechnicalHealthManager(INatsService natsService)
        {
            _natsService = natsService;
            StartHeartbeat();
        }

        public void StartHeartbeat()
        {
            Timer heartbeatTimer = new Timer(heartbeatTimerCallback, null, 0, 30000);
        }

        static void heartbeatTimerCallback(object state)
        {
            _natsService.Publish("technical_health", "heartbeat");
        }
    }
}
