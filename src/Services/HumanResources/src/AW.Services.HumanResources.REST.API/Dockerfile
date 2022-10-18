#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/HumanResources/src/AW.Services.HumanResources.REST.API/AW.Services.HumanResources.REST.API.csproj", "Services/HumanResources/src/AW.Services.HumanResources.REST.API/"]
RUN dotnet restore "Services/HumanResources/src/AW.Services.HumanResources.REST.API/AW.Services.HumanResources.REST.API.csproj"
COPY . .
WORKDIR "/src/Services/HumanResources/src/AW.Services.HumanResources.REST.API"
RUN dotnet build "AW.Services.HumanResources.REST.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AW.Services.HumanResources.REST.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AW.Services.HumanResources.REST.API.dll"]