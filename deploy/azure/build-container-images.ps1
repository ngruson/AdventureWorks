param(
    [string]$containerRegistry
)

function FindFile {
    param (      
        [string]$folder,  
        [string]$fileName
    )
    
    $found = $false
    $filePath = Join-Path $folder $fileName

    while (!$found)
    {
        $filePath = Join-Path $folder $fileName
        $file = Get-ChildItem $filePath -Recurse -Filter *.csproj

        if ($file)
        {
            # We found the project file
            $found = $true
            return $file
        }
    
        # Walk up to parent folder
        $folder = Split-Path $folder -Parent
    }
}

function Build-And-Push-Image
{
    param(
        [string]$projectFile,
        [string]$containerRegistry,
        [string]$appName
    )
    
    $path = FindFile (Get-Location) $projectFile
    $folder = Split-Path $path -Parent
    
    $tag = ("$containerRegistry.azurecr.io/$appName" + ":latest")
    Write-Host "Building image $tag from $folder\Dockerfile"
    docker build -f "$folder\Dockerfile" -t $tag ..\..\src
    docker push $tag
}

Write-Host "Logging on to container registry $containerRegistry"
az acr login --name $containerRegistry

# Build container images
Build-And-Push-Image AW.Services.Basket.REST.API.csproj $containerRegistry adventureworks_basketapi
Build-And-Push-Image AW.Services.Customer.REST.API.csproj $containerRegistry adventureworks_customerapi
Build-And-Push-Image AW.Services.HumanResources.Department.REST.API.csproj $containerRegistry adventureworks_departmentapi
Build-And-Push-Image AW.Services.HumanResources.Employee.REST.API.csproj $containerRegistry adventureworks_employeeapi
Build-And-Push-Image AW.Services.HumanResources.Shift.REST.API.csproj $containerRegistry adventureworks_shiftapi
Build-And-Push-Image AW.Services.IdentityServer.csproj $containerRegistry adventureworks_identityserver
Build-And-Push-Image AW.Services.Product.REST.API.csproj $containerRegistry adventureworks_productapi
Build-And-Push-Image AW.Services.ReferenceData.REST.API.csproj $containerRegistry adventureworks_referencedataapi
Build-And-Push-Image AW.Services.Sales.Order.REST.API.csproj $containerRegistry adventureworks_salesorderapi
Build-And-Push-Image AW.Services.Sales.SalesPerson.REST.API.csproj $containerRegistry adventureworks_salespersonapi
Build-And-Push-Image AW.UI.Web.Internal.csproj $containerRegistry adventureworks_mvc_internal
Build-And-Push-Image AW.UI.Web.Store.csproj $containerRegistry adventureworks_mvc_store