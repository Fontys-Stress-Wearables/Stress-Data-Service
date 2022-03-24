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
    public class HeartRateMeasurementsController : ControllerBase
    {
        private readonly HeartRateMeasurementsRepository repository;

        public HeartRateMeasurementsController(HeartRateMeasurementsRepository repository)
        {
            this.repository = repository;
        }

        // GET: /heartratemeasurements
        [HttpGet]
        public List<HeartRateMeasurement> Get()
        {
            return repository.GetAllMeasurements();
        }

        // GET: /heartratemeasurements/wearable/550e8400-e29b-41d4-a716-446655440000 
        [HttpGet("wearable/{wearableId}")]
        public List<HeartRateMeasurement> GetByWearableId(Guid wearableId)
        {
            return repository.GetMeasurementsByWearableId(wearableId);
        }

        // GET /heartratemeasurements/550e8400-e29b-41d4-a716-446655440000 
        [HttpGet("{id}")]
        public HeartRateMeasurement GetById(Guid id)
        {
            return repository.GetMeasurementById(id);
        }

        // POST api/heartratemeasurements
        [HttpPost]
        public void Post([FromBody] string value)
        {
            throw new NotImplementedException();
        }

        // PUT api/heartratemeasurements
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            throw new NotImplementedException();
        }

        // DELETE api/heartratemeasurements/550e8400-e29b-41d4-a716-446655440000 
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            repository.DeleteMeasurementById(id);
        }
    }
}
