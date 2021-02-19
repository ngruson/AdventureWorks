#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Services/CountryRegion/AW.Services.CountryRegion.REST.API/AW.Services.CountryRegion.REST.API.csproj", "Services/CountryRegion/AW.Services.CountryRegion.REST.API/"]
COPY ["Services/CountryRegion/AW.Services.CountryRegion.Application/AW.Services.CountryRegion.Application.csproj", "Services/CountryRegion/AW.Services.CountryRegion.Application/"]
COPY ["Services/CountryRegion/AW.Services.CountryRegion.Domain/AW.Services.CountryRegion.Domain.csproj", "Services/CountryRegion/AW.Services.CountryRegion.Domain/"]
COPY ["Services/CountryRegion/AW.Services.CountryRegion.Persistence.EntityFrameworkCore/AW.Services.CountryRegion.Persistence.EntityFrameworkCore.csproj", "Services/CountryRegion/AW.Services.CountryRegion.Persistence.EntityFrameworkCore/"]
RUN dotnet restore "Services/CountryRegion/AW.Services.CountryRegion.REST.API/AW.Services.CountryRegion.REST.API.csproj"
COPY . .
WORKDIR "/src/Services/CountryRegion/AW.Services.CountryRegion.REST.API"
RUN dotnet build "AW.Services.CountryRegion.REST.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AW.Services.CountryRegion.REST.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AW.Services.CountryRegion.REST.API.dll"]