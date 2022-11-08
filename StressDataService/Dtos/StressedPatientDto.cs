using System;

namespace StressDataService.Dtos
{
    // ToDo Convert to proper Dto or Model
    public class StressedPatientDto
    {
        public Guid PatientId { get; set; }
        public string FirstName { get; set; }
        public string LastNamePrefix { get; set; }
        public string LastName { get; set; }
        public float HeartRateVariability { get; set; }
        public DateTime Timestamp { get; set; }

        public StressedPatientDto(Guid _patientId, string _firstName, string _lastNamePrefix, string _lastName, float _heartRateVariability, DateTime _timestamp)
        {
            PatientId = _patientId;
            FirstName = _firstName;
            LastNamePrefix = _lastNamePrefix;
            LastName = _lastName;
            HeartRateVariability = _heartRateVariability;
            Timestamp = _timestamp;
        }
    }
}
