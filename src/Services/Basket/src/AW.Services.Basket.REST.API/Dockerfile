FROM mcr.microsoft.com/dotnet/aspnet:6.0-bullseye-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0-bullseye-slim AS build
WORKDIR /src
COPY ["Services/Basket/src/AW.Services.Basket.REST.API/AW.Services.Basket.REST.API.csproj", "Services/Basket/src/AW.Services.Basket.REST.API/"]
COPY aw-ca.pem .
RUN dotnet restore "Services/Basket/src/AW.Services.Basket.REST.API/AW.Services.Basket.REST.API.csproj"
COPY . .
WORKDIR "/src/Services/Basket/src/AW.Services.Basket.REST.API"
RUN dotnet build "AW.Services.Basket.REST.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AW.Services.Basket.REST.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=build /src/aw-ca.pem /etc/ssl/certs/aw-ca.pem
ENTRYPOINT ["dotnet", "AW.Services.Basket.REST.API.dll"]