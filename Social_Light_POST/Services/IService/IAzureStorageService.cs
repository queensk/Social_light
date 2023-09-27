using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Social_Light_POST.Services.IService
{
    public interface IAzureStorageService
    {
        Task<string> UploadFileToStorage(Stream stream, string containerName, string fileName);
    }
}