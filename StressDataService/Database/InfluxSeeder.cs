using System;
using System.Collections.Generic;
using System.Linq;
using StressDataService.Models;
using StressDataService.Repositories;

namespace StressDataService.Database;

public class InfluxSeeder
{
    private readonly IDatabaseHandler _mockDatabase;
    private readonly HrvMeasurementRepository _hrvMeasurementRepository;

    private const int MinStress = 20;
    private const int MaxStress = 100;
    private const int PointCount = 10;
    
    public InfluxSeeder(HrvMeasurementRepository hrvMeasurementRepository, IDatabaseHandler mockDatabase)
    {
        _mockDatabase = mockDatabase;
        _hrvMeasurementRepository = hrvMeasurementRepository;
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
        List<Wearable> wearables = _mockDatabase.GetWearables();
        List<HrvMeasurement> measurements = new List<HrvMeasurement>();

        wearables.ForEach(wearable =>
        {
            DateTime date = DateTime.UtcNow;

            for (int i = 0; i < PointCount; i++)
            {
                measurements.Add(new HrvMeasurement
                {
                    Id = Guid.NewGuid(),
                    PatientId = wearable.PatientId,
                    WearableId = wearable.Id,
                    TimeStamp = date,
                    HeartRateVariability = random.Next(MinStress, MaxStress)
                });

                date = date.AddDays(1);
            }
        });
        return measurements;
    }
}