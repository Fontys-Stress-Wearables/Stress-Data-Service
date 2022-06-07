using StressDataService.Models;
using StressDataService.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StressDataService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HeartRateVariabilityMeasurementsController : ControllerBase
    {
        static INatsService _natsService;

        private readonly HeartRateVariabilityMeasurementsRepository repository;

        public HeartRateVariabilityMeasurementsController(HeartRateVariabilityMeasurementsRepository repository, INatsService natsService)
        {
            this.repository = repository;
            _natsService = natsService;
        }

        // GET: /HeartRateVariabilitymeasurements/patient/550e8400-e29b-41d4-a716-446655440000 
        [HttpGet("patient/{patientId}")]
        public List<HeartRateVariabilityMeasurement> GetByPatientId(Guid patientId)
        {
            List<HeartRateVariabilityMeasurement> measurements;
            try
            {
                measurements = repository.GetMeasurementsByPatientId(patientId);
                _natsService.Publish("th_logs", "Stress measurements were retrieved for patientId: " + patientId);
            } 
            catch (Exception ex)
            {
                _natsService.Publish("th_warnings", "Something went wrong when attempting to get stress measurements for patientId: " + patientId + " - " + ex.Message);
                measurements = null;
            }
            return measurements;
        }

        // GET: /HeartRateVariabilitymeasurements/wearable/550e8400-e29b-41d4-a716-446655440000 
        [HttpGet("wearable/{wearableId}")]
        public List<HeartRateVariabilityMeasurement> GetByWearableIdWithinTimePeriod(Guid wearableId, DateTime startTime, DateTime endTime)
        {
            List<HeartRateVariabilityMeasurement> measurements;
            try
            {
                measurements = repository.GetMeasurementsWithinTimePeriodByWearableId(startTime, endTime, wearableId);
            }
            catch (Exception ex)
            {
                _natsService.Publish("th_warnings", "Something went wrong when attempting to get stress measurements for wearableId: " + wearableId + 
                    " between timestamps: " + startTime + " / " + endTime + " - " + ex.Message);
                measurements = null;
            }
            return measurements;
        }

        // GET /HeartRateVariabilitymeasurements/550e8400-e29b-41d4-a716-446655440000 
        [HttpGet("{id}")]
        public HeartRateVariabilityMeasurement GetById(Guid id)
        {
            HeartRateVariabilityMeasurement measurement;
            try
            {
                measurement = repository.GetMeasurementById(id);
            }
            catch (Exception ex)
            {
                _natsService.Publish("th_warnings", "Something went wrong when attempting to get stress measurement by id: " + id + " - " + ex.Message);
                measurement = null;
            }
            return measurement;
        }

        // DELETE api/HeartRateVariabilitymeasurements/550e8400-e29b-41d4-a716-446655440000 
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            try
            {
                repository.DeleteMeasurementById(id);
            }
            catch (Exception ex)
            {
                _natsService.Publish("th_warnings", "Something went wrong when attempting to get delete measurement by id: " + id + " - " + ex.Message);
            }
        }
    }
}
