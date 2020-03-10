function Start-WebServer() {
    param (
        [string]$ProjectName,
        [int] $Port,
        [string] $Args
    )
    return Start-Process -FilePath "$PSScriptRoot\src\$ProjectName\bin\Debug\netcoreapp3.1\$ProjectName.exe " `
        -ArgumentList "Urls=https://localhost:$port $args" -NoNewWindow -PassThru
}

$SecretSantaApiServer = Start-WebServer -ProjectName 'SecretSanta.Api' -Port 44388 -args " ConnectionStrings:DefaultConnection='Data Source=SecretSanta.db'"
$SecretSantaWebServer = Start-WebServer -ProjectName 'SecretSanta.Web' -Port 44394 -args " ApiUrl=https://localhost:44388"

dotnet test "$PSScriptRoot\test\SecretSanta.Web.Tests\"

$SecretSantaApiServer, $SecretSantaWebServer | Stop-Process 