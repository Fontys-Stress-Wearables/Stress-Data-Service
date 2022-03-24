using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StressDataService.Models
{
    public class Wearable
    {
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }

        public Wearable(Guid patientId)
        {
            Id = Guid.NewGuid();
            this.PatientId = patientId;
        }

    }
}
