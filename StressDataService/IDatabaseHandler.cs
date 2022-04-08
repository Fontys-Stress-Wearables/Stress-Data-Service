using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StressDataService.Models;

namespace StressDataService
{
    public interface IDatabaseHandler
    {
        #region HeartRateVariabilityMeasurements
        //Get collection
        public List<HeartRateVariabilityMeasurement> GetAllHeartRateVariabilityMeasurements();
        public List<HeartRateVariabilityMeasurement> GetHeartRateVariabilityMeasurementsByWearableId(Guid wearableId);
        public List<HeartRateVariabilityMeasurement> GetHeartRateVariabilityMeasurementsWithinTimePeriodByWearableId(DateTime periodStartTime, DateTime periodEndTime, Guid wearableId);
        public List<HeartRateVariabilityMeasurement> GetHeartRateVariabilityMeasurementsByPatientId(Guid patientId);


        //Get singular
        public HeartRateVariabilityMeasurement GetHeartRateVariabilityMeasurementById(Guid id);

        //Insert
        public void InsertHeartRateVariabilityMeasurement(HeartRateVariabilityMeasurement measurement);

        //Update
        public void UpdateHeartRateVariabilityMeasurement(HeartRateVariabilityMeasurement measurement);

        //Delete collection
        public void DeleteHeartRateVariabilityMeasurementsByWearableId(Guid wearableId);

        //Delete singular
        public void DeleteHeartRateVariabilityMeasurementById(Guid id);

        #endregion

        #region Patients
        public List<Patient> GetPatients();
        public Patient GetPatientById(Guid id);
        public void InsertPatient(Patient patient);
        #endregion

    }
}
