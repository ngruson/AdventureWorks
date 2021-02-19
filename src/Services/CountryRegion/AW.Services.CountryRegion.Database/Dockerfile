# escape=`
FROM ngruson/msbuild:netfx-4.7.2-ssdt AS builder

WORKDIR C:\src\AW.Services.CountryRegion.Database
COPY Services\CountryRegion\AW.Services.CountryRegion.Database .
RUN msbuild AW.Services.CountryRegion.Database.sqlproj `
    /p:SQLDBExtensionsRefPath="C:\Microsoft.Data.Tools.Msbuild.10.0.61804.210\lib\net46" `
    /p:SQLServerRedistPath="C:\Microsoft.Data.Tools.Msbuild.10.0.61804.210\lib\net46"

FROM microsoft/mssql-server-windows-express

ENV ACCEPT_EULA="Y" `
    DATA_PATH="C:\data" `
    sa_password="p@ssw0rdSQ!"

VOLUME ${DATA_PATH}
COPY Services\CountryRegion\AW.Services.CountryRegion.Database\Initialize-Database.ps1 .
COPY --from=builder C:\Microsoft.Data.Tools.Msbuild.10.0.61804.210 C:\Microsoft.Data.Tools.Msbuild.10.0.61804.210
COPY --from=builder C:\src\AW.Services.CountryRegion.Database\bin\Debug\AW.Services.CountryRegion.Database.dacpac .

WORKDIR C:\init
CMD powershell ./Initialize-Database.ps1 -sa_password $env:sa_password -data_path $env:data_path -Verbose
WORKDIR C:\