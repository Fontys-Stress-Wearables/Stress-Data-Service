using StressDataService.Models;
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
        MockDatabase database = new MockDatabase();

        // GET: /HeartRateMeasurements
        [HttpGet]
        public List<HeartRateMeasurement> Get()
        {
            return database.HeartRateMeasurements;
        }

        // GET /HeartRateMeasurements/550e8400-e29b-41d4-a716-446655440000 
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<HeartRateMeasurementsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<HeartRateMeasurementsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<HeartRateMeasurementsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
