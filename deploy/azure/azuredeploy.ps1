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
$deploymentOutput = New-AzResourceGroupDeployment -ResourceGroupName $resourceGroupName `
    -TemplateFile .\sql\sqldeploy.json `
    -TemplateParameterFile .\sql\sqldeploy.parameters.json `
    -adminpwd (ConvertFrom-SecureString $adminPassword -AsPlainText)

$sqlServer = Get-AzSqlServer -ResourceGroupName $resourceGroupName | Select-Object -ExpandProperty "FullyQualifiedDomainName"
Write-Host "SQL Server: $sqlServer"

.\sql\sql-import-databases.ps1 `
    -sqlServer $sqlServer `
    -adminPassword $adminPassword