using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Options;
using Social_Light_POST.Models.Config;
using Social_Light_POST.Services.IService;

namespace Social_Light_POST.Services
{
    public class AzureStorageService: IAzureStorageService
    {
        private readonly AzureStorageConfig _azureStorageConfig = new();
        private readonly BlobServiceClient blobServiceClient;

        public AzureStorageService(IOptions<AzureStorageConfig> options)
        {
            // _azureStorageConfig =options.Value;
            Console.WriteLine("options.Value.AccountName");
            Console.WriteLine(options.Value.AccountName);
            Console.WriteLine(options.Value.AccountKey);
            _azureStorageConfig.AccountName = "thesocialightstore";
            _azureStorageConfig.AccountKey = "Qkt+9Y/Mu01AlqIeUzeOxk7o+L4ysGDubErMv6ZykuKS31JYRfKr/RPa9Hq6a74gzOIfw5qzLZ94+AStxL8kOA==";
            var connectionString = $"DefaultEndpointsProtocol=https;AccountName={_azureStorageConfig.AccountName};AccountKey={_azureStorageConfig.AccountKey};EndpointSuffix=core.windows.net";
            blobServiceClient = new BlobServiceClient(connectionString);
        }


        public async Task<string> UploadFileToStorage(Stream stream, string containerName, string fileName)
        {
            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            await containerClient.CreateIfNotExistsAsync();
            var blobClient = containerClient.GetBlobClient(fileName);
            await blobClient.UploadAsync(stream, true);
            return blobClient.Uri.ToString();
        }
    }
}