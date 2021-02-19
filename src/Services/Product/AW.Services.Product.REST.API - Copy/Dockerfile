#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Services/Product/AW.Services.Product.REST.API/AW.Services.Product.REST.API.csproj", "Services/Product/AW.Services.Product.REST.API/"]
COPY ["Services/Product/AW.Services.Product.Application/AW.Services.Product.Application.csproj", "Services/Product/AW.Services.Product.Application/"]
COPY ["Services/Product/AW.Services.Product.Domain/AW.Services.Product.Domain.csproj", "Services/Product/AW.Services.Product.Domain/"]
COPY ["Services/Product/AW.Services.Product.Persistence.EntityFrameworkCore/AW.Services.Product.Persistence.EntityFrameworkCore.csproj", "Services/Product/AW.Services.Product.Persistence.EntityFrameworkCore/"]
RUN dotnet restore "Services/Product/AW.Services.Product.REST.API/AW.Services.Product.REST.API.csproj"
COPY . .
WORKDIR "/src/Services/Product/AW.Services.Product.REST.API"
RUN dotnet build "AW.Services.Product.REST.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AW.Services.Product.REST.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AW.Services.Product.REST.API.dll"]