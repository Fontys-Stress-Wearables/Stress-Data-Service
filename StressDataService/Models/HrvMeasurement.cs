using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StressDataService.Dtos;

namespace StressDataService.Models
{
    public class HrvMeasurement
    {
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }
        public Guid WearableId { get; set; }
        public DateTime TimeStamp { get; set; }
        public float HeartRateVariability { get; set; }

        public HrvMeasurementDto AsDto()
        {
            return new HrvMeasurementDto(
                Id, PatientId, WearableId, TimeStamp, HeartRateVariability
            );
        }
    }
}
