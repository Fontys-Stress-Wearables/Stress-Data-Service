using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StressDataService.Models
{
    public class HeartRateMeasurement
    {
        public Guid Id { get; set; }
        public Guid WearableId { get; set; }
        public DateTime TimeStamp { get; set; }
        public int HeartRate { get; set; }
        public int HeartbeatInterval { get; set; }
        public int HeartRateVariability { get; set; }

        public HeartRateMeasurement()
        {

        }

        public HeartRateMeasurement(DateTime timeStamp, int heartRate, int heartbeatInterval, int heartRateVariability)
        {
            Id = Guid.NewGuid();
            WearableId = Guid.NewGuid();
            TimeStamp = timeStamp;
            HeartRate = heartRate;
            HeartbeatInterval = heartbeatInterval;
            HeartRateVariability = heartRateVariability;
        }
    }
}
