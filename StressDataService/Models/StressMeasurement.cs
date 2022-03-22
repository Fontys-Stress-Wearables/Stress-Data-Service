using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StressDataService.Models
{
    public class StressMeasurement
    {
        public Guid Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public int StressValue { get; set; }

        public StressMeasurement()
        {

        }

        public StressMeasurement(DateTime timeStamp, int stressValue)
        {
            Id = Guid.NewGuid();
            TimeStamp = timeStamp;
            StressValue = stressValue;
        }
    }
}
