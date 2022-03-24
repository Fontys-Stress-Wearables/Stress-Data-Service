using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StressDataService.Models
{
    public class Patient
    {
        public Guid Id { get; set; }
        public Guid PatientClusterId { get; set; }
        public string FirstName { get; set; }
        public string LastNamePrefix { get; set; }
        public string LastName { get; set; }
        public DateTime Birthdate { get; set; }
        public string EmailAddress { get; set; }

        public Patient(string firstName, string lastNamePrefix, string lastName, DateTime birthdate, string emailAddress)
        {
            Id = Guid.NewGuid();
            PatientClusterId = Guid.NewGuid();
            FirstName = firstName;
            LastNamePrefix = lastNamePrefix;
            LastName = lastName;
            Birthdate = birthdate;
            EmailAddress = emailAddress;
        }
    }
}
