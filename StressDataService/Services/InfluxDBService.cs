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
    private readonly string _org;
    private readonly string _bucket;
    private readonly string _username;
    private readonly char[] _password;

    
    public InfluxDbService(IConfiguration configuration)
    {
        _connectionString = configuration.GetSection("database")["connectionString"];
        _org = configuration.GetSection("database")["org"];
        _bucket = configuration.GetSection("database")["bucket"];
        _username = configuration.GetSection("database")["username"];
        _password = configuration.GetSection("database")["password"].ToCharArray();
    }

    public void WritePoint(PointData pointData)
    {
        using var client = InfluxDBClientFactory.Create(_connectionString, _username, _password);
        using var writeApi = client.GetWriteApi();
        writeApi.WritePoint(pointData, _bucket, _org);
    }

    public async Task<T> QueryAsync<T>(Func<QueryApi, Task<T>> action)
    {
        using var client = InfluxDBClientFactory.Create(_connectionString, _username, _password);
        var query = client.GetQueryApi();
        return await action(query);
    }
}