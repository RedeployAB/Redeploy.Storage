using System;
using System.Management.Automation;
using Microsoft.WindowsAzure.Storage.Blob; 
using Microsoft.WindowsAzure.Storage.Auth;
using Redeploy.Azure.Storage;

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
            HelpMessage = "A StorageCredential object. Create one with 'New-AZStorageCredential'."
        )]
        public StorageCredentials StorageCredential
        {
            get { return _storageCredential; }
            set { _storageCredential = value; }
        }

        private StorageCredentials _storageCredential;

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
            StorageCredentials credential = _storageCredential;
            BlobStorageHelper storageHelper = new BlobStorageHelper(credential);
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
