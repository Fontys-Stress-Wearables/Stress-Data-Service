using System.Threading;
using StressDataService.Interfaces;

namespace StressDataService.Nats
{
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
            Timer heartbeatTimer = new Timer(HeartbeatTimerCallback, null, 0, 30000);
        }

        static void HeartbeatTimerCallback(object state)
        {
            _natsService.Publish("technical_health", "heartbeat");
        }
    }
}
