using System;
using System.Management.Automation;
using Microsoft.WindowsAzure.Storage.Blob;
using Redeploy.Azure.Storage.Models;
using Redeploy.Azure.Storage.Blob;

namespace Redeploy.Azure.Storage.Commands
{
    [OutputType(typeof(CloudBlobContainer))]
    [Cmdlet("Get", "AZStorageContainer")]
    public class GetAZStorageContainerCommand : Cmdlet
    {
        [ValidateNotNull]
        [Parameter(
            Mandatory = true,
            Position = 0,
            HelpMessage = "A StorageContext object. Create one with 'New-AZStorageContext'."
        )]
        public StorageContext StorageContext
        {
            get { return _storageContext; }
            set { _storageContext = value; }
        }

        private StorageContext _storageContext;

        [ValidateNotNullOrEmpty]
        [Parameter(
            Mandatory = true,
            Position = 1,
            HelpMessage = "Name of Container in Storage Account"
        )]
        public string ContainerName
        {
            get { return _containerName; }
            set { _containerName = value; }
        }

        private string _containerName;

        protected override void ProcessRecord()
        {
            StorageContext context = _storageContext;
            BlobHelper storageHelper = new BlobHelper(context);
            CloudBlobContainer container;

            try
            {
                container = storageHelper.GetContainer(_containerName);
            }
            catch (System.AggregateException exception)
            {
                throw new System.AggregateException(exception);
            }

            WriteObject(container);
        }
    }
}
