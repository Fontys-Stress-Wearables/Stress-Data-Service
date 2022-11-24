using StressDataService.Models;
using StressDataService.Repositories;
using StressDataService.Interfaces;
using StressDataService.Nats;

namespace StressDataService.Services
{
    public class ProcessedDataService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IHrvMeasurementRepository _repository;

        public ProcessedDataService(IServiceScopeFactory scopeFactory, IHrvMeasurementRepository repository, INatsService nats)
        {
            _scopeFactory = scopeFactory;
            _repository = repository;
            
            nats.Subscribe<NatsMessage<List<HrvMeasurement>>>("stress:created", OnNewHRVData);
        }

        private void OnNewHRVData(NatsMessage<List<HrvMeasurement>> message)
        {
            List<HrvMeasurement> hrvData = message.Message;

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
