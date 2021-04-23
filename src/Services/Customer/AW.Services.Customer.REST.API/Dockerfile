#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Services/Customer/AW.Services.Customer.REST.API/AW.Services.Customer.REST.API.csproj", "Services/Customer/AW.Services.Customer.REST.API/"]
RUN dotnet restore "Services/Customer/AW.Services.Customer.REST.API/AW.Services.Customer.REST.API.csproj"
COPY . .
WORKDIR "/src/Services/Customer/AW.Services.Customer.REST.API"
RUN dotnet build "AW.Services.Customer.REST.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AW.Services.Customer.REST.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AW.Services.Customer.REST.API.dll"]