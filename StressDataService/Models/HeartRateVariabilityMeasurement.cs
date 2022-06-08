using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StressDataService.Models
{
    public class HeartRateVariabilityMeasurement
    {
        public Guid PatientId { get; set; }
        public Guid WearableId { get; set; }
        public DateTime TimeStamp { get; set; }
        public int HeartRateVariability { get; set; }

        public HeartRateVariabilityMeasurement(Guid patientId, Guid wearableId, DateTime timeStamp, int heartRateVariability)
        {
            PatientId = patientId;
            WearableId = wearableId;
            TimeStamp = timeStamp;
            HeartRateVariability = heartRateVariability;
        }
    }
}
