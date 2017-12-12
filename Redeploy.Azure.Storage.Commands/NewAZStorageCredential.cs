using System;
using System.Management.Automation;
using Microsoft.WindowsAzure.Storage.Auth;
using Redeploy.Azure.Storage;

namespace Redeploy.Azure.Storage.Commands
{
    [OutputType(typeof(StorageCredentials))]
    [Cmdlet("New", "AZStorageCredential")]
    public class NewAZStorageCredential : Cmdlet
    {
        [ValidateNotNullOrEmpty]
        [Parameter(
            Mandatory = true,
            Position = 0,
            HelpMessage = "Name of the Storage Account")]
        [Alias("Name")]
        public string StorageAccountName 
        { 
            get { return _storageAccountName; }
            set { _storageAccountName = value; }
        }

        private string _storageAccountName;

        [ValidateNotNullOrEmpty]
        [Parameter(
            Mandatory = true,
            Position = 1,
            HelpMessage = "Key to the Storage Account")]
        [Alias("Key")]
        public string StorageAccountKey
        {
            get { return _storageAccountKey; }
            set { _storageAccountKey = value; }
        }

        private string _storageAccountKey;

        protected override void ProcessRecord()
        {
            WriteObject(BlobStorageHelper.StorageCredential(_storageAccountName, _storageAccountKey));
        }
    }
}