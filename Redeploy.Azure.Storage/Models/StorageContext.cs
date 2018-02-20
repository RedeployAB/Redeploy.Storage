using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using System;

namespace Redeploy.Azure.Storage.Models
{
    public class StorageContext
    {
        public StorageContext(string storageAccountName, string storageAccountKey)
        {
            StorageAccount = new CloudStorageAccount(
                new StorageCredentials(storageAccountName, storageAccountKey), true);

            StorageAccountName = storageAccountName;
            BlobEndpoint = StorageAccount.BlobEndpoint;
            TableEndPoint = StorageAccount.TableEndpoint;
            QueueEndpoint = StorageAccount.QueueEndpoint;
            FileEndPoint = StorageAccount.FileEndpoint;
        }

        public StorageContext(StorageCredentials credentials)
        {
            StorageAccount = new CloudStorageAccount(credentials, true);
        }

        public string StorageAccountName { get; }
        public Uri BlobEndpoint { get; }        
        public Uri TableEndPoint { get; }
        public Uri QueueEndpoint { get; }
        public Uri FileEndPoint { get; }
        public CloudStorageAccount StorageAccount { get; set; }

        public string EndPointSuffix { get; } = "core.windows.net";
    }
}