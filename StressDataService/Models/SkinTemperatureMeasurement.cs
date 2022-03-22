using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StressDataService.Models
{
    public class SkinTemperatureMeasurement
    {
        public Guid Id { get; set; }
        public Guid WearableId { get; set; }
        public DateTime TimeStamp { get; set; }
        public float SkinTemperature { get; set; }

        public SkinTemperatureMeasurement()
        {

        }

        public SkinTemperatureMeasurement(DateTime timeStamp, float skinTemperature)
        {
            Id = Guid.NewGuid();
            WearableId = Guid.NewGuid();
            TimeStamp = timeStamp;
            SkinTemperature = skinTemperature;
        }

        public SkinTemperatureMeasurement(Guid wearableId, DateTime timeStamp, float skinTemperature)
        {
            Id = Guid.NewGuid();
            WearableId = wearableId;
            TimeStamp = timeStamp;
            SkinTemperature = skinTemperature;
        }
    }
}
