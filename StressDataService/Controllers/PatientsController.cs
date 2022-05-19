﻿using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("stressed/{belowValue}")]
        public List<StressedPatientDTO> GetStressedPatients(int belowValue)
        {
            return database.GetStressedPatientsBelowValue(belowValue);
        }

        // GET <PatientController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
    }
}
