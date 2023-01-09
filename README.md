
# Stress-Data-Service
The Stress Data Service is responsible for handling stress data and communicating with the stress measurements database.

The processed data should be received from the wearables or Stress Algorithm Service, but this connection is currently not implemented.

This data is used by the Caregiver Dashboard to retrieve this stress data.

## Running The Service
To run the service, you may use the command:
```
docker compose up
```
This will start both the service and the database.

## Ports: 5031 & 8086
* The Stress Data Service runs on port **5031**.
* The corresponding database runs on port **8086**.

## Database
The Stress Data Service uses [InfluxDB](https://www.influxdata.com/) to store its data, which is a time series database that excels at time based queries. 

**It is highly recommended to start the database through docker compose, as it will be pre-configured.**

To run the database manually, you can execute the following docker command.
```
docker run -p 8086:8086 influxdb
```
Then go to http://localhost:8086, and set up credentials. Create an organisation with a bucket named "StressData" and generate an API token with read/write access.

The credentials that need to be added to the secrets, appsettings.json file or the environment are:
```
"database": {
    "connectionString": "http://localhost:8086", //Or another port if you configure it another way
    "token": "{Token of the database}",
    "org": "SWSP", //Or whatever you name the organisation
    "bucket": "StressData"
  }
  ```
  Afterwards, you should be able to run everything
## API endpoints
```
[HttpGet] GetAll
/HrvMeasurements

[HttpGet] GetById(Guid id)
/HrvMeasurements/{id}

[HttpGet] GetByPatientId(Guid patientId)
/HrvMeasurements/patient/{patientId}

[HttpGet] GetByPatientIdAndDate(Guid patientId, DateTime date)
/HrvMeasurements/date/{patientId}

[HttpGet] GetByPatientIdAndTimespan(Guid patientId, DateTime startTime, DateTime endTime)
/HrvMeasurements/patient/{patientId}/timespan

[HttpPost] Create(CreateHrvMeasurementDto measurementDto)
/HrvMeasurements

[HttpPut] Update(Guid id, UpdateHrvMeasurementDto measurementDto)
/HrvMeasurements
```

The following endpoint exist for development
```
[HttpGet] SimulateNats
/HrvMeasurements/nats/simulate
```

Lastly, these endpoints are not implemented.
```
[HttpDelete] Delete
/HrvMeasurements

[HttpGet] GetStressedPatients
/patients/stressed/{belowValue}
```


## Nats
The service subscribes to the following topics:
```
stress:created
```
## Docker
If you want to manually build a Docker container of this service and running, use the following commands in a CLI:
```
docker build -t stress_data_service --name StressDataService .
```
Then
```
docker run -p 5031:80 --network=swsp stress_data_service
```
