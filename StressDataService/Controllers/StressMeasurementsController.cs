using Microsoft.AspNetCore.Mvc;
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
        MockDatabase database = new MockDatabase();

        // GET: /stressmeasurements
        [HttpGet]
        public List<StressMeasurement> Get()
        {
            return database.StressMeasurements;
        }

        // GET /<StressMeasurementsController>/5
        [HttpGet("{id}")]
        public StressMeasurement Get(Guid id)
        {
            return database.FindStressMeasurementById(id);
        }

        // POST api/<StressMeasurementsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<StressMeasurementsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<StressMeasurementsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
