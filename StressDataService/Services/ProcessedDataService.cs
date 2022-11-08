using Microsoft.Extensions.DependencyInjection;
using StressDataService.Models;
using StressDataService.Repositories;
using System.Collections.Generic;
using StressDataService.Nats;

namespace StressDataService.Services
{
    public class ProcessedDataService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly HrvMeasurementRepository _repository;

        public ProcessedDataService(IServiceScopeFactory scopeFactory, HrvMeasurementRepository repository, INatsService nats)
        {
            _scopeFactory = scopeFactory;
            _repository = repository;
            
            nats.Subscribe<NatsMessage<List<HrvMeasurement>>>("stress:created", OnNewHRVData);
        }

        private void OnNewHRVData(NatsMessage<List<HrvMeasurement>> message)
        {
            List<HrvMeasurement> hrvData = message.message;

            using (_scopeFactory.CreateScope())
            {
                hrvData.ForEach(data =>
                {
                    _repository.Create(data);
                });
            }
        }
    }
}
