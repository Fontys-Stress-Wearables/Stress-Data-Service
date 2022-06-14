
using Microsoft.AspNetCore.Mvc;
using StressDataService.Models;
using System.Collections.Generic;
using System;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StressDataService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IDatabaseHandler database;
        public PatientsController(IDatabaseHandler database)
        {
            this.database = database;
        }

        // GET: <PatientsController>
        [HttpGet]
        public List<Patient> Get()
        {
            return database.GetPatients();
        }

        [HttpGet("stressed/{belowValue}")]
        public List<StressedPatientDTO> GetStressedPatients(int belowValue)
        {
            return database.GetStressedPatientsBelowValue(belowValue);

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

        // GET <PatientController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
    }
}
