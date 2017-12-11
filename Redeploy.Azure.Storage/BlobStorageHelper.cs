using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.RetryPolicies;

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
        /// <returns>CloudBlobContainer</returns>
        public CloudBlobContainer GetContainer(string containerName)
        {
            CloudBlobClient storageClient = _createBlobClient();
            CloudBlobContainer container = storageClient.GetContainerReference(containerName);

            bool exists;

            try 
            {
                exists = _containerExists(containerName).Result;
            }
            catch (System.AggregateException exception)
            {
                throw new System.AggregateException(exception);
            }
            

            if (!exists)
                container = null;

            return container;
        }
        
        /// <summary>
        /// Creates an Azure Storage Blob Container.
        /// </summary>
        /// <param name="containerName"></param>
        /// <returns>CloudBlobContainer</returns>
        public async Task<CloudBlobContainer> CreateContainer(string containerName)
        {
            CloudBlobClient storageClient = _createBlobClient();
            CloudBlobContainer container = storageClient.GetContainerReference(containerName);

            bool result;

            try 
            {
                result = await container.CreateIfNotExistsAsync();
            }
            catch (System.AggregateException exception)
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
        /// <returns>Task<CloudBlob></returns>
        public async Task<CloudBlob> UploadBlob(string containerName, string filePath, string blob)
        {
            CloudBlobClient storageClient = _createBlobClient();
            CloudBlobContainer container = storageClient.GetContainerReference(containerName);

            CloudBlockBlob blockBlob = container.GetBlockBlobReference(blob);

            CloudBlockBlob resultBlob;

            try 
            {
                using (var fileStream = System.IO.File.OpenRead(filePath))
                {
                    await blockBlob.UploadFromStreamAsync(fileStream);
                }
            }
            catch (System.IO.IOException exception)
            {
                throw new System.IO.IOException(exception.Message);
            }
            catch (System.AggregateException exception)
            {
                throw new System.AggregateException(exception);
            }

            resultBlob = container.GetBlockBlobReference(blob);

            return resultBlob;
        }

        /// <summary>
        /// Static method to create new Storage Account Credentials.
        /// </summary>
        /// <param name="storageAccountName"></param>
        /// <param name="storageAccountKey"></param>
        /// <returns>StorageCredentials</returns>
        public static StorageCredentials StorageCredential(string storageAccountName, string storageAccountKey)
        {
            return new StorageCredentials(storageAccountName, storageAccountKey);
        }

        /// <summary>
        /// Checks if a container exists.
        /// </summary>
        /// <param name="containerName"></param>
        /// <returns>Task<bool></returns>
        private async Task<bool> _containerExists(string containerName)
        {
            CloudBlobClient storageClient = _createBlobClient();
            CloudBlobContainer container = storageClient.GetContainerReference(containerName);

            bool result;

            try
            {
                result = await container.ExistsAsync();
            }
            catch (System.AggregateException exception)
            {
                throw new System.AggregateException(exception);
            }

            return result;
        }

        /// <summary>
        /// Private method to create Blob Client. This method is prepared to make
        /// optional settings easier.
        /// </summary>
        /// <returns>CloudBlobClient</returns>
        private CloudBlobClient _createBlobClient()
        {
            CloudBlobClient storageClient = StorageAccount.CreateCloudBlobClient();
            // Set options here.
            storageClient.DefaultRequestOptions = new BlobRequestOptions { ServerTimeout = new System.TimeSpan(0, 0, 2) };

            return storageClient;
        }
    }
}
