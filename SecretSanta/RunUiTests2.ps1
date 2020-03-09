
Start-Process -FilePath .\src\SecretSanta.Api\bin\Debug\netcoreapp3.1\SecretSanta.Api.exe -ArgumentList "Urls=https://localhost:44388 ConnectionStrings:DefaultConnection='Data Source=SecretSanta.db'" -NoNewWindow -PassThru
Start-Process -FilePath .\src\SecretSanta.Web\bin\Debug\netcoreapp3.1\SecretSanta.Web.exe -ArgumentList "Urls=https://localhost:44394  ApiUrl=https://localhost:44388" -NoNewWindow -PassThru

#dotnet run -p .\src\SecretSanta.Api\SecretSanta.Api.csproj
#dotnet run -p .\src\SecretSanta.Web\SecretSanta.Web.csproj

dotnet test ".\test\SecretSanta.Web.Tests\"