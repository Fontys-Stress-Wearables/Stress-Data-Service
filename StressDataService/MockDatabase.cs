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

            DateTime timeStamp = DateTime.Now;
            for (int i = 0; i < 100000; i++)
            {
                timeStamp = timeStamp.AddSeconds(1);
                Random random = new Random();
                int HeartRateVariability = random.Next(200, 1200);
                HeartRateVariabilityMeasurements.Add(new HeartRateVariabilityMeasurement(Wearables[random.Next(0,10)].Id, timeStamp, HeartRateVariability));
            }
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

        public List<HeartRateVariabilityMeasurement> GetHeartRateVariabilityMeasurementsWithinTimePeriodByWearableId(DateTime periodStartTime, DateTime periodEndTime, Guid wearableId)
        {
            IEnumerable<HeartRateVariabilityMeasurement> measurements =
               from HeartRateVariabilityMeasurement measurement in HeartRateVariabilityMeasurements
               where (measurement.WearableId).Equals(wearableId) && measurement.TimeStamp >= periodStartTime && measurement.TimeStamp <= periodEndTime
               select measurement;
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
        public HeartRateVariabilityMeasurement GetHeartRateVariabilityMeasurementById(Guid id)
        {
            IEnumerable<HeartRateVariabilityMeasurement> measurements =
               from HeartRateVariabilityMeasurement measurement in HeartRateVariabilityMeasurements
               where (measurement.Id).Equals(id)
               select measurement;
            return measurements.FirstOrDefault();
        }

        //Insert
        public void InsertHeartRateVariabilityMeasurement(HeartRateVariabilityMeasurement measurement)
        {
            HeartRateVariabilityMeasurements.Add(measurement);
        }

        //Update
        public void UpdateHeartRateVariabilityMeasurement(HeartRateVariabilityMeasurement measurement)
        {
            HeartRateVariabilityMeasurement oldMeasurement = GetHeartRateVariabilityMeasurementById(measurement.Id);

            if (oldMeasurement == null)
            {
                return;
            }

            int index = HeartRateVariabilityMeasurements.IndexOf(oldMeasurement);
            HeartRateVariabilityMeasurements[index] = measurement;
        }

        //Delete collection
        public void DeleteHeartRateVariabilityMeasurementsByWearableId(Guid wearableId)
        {
            List<HeartRateVariabilityMeasurement> measurements = GetHeartRateVariabilityMeasurementsByWearableId(wearableId);

            foreach (HeartRateVariabilityMeasurement measurement in measurements)
            {
                DeleteHeartRateVariabilityMeasurementById(measurement.Id);
            }
        }

        //Delete singular
        public void DeleteHeartRateVariabilityMeasurementById(Guid id)
        {
            HeartRateVariabilityMeasurement measurement = GetHeartRateVariabilityMeasurementById(id);

            HeartRateVariabilityMeasurements.Remove(measurement);
        }
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
