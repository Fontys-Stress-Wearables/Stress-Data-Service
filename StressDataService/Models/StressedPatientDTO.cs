using System;

namespace StressDataService.Models
{
    public class StressedPatientDTO
    {
        public Guid PatientId { get; set; }
        public string FirstName { get; set; }
        public string LastNamePrefix { get; set; }
        public string LastName { get; set; }
        public int HeartRateVariability { get; set; }
        public StressedPatientDTO(Guid _patientId, string _firstName, string _lastNamePrefix, string _lastName, int _heartRateVariability)
        {
            PatientId = _patientId;
            FirstName = _firstName;
            LastNamePrefix = _lastNamePrefix;
            LastName = _lastName;
            HeartRateVariability = _heartRateVariability;
        }
    }
}
