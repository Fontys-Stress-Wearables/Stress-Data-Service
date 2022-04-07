using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StressDataService.Models;

namespace StressDataService.Repositories
{
    public class HeartRateMeasurementsRepository
    {
        private IDatabaseHandler database;

        public HeartRateMeasurementsRepository(IDatabaseHandler database)
        {
            this.database = database;
        }

        //Get collection
        public List<HeartRateMeasurement> GetAllMeasurements()
        {
            return database.GetAllHeartRateMeasurements();
        }

        public List<HeartRateMeasurement> GetMeasurementsByWearableId(Guid wearableId)
        {
            return database.GetHeartRateMeasurementsByWearableId(wearableId);
        }

        public List<HeartRateMeasurement> GetMeasurementsWithinTimePeriodByWearableId(DateTime periodStart, DateTime periodEnd, Guid wearableId)
        {
            return database.GetHeartRateMeasurementsWithinTimePeriodByWearableId(periodStart, periodEnd, wearableId);
        }

        public List<HeartRateMeasurement> GetMeasurementsByPatientId(Guid patientId)
        {
            return database.GetHeartRateMeasurementsByPatientId(patientId);
        }

        //Get singular
        public HeartRateMeasurement GetMeasurementById(Guid id)
        {
            return database.GetHeartRateMeasurementById(id);
        }

        //Insert
        public void InsertMeasurement(HeartRateMeasurement measurement)
        {
            database.InsertHeartRateMeasurement(measurement);
        }

        //Update
        public void UpdateMeasurement(HeartRateMeasurement measurement)
        {
            database.UpdateHeartRateMeasurement(measurement);
        }

        //Delete collection
        public void DeleteMeasurementsByWearableId(Guid wearableId)
        {
            database.DeleteHeartRateMeasurementsByWearableId(wearableId);
        }

        //Delete singular
        public void DeleteMeasurementById(Guid measurementId)
        {
            database.DeleteHeartRateMeasurementById(measurementId);
        }
    }
}
