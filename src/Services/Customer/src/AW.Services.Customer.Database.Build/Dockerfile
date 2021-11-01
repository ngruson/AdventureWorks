FROM mcr.microsoft.com/dotnet/sdk:3.1 AS builder

WORKDIR /src
COPY Services/Customer/src/AW.Services.Customer.Database AW.Services.Customer.Database
COPY Services/Customer/src/AW.Services.Customer.Database.Build AW.Services.Customer.Database.Build
RUN dotnet build AW.Services.Customer.Database.Build/AW.Services.Customer.Database.Build.csproj

FROM mcr.microsoft.com/mssql/server:2019-latest

USER root

ENV ACCEPT_EULA="Y" \
  DATA_PATH="/data" \
  SA_PASSWORD="Welkom01!"

VOLUME ${DATA_PATH}
WORKDIR ./init

RUN apt-get update
#USER mssql

# Install PowerShell
RUN apt-get install -y powershell

# Install unzip
RUN apt-get install -y unzip

# Install SQLPackage for Linux and make it executable
RUN wget -progress=bar:force -q -O sqlpackage.zip https://go.microsoft.com/fwlink/?linkid=2157202 \
    && unzip -qq sqlpackage.zip -d /opt/sqlpackage \
    && chmod +x /opt/sqlpackage/sqlpackage

COPY Services/Customer/src/AW.Services.Customer.Database.Build/Initialize-Database.ps1 .
COPY --from=builder /src/AW.Services.Customer.Database.Build/bin/Debug/netstandard2.1/AW.Services.Customer.Database.Build.dacpac .

CMD ( /opt/mssql/bin/sqlservr & ) | grep -q "Service Broker manager has started" \
    && pwsh ./Initialize-Database.ps1 -sa_password $SA_PASSWORD -data_path $DATA_PATH -Verbose