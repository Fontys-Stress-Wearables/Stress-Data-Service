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
    public class SkinTemperatureMeasurementsController : ControllerBase
    {
        MockDatabase database = new MockDatabase();
        // GET: /skintemperaturemeasurements
        [HttpGet]
        public List<SkinTemperatureMeasurement> Get()
        {
            return database.SkinTemperatureMeasurements;
        }

        // GET api/<SkinTemperatureMeasurementsController>/5
        [HttpGet("{id}")]
        public SkinTemperatureMeasurement Get(Guid id)
        {
            return database.FindSkinTemperatureMeasurementById(id);
        }

        // POST api/<SkinTemperatureMeasurementsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<SkinTemperatureMeasurementsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SkinTemperatureMeasurementsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
