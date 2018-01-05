﻿# AZStorage.Netcore

## <a name=intro>Introduction</a>

This module was created to make up for the lack of an official module for handling of Azure Storage Accounts with PowerShell Core
(not the resources in ARM, but rather uploading blobs, getting containers etc).

To achieve this it uses the library: `WindowsAzure.Storage` (v8.6.0).

The cmdlets in this module works similar to the cmdlets in the official `Azure.Storage` module. They
will mostly have the same parameters, structure and functionality as their counterparts in that module. 
At the moment only a couple of cmdlets have been added. But more will be added as needed.

The module consists of the projects found in (that project and this one wil be consolidated further on):

[Redeploy.Azure]()

**Content**

* [Introduction](#intro)
* [Installation](#install)
* [Cmdlets](#cmdlets)
* [Usage Examples](#usage)
* [Compatability](#compatability)
* [Versions and Updates](#version)

This project will be discontinued the day Microsoft releases an official module for this.

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

| Operating System | PowerShell Edition | Compatible |
|------------------|--------------------|------------|
| **Windows 10**   | *5.1*              | **Yes**    |

## <a name=version>Versions and Updates</a>