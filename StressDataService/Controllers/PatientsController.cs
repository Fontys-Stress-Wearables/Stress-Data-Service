using Microsoft.AspNetCore.Mvc;
using StressDataService.Models;
using System.Collections.Generic;
using System;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StressDataService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IDatabaseHandler database;
        public PatientsController(IDatabaseHandler database)
        {
            this.database = database;
        }
        // GET: <PatientsController>
        [HttpGet]
        public List<Patient> Get()
        {
            return database.GetPatients();
        }

        // GET <PatientController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<PatientController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<PatientController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PatientController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
