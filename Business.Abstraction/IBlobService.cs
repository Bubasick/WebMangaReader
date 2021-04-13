using Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Abstraction
{
    public interface IBlobService
    {
        Task<BlobInfo> GetBlobAsync(string name);
        Task<IEnumerable<String>> ListBlobsAsync();
        Task UploadBlobFileAsync(string filePath, string fileName);
        Task UploadContentBlobAsync(string content, string fileName);
        Task DeleteBlobAsync(string blobName);
    }
}
