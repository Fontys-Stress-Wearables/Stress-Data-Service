using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StressDataService.Models;

namespace StressDataService.Repositories
{
    public class SkinTemperatureMeasurementsRepository
    {
        private IDatabaseHandler database;

        public SkinTemperatureMeasurementsRepository(IDatabaseHandler database)
        {
            this.database = database;
        }

        //Get collection
        public List<SkinTemperatureMeasurement> GetAllMeasurements()
        {
            return database.GetAllSkinTemperatureMeasurements();
        }

        public List<SkinTemperatureMeasurement> GetMeasurementsByWearableId(Guid wearableId)
        {
            return database.GetSkinTemperatureMeasurementsByWearableId(wearableId);
        }

        public List<SkinTemperatureMeasurement> GetMeasurementsWithinTimePeriodByWearableId(DateTime periodStart, DateTime periodEnd, Guid wearableId)
        {
            return database.GetSkinTemperatureMeasurementsWithinTimePeriodByWearableId(periodStart, periodEnd, wearableId);
        }

        //Get singular
        public SkinTemperatureMeasurement GetMeasurementById(Guid id)
        {
            return database.GetSkinTemperatureMeasurementById(id);
        }

        //Insert
        public void InsertMeasurement(SkinTemperatureMeasurement measurement)
        {
            database.InsertSkinTemperatureMeasurement(measurement);
        }

        //Update
        public void UpdateMeasurement(SkinTemperatureMeasurement measurement)
        {
            database.UpdateSkinTemperatureMeasurement(measurement);
        }

        //Delete collection
        public void DeleteMeasurementsByWearableId(Guid wearableId)
        {
            database.DeleteSkinTemperatureMeasurementsByWearableId(wearableId);
        }

        //Delete singular
        public void DeleteMeasurementById(Guid measurementId)
        {
            database.DeleteSkinTemperatureMeasurementById(measurementId);
        }
    }
}
