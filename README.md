# Stress-Data-Service
## Port: 5031
The service that is responsible for handling stress data and communicating with the stress measurements database.
This service gets processed stress data from the Stress Algorithm Service, stores it in a database and is used by the Caregiver Dashboard to retrieve this stress data.

## Database
To run the database locally, you need to have docker running, and execute the command:
```
docker run -p 8086:8086 influxdb
```
Or if you use a docker compose, add this to the compose file:
```
version: '3.9'
services:
  stress-data-influxdb:
    image: influxdb:latest
    ports:
      - "8086:8086"
    expose:
      - "8086"
    environment:
      database_connectionString: ${DATABASE_CONNECTIONSTRING}
      database_token: ${DATABASE_TOKEN}
      database_org: "SWSP"
      database_bucket: "StressData"
```
Then go to http://localhost:8086, and set up the credentials there, and create an organisation with a bucket
and after that, the credentials need to be added to the secrets, or appsettings.json file, or the environment
these are:
```
"database": {
    "connectionString": "http://localhost:8086", //Or another port if you configure it another way
    "token": "{Token of the database}",
    "org": "SWSP", //Or whatever you name the organisation
    "bucket": "StressData" //Or whatever you name the bucket
  }
  ```
  Afterwards, you should be able to run everything
## API endpoints
```
[HttpGET] GetByPatientId:
/HeartRateVariabilityMeasurements/patient/{patientId}

[HttpGet] GetByWearableIdWithinTimePeriod:
/HeartRateVariabilityMeasurements/wearable/{wearableId}

[HttpGet] GetById
/HeartRateVariabilityMeasurements/{id}
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
