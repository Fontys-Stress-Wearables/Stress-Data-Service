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

        public SkinConductanceMeasurement()
        {

        }

        public SkinConductanceMeasurement(DateTime timeStamp, float skinConductance)
        {
            Id = Guid.NewGuid();
            WearableId = Guid.NewGuid();
            TimeStamp = timeStamp;
            SkinConductance = skinConductance;
        }
    }
}
