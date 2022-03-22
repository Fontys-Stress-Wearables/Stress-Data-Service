using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StressDataService.Models
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
        }
    }
}
