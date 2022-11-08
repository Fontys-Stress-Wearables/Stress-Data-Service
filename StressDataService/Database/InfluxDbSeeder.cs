using System;
using System.Collections.Generic;
using System.Linq;
using StressDataService.Dtos;
using StressDataService.Interfaces;
using StressDataService.Models;
using StressDataService.Repositories;

namespace StressDataService.Database;

public class InfluxDbSeeder
{
    private readonly HrvMeasurementRepository _hrvMeasurementRepository;

    private const int MinStress = 20;
    private const int MaxStress = 100;
    private const int PatientCount = 10;
    private const int PointCount = 10;

    public InfluxDbSeeder(HrvMeasurementRepository hrvMeasurementRepository)
    {
        _hrvMeasurementRepository = hrvMeasurementRepository;

        Seed();
    }

    public async void Seed()
    {
        // Check if database has data already
        var measurements = (await _hrvMeasurementRepository.GetAll());
        if (measurements.Any()) return;

        // Fill database
        var newMeasurements = CreateData();
        newMeasurements.ForEach(measurement => { _hrvMeasurementRepository.Create(measurement); });
    }

    private List<HrvMeasurement> CreateData()
    {
        Random random = new Random(DateTime.Now.Second);
        List<HrvMeasurement> measurements = new List<HrvMeasurement>();

        for (int i = 0; i < PatientCount; i++)
        {
            var patientMeasurements = CreatePatientData(random);
            measurements.AddRange(patientMeasurements);
        }
        
        return measurements;
    }

    private List<HrvMeasurement> CreatePatientData(Random random)
    {
        List<HrvMeasurement> patientMeasurements = new List<HrvMeasurement>();

        DateTime date = DateTime.UtcNow;
        Guid patientId = Guid.NewGuid();
        Guid wearableId = Guid.NewGuid();
        
        for (int j = 0; j < PointCount; j++)
        {

            patientMeasurements.Add(new HrvMeasurement
            {
                Id = Guid.NewGuid(),
                PatientId = patientId,
                WearableId = wearableId,
                TimeStamp = date,
                HeartRateVariability = random.Next(MinStress, MaxStress)
            });

            date = date.AddHours(-1);
        }

        return patientMeasurements;
    }
}