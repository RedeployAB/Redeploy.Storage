using System.Threading.Tasks;
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

        public CloudBlobContainer GetContainer(string containerName)
        {
            CloudBlobClient blobClient = StorageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);

            var exists = _containerExists(containerName).Result;

            if (!exists)
                container = null;

            return container;
        }

        public async Task<CloudBlob> UploadBlob(string containerName, string filePath, string blob)
        {

            CloudBlobClient blobClient = StorageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);

            CloudBlockBlob blockBlob = container.GetBlockBlobReference(blob);

            using (var fileStream = System.IO.File.OpenRead(filePath))
            {
                await blockBlob.UploadFromStreamAsync(fileStream);
            }

            CloudBlockBlob resultBlob = container.GetBlockBlobReference(blob);

            return resultBlob;
        }

        private async Task<bool> _containerExists(string containerName)
        {
            CloudBlobClient blobClient = StorageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);

            var result = await container.ExistsAsync();

            return result;
        }
    }
}
