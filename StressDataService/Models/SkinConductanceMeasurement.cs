using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StressDataService.Models
{
    public class SkinConductanceMeasurement
    {
        public Guid Id { get; set; }
        public Guid WearableId { get; set; }
        public DateTime TimeStamp { get; set; }
        public float SkinConductance { get; set; }

        public SkinConductanceMeasurement(Guid wearableId, DateTime timeStamp, float skinConductance)
        {
            Id = Guid.NewGuid();
            WearableId = wearableId;
            TimeStamp = timeStamp;
            SkinConductance = skinConductance;
        }
    }
}
