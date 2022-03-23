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
    public class SkinConductanceMeasurementsController : ControllerBase
    {
        private readonly MockDatabase database;
        public SkinConductanceMeasurementsController(MockDatabase database)
        {
            this.database = database;
        }
        // GET: /skinconductancemeasurements
        [HttpGet]
        public List<SkinConductanceMeasurement> Get()
        {
            return database.SkinConductanceMeasurements;
        }

        // GET api/<SkinConductanceMeasurements>/5
        [HttpGet("{id}")]
        public SkinConductanceMeasurement Get(Guid id)
        {
            return database.FindSkinConductanceMeasurementById(id);
        }

        // POST api/<SkinConductanceMeasurements>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<SkinConductanceMeasurements>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SkinConductanceMeasurements>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
