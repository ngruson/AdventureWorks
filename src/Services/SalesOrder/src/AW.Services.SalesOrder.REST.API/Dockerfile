#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0-bullseye-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0-bullseye-slim AS build
WORKDIR /src
COPY ["Services/SalesOrder/src/AW.Services.SalesOrder.REST.API/AW.Services.SalesOrder.REST.API.csproj", "Services/SalesOrder/src/AW.Services.SalesOrder.REST.API/"]
COPY aw-ca.pem .
RUN dotnet restore "Services/SalesOrder/src/AW.Services.SalesOrder.REST.API/AW.Services.SalesOrder.REST.API.csproj"
COPY . .
WORKDIR "/src/Services/SalesOrder/src/AW.Services.SalesOrder.REST.API"
RUN dotnet build "AW.Services.SalesOrder.REST.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AW.Services.SalesOrder.REST.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=build /src/aw-ca.pem /etc/ssl/certs/aw-ca.pem
ENTRYPOINT ["dotnet", "AW.Services.SalesOrder.REST.API.dll"]