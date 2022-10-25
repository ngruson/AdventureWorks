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
    elseif ($key -eq "aksClusterName")
    {
        $aksClusterName = $deploymentOutput.Outputs[$key].Value
        az aks get-credentials --resource-group $resourceGroupName --name $aksClusterName --overwrite-existing        
    }
    elseif ($key -eq "containerRegistry")
    {
        $containerRegistry = $deploymentOutput.Outputs[$key].Value
    }
}

az aks update --resource-group $resourceGroupName --name $aksClusterName --attach-acr $containerRegistry

# .\build-container-images.ps1 $containerRegistry
.\setup-tls.ps1 $containerRegistry

# Write-Host "Importing SQL databases..."
# .\sql\sql-import-databases.ps1 `
#     -sqlServer $sqlServer `
#     -adminPassword $sqlAdminPassword

Write-Host "Deploy containers..."
helm install `
    --values ..\k8s\helm\adventureworks\inf.dev.azure.yaml `
    --set global.image.registry=$containerRegistry.azurecr.io `
    aw-deployment `
    ..\k8s\helm\adventureworks