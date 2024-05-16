Write-Host "🚢 Starting Docker Compose"
docker compose up -d

Write-Host "🚀 Creating and Seeding Database"
Set-Location ./tools/Database/
dotnet run

#
#Param(
#[switch]$skipDeploy = $false,
#[switch]$linux = $false
#)
#

#if (-not $skipDeploy) {
#
#    Write-Host "⚙️ Restore dotnet tools"
#    dotnet tool restore
#
#    Set-Location $($Script:MyInvocation.MyCommand.Path | Split-Path)
#
#    if ($linux) {
#        Write-Host "⚙️ Building GordonBeemingCom Database AppDbContext Bundle"
#        dotnet restore --runtime 'linux-x64'
#        Set-Location ./src/
#        dotnet ef migrations bundle --project 'GordonBeemingCom.Database' --startup-project 'GordonBeemingCom' --force --context AppDbContext --output AppDbContextEfBundle
#
#        Write-Host "🚀 Deploying GordonBeemingCom Database AppDbContext Bundle"
#        . ./AppDbContextEfBundle --connection "Server=.,1600;Database=GordonBeemingCom;User Id=sa;Password=Password!@2;MultipleActiveResultSets=true;TrustServerCertificate=True;"
#    }
#    else {
#        Write-Host "⚙️ Building GordonBeemingCom Database AppDbContext Bundle"
#        dotnet restore --runtime 'win-x64'
#        Set-Location .\src\
#        dotnet ef migrations bundle --project 'GordonBeemingCom.Database' --startup-project 'GordonBeemingCom' --force --context AppDbContext --output AppDbContextEfBundle.exe
#
#        Write-Host "🚀 Deploying GordonBeemingCom Database AppDbContext Bundle"
#        . .\AppDbContextEfBundle.exe --connection "Server=.,1600;Database=GordonBeemingCom;User Id=sa;Password=Password!@2;MultipleActiveResultSets=true;TrustServerCertificate=True;"
#    }
#
#    Set-Location $($Script:MyInvocation.MyCommand.Path | Split-Path)
#}
