using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Business.Abstraction;
using Microsoft.AspNetCore.Http;
using MyBlobInfo = Business.Models.MyBlobInfo;

namespace Business.Implementation.Services
{
    public class BlobService : IBlobService
    {
        private readonly BlobServiceClient _blobServiceClient;

        public BlobService(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }
        public async Task<MyBlobInfo> GetBlobAsync(string guid)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient("pages");
            var blobClient = containerClient.GetBlobClient(guid);
            var blobDownloadInfo =  await blobClient.DownloadAsync();
            return new MyBlobInfo(blobDownloadInfo.Value.Content, blobDownloadInfo.Value.ContentType);
        }

        public async Task<IEnumerable<string>> ListBlobsAsync()
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient("pages");
            var items = new List<string>();

            await foreach (var blobItem in containerClient.GetBlobsAsync())
            {
                items.Add(blobItem.Name);
            }

            return items;
        }
        public async Task UploadContentBlobAsync(IFormFile file, Guid guid)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient("pages");
            var blobClient = containerClient.GetBlobClient(guid.ToString());
            Stream imageStream = new MemoryStream();
            await file.CopyToAsync(imageStream);
            imageStream.Position = 0;
            await blobClient.UploadAsync(imageStream, new BlobHttpHeaders {ContentType = file.ContentType,ContentDisposition = file.ContentDisposition});

        }

        public async Task DeleteBlobAsync(string blobName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient("pages");
            var blobClient = containerClient.GetBlobClient(blobName);
            await blobClient.DeleteIfExistsAsync();
        }
    }
}