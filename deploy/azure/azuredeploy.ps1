[CmdletBinding()]
param(
    [Parameter(Mandatory)]
    [string]$resourceGroupName,
    [Parameter(Mandatory)]
    [string]$location,
    [Parameter(Mandatory)]
    [SecureString]$adminPassword
)

Write-Host "Creating resource group $resourceGroupName"
New-AzResourceGroup -Name $resourceGroupName -Location $location -Force

Write-Host "Creating Azure SQL Server..."
# $pwd = ConvertFrom-SecureString $adminPassword -AsPlainText
# Write-Host "pwd: $pwd"

New-AzResourceGroupDeployment -ResourceGroupName $resourceGroupName `
    -TemplateFile .\sql\sqldeploy.json `
    -TemplateParameterFile .\sql\sqldeploy.parameters.json `
    -adminpwd (ConvertFrom-SecureString $adminPassword -AsPlainText)

$sqlServer = Get-AzSqlServer -ResourceGroupName $resourceGroupName | Select-Object -ExpandProperty "FullyQualifiedDomainName"
Write-Host "SQL Server: $sqlServer"

.\sql-import-database.ps1 "AW.Services.Customer.Database.Build.csproj" `
    -targetServer $sqlServer `
    -targetDatabase "sqldb-adv-customer" `
    -adminPassword $adminPassword