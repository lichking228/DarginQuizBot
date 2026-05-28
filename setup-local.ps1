param(
    [Parameter(Mandatory = $true)]
    [string]$TelegramBotToken,

    [Parameter(Mandatory = $true)]
    [string]$PostgresPassword,

    [string]$PostgresUser = "postgres",
    [string]$PostgresHost = "localhost",
    [int]$PostgresPort = 5433,
    [string]$DatabaseName = "dargin_quiz_bot"
)

$ErrorActionPreference = "Stop"

function Find-CommandPath {
    param(
        [string]$CommandName,
        [string[]]$FallbackPaths
    )

    $command = Get-Command $CommandName -ErrorAction SilentlyContinue
    if ($command) {
        return $command.Source
    }

    foreach ($path in $FallbackPaths) {
        if (Test-Path $path) {
            return $path
        }
    }

    throw "Cannot find $CommandName. Install it or add it to PATH."
}

$dotnet = Find-CommandPath "dotnet" @(
    "C:\Program Files\dotnet\dotnet.exe"
)

$psql = Find-CommandPath "psql" @(
    "C:\Program Files\PostgreSQL\18\bin\psql.exe",
    "C:\Program Files\PostgreSQL\17\bin\psql.exe",
    "C:\Program Files\PostgreSQL\16\bin\psql.exe"
)

$createdb = Find-CommandPath "createdb" @(
    "C:\Program Files\PostgreSQL\18\bin\createdb.exe",
    "C:\Program Files\PostgreSQL\17\bin\createdb.exe",
    "C:\Program Files\PostgreSQL\16\bin\createdb.exe"
)

$env:PGPASSWORD = $PostgresPassword

Write-Host "Checking PostgreSQL connection..."
& $psql -h $PostgresHost -p $PostgresPort -U $PostgresUser -d postgres -c "select 1;" | Out-Null
if ($LASTEXITCODE -ne 0) {
    throw "Cannot connect to PostgreSQL at ${PostgresHost}:${PostgresPort} as user '$PostgresUser'."
}

$escapedDatabaseName = $DatabaseName.Replace("'", "''")
$databaseExistsOutput = & $psql -h $PostgresHost -p $PostgresPort -U $PostgresUser -d postgres -tAc "select 1 from pg_database where datname = '$escapedDatabaseName';"
if ($LASTEXITCODE -ne 0) {
    throw "Cannot check whether database '$DatabaseName' exists."
}

$databaseExists = if ($null -eq $databaseExistsOutput) { "" } else { $databaseExistsOutput.Trim() }

if ($databaseExists -ne "1") {
    Write-Host "Creating database $DatabaseName..."
    & $createdb -h $PostgresHost -p $PostgresPort -U $PostgresUser $DatabaseName
    if ($LASTEXITCODE -ne 0) {
        throw "Cannot create database '$DatabaseName'."
    }
}
else {
    Write-Host "Database $DatabaseName already exists."
}

$config = [ordered]@{
    ConnectionStrings = [ordered]@{
        DefaultConnection = "Host=$PostgresHost;Port=$PostgresPort;Database=$DatabaseName;Username=$PostgresUser;Password=$PostgresPassword"
    }
    TelegramBot = [ordered]@{
        Token = $TelegramBotToken
    }
    Logging = [ordered]@{
        LogLevel = [ordered]@{
            Default = "Information"
            "Microsoft.AspNetCore" = "Warning"
        }
    }
    AllowedHosts = "*"
}

$configPath = Join-Path $PSScriptRoot "appsettings.Development.json"
$config | ConvertTo-Json -Depth 5 | Set-Content -Path $configPath -Encoding UTF8

Write-Host "Wrote appsettings.Development.json."
Write-Host "Restoring and building project..."
& $dotnet restore "$PSScriptRoot\DarginQuizBot.sln"
& $dotnet build "$PSScriptRoot\DarginQuizBot.sln" --no-restore

Write-Host ""
Write-Host "Run with:"
Write-Host "& '$dotnet' run --project '$PSScriptRoot\DarginQuizBot.csproj' --launch-profile http"
