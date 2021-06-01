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

        private IConfiguration _configuration;
        private string DevConncetionString { get; }

        public AzureStorageService(
            ILogger<AzureStorageService> logger
            , IConfiguration configuration
            )
        {
            _logger = logger;
            _configuration = configuration;
            DevConncetionString = configuration["DevStorageConnectionString"];
        }
        public CloudBlobContainer GetBlobContainer(string azureConnectionString, string containerName)
        {
            var storageAccount = CloudStorageAccount.Parse(azureConnectionString);
            var blobClient = storageAccount.CreateCloudBlobClient();
            return blobClient.GetContainerReference(containerName);
        }
    }
}
