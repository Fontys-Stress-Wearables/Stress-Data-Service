using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StressDataService.Models;

namespace StressDataService.Repositories
{
    public class HeartRateVariabilityMeasurementsRepository
    {
        private DatabaseHandler database = new DatabaseHandler();
/*        private IDatabaseHandler database2;

        public HeartRateVariabilityMeasurementsRepository(IDatabaseHandler database)
        {
            this.database2 = database;
        }*/

        //Get collection
        public Task<List<HeartRateVariabilityMeasurement>> GetAllMeasurements()
        {
            return database.GetAllHeartRateVariabilityMeasurements();
        }

        public Task<List<HeartRateVariabilityMeasurement>> GetMeasurementsByWearableId(Guid wearableId)
        {
            return database.GetHeartRateVariabilityMeasurementsByWearableId(wearableId);
        }

        public Task<List<HeartRateVariabilityMeasurement>> GetMeasurementsWithinTimePeriodByWearableId(DateTime periodStart, DateTime periodEnd, Guid wearableId)
        {
            return database.GetHeartRateVariabilityMeasurementsWithinTimePeriodByWearableId(periodStart, periodEnd, wearableId);
        }

        public Task<List<HeartRateVariabilityMeasurement>> GetMeasurementsByPatientId(Guid patientId)
        {
            return database.GetHeartRateVariabilityMeasurementsByPatientId(patientId);
        }

        //Get singular
        public HeartRateVariabilityMeasurement GetMeasurementById(Guid id)
        {
            return database.GetHeartRateVariabilityMeasurementById(id);
        }

        //Insert
        public void InsertMeasurement(HeartRateVariabilityMeasurement measurement)
        {
            database.InsertHeartRateVariabilityMeasurement(measurement);
        }

        //Update
        public void UpdateMeasurement(HeartRateVariabilityMeasurement measurement)
        {
            database.UpdateHeartRateVariabilityMeasurement(measurement);
        }

        //Delete collection
        public void DeleteMeasurementsByWearableId(Guid wearableId)
        {
            database.DeleteHeartRateVariabilityMeasurementsByWearableId(wearableId);
        }

        //Delete singular
        public void DeleteMeasurementById(Guid measurementId)
        {
            database.DeleteHeartRateVariabilityMeasurementById(measurementId);
        }
    }
}
