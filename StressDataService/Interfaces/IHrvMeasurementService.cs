using StressDataService.Dtos;

namespace StressDataService.Interfaces;

public interface IHrvMeasurementService
{
    public Task<IEnumerable<HrvMeasurementDto>> GetAll();
    public Task<HrvMeasurementDto?> GetById(Guid id);
    public Task<IEnumerable<HrvMeasurementDto>> GetByPatientId(Guid patientId);
    public Task<IEnumerable<HrvMeasurementDto>> GetByPatientIdAndDate(Guid patientId, DateTime date);
    public Task<IEnumerable<HrvMeasurementDto>> GetByPatientIdAndTimespan(Guid patientId, DateTime startTime,
        DateTime endTime);
    public Task<IEnumerable<HrvMeasurementDto>> GetByWearableIdAndTimespan(Guid wearableId, DateTime startTime,
        DateTime endTime);

    public HrvMeasurementDto Create(CreateHrvMeasurementDto createIssueDto);
    public Task<HrvMeasurementDto> Update(Guid id, UpdateHrvMeasurementDto updateIssueDto);
    public Task<HrvMeasurementDto> Delete(Guid id);
}