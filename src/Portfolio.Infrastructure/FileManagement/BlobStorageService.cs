using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Portfolio.Application.Contracts.FileManagement;
using Portfolio.Application.Models.Files;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Portfolio.Infrastructure.FileManagement
{
    public class BlobStorageService : IFileStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;

        public BlobStorageService(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient ?? throw new ArgumentNullException(nameof(blobServiceClient));
        }

        public async Task DeleteFileIfExistAsync(string containerName, string fileFullName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            var fileName = fileFullName.Split(containerName + "/")[1];
            var blobClient = containerClient.GetBlobClient(fileName);
            var result = await blobClient.DeleteIfExistsAsync();
        }

        public async Task<UrslDto> UploadAsync(ICollection<FileDto> files, string baseFolder, string folderName)
        {
            if (files == null || files.Count == 0)
            {
                return null;
            }

            var containerClient = _blobServiceClient.GetBlobContainerClient("publicupload");

            var urls = new List<string>();

            foreach (var file in files)
            {
                var blobClient = containerClient.GetBlobClient(file.GetPathWithFileName(baseFolder,folderName));
                await blobClient.UploadAsync(file.Content, new BlobHttpHeaders { ContentType = file.ContentType });
                urls.Add(blobClient.Uri.ToString());
            }

            return new UrslDto(urls);
        }

        public async Task<UrslDto> UploadSingleAsync(FileDto file, string baseFolder, string folderName)
        {
            if (file == null)
            {
                return null;
            }

            var containerClient = _blobServiceClient.GetBlobContainerClient("privateuploads");

            var urls = new List<string>();

            var blobClient = containerClient.GetBlobClient(file.GetPathWithFileName(baseFolder, folderName));
            await blobClient.UploadAsync(file.Content, new BlobHttpHeaders { ContentType = file.ContentType });
            urls.Add(blobClient.Uri.ToString());

            return new UrslDto(urls);
        }
    }
}
