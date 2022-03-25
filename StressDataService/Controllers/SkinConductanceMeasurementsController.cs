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
    public class SkinConductanceMeasurementsController : ControllerBase
    {
        private readonly SkinConductanceMeasurementsRepository repository;

        public SkinConductanceMeasurementsController(SkinConductanceMeasurementsRepository repository)
        {
            this.repository = repository;
        }

        // GET: /skinconductancemeasurements
        [HttpGet]
        public List<SkinConductanceMeasurement> Get()
        {
            return repository.GetAllMeasurements();
        }


        // GET /skinconductancemeasurements/550e8400-e29b-41d4-a716-446655440000 
        [HttpGet("{id}")]
        public SkinConductanceMeasurement Get(Guid id)
        {
            return repository.GetMeasurementById(id);
        }

        /*// GET: /skinconductancemeasurements/wearable/550e8400-e29b-41d4-a716-446655440000 
        [HttpGet("wearable/{wearableId}")]
        public List<SkinConductanceMeasurement> GetByWearableId(Guid wearableId)
        {
            return repository.GetMeasurementsByWearableId(wearableId);
        }*/

        // GET: /skinconductancemeasurements/wearable/550e8400-e29b-41d4-a716-446655440000
        [HttpGet("wearable/{wearableId}")]
        public List<SkinConductanceMeasurement> GetByWearableIdWithinTimePeriod(Guid wearableId, DateTime startTime, DateTime endTime)
        {
            return repository.GetMeasurementsWithinTimePeriodByWearableId(startTime, endTime, wearableId);
        }

        // POST /skinconductancemeasurements
        [HttpPost]
        public void Post([FromBody] string value)
        {
            throw new NotImplementedException();
        }

        // PUT /skinconductancemeasurements
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            throw new NotImplementedException();
        }

        // DELETE /skinconductancemeasurements/550e8400-e29b-41d4-a716-446655440000 
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            repository.DeleteMeasurementById(id);
        }
    }
}
