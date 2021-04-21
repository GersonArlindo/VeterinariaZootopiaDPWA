using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Services
{
    public interface IFileUploadService
    {
        Task<string> SaveFileOnDisk(IFormFile file, string fileName, string folderDestination);
        Task<string> SaveFileOnAWSS3(IFormFile file, string fileName, string bucketName);
    }
}
