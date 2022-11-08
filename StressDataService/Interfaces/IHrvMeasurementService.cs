﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StressDataService.Dtos;

namespace StressDataService.Interfaces;

// ToDO Implementation
public interface IHrvMeasurement
{
    public Task<IEnumerable<HrvMeasurementDto>> GetAll();
    public Task<HrvMeasurementDto?> GetById(Guid id);
    public Task<HrvMeasurementDto?> Create(CreateHrvMeasurementDto createIssueDto);
    public Task<HrvMeasurementDto?> Update(Guid id, UpdateHrvMeasurementDto updateIssueDto);
    public Task<HrvMeasurementDto?> Delete(Guid id);
}