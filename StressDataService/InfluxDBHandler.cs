using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Core.Flux.Domain;
using InfluxDB.Client.Writes;
using Microsoft.Extensions.Configuration;
using StressDataService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StressDataService.Interfaces;

namespace StressDataService
{
    public class InfluxDBHandler
    {
        private readonly string _token;
        private readonly string _bucket;
        private readonly string _org;
        private readonly string _connectionString;

        private readonly IDatabaseHandler _mockDatabase;

        public InfluxDBHandler(IConfiguration configuration, IDatabaseHandler mockDatabase) {
            _connectionString = configuration.GetSection("database")["connectionString"];
            _org = configuration.GetSection("database")["org"];
            _bucket = configuration.GetSection("database")["bucket"];
            _token = configuration.GetSection("database")["token"];
            _mockDatabase = mockDatabase;
            Seed();
        }

        private async void Seed()
        {
            const int minStress = 20;
            const int maxStress = 100;
            const int pointCount = 10;

            // Check if database has data already
            List<HrvMeasurement> existing = (await GetAllHeartRateVariabilityMeasurements()).ToList();
            if (existing.Count > 0)
            {
                return;
            }
            
            // Fill database
            List<Wearable> wearables = _mockDatabase.GetWearables();
            Random random = new Random(DateTime.Now.Second);

            List<HrvMeasurement> measurements = new List<HrvMeasurement>();

            wearables.ForEach(wearable =>
            {
                DateTime date = DateTime.UtcNow;

                for (int i = 0; i < pointCount; i++)
                {
                    /*
                    measurements.Add(new HrvMeasurement(wearable.PatientId, wearable.Id, date, random.Next(minStress, maxStress)));
                    */
                    date = date.AddHours(1);
                }
            });
            
            measurements.ForEach(measurement =>
            {
                InsertHeartRateVariabilityMeasurement(measurement);
            });
        }

        private PointData CreatePoint(HrvMeasurement measurement)
        {
            return PointData
                .Measurement("mem")
                .Tag("wearable_id", measurement.WearableId.ToString())
                .Tag("patient_id", measurement.PatientId.ToString())
                .Field("stress_level", measurement.HeartRateVariability)
                .Timestamp(measurement.TimeStamp, WritePrecision.Ms);
        }
        
        public IEnumerable<HrvMeasurement> ConvertTable(List<FluxRecord> records)
        {
            return records.Select(record =>
                new HrvMeasurement
                {
                    WearableId = Guid.Parse(record.GetValueByKey("wearable_id").ToString()),
                    PatientId = Guid.Parse(record.GetValueByKey("patient_id").ToString()),
                    TimeStamp = (DateTime)record.GetTimeInDateTime(),
                    HeartRateVariability = float.Parse(record.GetValueByKey("_value").ToString())
                });
        }
        public void DeleteHeartRateVariabilityMeasurementById(Guid id)
        {

            throw new NotImplementedException();
        }

        public void DeleteHeartRateVariabilityMeasurementsByWearableId(Guid wearableId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<HrvMeasurement>> GetAllHeartRateVariabilityMeasurements()
        {
            using var client = InfluxDBClientFactory.Create(_connectionString, _token);
            var query = "from(bucket: \"StressData\") |> range(start: -100d)";
            var tables = await client.GetQueryApi().QueryAsync(query, _org);
            foreach (var record in tables.SelectMany(table => table.Records))
            {
                Console.WriteLine($"{record}");
            }

            return tables.SelectMany(table =>
                ConvertTable(table.Records));
        }

        public void InsertHeartRateVariabilityMeasurement(HrvMeasurement measurement)
        {
            using var client = InfluxDBClientFactory.Create(_connectionString, _token);
            var point = CreatePoint(measurement);

            using var writeApi = client.GetWriteApi();
            writeApi.WritePoint(point, _bucket, _org);
        }

        public void UpdateHeartRateVariabilityMeasurement(HrvMeasurement measurement)
        {
            throw new NotImplementedException();
        }
    }
}
