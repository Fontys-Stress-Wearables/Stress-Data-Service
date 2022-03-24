using Microsoft.AspNetCore.Mvc;
using StressDataService.Models;
using StressDataService.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StressDataService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SkinTemperatureMeasurementsController : ControllerBase
    {
        private readonly SkinTemperatureMeasurementsRepository repository;

        public SkinTemperatureMeasurementsController(SkinTemperatureMeasurementsRepository repository)
        {
            this.repository = repository;
        }

        // GET: /skintemperaturemeasurements/wearable/550e8400-e29b-41d4-a716-446655440000 
        [HttpGet("wearable/{wearableId}")]
        public List<SkinTemperatureMeasurement> GetByWearableId(Guid wearableId)
        {
            return repository.GetMeasurementsByWearableId(wearableId);
        }

        // GET: /skintemperaturemeasurements
        [HttpGet]
        public List<SkinTemperatureMeasurement> Get()
        {
            return repository.GetAllMeasurements();
        }

        // GET api/<SkinTemperatureMeasurementsController>/5
        [HttpGet("{id}")]
        public SkinTemperatureMeasurement Get(Guid id)
        {
            return repository.GetMeasurementById(id);
        }

        // POST api/<SkinTemperatureMeasurementsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
            throw new NotImplementedException();
        }

        // PUT api/<SkinTemperatureMeasurementsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            throw new NotImplementedException();
        }

        // DELETE api/<SkinTemperatureMeasurementsController>/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            repository.DeleteMeasurementById(id);
        }
    }
}
