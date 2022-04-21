# Stress-Data-Service
The service that is responsible for handling stress data and communicating with the stress measurements database

# Docker
docker build -t stress_data_service .
docker run -p 5031:80 --network=swsp stress_data_service