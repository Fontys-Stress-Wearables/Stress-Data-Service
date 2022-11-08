using System;
using System.Collections.Generic;
using System.Linq;
using StressDataService.Dtos;
using StressDataService.Interfaces;
using StressDataService.Models;

namespace StressDataService.Database
{
    public class MockDatabase : IMockDatabase
    {
        private List<Patient> Patients { get; }
        private List<Wearable> Wearables { get; }


        public MockDatabase()
        {
            Patients = new List<Patient>();
            Wearables = new List<Wearable>();

            Seed();
        }

        public List<Wearable> GetWearables()
        {
            return Wearables;
        }

        private void Seed()
        {
            var names = new[] { "aaron", "abdul", "abe", "abel", "abraham", "adam", "abby", "abigail", "adele", "adrian" };
            var lastNames = new[] { "abbott", "acosta", "adams", "adkins", "aguilar" };


            for (int i = 0; i < 10; i++)
            {
                Patient p = new Patient(
                    names[i], 
                    "", 
                    lastNames[i % 5], 
                    DateTime.Now, 
                    $"{names[i]}-{lastNames[i % 5]}@gmail.com"
                );
                Patients.Add(p);
            }

            for (int i = 0; i < 10; i++)
            {
                Wearables.Add(new Wearable(Patients[i].Id));
            }
        }
        
        public List<Patient> GetPatients()
        {
            return Patients;
        }
        public Patient GetPatientById(Guid patientId)
        {
            IEnumerable<Patient> patients =
                from Patient patient in Patients
                where (patient.Id).Equals(patientId)
                select patient;
            return patients.FirstOrDefault();
        }

        public void InsertPatient(Patient patient)
        {
            Patients.Add(patient);
        }
    }
}
