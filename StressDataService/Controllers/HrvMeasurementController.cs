using StressDataService.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StressDataService.Dtos;
using StressDataService.Nats;
using StressDataService.Services;


namespace StressDataService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HrvMeasurementsController : ControllerBase
    {
        private readonly INatsService _nats;
        private readonly HrvMeasurementService _hrvService;

        public HrvMeasurementsController(HrvMeasurementService hrvService, INatsService nats)
        {
            _nats = nats;
            _hrvService = hrvService;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HrvMeasurementDto>>> GetAll()
        {
            var measurements = await _hrvService.GetAll();
            
            return Ok(measurements);
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<HrvMeasurementDto>> GetById(Guid id)
        {
            var measurement = await _hrvService.GetById(id);

            if (measurement == null) return NotFound();
        
            return Ok(measurement);
        }
        
        [HttpGet("patient/{patientId}")]
        public async Task<ActionResult<List<HrvMeasurementDto>>> GetByPatientId(Guid patientId)
        {
            var measurements = await _hrvService.GetByPatientId(patientId);
            
            return Ok(measurements);
        }
        
        [HttpGet("date/{patientId}")]
        public async Task<ActionResult<IEnumerable<HrvMeasurementDto>>> GetByPatientIdAndDate(Guid patientId, DateTime date)
        {
            var measurements = await _hrvService.GetByPatientIdAndDate(patientId, date);

            return Ok(measurements);
        }

        [HttpGet("date/{patientId}/timespan")]
        public async Task<ActionResult<IEnumerable<HrvMeasurementDto>>> GetByPatientIdAndTimespan(Guid patientId, DateTime startTime, DateTime endTime)
        {
            var measurements = await _hrvService.GetByPatientIdAndTimespan(patientId, startTime, endTime);

            return Ok(measurements);
        }
        
        [HttpGet("wearable/{wearableId}/timespan")]
        public async Task<ActionResult<List<HrvMeasurement>>> GetByWearableIdAndTimespan(Guid wearableId, DateTime startTime, DateTime endTime)
        {
            var measurements = await _hrvService.GetByWearableIdAndTimespan(wearableId, startTime, endTime);

            return Ok(measurements);
        }

        [HttpPost]
        public ActionResult<HrvMeasurementDto> Create(CreateHrvMeasurementDto measurementDto)
        {
            var measurement = _hrvService.Create(measurementDto);
            
            return CreatedAtAction(nameof(GetById), new { id = measurement.Id }, measurement);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateHrvMeasurementDto updateSprintDto)
        {
            var measurement = await _hrvService.Update(id, updateSprintDto);

            if (measurement == null) return NotFound();

            return NoContent();
        }
        
        // ToDo Delete Implementation
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var sprint = await _hrvService.Delete(id);

            if (sprint == null) return NotFound();

            return NoContent();
        }

        // ToDo Delete All Ids from Wearable
        // Todo Delete all Ids from Patient
        
        /*[HttpGet("nats/simulate")]
        public void SimulateNats()
        {
            var measurement = new HrvMeasurement
            {
                Id = Guid.NewGuid(),
                PatientId = Guid.NewGuid(),
                WearableId = Guid.NewGuid(),
                TimeStamp = DateTime.Now,
                HeartRateVariability = 250
            };
            _nats.Publish("stress:created", new List<HrvMeasurement>() { measurement });
        }*/
    }
}
