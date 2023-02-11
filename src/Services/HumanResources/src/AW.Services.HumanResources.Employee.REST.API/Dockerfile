FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/HumanResources/src/AW.Services.HumanResources.Employee.REST.API/AW.Services.HumanResources.Employee.REST.API.csproj", "Services/HumanResources/src/AW.Services.HumanResources.Employee.REST.API/"]
COPY aw-ca.pem .
RUN dotnet restore "Services/HumanResources/src/AW.Services.HumanResources.Employee.REST.API/AW.Services.HumanResources.Employee.REST.API.csproj"
COPY . .
WORKDIR "/src/Services/HumanResources/src/AW.Services.HumanResources.Employee.REST.API"
RUN dotnet build "AW.Services.HumanResources.Employee.REST.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AW.Services.HumanResources.Employee.REST.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=build /src/aw-ca.pem /etc/ssl/certs/aw-ca.pem
ENTRYPOINT ["dotnet", "AW.Services.HumanResources.Employee.REST.API.dll"]