using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Auth;

namespace Redeploy.Azure.Storage
{
    public class BlobStorageHelper
    {
        public CloudStorageAccount StorageAccount;

        public BlobStorageHelper(string storageAccountName, string storageAccountKey)
        {
            StorageAccount = new CloudStorageAccount(
                new StorageCredentials(storageAccountName, storageAccountKey), true);
        }

        public BlobStorageHelper(StorageCredentials storageCredentials)
        {
            StorageAccount = new CloudStorageAccount(storageCredentials, true);
        }
    }
}
