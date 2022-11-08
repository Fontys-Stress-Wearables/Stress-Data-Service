using Microsoft.AspNetCore.Mvc;
using StressDataService.Models;
using System.Collections.Generic;
using StressDataService.Dtos;
using StressDataService.Interfaces;


namespace StressDataService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IDatabaseHandler _database;
        public PatientsController(IDatabaseHandler database)
        {
            this._database = database;
        }

        [HttpGet]
        public List<Patient> Get()
        {
            return _database.GetPatients();
        }
        
        // ToDo Implementation of stressed patients 
        [HttpGet("stressed/{belowValue}")]
        public List<StressedPatientDto> GetStressedPatients(int belowValue)
        {
            return _database.GetStressedPatientsBelowValue(belowValue);

            //// Get list of stressed patient ID's
            //List<HeartRateVariabilityMeasurement> stressedMeasurement = stressDatabase.GetStressedPatientsBelowValue(belowValue);

            //// Find users who belong to these ID's
            //List<StressedPatientDTO> stressedPatients = new List<string>();
            //stressedMeasurement.ForEach(measurement =>
            //{
            //    Patient patient = database.GetPatientById(measurement.PatientId);
            //    stressedPatients.Add(new StressedPatientDTO(measurement.PatientId, patient.Name, patient.LastNamePrefix, patient.LastName, measurement.HeartRateVariability, measurement.Timestamp));
            //});

            //return stress;
        }
    }
}
