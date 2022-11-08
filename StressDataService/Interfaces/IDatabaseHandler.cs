using System;
using System.Collections.Generic;
using StressDataService.Dtos;
using StressDataService.Models;

namespace StressDataService.Interfaces
{
    // ToDo This needs a lot of cleaning
    public interface IDatabaseHandler
    {
        public List<StressedPatientDto> GetStressedPatientsBelowValue(int valueBelow);

        public List<Wearable> GetWearables();

        #region HeartRateVariabilityMeasurements
        //Get collection
        public List<HrvMeasurement> GetAllHeartRateVariabilityMeasurements();
        public List<HrvMeasurement> GetHeartRateVariabilityMeasurementsByWearableId(Guid wearableId);
        public List<HrvMeasurement> GetHeartRateVariabilityMeasurementsByPatientIdAndDate(Guid patientId, string date);
        public List<HrvMeasurement> GetHeartRateVariabilityMeasurementsByPatientId(Guid patientId);


        //Get singular
        //public HeartRateVariabilityMeasurement GetHeartRateVariabilityMeasurementById(Guid id);

        //Insert
        public void InsertHeartRateVariabilityMeasurement(HrvMeasurement measurement);

        //Update
        //public void UpdateHeartRateVariabilityMeasurement(HeartRateVariabilityMeasurement measurement);

        //Delete collection
        //public void DeleteHeartRateVariabilityMeasurementsByWearableId(Guid wearableId);

        //Delete singular
        //public void DeleteHeartRateVariabilityMeasurementById(Guid id);

        #endregion

        #region Patients
        public List<Patient> GetPatients();
        public Patient GetPatientById(Guid id);
        public void InsertPatient(Patient patient);
        #endregion

    }
}
