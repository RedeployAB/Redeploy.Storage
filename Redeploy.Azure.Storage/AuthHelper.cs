using Microsoft.WindowsAzure.Storage.Auth;

namespace Redeploy.Azure.Storage
{
    public class AuthHelper
    {
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
    }
}