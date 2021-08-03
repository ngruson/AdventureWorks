#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Services/Product/src/AW.Services.Product.REST.API/AW.Services.Product.REST.API.csproj", "Services/Product/src/AW.Services.Product.REST.API/"]
COPY k8s/aw-ca.pem .
RUN dotnet restore "Services/Product/src/AW.Services.Product.REST.API/AW.Services.Product.REST.API.csproj"
COPY . .
WORKDIR "/src/Services/Product/src/AW.Services.Product.REST.API"
RUN dotnet build "AW.Services.Product.REST.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AW.Services.Product.REST.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=build /src/aw-ca.pem /etc/ssl/certs/aw-ca.pem
ENTRYPOINT ["dotnet", "AW.Services.Product.REST.API.dll"]