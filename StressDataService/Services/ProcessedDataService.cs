using Microsoft.Extensions.DependencyInjection;
using StressDataService.Models;
using StressDataService.Repositories;
using System.Collections.Generic;

namespace StressDataService.Services
{
    public class ProcessedDataService
    {
        private readonly INatsService nats;
        private readonly IServiceScopeFactory _scopeFactory;

        public ProcessedDataService(INatsService nats, IServiceScopeFactory scopeFactory)
        {
            this.nats = nats;
            this._scopeFactory = scopeFactory;
            this.nats.Subscribe<NatsMessage<List<HeartRateVariabilityMeasurement>>>("stress:created", OnNewHRVData);
        }

        public void OnNewHRVData(NatsMessage<List<HeartRateVariabilityMeasurement>> message)
        {
            List<HeartRateVariabilityMeasurement> hrvData = message.message;

            using (var Scope = _scopeFactory.CreateScope())
            {
                HeartRateVariabilityMeasurementsRepository repository = new HeartRateVariabilityMeasurementsRepository(Scope.ServiceProvider.GetRequiredService<InfluxDBHandler>());
                hrvData.ForEach(data =>
                {
                    repository.InsertMeasurement(data);
                });
            }
        }
    }
}
