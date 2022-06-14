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
        private readonly INatsService nats;
        private readonly HeartRateVariabilityMeasurementsRepository repository;

        public HeartRateVariabilityMeasurementsController(HeartRateVariabilityMeasurementsRepository repository, INatsService nats)
        {
            this.nats = nats;
            this.repository = repository;
        }

        [HttpGet("nats/simulate")]
        public void SimulateNats()
        {
            this.nats.Publish("stress:created", new List<HeartRateVariabilityMeasurement>() { new HeartRateVariabilityMeasurement(Guid.NewGuid(), Guid.NewGuid(), DateTime.Now, 250) });

        }

        // GET: /HeartRateVariabilitymeasurements
        [HttpGet]
        public List<HeartRateVariabilityMeasurement> Get()
        {
            return repository.GetAllMeasurements().Result;
        }

        // GET: /HeartRateVariabilitymeasurements/patient/550e8400-e29b-41d4-a716-446655440000 
        [HttpGet("patient/{patientId}")]
        public List<HeartRateVariabilityMeasurement> GetByPatientId(Guid patientId)
        {
            List<HeartRateVariabilityMeasurement> measurements;
            try
            {
                measurements = repository.GetMeasurementsByPatientId(patientId).Result;
                nats.Publish("th_logs", "Stress measurements were retrieved for patientId: " + patientId);
            } 
            catch (Exception ex)
            {
                nats.Publish("th_warnings", "Something went wrong when attempting to get stress measurements for patientId: " + patientId + " - " + ex.Message);
                measurements = null;
            }
            return measurements;
        }

        // GET: /HeartRateVariabilitymeasurements/wearable/550e8400-e29b-41d4-a716-446655440000 
        [HttpGet("patient/{patientId}/timeframe/{date}")]
        public List<HeartRateVariabilityMeasurement> GetByPatientIdAndDate(Guid patientId, string date)
        {
            List<HeartRateVariabilityMeasurement> measurements;
            try
            {
                measurements = repository.GetMeasurementsByPatientIdAndDate(patientId, date).Result;
            }
            catch (Exception ex)
            {
                nats.Publish("th_warnings", "Something went wrong when attempting to get stress measurements for patientId: " + patientId +
                    " on date: " + date + " - " + ex.Message);
                measurements = null;
            }
            return measurements;
        }
        [HttpGet("wearable/{wearableId}")]
        public List<HeartRateVariabilityMeasurement> GetByWearableIdWithinTimePeriod(Guid wearableId, DateTime startTime, DateTime endTime)
        {
            return repository.GetMeasurementsWithinTimePeriodByWearableId(startTime, endTime, wearableId).Result;
        }

        [HttpGet("patient/stressed/{belowValue}")]
        public List<HeartRateVariabilityMeasurement> GetPatientIdsWithStressBelowValue(int belowValue)
        {
            return repository.GetAllMeasurements().Result;
            //TODO:implementation
        }

        //POST /HeartRateVariabilitymeasurements
        [HttpPost]
        public void Post(HeartRateVariabilityMeasurement measurementToAdd)
        {
            repository.InsertMeasurement(measurementToAdd);
        }

        // GET /HeartRateVariabilitymeasurements/550e8400-e29b-41d4-a716-446655440000 
        /*[HttpGet("{id}")]
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
        }*/

        // DELETE api/HeartRateVariabilitymeasurements/550e8400-e29b-41d4-a716-446655440000 
        /*[HttpDelete("{id}")]
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
        }*/
    }
}
