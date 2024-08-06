docker build -t productapi .

docker run -d -p 8080:80 --name productapi_container productapi



dotnet run --project ProductApi.WebAPI