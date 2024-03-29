@{
    # If authoring a script module, the RootModule is the name of your .psm1 file
    RootModule = 'AZStorage.Netcore.psm1'

    Author = 'Karl Wallenius <karl.wallenius@redeploy.se>'

    CompanyName = 'Redeploy AB'

    ModuleVersion = '0.1.2'

    # Use the New-Guid command to generate a GUID, and copy/paste into the next line
    GUID = 'f5e178fa-15b5-4317-9c74-cfcc96b974a3'

    Copyright = '2017 Redeploy AB'

    Description = 'PowerShell Core module for handling Azure Storage accounts'

    # Minimum PowerShell version supported by this module (optional, recommended)
    PowerShellVersion = '5.1'

    # Which PowerShell Editions does this module work with? (Core, Desktop)
    CompatiblePSEditions = @('Desktop','Core')

    # Modules to import as nested modules of the module specified in RootModule/ModuleToProcess
    NestedModules = @('AZStorage.Netcore.psm1','netcoreapp2.0/Redeploy.Azure.Storage.Commands.dll')

    # Which PowerShell functions are exported from your module? (eg. Get-CoolObject)
    FunctionsToExport = @()

    # Which PowerShell aliases are exported from your module? (eg. gco)
    AliasesToExport = @()

    # Which PowerShell variables are exported from your module? (eg. Fruits, Vegetables)
    VariablesToExport = @()

    CmdletsToExport = 'Get-AZStorageContainer', 'New-AZStorageContainer',
                'New-AZStorageContext', 'Set-AZStorageBlobContent'

    # PowerShell Gallery: Define your module's metadata
    PrivateData = @{
        PSData = @{
            # What keywords represent your PowerShell module? (eg. cloud, tools, framework, vendor)
            Tags = @('Azure', 'Storage', 'Blob', 'Container')

            # What software license is your code being released under? (see https://opensource.org/licenses)
            LicenseUri = 'https://github.com/RedeployAB/Redeploy.Storage/blob/master/LICENSE'

            # What is the URL to your project's website?
            ProjectUri = 'https://github.com/RedeployAB/Redeploy.Storage'

            # What is the URI to a custom icon file for your project? (optional)
            IconUri = ''

            # What new features, bug fixes, or deprecated features, are part of this release?
            ReleaseNotes = @'
            Initial release. Misses A LOT of features. It's a work in progress.

            * Get-AZStorageContainer
            * New-AZStorageContainer
            * New-AZStorageContext
            * Set-AZStorageBlobContent

            These cmdlets misses some functionality, but the basics are there.
            Next update will contain possibility to list containers without specifying a name.
            Also listing blobs, and removing blobs will be added.

            It's a work in progress.
'@
        }
    }

    # If your module supports updateable help, what is the URI to the help archive? (optional)
    # HelpInfoURI = ''
}