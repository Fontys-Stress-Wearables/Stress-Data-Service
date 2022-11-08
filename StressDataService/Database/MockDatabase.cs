using System;
using System.Collections.Generic;
using System.Linq;
using StressDataService.Dtos;
using StressDataService.Interfaces;
using StressDataService.Models;

namespace StressDataService.Database
{
    // ToDo Clean up
    public class MockDatabase : IDatabaseHandler
    {
        public List<HrvMeasurement> HeartRateVariabilityMeasurements { get; }
        public List<Patient> Patients { get; }
        public List<Wearable> Wearables { get; }


        public MockDatabase()
        {
            HeartRateVariabilityMeasurements = new List<HrvMeasurement>();

            Patients = new List<Patient>();
            Wearables = new List<Wearable>();

            Seed();
        }

        public List<Wearable> GetWearables()
        {
            return Wearables;
        }

        private void Seed()
        {
            string[] maleNames = new string[10] { "aaron", "abdul", "abe", "abel", "abraham", "adam", "adan", "adolfo", "adolph", "adrian" };
            string[] femaleNames = new string[4] { "abby", "abigail", "adele", "adrian" };
            string[] lastNames = new string[5] { "abbott", "acosta", "adams", "adkins", "aguilar" };


            for (int i = 0; i < 10; i++)
            {
                string FirstName;
                Random rand = new Random(DateTime.Now.Second);
                if (rand.Next(1, 2) == 1)
                {
                    FirstName = maleNames[rand.Next(0, maleNames.Length - 1)];
                }
                else
                {
                    FirstName = femaleNames[rand.Next(0, femaleNames.Length - 1)];
                }

                Patient p = new Patient(maleNames[i], "", lastNames[i % 5], DateTime.Now, "test@test.com");
                Patients.Add(p);
            }

            for (int i = 0; i < 10; i++)
            {
                Guid id = Guid.NewGuid();
                Wearables.Add(new Wearable(Patients[i].Id));
            }

            foreach(Wearable wearable in Wearables)
            {
                int daysToGenerateDataFor = 30;

                DateTime timeStamp = DateTime.Today;
                Random random = new Random();
                int HeartRateVariability = random.Next(25, 75);

                for (int i = 0; i < 96 * daysToGenerateDataFor; i++)
                {
                    timeStamp = timeStamp.AddMinutes(15);
                    HeartRateVariabilityMeasurements.Add(
                        new HrvMeasurement {
                            Id = Guid.NewGuid(),
                            PatientId = wearable.PatientId,
                            WearableId = wearable.Id,
                            TimeStamp = timeStamp,
                            HeartRateVariability = HeartRateVariability
                        }
                    );
                    HeartRateVariability += random.Next(-10, 10);
                    if(HeartRateVariability < 20)
                    {
                        HeartRateVariability = random.Next(20, 30);
                    }
                    if(HeartRateVariability > 80)
                    {
                        HeartRateVariability = random.Next(70,80);
                    }
                }
            }
        }

        public List<StressedPatientDto> GetStressedPatientsBelowValue(int valueBelow)
        {
            List<StressedPatientDto> stressedPatientMeasurements = new List<StressedPatientDto>();
            List<StressedPatientDto> AllStressedPatientMeasurements = new List<StressedPatientDto>();

            foreach (HrvMeasurement measurement in HeartRateVariabilityMeasurements)
            {
                if (measurement.HeartRateVariability <= valueBelow)
                {
                    Wearable wearable = Wearables.Find(wearable => wearable.Id == measurement.WearableId);
                    if (wearable != null)
                    {
                        Patient patient = Patients
                            .Find(patient =>
                            patient.Id == wearable.PatientId);
                        if (patient != null)
                        {
                            AllStressedPatientMeasurements.Add(new StressedPatientDto(patient.Id, patient.FirstName, patient.LastNamePrefix, patient.LastName, measurement.HeartRateVariability, measurement.TimeStamp));
                        }
                    }
                }
            }
            foreach (Patient patient in Patients)
            {
                List<StressedPatientDto> patientMeasurements = AllStressedPatientMeasurements.FindAll(measurement => measurement.PatientId == patient.Id);
                if (patientMeasurements != null && patientMeasurements.Count != 0)
                {
                    StressedPatientDto lowestMeasurement = patientMeasurements.First();
                    foreach (StressedPatientDto stressedPatientMeasurement in patientMeasurements)
                    {
                        if(stressedPatientMeasurement.HeartRateVariability < lowestMeasurement.HeartRateVariability)
                        {
                            lowestMeasurement = stressedPatientMeasurement;
                        }
                    }
                    stressedPatientMeasurements.Add(lowestMeasurement);
                }
            }
            return stressedPatientMeasurements;
        }

        #region HeartRateVariabilityMeasurements
        //Get collection
        public List<HrvMeasurement> GetAllHeartRateVariabilityMeasurements()
        {
            return HeartRateVariabilityMeasurements;
        }

        public List<HrvMeasurement> GetHeartRateVariabilityMeasurementsByWearableId(Guid wearableId)
        {
            IEnumerable<HrvMeasurement> measurements =
               from HrvMeasurement measurement in HeartRateVariabilityMeasurements
               where (measurement.WearableId).Equals(wearableId)
               select measurement;
            return measurements.ToList();
        }

        public List<HrvMeasurement> GetHeartRateVariabilityMeasurementsByPatientIdAndDate(Guid patientId, string date)
        {
            IEnumerable<HrvMeasurement> measurements =
               from HrvMeasurement measurement in HeartRateVariabilityMeasurements
               where (measurement.PatientId).Equals(patientId) && measurement.TimeStamp.ToString("dd-MM-yyyy").Contains(date)
               select measurement;
            List<HrvMeasurement> measurements2 = new List<HrvMeasurement>();
            measurements2 = measurements.ToList();
            measurements2.Count();
            return measurements.ToList();
        }

        public List<HrvMeasurement> GetHeartRateVariabilityMeasurementsByPatientId(Guid patientId)
        {
            List<Wearable> wearables = Wearables.FindAll(w => w.PatientId == patientId);
            List<HrvMeasurement> measurements = new List<HrvMeasurement>();

            foreach (Wearable wearable in wearables)
            {
                IEnumerable<HrvMeasurement> measurementsForWearable =
                from HrvMeasurement measurement in HeartRateVariabilityMeasurements
                where (measurement.WearableId).Equals(wearable.Id)
                select measurement;

                foreach (HrvMeasurement measurement in measurementsForWearable)
                {
                    measurements.Add(measurement);
                }
            }
            return measurements;
        }


        //Get singular
        /*public HeartRateVariabilityMeasurement GetHeartRateVariabilityMeasurementById(Guid id)
        {
            IEnumerable<HeartRateVariabilityMeasurement> measurements =
               from HeartRateVariabilityMeasurement measurement in HeartRateVariabilityMeasurements
               where (measurement.Id).Equals(id)
               select measurement;
            return measurements.FirstOrDefault();
        }*/

        //Insert
        public void InsertHeartRateVariabilityMeasurement(HrvMeasurement measurement)
        {
            HeartRateVariabilityMeasurements.Add(measurement);
        }

        //Update
        /*public void UpdateHeartRateVariabilityMeasurement(HeartRateVariabilityMeasurement measurement)
        {
            HeartRateVariabilityMeasurement oldMeasurement = GetHeartRateVariabilityMeasurementById(measurement.Id);

            if (oldMeasurement == null)
            {
                return;
            }

            int index = HeartRateVariabilityMeasurements.IndexOf(oldMeasurement);
            HeartRateVariabilityMeasurements[index] = measurement;
        }*/

        //Delete collection
        /*public void DeleteHeartRateVariabilityMeasurementsByWearableId(Guid wearableId)
        {
            List<HeartRateVariabilityMeasurement> measurements = GetHeartRateVariabilityMeasurementsByWearableId(wearableId);

            foreach (HeartRateVariabilityMeasurement measurement in measurements)
            {
                DeleteHeartRateVariabilityMeasurementById(measurement.Id);
            }
        }*/

        //Delete singular
        /*public void DeleteHeartRateVariabilityMeasurementById(Guid id)
        {
            HeartRateVariabilityMeasurement measurement = GetHeartRateVariabilityMeasurementById(id);

            HeartRateVariabilityMeasurements.Remove(measurement);
        }*/
        #endregion

        #region Patients
        public List<Patient> GetPatients()
        {
            return Patients;
        }
        public Patient GetPatientById(Guid patientId)
        {
            IEnumerable<Patient> patients =
                from Patient patient in Patients
                where (patient.Id).Equals(patientId)
                select patient;
            return patients.FirstOrDefault();
        }

        public void InsertPatient(Patient patient)
        {
            Patients.Add(patient);
        }
        #endregion
    }
}
