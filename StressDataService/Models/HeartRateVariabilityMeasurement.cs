﻿using System;
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
        public int HeartRateVariability { get; set; }

        public HeartRateVariabilityMeasurement(Guid wearableId, DateTime timeStamp, int heartRateVariability)
        {
            Id = Guid.NewGuid();
            WearableId = wearableId;
            TimeStamp = timeStamp;
            HeartRateVariability = heartRateVariability;
        }
    }
}