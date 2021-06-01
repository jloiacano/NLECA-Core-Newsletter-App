using Microsoft.WindowsAzure.Storage.Blob;

namespace NLECA_Core_Newsletter_App.Service.Interfaces
{
    public interface IAzureStorageService
    {
        CloudBlobContainer GetBlobContainer(string azureConnectionString, string containerName);
    }
}
