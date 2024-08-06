# Use the official Microsoft ASP.NET Core runtime as a parent image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Use the official Microsoft .NET SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the solution file and restore any dependencies
COPY ["ProductApi.sln", "./"]
COPY ["ProductApi.Core/ProductApi.Core.csproj", "ProductApi.Core/"]
COPY ["ProductApi.Infrastructure/ProductApi.Infrastructure.csproj", "ProductApi.Infrastructure/"]
COPY ["ProductApi.Application/ProductApi.Application.csproj", "ProductApi.Application/"]
COPY ["ProductApi.WebAPI/ProductApi.WebAPI.csproj", "ProductApi.WebAPI/"]

RUN dotnet restore "ProductApi.WebAPI/ProductApi.WebAPI.csproj"

# Copy everything else and build the app
COPY . .
WORKDIR "/src/ProductApi.WebAPI"
RUN dotnet build "ProductApi.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProductApi.WebAPI.csproj" -c Release -o /app/publish

# Final stage/image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProductApi.WebAPI"]
