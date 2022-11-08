using System;
using System.Collections.Generic;
using StressDataService.Dtos;
using StressDataService.Models;

namespace StressDataService.Interfaces
{
    // ToDo Mock Database Should Only Exist In Development Environment
    public interface IMockDatabase
    {
        public List<Wearable> GetWearables();
        public List<Patient> GetPatients();
        public Patient GetPatientById(Guid id);
        public void InsertPatient(Patient patient);
    }
}
