using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StressDataService.Models
{
    public class StressMeasurement
    {
        public Guid Id { get; set; }
        public Guid WearableId { get; set; }
        public DateTime TimeStamp { get; set; }
        public int StressValue { get; set; }

        public StressMeasurement(Guid wearableId, DateTime timeStamp, int stressValue)
        {
            Id = Guid.NewGuid();
            WearableId = wearableId;
            TimeStamp = timeStamp;
            StressValue = stressValue;
        }
    }
}
