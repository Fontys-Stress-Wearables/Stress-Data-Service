using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Core.Flux.Domain;
using InfluxDB.Client.Writes;
using StressDataService.Dtos;
using StressDataService.Interfaces;
using StressDataService.Models;
using StressDataService.Services;

namespace StressDataService.Repositories;

public class HrvMeasurementRepository : IHrvMeasurementRepository
{
    private readonly InfluxDbService _service;
    private readonly string _org;
    private readonly string _bucket;
    
    public HrvMeasurementRepository(IConfiguration configuration, InfluxDbService service)
    {
        _service = service;
        _org = configuration.GetSection("database")["org"];
        _bucket = configuration.GetSection("database")["bucket"];
    }

    public async Task<IEnumerable<HrvMeasurementDto>> GetAll()
    {
        var results = await _service.QueryAsync(async query =>
        {
            var flux = $"from(bucket:\"{_bucket}\") |> range(start: 0)";
            var tables = await query.QueryAsync(flux, _org);
            return tables.SelectMany(table =>
                table.Records.Select(ToHrvMeasurement)
            );
        });

        return results;
    }
    
    public async Task<HrvMeasurementDto> GetById(Guid id)
    {
        var results = await _service.QueryAsync(async query =>
        {
            var flux = $"from(bucket:\"{_bucket}\") |> range(start: 0)" +
                       $"|> filter(fn: (r) => r.id == \"{id}\")";
            
            var tables = await query.QueryAsync(flux, _org);
            return tables.SelectMany(table =>
                table.Records.Select(ToHrvMeasurement)
            );
        });

        return results.FirstOrDefault();
    }
    
    public async Task<IEnumerable<HrvMeasurementDto>> GetByPatientId(Guid patientId)
    {
        var results = await _service.QueryAsync(async query =>
        {
            var flux = $"from(bucket:\"{_bucket}\") |> range(start: 0)" +
                        $"|> filter(fn: (r) => r.patient_id == \"{patientId}\")";
            
            var tables = await query.QueryAsync(flux, _org);
            return tables.SelectMany(table =>
                table.Records.Select(ToHrvMeasurement)
            );
        });

        return results;
    }
    
    public void Create(HrvMeasurement hrvMeasurement)
    {
        var point = CreatePoint(hrvMeasurement);
        
        _service.WritePoint(point);
    }
    
        
    public void Update(HrvMeasurement hrvMeasurement)
    {
        var point = CreatePoint(hrvMeasurement);
        
        _service.WritePoint(point);
    }
    
    public async Task Delete(Guid measurementId)
    {
        throw new NotImplementedException();
    }

    
    public async Task<IEnumerable<HrvMeasurementDto>> GetByPatientIdAndDate(Guid patientId, DateTime dateTime)
    {
        var results = await _service.QueryAsync(async query =>
        {
            DateTime date = dateTime.Date;
            var flux = $"from(bucket: \"{_bucket}\")" + 
                       " |> range(start: " + date.ToString("yyyy-MM-ddTHH:mm:ssZ") + 
                       ", stop: " + date.AddDays(1).ToString("yyyy-MM-ddTHH:mm:ssZ") + ")" + 
                       $"|> filter(fn: (r) => r.patient_id == \"{patientId}\")";
            
            var tables = await query.QueryAsync(flux, _org);
            return tables.SelectMany(table =>
                table.Records.Select(ToHrvMeasurement)
            );
        });

        return results;
    }
    
    public async Task<IEnumerable<HrvMeasurementDto>> GetByPatientIdAndTimespan(Guid patientId, DateTime startTime, DateTime endTime)
    {
        var results = await _service.QueryAsync(async query =>
        {
            var flux = $"from(bucket: \"{_bucket}\")" +
                       " |> range(start: " + startTime.ToString("yyyy-MM-ddTHH:mm:ssZ") + 
                       ", stop: " + endTime.ToString("yyyy-MM-ddTHH:mm:ssZ") + ")" +
                       $"|> filter(fn: (r) => r.patient_id == \"{patientId}\")";
            
            var tables = await query.QueryAsync(flux, _org);
            return tables.SelectMany(table =>
                table.Records.Select(ToHrvMeasurement)
            );
        });

        return results;
    }
    
    public async Task<IEnumerable<HrvMeasurementDto>> GetByWearableIdAndTimespan(Guid wearableId, DateTime startTime, DateTime endTime)
    {
        var results = await _service.QueryAsync(async query =>
        {
            var flux = $"from(bucket: \"{_bucket}\")" +
                       " |> range(start: " + startTime.ToString("yyyy-MM-ddTHH:mm:ssZ") + 
                       ", stop: " + endTime.ToString("yyyy-MM-ddTHH:mm:ssZ") + ")" +
                       $"|> filter(fn: (r) => r.wearable_id == \"{wearableId}\")";
            
            var tables = await query.QueryAsync(flux, _org);
            return tables.SelectMany(table =>
                table.Records.Select(ToHrvMeasurement)
            );
        });

        return results;
    }

    private PointData CreatePoint(HrvMeasurement measurement)
    {
        return PointData
            .Measurement("mem")
            .Tag("id", measurement.Id.ToString())
            .Tag("wearable_id", measurement.WearableId.ToString())
            .Tag("patient_id", measurement.PatientId.ToString())
            .Field("stress_level", measurement.HeartRateVariability)
            .Timestamp(measurement.TimeStamp, WritePrecision.Ms);
    }
    
    private HrvMeasurementDto ToHrvMeasurement(FluxRecord record)
    {
        return new HrvMeasurementDto(
            Guid.Parse(record.GetValueByKey("id").ToString()),
            Guid.Parse(record.GetValueByKey("patient_id").ToString()),
            Guid.Parse(record.GetValueByKey("wearable_id").ToString()),
            (DateTime) record.GetTimeInDateTime(),
            float.Parse(record.GetValueByKey("_value").ToString())
        );
    }
}