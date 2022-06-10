using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StressDataService.Models;

namespace StressDataService
{
    public class MockDatabase : IDatabaseHandler
    {
        public List<HeartRateVariabilityMeasurement> HeartRateVariabilityMeasurements { get; }
        public List<Patient> Patients { get; }
        public List<Wearable> Wearables { get; }


        public MockDatabase()
        {
            HeartRateVariabilityMeasurements = new List<HeartRateVariabilityMeasurement>();

            Patients = new List<Patient>();
            Wearables = new List<Wearable>();

            Seed();
        }

        public static List<Wearable> getWearables()
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
                    HeartRateVariabilityMeasurements.Add(new HeartRateVariabilityMeasurement(wearable.PatientId, wearable.Id, timeStamp, HeartRateVariability));
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

        public List<StressedPatientDTO> GetStressedPatientsBelowValue(int valueBelow)
        {
            List<StressedPatientDTO> stressedPatientMeasurements = new List<StressedPatientDTO>();
            List<StressedPatientDTO> AllStressedPatientMeasurements = new List<StressedPatientDTO>();

            foreach (HeartRateVariabilityMeasurement measurement in HeartRateVariabilityMeasurements)
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
                            AllStressedPatientMeasurements.Add(new StressedPatientDTO(patient.Id, patient.FirstName, patient.LastNamePrefix, patient.LastName, measurement.HeartRateVariability, measurement.TimeStamp));
                        }
                    }
                }
            }
            foreach (Patient patient in Patients)
            {
                List<StressedPatientDTO> patientMeasurements = AllStressedPatientMeasurements.FindAll(measurement => measurement.PatientId == patient.Id);
                if (patientMeasurements != null && patientMeasurements.Count != 0)
                {
                    StressedPatientDTO lowestMeasurement = patientMeasurements.First();
                    foreach (StressedPatientDTO stressedPatientMeasurement in patientMeasurements)
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
        public List<HeartRateVariabilityMeasurement> GetAllHeartRateVariabilityMeasurements()
        {
            return HeartRateVariabilityMeasurements;
        }

        public List<HeartRateVariabilityMeasurement> GetHeartRateVariabilityMeasurementsByWearableId(Guid wearableId)
        {
            IEnumerable<HeartRateVariabilityMeasurement> measurements =
               from HeartRateVariabilityMeasurement measurement in HeartRateVariabilityMeasurements
               where (measurement.WearableId).Equals(wearableId)
               select measurement;
            return measurements.ToList();
        }

        public List<HeartRateVariabilityMeasurement> GetHeartRateVariabilityMeasurementsByPatientIdAndDate(Guid patientId, string date)
        {
            IEnumerable<HeartRateVariabilityMeasurement> measurements =
               from HeartRateVariabilityMeasurement measurement in HeartRateVariabilityMeasurements
               where (measurement.PatientId).Equals(patientId) && measurement.TimeStamp.ToString("dd-MM-yyyy").Contains(date)
               select measurement;
            List<HeartRateVariabilityMeasurement> measurements2 = new List<HeartRateVariabilityMeasurement>();
            measurements2 = measurements.ToList();
            measurements2.Count();
            return measurements.ToList();
        }

        public List<HeartRateVariabilityMeasurement> GetHeartRateVariabilityMeasurementsByPatientId(Guid patientId)
        {
            List<Wearable> wearables = Wearables.FindAll(w => w.PatientId == patientId);
            List<HeartRateVariabilityMeasurement> measurements = new List<HeartRateVariabilityMeasurement>();

            foreach (Wearable wearable in wearables)
            {
                IEnumerable<HeartRateVariabilityMeasurement> measurementsForWearable =
                from HeartRateVariabilityMeasurement measurement in HeartRateVariabilityMeasurements
                where (measurement.WearableId).Equals(wearable.Id)
                select measurement;

                foreach (HeartRateVariabilityMeasurement measurement in measurementsForWearable)
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
        public void InsertHeartRateVariabilityMeasurement(HeartRateVariabilityMeasurement measurement)
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
