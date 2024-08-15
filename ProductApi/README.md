# Product API

## Deploy Docker
docker build -t productapi .
docker run -d -p 8080:80 --name productapi_container productapi

## Run Back End
dotnet run --project ProductApi.WebAPI