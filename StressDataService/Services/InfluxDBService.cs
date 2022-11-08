using System;
using System.Threading.Tasks;
using InfluxDB.Client;
using InfluxDB.Client.Writes;
using Microsoft.Extensions.Configuration;
using StressDataService.Interfaces;

namespace StressDataService.Services;

public class InfluxDbService
{
    private readonly string _connectionString;
    private readonly string _token;
    private readonly string _bucket;
    private readonly string _org;

    private readonly IDatabaseHandler _mockDatabase;

    public InfluxDbService(IConfiguration configuration, IDatabaseHandler mockDatabase)
    {
        _connectionString = configuration.GetSection("database")["connectionString"];
        _org = configuration.GetSection("database")["org"];
        _bucket = configuration.GetSection("database")["bucket"];
        _token = configuration.GetSection("database")["token"];
        _mockDatabase = mockDatabase;
    }

    public void WritePoint(PointData pointData)
    {
        using var client = InfluxDBClientFactory.Create(_connectionString, _token);
        using var writeApi = client.GetWriteApi();
        writeApi.WritePoint(pointData, _bucket, _org);
    }

    public async Task<T> QueryAsync<T>(Func<QueryApi, Task<T>> action)
    {
        using var client = InfluxDBClientFactory.Create(_connectionString, _token);
        var query = client.GetQueryApi();
        return await action(query);
    }
}