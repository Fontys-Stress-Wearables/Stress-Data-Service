using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StressDataService.Models;

namespace StressDataService.Repositories
{
    public class StressMeasurementsRepository
    {
        private IDatabaseHandler database;

        public StressMeasurementsRepository(IDatabaseHandler database)
        {
            this.database = database;
        }

        //Get collection
        public List<StressMeasurement> GetAllMeasurements()
        {
            return database.GetAllStressMeasurements();
        }

        public List<StressMeasurement> GetMeasurementsByWearableId(Guid wearableId)
        {
            return database.GetStressMeasurementsByWearableId(wearableId);
        }

        public List<StressMeasurement> GetMeasurementsWithinTimePeriodByWearableId(DateTime periodStart, DateTime periodEnd, Guid wearableId)
        {
            return database.GetStressMeasurementsWithinTimePeriodByWearableId(periodStart, periodEnd, wearableId);
        }

        //Get singular
        public StressMeasurement GetMeasurementById(Guid id)
        {
            return database.GetStressMeasurementById(id);
        }

        //Insert
        public void InsertMeasurement(StressMeasurement measurement)
        {
            database.InsertStressMeasurement(measurement);
        }

        //Update
        public void UpdateMeasurement(StressMeasurement measurement)
        {
            database.UpdateStressMeasurement(measurement);
        }

        //Delete collection
        public void DeleteMeasurementsByWearableId(Guid wearableId)
        {
            database.DeleteStressMeasurementsByWearableId(wearableId);
        }

        //Delete singular
        public void DeleteMeasurementById(Guid measurementId)
        {
            database.DeleteStressMeasurementById(measurementId);
        }

    }
}
