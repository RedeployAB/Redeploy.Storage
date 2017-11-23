using System;
using System.Management.Automation;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Redeploy.Azure.Storage.Commands
{
    [OutputType(typeof(CloudBlobContainer))]
    [Cmdlet("Get", "AZStorageContainer")]
    public class GetAZStorageContainerCommand : Cmdlet
    {
    }
}
