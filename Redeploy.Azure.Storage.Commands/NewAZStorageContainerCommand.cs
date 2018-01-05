using System;
using System.Management.Automation;
using Microsoft.WindowsAzure.Storage.Blob;
using Redeploy.Azure.Storage.Models;
using Redeploy.Azure.Storage.Blob;

namespace Redeploy.Azure.Storage.Commands
{
    [OutputType(typeof(CloudBlobContainer))]
    [Cmdlet("New","AZStorageContainer")]
    public class NewAZStorageContainerCommand : Cmdlet
    {
        [ValidateNotNull]
        [Parameter(
            Mandatory = true,
            Position = 0,
            HelpMessage = "Name of Container"
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
            StorageContext context = Context;
            BlobHelper storageHelper = new BlobHelper(context);

            CloudBlobContainer container;
            
            try
            {
                container = storageHelper.CreateContainer(Name).Result;
            }
            catch (System.AggregateException exception)
            {
                throw new System.AggregateException(exception.InnerException.GetBaseException().Message);
            }

            if (container == null)
            {
                throw new HaltCommandException("A container with that name already exists.");
            }

            WriteObject(container);
        }
    }
}