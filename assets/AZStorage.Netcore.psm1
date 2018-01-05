
if ($PSVersionTable.PSEdition -ne 'Core' -and "$($PSVersionTable.PSVersion.Major)." + "$($PSVersionTable.PSVersion.Minor)" -ge "5.1") {
    # Check if files exists.
    $httpPath = "C:\Program Files\dotnet\sdk\NuGetFallbackFolder\system.net.http\4.1.0\runtimes\win\lib\net46\System.Net.Http.dll"
    $diagPath = "C:\Program Files\dotnet\sdk\NuGetFallbackFolder\system.diagnostics.diagnosticsource\4.0.0\lib\net46\System.Diagnostics.DiagnosticSource.dll"
    
    if (!(Test-Path $httpPath) -or !(Test-Path $diagPath)) {
        throw "Plase install NET Core 2."
    }
    
    Add-Type -Path $httpPath
    Add-Type -Path $diagPath
}