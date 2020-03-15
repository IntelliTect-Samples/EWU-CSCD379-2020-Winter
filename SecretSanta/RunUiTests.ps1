function Start-WebServer() {
    param (
        [string]$ProjectName,
        [int] $Port,
        [string] $Args
    )
    return Start-Process -FilePath "$PSScriptRoot\src\$ProjectName\bin\Debug\netcoreapp3.1\$ProjectName.exe " `
        -ArgumentList "Urls=https://localhost:$port $args" -NoNewWindow -PassThru
}

$BlogEngineApiServer = Start-WebServer -ProjectName 'SecretSanta.Api' -Port 5000 -args " ConnectionStrings:DefaultConnection='Data Source=SecretSanta.db'"
$BlogEngineWebServer = Start-WebServer -ProjectName 'SecretSanta.Web' -Port 5001 -args " ApiUrl=https://localhost:5000"

dotnet test "$PSScriptRoot\test\SecretSanta.Web.Tests\"

$SecretSantaApiServer, $SecretSantaWebServer | Stop-Process