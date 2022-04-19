using StressDataService.Models;
using StressDataService.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StressDataService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HeartRateVariabilityMeasurementsController : ControllerBase
    {
        private readonly HeartRateVariabilityMeasurementsRepository repository;

        public HeartRateVariabilityMeasurementsController(HeartRateVariabilityMeasurementsRepository repository)
        {
            this.repository = repository;
        }

        // GET: /HeartRateVariabilitymeasurements
        [HttpGet]
        public List<HeartRateVariabilityMeasurement> Get()
        {
            return repository.GetAllMeasurements();
        }

        // GET: /HeartRateVariabilitymeasurements/patient/550e8400-e29b-41d4-a716-446655440000 
        [HttpGet("patient/{patientId}")]
        public List<HeartRateVariabilityMeasurement> GetByPatientId(Guid patientId)
        {
            return repository.GetMeasurementsByPatientId(patientId);
        }

        // GET: /HeartRateVariabilitymeasurements/wearable/550e8400-e29b-41d4-a716-446655440000 
        [HttpGet("wearable/{wearableId}")]
        public List<HeartRateVariabilityMeasurement> GetByWearableIdWithinTimePeriod(Guid wearableId, DateTime startTime, DateTime endTime)
        {
            return repository.GetMeasurementsWithinTimePeriodByWearableId(startTime, endTime, wearableId);
        }

        // GET /HeartRateVariabilitymeasurements/550e8400-e29b-41d4-a716-446655440000 
        [HttpGet("{id}")]
        public HeartRateVariabilityMeasurement GetById(Guid id)
        {
            return repository.GetMeasurementById(id);
        }

        // DELETE api/HeartRateVariabilitymeasurements/550e8400-e29b-41d4-a716-446655440000 
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            repository.DeleteMeasurementById(id);
        }
    }
}
