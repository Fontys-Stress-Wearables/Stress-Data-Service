using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StressDataService.Models;

namespace StressDataService
{
    public class MockDatabase : IDatabaseHandler
    {
        public List<HeartRateMeasurement> HeartRateMeasurements { get; }
        public List<SkinConductanceMeasurement> SkinConductanceMeasurements { get; }
        public List<SkinTemperatureMeasurement> SkinTemperatureMeasurements { get; }
        public List<StressMeasurement> StressMeasurements { get; }
        public List<Patient> Patients { get; }
        public List<Wearable> Wearables { get; }

        public MockDatabase()
        {
            HeartRateMeasurements = new List<HeartRateMeasurement>();
            SkinConductanceMeasurements = new List<SkinConductanceMeasurement>();
            SkinTemperatureMeasurements = new List<SkinTemperatureMeasurement>();
            StressMeasurements = new List<StressMeasurement>();

            Patients = new List<Patient>();
            Wearables = new List<Wearable>();

            Seed();
        }

        private void Seed()
        {
            Guid wearableId = Guid.NewGuid();
            DateTime timeStamp = DateTime.Now;
            for (int i = 0; i < 100; i++)
            {
                timeStamp = timeStamp.AddSeconds(1);
                Random random = new Random();
                int heartRate = random.Next(60, 200);
                int heartbeatInterval = Convert.ToInt32(heartRate * 1000 / 60);
                int heartRateVariability = random.Next(10, 90);
                HeartRateMeasurements.Add(new HeartRateMeasurement(wearableId, timeStamp, heartRate, heartbeatInterval, heartRateVariability));
                if (i == 40)
                {
                    wearableId = Guid.NewGuid();
                }
            }
            wearableId = Guid.NewGuid();
            timeStamp = DateTime.Now;
            for (int i = 0; i < 100; i++)
            {
                timeStamp = timeStamp.AddSeconds(1);
                Random random = new Random();
                float skinConductance = (float)(random.NextDouble() * random.Next(1, 20));
                SkinConductanceMeasurements.Add(new SkinConductanceMeasurement(wearableId, timeStamp, skinConductance));
                if (i == 49)
                {
                    wearableId = Guid.NewGuid();
                }
            }

            wearableId = Guid.NewGuid();
            timeStamp = DateTime.Now;
            for (int i = 0; i < 100; i++)
            {
                timeStamp = timeStamp.AddSeconds(1);
                Random random = new Random();
                float skinTemperature = (float)(random.Next(25, 40) + random.NextDouble());
                SkinTemperatureMeasurements.Add(new SkinTemperatureMeasurement(wearableId, timeStamp, skinTemperature));
                if (i == 69)
                {
                    wearableId = Guid.NewGuid();
                }
            }

            wearableId = Guid.NewGuid();
            timeStamp = DateTime.Now;
            for (int i = 0; i < 100; i++)
            {
                timeStamp = timeStamp.AddSeconds(1);
                Random random = new Random();
                int stressValue = random.Next(0, 100);
                StressMeasurements.Add(new StressMeasurement(wearableId, timeStamp, stressValue));
                if (i == 29)
                {
                    wearableId = Guid.NewGuid();
                }
            }
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
        }

        #region StressMeasurements
        //Get collection
        public List<StressMeasurement> GetAllStressMeasurements()
        {
            return StressMeasurements;
        }

        public List<StressMeasurement> GetStressMeasurementsByWearableId(Guid wearableId)
        {
            IEnumerable<StressMeasurement> measurements =
              from StressMeasurement measurement in StressMeasurements
              where (measurement.WearableId).Equals(wearableId)
              select measurement;
            return measurements.ToList();
        }

        public List<StressMeasurement> GetStressMeasurementsWithinTimePeriodByWearableId(DateTime periodStartTime, DateTime periodEndTime, Guid wearableId)
        {
            IEnumerable<StressMeasurement> measurements =
               from StressMeasurement measurement in StressMeasurements
               where (measurement.WearableId).Equals(wearableId) && measurement.TimeStamp >= periodStartTime && measurement.TimeStamp <= periodEndTime
               select measurement;
            return measurements.ToList();
        }

        //Get singular
        public StressMeasurement GetStressMeasurementById(Guid id)
        {
            IEnumerable<StressMeasurement> measurements =
                from StressMeasurement measurement in StressMeasurements
                where (measurement.Id).Equals(id)
                select measurement;
            return measurements.FirstOrDefault();
        }

        //Insert
        public void InsertStressMeasurement(StressMeasurement measurement)
        {
            StressMeasurements.Add(measurement);
        }

        //Update
        public void UpdateStressMeasurement(StressMeasurement measurement)
        {
            StressMeasurement oldMeasurement = GetStressMeasurementById(measurement.Id);

            if (oldMeasurement == null)
            {
                return;
            }

            int index = StressMeasurements.IndexOf(oldMeasurement);


            StressMeasurements[index] = measurement;
        }

        //Delete collection
        public void DeleteStressMeasurementsByWearableId(Guid wearableId)
        {
            List<StressMeasurement> measurements = GetStressMeasurementsByWearableId(wearableId);

            foreach (StressMeasurement measurement in measurements)
            {
                DeleteStressMeasurementById(measurement.Id);
            }
        }

        //Delete singular
        public void DeleteStressMeasurementById(Guid id)
        {
            StressMeasurement measurement = GetStressMeasurementById(id);

            StressMeasurements.Remove(measurement);
        }
        #endregion

        #region HeartRateMeasurements
        //Get collection
        public List<HeartRateMeasurement> GetAllHeartRateMeasurements()
        {
            return HeartRateMeasurements;
        }

        public List<HeartRateMeasurement> GetHeartRateMeasurementsByWearableId(Guid wearableId)
        {
            IEnumerable<HeartRateMeasurement> measurements =
               from HeartRateMeasurement measurement in HeartRateMeasurements
               where (measurement.WearableId).Equals(wearableId)
               select measurement;
            return measurements.ToList();
        }

        public List<HeartRateMeasurement> GetHeartRateMeasurementsWithinTimePeriodByWearableId(DateTime periodStartTime, DateTime periodEndTime, Guid wearableId)
        {
            IEnumerable<HeartRateMeasurement> measurements =
               from HeartRateMeasurement measurement in HeartRateMeasurements
               where (measurement.WearableId).Equals(wearableId) && measurement.TimeStamp >= periodStartTime && measurement.TimeStamp <= periodEndTime
               select measurement;
            return measurements.ToList();
        }

        //Get singular
        public HeartRateMeasurement GetHeartRateMeasurementById(Guid id)
        {
            IEnumerable<HeartRateMeasurement> measurements =
               from HeartRateMeasurement measurement in HeartRateMeasurements
               where (measurement.Id).Equals(id)
               select measurement;
            return measurements.FirstOrDefault();
        }

        //Insert
        public void InsertHeartRateMeasurement(HeartRateMeasurement measurement)
        {
            HeartRateMeasurements.Add(measurement);
        }

        //Update
        public void UpdateHeartRateMeasurement(HeartRateMeasurement measurement)
        {
            HeartRateMeasurement oldMeasurement = GetHeartRateMeasurementById(measurement.Id);

            if (oldMeasurement == null)
            {
                return;
            }

            int index = HeartRateMeasurements.IndexOf(oldMeasurement);
            HeartRateMeasurements[index] = measurement;
        }

        //Delete collection
        public void DeleteHeartRateMeasurementsByWearableId(Guid wearableId)
        {
            List<HeartRateMeasurement> measurements = GetHeartRateMeasurementsByWearableId(wearableId);

            foreach (HeartRateMeasurement measurement in measurements)
            {
                DeleteHeartRateMeasurementById(measurement.Id);
            }
        }

        //Delete singular
        public void DeleteHeartRateMeasurementById(Guid id)
        {
            HeartRateMeasurement measurement = GetHeartRateMeasurementById(id);

            HeartRateMeasurements.Remove(measurement);
        }
        #endregion

        #region SkinConductanceMeasurements
        //Get collection
        public List<SkinConductanceMeasurement> GetAllSkinConductanceMeasurements()
        {
            return SkinConductanceMeasurements;
        }

        public List<SkinConductanceMeasurement> GetSkinConductanceMeasurementsByWearableId(Guid wearableId)
        {
            return
                (from SkinConductanceMeasurement measurement in SkinConductanceMeasurements
                 where (measurement.WearableId).Equals(wearableId)
                 select measurement).ToList();
        }

        public List<SkinConductanceMeasurement> GetSkinConductanceMeasurementsWithinTimePeriodByWearableId(DateTime periodStartTime, DateTime periodEndTime, Guid wearableId)
        {
            return
                (from SkinConductanceMeasurement measurement in SkinConductanceMeasurements
                 where (measurement.WearableId).Equals(wearableId) && measurement.TimeStamp >= periodStartTime && measurement.TimeStamp <= periodEndTime
                 select measurement).ToList();
        }

        //Get singular
        public SkinConductanceMeasurement GetSkinConductanceMeasurementById(Guid id)
        {
            IEnumerable<SkinConductanceMeasurement> measurements =
                from SkinConductanceMeasurement measurement in SkinConductanceMeasurements
                where (measurement.Id).Equals(id)
                select measurement;
            return measurements.FirstOrDefault();
        }

        //Insert
        public void InsertSkinConductanceMeasurement(SkinConductanceMeasurement measurement)
        {
            SkinConductanceMeasurements.Add(measurement);
        }

        //Update
        public void UpdateSkinConductanceMeasurement(SkinConductanceMeasurement measurement)
        {
            SkinConductanceMeasurement oldMeasurement = GetSkinConductanceMeasurementById(measurement.Id);

            if (oldMeasurement == null)
            {
                return;
            }

            int index = SkinConductanceMeasurements.IndexOf(oldMeasurement);


            SkinConductanceMeasurements[index] = measurement;

        }

        //Delete collection
        public void DeleteSkinConductanceMeasurementsByWearableId(Guid wearableId)
        {
            SkinConductanceMeasurements.RemoveAll(m => m.WearableId.Equals(wearableId));
        }

        //Delete singular
        public void DeleteSkinConductanceMeasurementById(Guid id)
        {
            SkinConductanceMeasurements.Remove(GetSkinConductanceMeasurementById(id));
        }
        #endregion

        #region SkinTemperatureMeasurements
        //Get collection
        public List<SkinTemperatureMeasurement> GetAllSkinTemperatureMeasurements()
        {
            return SkinTemperatureMeasurements;
        }

        public List<SkinTemperatureMeasurement> GetSkinTemperatureMeasurementsByWearableId(Guid wearableId)
        {
            return
                (from SkinTemperatureMeasurement measurement in SkinTemperatureMeasurements
                 where measurement.WearableId.Equals(wearableId)
                 select measurement).ToList();

        }

        public List<SkinTemperatureMeasurement> GetSkinTemperatureMeasurementsWithinTimePeriodByWearableId(DateTime periodStartTime, DateTime periodEndTime, Guid wearableId)
        {
            return
                (from SkinTemperatureMeasurement measurement in SkinTemperatureMeasurements
                 where measurement.WearableId.Equals(wearableId) && measurement.TimeStamp >= periodEndTime && measurement.TimeStamp <= periodEndTime
                 select measurement).ToList();
        }

        //Get singular
        public SkinTemperatureMeasurement GetSkinTemperatureMeasurementById(Guid id)
        {
            IEnumerable<SkinTemperatureMeasurement> measurements =
                from SkinTemperatureMeasurement measurement in SkinTemperatureMeasurements
                where (measurement.Id).Equals(id)
                select measurement;
            return measurements.FirstOrDefault();
        }

        //Insert
        public void InsertSkinTemperatureMeasurement(SkinTemperatureMeasurement measurement)
        {
            SkinTemperatureMeasurements.Add(measurement);
        }

        //Update
        public void UpdateSkinTemperatureMeasurement(SkinTemperatureMeasurement measurement)
        {
            SkinTemperatureMeasurement oldMeasurement = GetSkinTemperatureMeasurementById(measurement.Id);

            if (oldMeasurement == null)
            {
                return;
            }

            int index = SkinTemperatureMeasurements.IndexOf(oldMeasurement);
            SkinTemperatureMeasurements[index] = measurement;

        }

        //Delete collection
        public void DeleteSkinTemperatureMeasurementsByWearableId(Guid wearableId)
        {
            SkinTemperatureMeasurements.RemoveAll(m => m.WearableId.Equals(wearableId));
        }

        //Delete singular
        public void DeleteSkinTemperatureMeasurementById(Guid id)
        {
            SkinTemperatureMeasurements.Remove(GetSkinTemperatureMeasurementById(id));
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
