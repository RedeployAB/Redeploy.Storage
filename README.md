# AZStorage.Netcore

## <a name=intro>Introduction</a>

This module was created to make up for the lack of an official module (*At least I have not found one*) for handling of Azure Storage Accounts with PowerShell Core
(not the resources in ARM, but rather uploading blobs, getting containers etc). It is mostly developed because I needed the functionality in another project.

The cmdlets in this module works similar to the cmdlets in the official `Azure.Storage` module, found in the repository for [Azure PowerShell](https://github.com/Azure/azure-powershell). They will mostly have the same parameters, structure and functionality as their counterparts in that module. 
At the moment only a couple of cmdlets have been added. But more will be added as needed. Not all parameters from the official modules cmdlets are supported. All likeliness with those cmdlets are intentional to make it easier for experienced users of the official module
to use this adaption.

As soon as Microsoft releases an official module that works with PowerShell Core (or if I found another one that does), this project will be discontinued.


**Content**

* [Introduction](#intro)
* [Installation](#install)
* [Cmdlets](#cmdlets)
* [Usage Examples](#usage)
* [Compatability](#compatability)
* [ToDo](#todo)
* [Versions and Updates](#version)


## <a name=install>Installation</a>

To install the module issue this command in a PowerShell terminal.

```
Install-Module -Name AZStorage.Netcore
```

## <a name="cmdlets">Cmdlets</a>

**`Get-AZStorageContainer`**


**`New-AZStorageContainer`**


**`New-AZStorageContext`**


**`Set-AZStorageBlobContent`**


## <a name=usage>Usage Examples</a>

To upload a file to a blob in a Storage Account:

```
$ctx = New-AZStorageContext -StorageAccountName <storage account name> -StorageAccountKey <storage account key>
Set-AZStorageBlobContent -Context $ctx -File C:\<path>\<to>\file.txt -Blob file.txt -Container <container>
```

## <a name=compatability>Compatability</a>

Tested on the following operating systems, distrobutions etc.

| Operating System    | PowerShell Edition  | Compatible                              | Notes                                                                                                           |
|---------------------|---------------------|-----------------------------------------|-----------------------------------------------------------------------------------------------------------------|
| Windows 10          | *5.1 (Desktop)*     | **Yes (If dotnet core 2 is installed)** | Some Cmdlets native to PS Desktop will stop working after import. Close the terminal to get functionality back. |
| Windows 10          | *6.0.1 (Core)*      | **Yes**                                 |                                                                                                                 |
| Nano Server         | *6.0.0 (Core)*      | **Yes**                                 |                                                                                                                 |
| WSL Ubuntu (16.04)  | *6.0.0-rc.2 (Core)* | **Yes**                                 |                                                                                                                 |
| WSL Ubuntu (16.04)  | *6.0.1 (Core)*      | **Yes**                                 |                                                                                                                 |


## <a name=todo>ToDo</a>

* Add cmdlet `Get-AZStorageBlob`.
* Update `Get-AZStorageContainer` to list all containers if no name is specified. 
* Update all `Get` and `Set` to handle cancellation better.
* More to be addded...


## <a name=version>Versions and Updates</a>
