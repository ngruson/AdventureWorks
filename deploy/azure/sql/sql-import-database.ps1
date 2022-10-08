param(
    $dbProjectFile,
    $targetServer,
    $targetDatabase,
    [SecureString]$adminPassword
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
        $file = Get-ChildItem $filePath -Recurse

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
    
Write-Host "Locate database project..."
$dbProjectFilePath = FindFile (Get-Location) $dbProjectFile
Write-Host "Found database project: $dbProjectFilePath"

Write-Host "Building project $dbProjectFile"
& dotnet build $dbProjectFilePath

$dacpac = (Split-Path $dbProjectFilePath -Parent) + "\bin\Debug\netstandard2.0\" + `
    ([System.IO.Path]::GetFileNameWithoutExtension($dbProjectFilePath) + ".dacpac")
$outputPath = (Split-Path $dacpac -Parent) + "/output.sql"
$adminPasswordPlain = ConvertFrom-SecureString $adminPassword -AsPlainText

Write-Host "Importing database with dacpac file $dacpac"
# & ..\tools\sqlpackage\sqlpackage.exe `
#     /Action:Script `
#     /OutputPath:$outputPath `
#     /tsn:$targetServer `
#     /tdn:$targetDatabase `
#     /tu:sqladmin /tp:$adminPasswordPlain `
#     /sf:$dacpac

& ..\tools\sqlpackage\sqlpackage.exe `
    /Action:Publish `
    /tsn:$targetServer `
    /tdn:$targetDatabase `
    /tu:sqladmin /tp:$adminPasswordPlain `
    /sf:$dacpac