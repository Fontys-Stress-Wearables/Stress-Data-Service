using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StressDataService.Models;

namespace StressDataService
{
    public class MockDatabase
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

        public HeartRateMeasurement FindHeartRateMeasurementById(Guid id)
        {
 /*           foreach(HeartRateMeasurement h in HeartRateMeasurements)
            {
                if (h.Id.Equals(id))
                {
                    return h;
                }
            }
            return default;*/
            //HeartRateMeasurement t = new HeartRateMeasurement();

            IEnumerable<HeartRateMeasurement> measurements =
                from HeartRateMeasurement measurement in HeartRateMeasurements
                where (measurement.Id).Equals(id)
                select measurement;
            return measurements.FirstOrDefault();
        }
        public SkinConductanceMeasurement FindSkinConductanceMeasurementById(Guid id)
        {
            IEnumerable<SkinConductanceMeasurement> measurements =
                from SkinConductanceMeasurement measurement in SkinConductanceMeasurements
                where (measurement.Id).Equals(id)
                select measurement;
            return measurements.FirstOrDefault();
        }
        public SkinTemperatureMeasurement FindSkinTemperatureMeasurementById(Guid id)
        {
            IEnumerable<SkinTemperatureMeasurement> measurements =
                from SkinTemperatureMeasurement measurement in SkinTemperatureMeasurements
                where (measurement.Id).Equals(id)
                select measurement;
            return measurements.FirstOrDefault();
        }
        public StressMeasurement FindStressMeasurementById(Guid id)
        {
            IEnumerable<StressMeasurement> measurements =
                from StressMeasurement measurement in StressMeasurements
                where (measurement.Id).Equals(id)
                select measurement;
            return measurements.FirstOrDefault();
        }

        public void Seed()
        {
            DateTime timeStamp = DateTime.Now;
            for(int i = 0; i < 100; i++)
            {
                timeStamp = timeStamp.AddSeconds(1);
                Random random = new Random();
                int heartRate = random.Next(60, 200);
                int heartbeatInterval = Convert.ToInt32(heartRate * 1000 / 60);
                int heartRateVariability = random.Next(10, 90);
                HeartRateMeasurements.Add(new HeartRateMeasurement(timeStamp, heartRate, heartbeatInterval, heartRateVariability));
            }

            timeStamp = DateTime.Now;
            for (int i = 0; i < 100; i++)
            {
                timeStamp = timeStamp.AddSeconds(1);
                Random random = new Random();
                float skinConductance = (float)(random.NextDouble() * random.Next(1, 20));
                SkinConductanceMeasurements.Add(new SkinConductanceMeasurement(timeStamp, skinConductance));
            }

            timeStamp = DateTime.Now;
            for (int i = 0; i < 100; i++)
            {
                timeStamp = timeStamp.AddSeconds(1);
                Random random = new Random();
                float skinTemperature = (float)(random.Next(25, 40) + random.NextDouble());
                SkinTemperatureMeasurements.Add(new SkinTemperatureMeasurement(timeStamp, skinTemperature));
            }

            timeStamp = DateTime.Now;
            for (int i = 0; i < 100; i++)
            {
                timeStamp = timeStamp.AddSeconds(1);
                Random random = new Random();
                int stressValue = random.Next(0,100);
                StressMeasurements.Add(new StressMeasurement(timeStamp, stressValue));
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

                Patient p = new Patient(maleNames[i], "", lastNames[i%5], DateTime.Now, "test@test.com");
                Patients.Add(p);
            }
        }
    }
}
