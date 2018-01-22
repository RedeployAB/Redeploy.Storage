using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.RetryPolicies;
using Redeploy.Azure.Storage.Models;

namespace Redeploy.Azure.Storage.Blob
{
    /// <summary>
    /// Class that helps with Azure Blob Storage actions.
    /// </summary>
    public class BlobHelper
    {
        /// <summary>
        /// Storage Account context with credentials and URIs.
        /// </summary>
        public StorageContext StorageContext;

        /// <summary>
        /// Constructor. Takes a StorageContext.
        /// </summary>
        /// <param name="storageContext"></param>
        public BlobHelper(StorageContext storageContext)
        {
            StorageContext = storageContext;
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
                    //await blockBlob.UploadFromStreamAsync(fileStream, fileStream.Length);
                    var requestOptions = new BlobRequestOptions { ServerTimeout = new System.TimeSpan(0, 30, 0), RetryPolicy = new LinearRetry(System.TimeSpan.FromMilliseconds(1000), 3) };
                    var operationContext = new OperationContext();
                    await blockBlob.UploadFromStreamAsync(fileStream, fileStream.Length, null, requestOptions, operationContext);
                }
                
                //await blockBlob.UploadFromFileAsync(filePath);
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
        /// Checks if a blob exists.
        /// </summary>
        /// <param name="containerName"></param>
        /// <param name="blob"></param>
        /// <returns>Task<bool></returns>
        public async Task<bool> BlobExists(string containerName, string blob)
        {

            CloudBlobClient storageClient = _createBlobClient();
            CloudBlobContainer container = storageClient.GetContainerReference(containerName);

            CloudBlockBlob blockBlob = container.GetBlockBlobReference(blob);

            bool result;

            try
            {
                result = await blockBlob.ExistsAsync();
            }
            catch (System.AggregateException exception)
            {
                throw new System.AggregateException(exception);
            }

            return result;
        }

        /// <summary>
        /// Private methot to create Blob Client. This method is prepared to make
        /// optional settings easier.
        /// </summary>
        /// <returns>CloudBlobClient</returns>
        private CloudBlobClient _createBlobClient()
        {
            CloudBlobClient storageClient = StorageContext.StorageAccount.CreateCloudBlobClient();
            // Set options here.
            storageClient.DefaultRequestOptions = new BlobRequestOptions { 
                ServerTimeout = new System.TimeSpan(0, 0, 10),
                RetryPolicy = new LinearRetry(System.TimeSpan.FromMilliseconds(500), 3) };

            /*storageClient.DefaultRequestOptions = new BlobRequestOptions { 
                ServerTimeout = new System.TimeSpan(0, 0, 10),
                RetryPolicy = new LinearRetry(System.TimeSpan.FromMilliseconds(500), 3) };*/

            return storageClient;
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
    }
}