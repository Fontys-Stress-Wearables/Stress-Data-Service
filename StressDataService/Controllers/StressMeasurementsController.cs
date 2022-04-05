using Microsoft.AspNetCore.Mvc;
using StressDataService.Repositories;
using StressDataService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StressDataService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StressMeasurementsController : ControllerBase
    {
        private readonly INatsService _natsService;
        private readonly StressMeasurementsRepository repository;

        public StressMeasurementsController(StressMeasurementsRepository repository, INatsService natsService)
        {
            this.repository = repository;
            _natsService = natsService;
        }

        /*// GET: /stressmeasurements/wearable/550e8400-e29b-41d4-a716-446655440000 
        [HttpGet("wearable/{wearableId}")]
        public List<StressMeasurement> GetByWearableId(Guid wearableId)
        {
            return repository.GetMeasurementsByWearableId(wearableId);
        }*/

        // GET: /stressmeasurements/wearable/550e8400-e29b-41d4-a716-446655440000 
        [HttpGet("wearable/{wearableId}")]
        public List<StressMeasurement> GetByWearableIdWithinTimePeriod(Guid wearableId, DateTime startTime, DateTime endTime)
        {
            return repository.GetMeasurementsWithinTimePeriodByWearableId(startTime, endTime, wearableId);
        }

        // GET: /stressmeasurements
        [HttpGet]
        public List<StressMeasurement> Get()
        {
            _natsService.Publish("technical_health", "hearthbeat");
            return repository.GetAllMeasurements();
        }

        // GET /stressmeasurements/550e8400-e29b-41d4-a716-446655440000 
        [HttpGet("{id}")]
        public StressMeasurement Get(Guid id)
        {
            return repository.GetMeasurementById(id);
        }

        // POST /stressmeasurements
        [HttpPost]
        public void Post([FromBody] string value)
        {
            throw new NotImplementedException();
        }

        // PUT /stressmeasurements
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            throw new NotImplementedException();
        }

        // DELETE /stressmeasurements/550e8400-e29b-41d4-a716-446655440000 
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            repository.DeleteMeasurementById(id);
        }
    }
}
