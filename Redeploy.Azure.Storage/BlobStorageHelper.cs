using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Auth;

namespace Redeploy.Azure.Storage
{
    /// <summary>
    /// Class that helps with Azure Blob Storage actions.
    /// </summary>
    public class BlobStorageHelper
    {
        /// <summary>
        /// Storage Account object with credentials set.
        /// </summary>
        public CloudStorageAccount StorageAccount;

        /// <summary>
        /// Constructor. Takes Storage Account name and Storage Account key.
        /// </summary>
        /// <param name="storageAccountName"></param>
        /// <param name="storageAccountKey"></param>
        public BlobStorageHelper(string storageAccountName, string storageAccountKey)
        {
            StorageAccount = new CloudStorageAccount(
                new StorageCredentials(storageAccountName, storageAccountKey), true);
        }

        /// <summary>
        /// Constructor. Takes a StorageCredentials object.
        /// </summary>
        /// <param name="storageCredentials"></param>
        public BlobStorageHelper(StorageCredentials storageCredentials)
        {
            StorageAccount = new CloudStorageAccount(storageCredentials, true);
        }

        /// <summary>
        /// Gets a container from an Azure Storage Account. Returns null if it does not exist.
        /// </summary>
        /// <param name="containerName"></param>
        /// <returns></returns>
        public CloudBlobContainer GetContainer(string containerName)
        {
            CloudBlobClient storageClient = StorageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = storageClient.GetContainerReference(containerName);

            var exists = _containerExists(containerName).Result;

            if (!exists)
                container = null;

            return container;
        }
        
        /// <summary>
        /// Creates an Azure Storage Blob Container.
        /// </summary>
        /// <param name="containerName"></param>
        /// <returns></returns>
        public async Task<CloudBlobContainer> CreateContainer(string containerName)
        {
            CloudBlobClient storageClient = StorageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = storageClient.GetContainerReference(containerName);

            bool result;

            try 
            {
                result = await container.CreateIfNotExistsAsync();
            }
            catch (StorageException exception)
            {
                throw new System.AggregateException(exception);
            }
            

            if (result == true) 
            {
                return storageClient.GetContainerReference(containerName);
            } 
            else 
            {
                return null;
            }
        }

        /// <summary>
        /// Uploads a blob to an Azure Blob Storage.
        /// </summary>
        /// <param name="containerName"></param>
        /// <param name="filePath"></param>
        /// <param name="blob"></param>
        /// <returns></returns>
        public async Task<CloudBlob> UploadBlob(string containerName, string filePath, string blob)
        {

            CloudBlobClient storageClient = StorageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = storageClient.GetContainerReference(containerName);

            CloudBlockBlob blockBlob = container.GetBlockBlobReference(blob);

            using (var fileStream = System.IO.File.OpenRead(filePath))
            {
                await blockBlob.UploadFromStreamAsync(fileStream);
            }

            CloudBlockBlob resultBlob = container.GetBlockBlobReference(blob);

            return resultBlob;
        }

        /// <summary>
        /// Checks if a container exists.
        /// </summary>
        /// <param name="containerName"></param>
        /// <returns></returns>
        private async Task<bool> _containerExists(string containerName)
        {
            CloudBlobClient storageClient = StorageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = storageClient.GetContainerReference(containerName);

            var result = await container.ExistsAsync();

            return result;
        }

        /// <summary>
        /// Static method to create new Storage Account Credentials.
        /// </summary>
        /// <param name="storageAccountName"></param>
        /// <param name="storageAccountKey"></param>
        /// <returns></returns>
        public static StorageCredentials NewStorageCredential(string storageAccountName, string storageAccountKey)
        {
            return new StorageCredentials(storageAccountName, storageAccountKey);
        }
    }
}
