[CmdletBinding()]
param(
    [Parameter(Mandatory)]
    [string]$resourceGroupName,
    [Parameter(Mandatory)]
    [string]$location,
    [Parameter(Mandatory)]
    [SecureString]$sqlAdminPassword
)

Write-Host "Creating resource group $resourceGroupName"
New-AzResourceGroup -Name $resourceGroupName -Location $location -Force

Write-Host "Creating deployment..."
$deploymentOutput = New-AzResourceGroupDeployment -ResourceGroupName $resourceGroupName `
    -TemplateFile deploy.json `
    -TemplateParameterFile deploy.parameters.json `
    -sqlAdminPwd (ConvertFrom-SecureString $sqlAdminPassword -AsPlainText)
    
Write-Host $deploymentOutput.OutputsString

foreach ($key in $deploymentOutput.Outputs.Keys)
{
    if ($key -eq "sqlServer") {
        $sqlServer = $deploymentOutput.Outputs[$key].Value
    }
}

# Write-Host "Importing SQL databases..."
# .\sql\sql-import-databases.ps1 `
#     -sqlServer $sqlServer `
#     -adminPassword $sqlAdminPassword