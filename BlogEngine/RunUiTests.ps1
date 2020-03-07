function Start-WebServer() {
    param (
        [string]$ProjectName,
        [int] $Port,
        [string] $Args
    )
    return Start-Process -FilePath "$PSScriptRoot\src\$ProjectName\bin\Debug\netcoreapp3.1\$ProjectName.exe " `
        -ArgumentList "Urls=https://localhost:$port $args" -NoNewWindow -PassThru
}

$BlogEngineApiServer = Start-WebServer -ProjectName 'BlogEngine.Api' -Port 5000 -args " ConnectionStrings:DefaultConnection='Data Source=Blog.db'"
$BlogEngineWebServer = Start-WebServer -ProjectName 'BlogEngine.Web' -Port 5001 -args " ApiUrl=https://localhost:5000"

dotnet test "$PSScriptRoot\test\BlogEngine.Web.Tests\"

$BlogEngineApiServer, $BlogEngineWebServer | Stop-Process