using StressDataService.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StressDataService.Dtos;
using StressDataService.Interfaces;
using StressDataService.Services;


namespace StressDataService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HrvMeasurementsController : ControllerBase
    {
        private readonly INatsService _nats;
        private readonly IHrvMeasurementService _hrvService;

        public HrvMeasurementsController(IHrvMeasurementService hrvService, INatsService nats)
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
        public async Task<ActionResult<IEnumerable<HrvMeasurementDto>>> GetByPatientId(Guid patientId)
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
        public async Task<ActionResult<IEnumerable<HrvMeasurementDto>>> GetByWearableIdAndTimespan(Guid wearableId, DateTime startTime, DateTime endTime)
        {
            var measurements = await _hrvService.GetByWearableIdAndTimespan(wearableId, startTime, endTime);

            return Ok(measurements);
        }

        [HttpPost]
        public ActionResult<HrvMeasurementDto> Create(CreateHrvMeasurementDto measurementDto)
        {
            var measurement = _hrvService.Create(measurementDto);
            
            if (measurement == null) return BadRequest();

            return CreatedAtAction(nameof(GetById), new { id = measurement.Id }, measurement);
        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, UpdateHrvMeasurementDto updateSprintDto)
        {
            var measurement = await _hrvService.Update(id, updateSprintDto);

            if (measurement == null) return NotFound();

            return NoContent();
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var sprint = await _hrvService.Delete(id);

            if (sprint == null) return NotFound();

            return NoContent();
        }

        [HttpGet("nats/simulate")]
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
        }
    }
}
