using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Portfolio.Application.Contracts.Infrastructure.ImageHandler;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Portfolio.Infrastructure.ImageHandler
{
    public class ImageHandlerService : IImageHandlerService
    {
        private readonly IWebHostEnvironment _env;

        public ImageHandlerService(IWebHostEnvironment env)
        {
            _env = env ?? throw new ArgumentNullException(nameof(env));
        }

        public async Task RemoveFile(string path)
        {
            if (!String.IsNullOrEmpty(path))
            {
                //delete the image from disk. 
                var rootPath = _env.WebRootPath;
                var fullPath = (rootPath + path);

                if (File.Exists(fullPath))
                {
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    await Task.Run(() => File.Delete(fullPath));
                }
            }
        }

        public async Task<string> UploadImage(IFormFile file, string folder)
        {
            var destinationDirectory = Path.Combine("MockBlobStorage/img", folder);
            var fullPath = Path.Combine(_env.WebRootPath, destinationDirectory);
            var fileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            var dbPath = Path.Combine(destinationDirectory, fileName);

            EnsureFolder(fullPath);
            var streamPath = Path.Combine(fullPath, fileName);

            using var stream = new FileStream(streamPath, FileMode.Create, FileAccess.Write);
            await file.CopyToAsync(stream);

            return "/" + dbPath;
        }

        private void EnsureFolder(string path)
        {
            if (!Directory.Exists(path))
            {
                // Create all directories on the path that don't already exist
                var x = Directory.CreateDirectory(path);
            }
        }
    }

}
