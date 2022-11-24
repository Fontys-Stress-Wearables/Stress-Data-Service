using StressDataService.Dtos;
using StressDataService.Models;

namespace StressDataService.Interfaces;

public interface IHrvMeasurementRepository
{
    public Task<IEnumerable<HrvMeasurementDto>> GetAll();
    public Task<HrvMeasurementDto?> GetById(Guid id);
    public Task<IEnumerable<HrvMeasurementDto>> GetByPatientId(Guid patientId);
    public Task<IEnumerable<HrvMeasurementDto>> GetByPatientIdAndDate(Guid patientId, DateTime date);
    public Task<IEnumerable<HrvMeasurementDto>> GetByPatientIdAndTimespan(Guid patientId, DateTime startTime,
        DateTime endTime);
    public Task<IEnumerable<HrvMeasurementDto>> GetByWearableIdAndTimespan(Guid wearableId, DateTime startTime,
        DateTime endTime);
    public void Create(HrvMeasurement hrvMeasurement);
    public void Update(HrvMeasurement hrvMeasurement);
    public Task Delete(Guid id);
}