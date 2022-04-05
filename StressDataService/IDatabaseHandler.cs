using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StressDataService.Models;

namespace StressDataService
{
    public interface IDatabaseHandler
    {
        #region StressMeasurements

        //Get collection
        public List<StressMeasurement> GetAllStressMeasurements();
        public List<StressMeasurement> GetStressMeasurementsByWearableId(Guid wearableId);
        public List<StressMeasurement> GetStressMeasurementsWithinTimePeriodByWearableId(DateTime periodStartTime, DateTime periodEndTime, Guid wearableId);

        //Get singular
        public StressMeasurement GetStressMeasurementById(Guid id);

        //Insert
        public void InsertStressMeasurement(StressMeasurement measurement);

        //Update
        public void UpdateStressMeasurement(StressMeasurement measurement);

        //Delete collection
        public void DeleteStressMeasurementsByWearableId(Guid wearableId);

        //Delete singular
        public void DeleteStressMeasurementById(Guid id);

        #endregion

        #region HeartRateMeasurements
        //Get collection
        public List<HeartRateMeasurement> GetAllHeartRateMeasurements();
        public List<HeartRateMeasurement> GetHeartRateMeasurementsByWearableId(Guid wearableId);
        public List<HeartRateMeasurement> GetHeartRateMeasurementsWithinTimePeriodByWearableId(DateTime periodStartTime, DateTime periodEndTime, Guid wearableId);

        //Get singular
        public HeartRateMeasurement GetHeartRateMeasurementById(Guid id);

        //Insert
        public void InsertHeartRateMeasurement(HeartRateMeasurement measurement);

        //Update
        public void UpdateHeartRateMeasurement(HeartRateMeasurement measurement);

        //Delete collection
        public void DeleteHeartRateMeasurementsByWearableId(Guid wearableId);

        //Delete singular
        public void DeleteHeartRateMeasurementById(Guid id);

        #endregion

        #region SkinConductanceMeasurements
        //Get collection
        public List<SkinConductanceMeasurement> GetAllSkinConductanceMeasurements();
        public List<SkinConductanceMeasurement> GetSkinConductanceMeasurementsByWearableId(Guid wearableId);
        public List<SkinConductanceMeasurement> GetSkinConductanceMeasurementsWithinTimePeriodByWearableId(DateTime periodStartTime, DateTime periodEndTime, Guid wearableId);

        //Get singular
        public SkinConductanceMeasurement GetSkinConductanceMeasurementById(Guid id);

        //Insert
        public void InsertSkinConductanceMeasurement(SkinConductanceMeasurement measurement);
        
        //Update
        public void UpdateSkinConductanceMeasurement(SkinConductanceMeasurement measurement);

        //Delete collection
        public void DeleteSkinConductanceMeasurementsByWearableId(Guid wearableId);

        //Delete singular
        public void DeleteSkinConductanceMeasurementById(Guid id);
        #endregion

        #region SkinTemperatureMeasurements
        //Get collection
        public List<SkinTemperatureMeasurement> GetAllSkinTemperatureMeasurements();
        public List<SkinTemperatureMeasurement> GetSkinTemperatureMeasurementsByWearableId(Guid wearableId);
        public List<SkinTemperatureMeasurement> GetSkinTemperatureMeasurementsWithinTimePeriodByWearableId(DateTime periodStartTime, DateTime periodEndTime, Guid wearableId);

        //Get singular
        public SkinTemperatureMeasurement GetSkinTemperatureMeasurementById(Guid id);

        //Insert
        public void InsertSkinTemperatureMeasurement(SkinTemperatureMeasurement measurement);

        //Update
        public void UpdateSkinTemperatureMeasurement(SkinTemperatureMeasurement measurement);

        //Delete collection
        public void DeleteSkinTemperatureMeasurementsByWearableId(Guid wearableId);

        //Delete singular
        public void DeleteSkinTemperatureMeasurementById(Guid id);
        #endregion

        #region Patients
        public List<Patient> GetPatients();
        public Patient GetPatientById(Guid id);
        public void InsertPatient(Patient patient);
        #endregion

    }
}
