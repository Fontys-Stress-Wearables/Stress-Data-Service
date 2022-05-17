using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StressDataService.Models
{
    public class HeartRateVariabilityMeasurement
    {
        public Guid Id { get; set; }
        public Guid WearableId { get; set; }
        public DateTime TimeStamp { get; set; }
        public float HeartRateVariability { get; set; }

        public HeartRateVariabilityMeasurement()
        {

        }
        public HeartRateVariabilityMeasurement(Guid wearableId, DateTime timeStamp, float heartRateVariability)
        {
            Id = Guid.NewGuid();
            WearableId = wearableId;
            TimeStamp = timeStamp;
            HeartRateVariability = heartRateVariability;
        }
    }
}
