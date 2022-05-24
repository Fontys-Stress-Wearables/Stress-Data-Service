# Stress-Data-Service
## Port: 5031
The service that is responsible for handling stress data and communicating with the stress measurements database.
This service gets processed stress data from the Stress Algorithm Service, stores it in a database and is used by the Caregiver Dashboard to retrieve this stress data.
## API endpoints
```
/HeartRateVariabilityMeasurements
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
