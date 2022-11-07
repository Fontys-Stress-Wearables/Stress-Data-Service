using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace StressDataService.Dtos;

public record HrvMeasurementDto(
    [Required] Guid Id,
    [Required] Guid PatientId,
    [Required] Guid WearableId, 
    [Required] DateTime TimeStamp, 
    [Required] float HeartRateVariability
);