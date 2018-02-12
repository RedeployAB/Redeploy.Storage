param(
    [Parameter(Mandatory = $false)]
    [String]
    $ProjectDirectory,
    [Parameter(Mandatory = $false)]
    $BuildDirectory
)

# Needs additional settings for versionin. This is an early build.§

# Predefine variables.
$moduleName = "AZStorage.Netcore"
$outputPath = "build/$moduleName/netcoreapp2.0"

if ([string]::IsNullOrEmpty($ProjectDirectory)) {
    $ProjectDirectory = (Resolve-Path (Get-Location).Path).Path
}

if ([string]::IsNullOrEmpty($BuildDirectory)) {
    $BuildDirectory = Join-Path $ProjectDirectory $outputPath
}

dotnet publish $ProjectDirectory --output $BuildDirectory