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

namespace StressDataService
{
    public class InfluxDBHandler
    {
        // TODO: PUT THIS SOMEWHERE ELSE!!!!!!!
        private string token;
        private string bucket;
        private string org;
        private string connectionString;

        IDatabaseHandler mockDatabase;

        public InfluxDBHandler(IConfiguration configuration, IDatabaseHandler _mockDatabase ) {
            connectionString = configuration.GetSection("database")["connectionString"];
            org = configuration.GetSection("database")["org"];
            bucket = configuration.GetSection("database")["bucket"];
            token = configuration.GetSection("database")["token"];
            mockDatabase = _mockDatabase;
            Seed();
        }

        private async void Seed()
        {
            const int minStress = 20;
            const int maxStress = 100;
            const int pointCount = 10;

            // Check if database has data already
            List<HeartRateVariabilityMeasurement> existing = await GetAllHeartRateVariabilityMeasurements();
            if (existing.Count > 0)
            {
                return;
            }
            
            // Fill database
            List<Wearable> wearables = mockDatabase.GetWearables();
            Random random = new Random(DateTime.Now.Second);

            List<HeartRateVariabilityMeasurement> measurements = new List<HeartRateVariabilityMeasurement>();

            wearables.ForEach(wearable =>
            {
                DateTime date = DateTime.UtcNow;

                for (int i = 0; i < pointCount; i++)
                {
                    measurements.Add(new HeartRateVariabilityMeasurement(wearable.PatientId, wearable.Id, date, random.Next(minStress, maxStress)));
                    date = date.AddHours(1);
                }
            });
            
            measurements.ForEach(measurement =>
            {
                InsertHeartRateVariabilityMeasurement(measurement);
            });
        }

        private PointData CreatePoint(HeartRateVariabilityMeasurement measurement)
        {
            return PointData
                .Measurement("mem")
                .Tag("wearable_id", measurement.WearableId.ToString())
                .Tag("patient_id", measurement.PatientId.ToString())
                .Field("stress_level", measurement.HeartRateVariability)
                .Timestamp(measurement.TimeStamp, WritePrecision.Ms);
        }
        public IEnumerable<HeartRateVariabilityMeasurement> ConvertTable(List<FluxRecord> records)
        {
            return records.Select(record =>
                new HeartRateVariabilityMeasurement
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

        public async Task<List<HeartRateVariabilityMeasurement>> GetAllHeartRateVariabilityMeasurements()
        {
            using var client = InfluxDBClientFactory.Create(connectionString, token);
            var query = "from(bucket: \"StressData\") |> range(start: -100d)";
            var tables = await client.GetQueryApi().QueryAsync(query, org);
            foreach (var record in tables.SelectMany(table => table.Records))
            {
                Console.WriteLine($"{record}");
            }
            return tables.SelectMany(table =>
            ConvertTable(table.Records)).ToList();
        }

        public HeartRateVariabilityMeasurement GetHeartRateVariabilityMeasurementById(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<HeartRateVariabilityMeasurement>> GetHeartRateVariabilityMeasurementsByPatientId(Guid patientId)
        {
            using var client = InfluxDBClientFactory.Create(connectionString, token);

            var query = "from(bucket: \"StressData\")" +
                " |> range(start: -365d)" +
                "|> filter(fn: (r) => " +
                "r.patient_id == \"" + patientId + "\")";

            var tables = await client.GetQueryApi().QueryAsync(query, org);

            /// TODO: Remove below foreach after testing
            foreach (var record in tables.SelectMany(table => table.Records))
            {
                Console.WriteLine($"{record}");
            }

            return tables.SelectMany(table =>
            ConvertTable(table.Records)).ToList();
        }

        public async Task<List<HeartRateVariabilityMeasurement>> GetHeartRateVariabilityMeasurementsByWearableId(Guid wearableId)
        {
            using var client = InfluxDBClientFactory.Create(connectionString, token);

            var query = "from(bucket: \"StressData\")" +
                                " |> range(start: -365d)" +
                                "|> filter(fn: (r) => " +
                                "r.wearable_id == \"" + wearableId + "\")";

            var tables = await client.GetQueryApi().QueryAsync(query, org);

            /// TODO: Remove below foreach after testing
            foreach (var record in tables.SelectMany(table => table.Records))
            {
                Console.WriteLine($"{record}");
            }

            return tables.SelectMany(table =>
            ConvertTable(table.Records)).ToList();
        }

        public async Task<List<HeartRateVariabilityMeasurement>> GetHeartRateVariabilityMeasurementsWithinTimePeriodByWearableId(DateTime periodStartTime, DateTime periodEndTime, Guid wearableId)
        {
            using var client = InfluxDBClientFactory.Create(connectionString, token);

            var query = "from(bucket: \"StressData\")" +
                                " |> range(start: " + periodStartTime.ToString("yyyy-MM-ddTHH:mm:ssZ") + ", stop: " + periodEndTime.ToString("yyyy-MM-ddTHH:mm:ssZ") + ")" +
                                "|> filter(fn: (r) => " +
                                "r.wearable_id == \"" + wearableId + "\")";

            var tables = await client.GetQueryApi().QueryAsync(query, org);

            /// TODO: Remove below foreach after testing
            foreach (var record in tables.SelectMany(table => table.Records))
            {
                Console.WriteLine($"{record}");
            }

            return tables.SelectMany(table =>
            ConvertTable(table.Records)).ToList();
        }
        public async Task<List<HeartRateVariabilityMeasurement>> GetHeartRateVariabilityMeasurementsWithinTimePeriodByPatientId(DateTime periodStartTime, DateTime periodEndTime, Guid patientId)
        {
            using var client = InfluxDBClientFactory.Create(connectionString, token);

            var query = "from(bucket: \"StressData\")" +
                                " |> range(start: " + periodStartTime.ToString("yyyy-MM-ddTHH:mm:ssZ") + ", stop: " + periodEndTime.ToString("yyyy-MM-ddTHH:mm:ssZ") + ")" +
                                "|> filter(fn: (r) => " +
                                "r.patient_id == \"" + patientId + "\")";

            var tables = await client.GetQueryApi().QueryAsync(query, org);

            /// TODO: Remove below foreach after testing
            foreach (var record in tables.SelectMany(table => table.Records))
            {
                Console.WriteLine($"{record}");
            }

            return tables.SelectMany(table =>
            ConvertTable(table.Records)).ToList();
        }
        public async Task<List<HeartRateVariabilityMeasurement>> GetHeartRateVariabilityMeasurementsByPatientIdAndDate(Guid patientId, DateTime date)
        {
            using var client = InfluxDBClientFactory.Create(connectionString, token);
            DateTime dateWithoutTime = date.Date;
            var query = "from(bucket: \"StressData\")" +
                                " |> range(start: " + dateWithoutTime.ToString("yyyy-MM-ddTHH:mm:ssZ") + ", stop: " + dateWithoutTime.AddDays(1).ToString("yyyy-MM-ddTHH:mm:ssZ") + ")" +
                                "|> filter(fn: (r) => " +
                                "r.patient_id == \"" + patientId + "\")";

            var tables = await client.GetQueryApi().QueryAsync(query, org);

            return tables.SelectMany(table =>
            ConvertTable(table.Records)).ToList();
        }

        public void InsertHeartRateVariabilityMeasurement(HeartRateVariabilityMeasurement measurement)
        {
            using var client = InfluxDBClientFactory.Create(connectionString, token);
            var point = CreatePoint(measurement);

            using var writeApi = client.GetWriteApi();
            writeApi.WritePoint(point, bucket, org);
        }

        public void UpdateHeartRateVariabilityMeasurement(HeartRateVariabilityMeasurement measurement)
        {
            throw new NotImplementedException();
        }
    }
}
