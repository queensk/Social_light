using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Social_light_Frontend.Services.AzureFileUpload
{
    public interface IAzureStorageService
    {
        Task<string> UploadFileToStorage(Stream stream, string containerName, string fileName);
    }
}