using StressDataService.Dtos;
using StressDataService.Interfaces;
using StressDataService.Models;
using StressDataService.Repositories;

namespace StressDataService.Services;

public class HrvMeasurementService : IHrvMeasurementService
{
    private readonly HrvMeasurementRepository _hrvRepository;


    public HrvMeasurementService(HrvMeasurementRepository hrvRepository)
    {
        _hrvRepository = hrvRepository;
    }
    
    public async Task<IEnumerable<HrvMeasurementDto>> GetAll()
    {
        return await _hrvRepository.GetAll();
    }
    
    public async Task<HrvMeasurementDto?> GetById(Guid id)
    {
        var sprint = await _hrvRepository.GetById(id);

        return sprint;
    }
    
    public async Task<IEnumerable<HrvMeasurementDto>> GetByPatientId(Guid patientId)
    {
        var measurements = await _hrvRepository.GetByPatientId(patientId);
        
        return measurements;
    }
    
    public async Task<IEnumerable<HrvMeasurementDto>> GetByPatientIdAndDate(Guid patientId, DateTime date)
    {
       var measurements = await _hrvRepository.GetByPatientIdAndDate(patientId, date);
       
       return measurements;
    }
    
    public async Task<IEnumerable<HrvMeasurementDto>> GetByPatientIdAndTimespan(Guid patientId, DateTime startTime, DateTime endTime)
    {
        return await _hrvRepository.GetByPatientIdAndTimespan(patientId, startTime, endTime);
    }
    
    public async Task<IEnumerable<HrvMeasurementDto>> GetByWearableIdAndTimespan(Guid wearableId, DateTime startTime, DateTime endTime)
    {
        return await _hrvRepository.GetByWearableIdAndTimespan(wearableId, startTime, endTime);
    }
    
    public HrvMeasurementDto Create(CreateHrvMeasurementDto createHrvMeasurementDto)
    {
        var hrvMeasurement = new HrvMeasurement {
            Id = Guid.NewGuid(),
            PatientId = createHrvMeasurementDto.PatientId,
            WearableId = createHrvMeasurementDto.WearableId,
            TimeStamp = createHrvMeasurementDto.TimeStamp,
            HeartRateVariability = createHrvMeasurementDto.HeartRateVariability
        };
        
        _hrvRepository.Create(hrvMeasurement);

        return hrvMeasurement.AsDto();
    }

    public async Task<HrvMeasurementDto> Update(Guid id, UpdateHrvMeasurementDto updateIssueDto)
    {
        var measurement = await _hrvRepository.GetById(id);
        
        if (measurement == null) return null!;
        
        var hrvMeasurement = new HrvMeasurement {
            Id = measurement.Id,
            PatientId = measurement.PatientId,
            WearableId = measurement.WearableId,
            TimeStamp = measurement.TimeStamp,
            HeartRateVariability = updateIssueDto.HeartRateVariability
        };
        
        _hrvRepository.Update(hrvMeasurement);

        return hrvMeasurement.AsDto();
    }

    public async Task<HrvMeasurementDto> Delete(Guid id)
    {
        var measurement = await _hrvRepository.GetById(id);

        if (measurement == null) return null;

        await _hrvRepository.Delete(measurement.Id);

        return measurement;
    }
}