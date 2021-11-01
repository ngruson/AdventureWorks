param(
    [Parameter(Mandatory=$true)]
    [string] $sa_password,

    [Parameter(Mandatory=$true)]
    [string] $data_path)

# start the service
#Write-Verbose 'Starting SQL Server'
#Start-Service MSSQL`$SQLEXPRESS

Set-PSRepository -Name PSGallery -InstallationPolicy Trusted
Install-Module SQLServer -AllowPreRelease

$password = ConvertTo-SecureString -String $sa_password -AsPlainText -Force
$credential = New-Object -TypeName System.Management.Automation.PSCredential("sa", $password)

if ($sa_password -ne "_") {
	Write-Verbose 'Changing SA login credentials'
    $sqlcmd = "ALTER LOGIN sa with password='$sa_password'; ALTER LOGIN sa ENABLE;"
    Invoke-SqlCmd -Query $sqlcmd -ServerInstance "." -Credential $credential
}

$mdfPath = "$data_path/sqldb-adv-customer_Primary.mdf"
$ldfPath = "$data_path/sqldb-adv-customer_Primary.ldf"

# attach data files if they exist: 
if ((Test-Path $mdfPath) -eq $true) {
    $sqlcmd = "IF DB_ID('sqldb-adv-customer') IS NULL BEGIN CREATE DATABASE [sqldb-adv-customer] ON (FILENAME = N'$mdfPath')"
    if ((Test-Path $ldfPath) -eq $true) {
        $sqlcmd =  "$sqlcmd, (FILENAME = N'$ldfPath')"
    }
    $sqlcmd = "$sqlcmd FOR ATTACH; END"
    Write-Verbose 'Data files exist - will attach and upgrade database'
    Invoke-Sqlcmd -Query $sqlcmd -ServerInstance "." -Credential $credential
}
else {
     Write-Verbose 'No data files - will create new database'
}

# deploy or upgrade the database:
$SqlPackagePath = '/opt/sqlpackage/sqlpackage'
& $SqlPackagePath `
    /sf:AW.Services.Customer.Database.Build.dacpac `
    /a:Script /op:deploy.sql /p:CommentOutSetVarDeclarations=true `
    /tsn:. /tdn:sqldb-adv-customer /tu:sa /tp:$sa_password

$SqlCmdVars = "DatabaseName=sqldb-adv-customer", "DefaultFilePrefix=sqldb-adv-customer", "DefaultDataPath=$data_path\", "DefaultLogPath=$data_path\"
Invoke-Sqlcmd -InputFile deploy.sql -Variable $SqlCmdVars -Verbose -Credential $credential

Write-Verbose "Deployed sqldb-adv-customer database, data files at: $data_path"

#$lastCheck = (Get-Date).AddSeconds(-5) 
#while ($true) { 
#    Get-EventLog -LogName Application -Source "MSSQL*" -After $lastCheck | Select-Object TimeGenerated, EntryType, Message
#    $lastCheck = Get-Date 
#    Start-Sleep -Seconds 5 
#}

while ($true) {

}