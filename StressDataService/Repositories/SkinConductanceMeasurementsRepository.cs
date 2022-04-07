using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StressDataService.Models;

namespace StressDataService.Repositories
{
    public class SkinConductanceMeasurementsRepository
    {
        private IDatabaseHandler database;

        public SkinConductanceMeasurementsRepository(IDatabaseHandler database)
        {
            this.database = database;
        }

        //Get collection
        public List<SkinConductanceMeasurement> GetAllMeasurements()
        {
            return database.GetAllSkinConductanceMeasurements();
        }

        public List<SkinConductanceMeasurement> GetMeasurementsByWearableId(Guid wearableId)
        {
            return database.GetSkinConductanceMeasurementsByWearableId(wearableId);
        }

        public List<SkinConductanceMeasurement> GetMeasurementsWithinTimePeriodByWearableId(DateTime periodStart, DateTime periodEnd, Guid wearableId)
        {
            return database.GetSkinConductanceMeasurementsWithinTimePeriodByWearableId(periodStart, periodEnd, wearableId);
        }

        //Get singular
        public SkinConductanceMeasurement GetMeasurementById(Guid id)
        {
            return database.GetSkinConductanceMeasurementById(id);
        }

        public List<SkinConductanceMeasurement> GetMeasurementsByPatientId(Guid patientId)
        {
            return database.GetSkinConductanceMeasurementsByPatientId(patientId);
        }

        //Insert
        public void InsertMeasurement(SkinConductanceMeasurement measurement)
        {
            database.InsertSkinConductanceMeasurement(measurement);
        }

        //Update
        public void UpdateMeasurement(SkinConductanceMeasurement measurement)
        {
            database.UpdateSkinConductanceMeasurement(measurement);
        }

        //Delete collection
        public void DeleteMeasurementsByWearableId(Guid wearableId)
        {
            database.DeleteSkinConductanceMeasurementsByWearableId(wearableId);
        }

        //Delete singular
        public void DeleteMeasurementById(Guid measurementId)
        {
            database.DeleteSkinConductanceMeasurementById(measurementId);
        }
    }
}
