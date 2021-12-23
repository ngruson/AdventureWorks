#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0-bullseye-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0-bullseye-slim AS build
WORKDIR /src
COPY ["Services/IdentityServer/src/AW.Services.IdentityServer/AW.Services.IdentityServer.csproj", "Services/IdentityServer/src/AW.Services.IdentityServer/"]
COPY Services/IdentityServer/src/AW.Services.IdentityServer/identityserver.pfx .
RUN dotnet restore "Services/IdentityServer/src/AW.Services.IdentityServer/AW.Services.IdentityServer.csproj"
COPY . .
WORKDIR "/src/Services/IdentityServer/src/AW.Services.IdentityServer"
RUN dotnet build "AW.Services.IdentityServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AW.Services.IdentityServer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=build /src/identityserver.pfx .
ENTRYPOINT ["dotnet", "AW.Services.IdentityServer.dll"]