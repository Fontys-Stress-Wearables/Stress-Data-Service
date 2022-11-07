using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace StressDataService.Dtos;

public record UpdateHrvMeasurementDto(
    [Required] float HeartRateVariability
);