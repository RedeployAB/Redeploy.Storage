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
        public StorageContext Context
        {
            get { return _context; }
            set { _context = value; }
        }

        private StorageContext _context;

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
            StorageContext context = _context;
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
