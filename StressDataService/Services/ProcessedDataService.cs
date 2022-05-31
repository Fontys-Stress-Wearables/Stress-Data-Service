using StressDataService.Models;
using StressDataService.Repositories;
using System.Collections.Generic;

namespace StressDataService.Services
{
    public class ProcessedDataService
    {
        private readonly INatsService nats;
        private readonly HeartRateVariabilityMeasurementsRepository repository;

        public ProcessedDataService(INatsService nats, HeartRateVariabilityMeasurementsRepository repository)
        {
            this.nats = nats;
            this.repository = repository;
            this.nats.Subscribe<NatsMessage<List<HeartRateVariabilityMeasurement>>>("stress:created", OnNewHRVData);
        }

        public void OnNewHRVData(NatsMessage<List<HeartRateVariabilityMeasurement>> message)
        {
            List<HeartRateVariabilityMeasurement> hrvData = message.message;

            hrvData.ForEach(data =>
            {
                repository.InsertMeasurement(data);
            });
        }
    }
}
