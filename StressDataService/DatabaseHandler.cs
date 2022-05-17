using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Writes;
using StressDataService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StressDataService
{
    public class DatabaseHandler
    {
        // TODO: PUT THIS SOMEWHERE ELSE!!!!!!!
        private const string token = "QRyklx_gnFqL5F4OT7Vsnu__24XF8xED-d8yISi7-5sqnRLQdAdAV3kwViwPmhwY7nf_LiZK0MAKlpBXTJpQHA==";
        private const string bucket = "StressData";
        private const string org = "SWSP";
        private const string connectionString = "http://localhost:8086";

        private PointData CreatePoint(HeartRateVariabilityMeasurement measurement)
        {
            return PointData
                .Measurement("mem")
                .Tag("wearable_id", measurement.WearableId.ToString())
                .Field("stress_level", measurement.HeartRateVariability)
                .Timestamp(measurement.TimeStamp, WritePrecision.Ms);
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
            List<HeartRateVariabilityMeasurement> measurements = tables.SelectMany(table =>
            table.Records.Select(record =>
                new HeartRateVariabilityMeasurement
                {
                    TimeStamp = (DateTime)record.GetTimeInDateTime(),
                    HeartRateVariability = float.Parse(record.GetValueByKey("_value").ToString()),
                    WearableId = Guid.Parse(record.GetValueByKey("wearable_id").ToString())
                })).ToList();

            return measurements;
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
            table.Records.Select(record =>
                new HeartRateVariabilityMeasurement
                {
                    WearableId = Guid.Parse(record.GetValueByKey("wearable_id").ToString()),
                    TimeStamp = (DateTime)record.GetTimeInDateTime(),
                    HeartRateVariability = float.Parse(record.GetValueByKey("_value").ToString())
                })).ToList();
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
            table.Records.Select(record =>
                new HeartRateVariabilityMeasurement
                {
                    WearableId = Guid.Parse(record.GetValueByKey("wearable_id").ToString()),
                    TimeStamp = (DateTime)record.GetTimeInDateTime(),
                    HeartRateVariability = float.Parse(record.GetValueByKey("_value").ToString())
                })).ToList();
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
            table.Records.Select(record =>
                new HeartRateVariabilityMeasurement
                {
                    WearableId = Guid.Parse(record.GetValueByKey("wearable_id").ToString()),
                    TimeStamp = (DateTime)record.GetTimeInDateTime(),
                    HeartRateVariability = float.Parse(record.GetValueByKey("_value").ToString())
                })).ToList();
        }

        public Patient GetPatientById(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<Patient> GetPatients()
        {
            throw new NotImplementedException();
        }

        public void InsertHeartRateVariabilityMeasurement(HeartRateVariabilityMeasurement measurement)
        {
            using var client = InfluxDBClientFactory.Create(connectionString, token);
            var point = CreatePoint(measurement);

            using var writeApi = client.GetWriteApi();
            writeApi.WritePoint(point, bucket, org);
        }

        public void InsertPatient(Patient patient)
        {
            throw new NotImplementedException();
        }

        public void UpdateHeartRateVariabilityMeasurement(HeartRateVariabilityMeasurement measurement)
        {
            throw new NotImplementedException();
        }
    }
}
