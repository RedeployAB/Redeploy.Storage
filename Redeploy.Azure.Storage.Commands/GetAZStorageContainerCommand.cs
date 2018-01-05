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
        [ValidateNotNullOrEmpty]
        [Parameter(
            Mandatory = true,
            Position = 0,
            HelpMessage = "Name of Container in Storage Account"
        )]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        private string _name;
        
        [ValidateNotNull]
        [Parameter(
            Mandatory = true,
            Position = 1,
            HelpMessage = "A StorageContext object. Create one with 'New-AZStorageContext'."
        )]
        public StorageContext Context
        {
            get { return _context; }
            set { _context = value; }
        }
        private StorageContext _context;

        protected override void ProcessRecord()
        {
            StorageContext context = _context;
            BlobHelper storageHelper = new BlobHelper(context);
            CloudBlobContainer container;

            try
            {
                container = storageHelper.GetContainer(_name);
            }
            catch (System.AggregateException exception)
            {
                throw new System.AggregateException(exception.InnerException.GetBaseException().Message);
            }

            WriteObject(container);
        }
    }
}
