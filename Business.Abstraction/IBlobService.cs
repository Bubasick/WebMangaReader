using Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Business.Abstraction
{
    public interface IBlobService
    {
        Task<MyBlobInfo> GetBlobAsync(string guid);
        Task<IEnumerable<String>> ListBlobsAsync();
        Task UploadContentBlobAsync(IFormFile content, Guid guid);
        Task DeleteBlobAsync(string blobName);
    }
}
