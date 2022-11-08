﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StressDataService.Dtos;
using StressDataService.Models;
using StressDataService.Repositories;

namespace StressDataService.Services;

public class HrvMeasurementService
{
    private readonly HrvMeasurementRepository _hrvRepository;
    private readonly INatsService _nats;

    
    public HrvMeasurementService(HrvMeasurementRepository hrvRepository, INatsService nats)
    {
        _hrvRepository = hrvRepository;
        _nats = nats;
    }
    
    public async Task<IEnumerable<HrvMeasurementDto>> GetAll()
    {
        return await _hrvRepository.GetAll();
    }
    
    public async Task<HrvMeasurementDto> GetById(Guid id)
    {
        var sprint = await _hrvRepository.GetById(id);

        return sprint;
    }
    
    public async Task<IEnumerable<HrvMeasurementDto>> GetByPatientId(Guid patientId)
    {
        try
        {
            var measurements = await _hrvRepository.GetByPatientId(patientId);
            _nats.Publish("th_logs", "Stress measurements were retrieved for patientId: " + patientId);
            return measurements;
        } 
        catch (Exception ex)
        {
            _nats.Publish("th_warnings", 
                "Something went wrong when attempting to get stress measurements for patientId: " 
                + patientId + " - " + ex.Message);
            return null;
        }
    }
    
    public async Task<IEnumerable<HrvMeasurementDto>> GetByPatientIdAndDate(Guid patientId, DateTime date)
    {
        try
        {
            var measurements = await _hrvRepository.GetByPatientIdAndDate(patientId, date);
            return measurements;
        }
        catch (Exception ex)
        {
            _nats.Publish("th_warnings", 
                "Something went wrong when attempting to get stress measurements for patientId: " 
                + patientId + " on date: " + date + " - " + ex.Message);
            return null;
        }
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