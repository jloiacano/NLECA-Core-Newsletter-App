using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using NLECA_Core_Newsletter_App.Service.Interfaces;

namespace NLECA_Core_Newsletter_App.Service.Services
{
    public class AzureStorageService : IAzureStorageService
    {

        private readonly ILogger<AzureStorageService> _logger;

        public AzureStorageService(
            ILogger<AzureStorageService> logger
            )
        {
            _logger = logger;
        }
        public CloudBlobContainer GetBlobContainer(string azureConnectionString, string containerName)
        {
            CloudStorageAccount storageAccount;
            CloudBlobClient blobClient;
            try
            {
                storageAccount = CloudStorageAccount.Parse(azureConnectionString);
                blobClient = storageAccount.CreateCloudBlobClient();
                return blobClient.GetContainerReference(containerName);
            }
            catch (System.Exception ex)
            {
                _logger.LogError("Error in AzureStorageService/GetBlobContainer" + azureConnectionString, ex);
                throw;
            }
        }
    }
}
