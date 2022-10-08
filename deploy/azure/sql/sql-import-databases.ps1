param(
    $sqlServer,
    [SecureString]$adminPassword
)

.\sql\sql-import-database.ps1 "AW.Services.Customer.Database.Build.csproj" `
    -targetServer $sqlServer `
    -targetDatabase "sqldb-adv-customer" `
    -adminPassword $adminPassword

.\sql\sql-import-database.ps1 "AW.Services.HumanResources.Database.Build.csproj" `
    -targetServer $sqlServer `
    -targetDatabase "sqldb-adv-hr" `
    -adminPassword $adminPassword