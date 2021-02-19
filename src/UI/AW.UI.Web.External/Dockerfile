#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["UI/AW.UI.Web.External/AW.UI.Web.External.csproj", "UI/AW.UI.Web.External/"]
COPY ["Infrastructure/AW.Infrastructure.Api.REST/AW.Infrastructure.Api.REST.csproj", "Infrastructure/AW.Infrastructure.Api.REST/"]
COPY ["Infrastructure/AW.Infrastructure/AW.Infrastructure.csproj", "Infrastructure/AW.Infrastructure/"]
COPY ["Core/AW.Core.Abstractions/AW.Core.Abstractions.csproj", "Core/AW.Core.Abstractions/"]
COPY ["Core/AW.Core.Domain/AW.Core.Domain.csproj", "Core/AW.Core.Domain/"]
COPY ["Core/AW.Core.Application.AutoMapper/AW.Core.Application.AutoMapper.csproj", "Core/AW.Core.Application.AutoMapper/"]
RUN dotnet restore "UI/AW.UI.Web.External/AW.UI.Web.External.csproj"
COPY . .
WORKDIR "/src/UI/AW.UI.Web.External"
RUN dotnet build "AW.UI.Web.External.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AW.UI.Web.External.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AW.UI.Web.External.dll"]