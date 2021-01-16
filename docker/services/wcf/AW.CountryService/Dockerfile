# escape=`
# FROM mcr.microsoft.com/dotnet/sdk:3.1
FROM mcr.microsoft.com/dotnet/framework/sdk:4.8 AS build-agent

WORKDIR /src/Core/AW.Core.Domain
# WORKDIR \src\Core\AW.Core.Domain
COPY src\Core\AW.Core.Domain\ .
RUN dotnet build

WORKDIR /src/Core/AW.Core.Application.AutoMapper
# WORKDIR C:\src\Core\AW.Core.Application.AutoMapper
COPY src\Core\AW.Core.Application.AutoMapper\ .
RUN dotnet build

WORKDIR /src/Core/AW.Core.Application
# WORKDIR C:\src\Core\AW.Core.Application
COPY src\Core\AW.Core.Application\ .
RUN dotnet build

WORKDIR /src/Persistence/Ardalis.Specification.EntityFramework
# WORKDIR C:\src\Persistence\Ardalis.Specification.EntityFramework
COPY src\Persistence\Ardalis.Specification.EntityFramework\ .
RUN nuget restore packages.config -PackagesDirectory ..\..\packages
RUN msbuild Ardalis.Specification.EntityFramework.csproj
# `
  # /p:VSToolsPath=C:\MSBuild.Microsoft.VisualStudio.Web.targets.14.0.0.3\tools\VSToolsPath

WORKDIR /src/Persistence/AW.Persistence.EntityFramework
COPY src\Persistence\AW.Persistence.EntityFramework\ .
RUN dotnet build

WORKDIR /src/Services/WCF\AW.CountryService
#COPY src\Services\WCF\AW.CountryService\packages.config .
COPY src\Services\WCF\AW.CountryService\ .
RUN nuget restore packages.config -PackagesDirectory ..\..\..\packages

RUN msbuild AW.CountryService.csproj /p:OutputPath=C:\out /p:DeployOnBuild=true
#     /p:VSToolsPath=C:\MSBuild.Microsoft.VisualStudio.Web.targets.14.0.0.3\tools\VSToolsPath