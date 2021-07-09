#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/SalesPerson/AW.Services.SalesPerson.REST.API/AW.Services.SalesPerson.REST.API.csproj", "Services/SalesPerson/AW.Services.SalesPerson.REST.API/"]
RUN dotnet restore "Services/SalesPerson/AW.Services.SalesPerson.REST.API/AW.Services.SalesPerson.REST.API.csproj"
COPY . .
WORKDIR "/src/Services/SalesPerson/AW.Services.SalesPerson.REST.API"
RUN dotnet build "AW.Services.SalesPerson.REST.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AW.Services.SalesPerson.REST.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AW.Services.SalesPerson.REST.API.dll"]