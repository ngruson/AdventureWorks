#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["UI/AW.UI.Web.Internal/AW.UI.Web.Internal.csproj", "UI/AW.UI.Web.Internal/"]
RUN dotnet restore "UI/AW.UI.Web.Internal/AW.UI.Web.Internal.csproj"
COPY . .
WORKDIR "/src/UI/AW.UI.Web.Internal"
RUN dotnet build "AW.UI.Web.Internal.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AW.UI.Web.Internal.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AW.UI.Web.Internal.dll"]