using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Portfolio.Application.Contracts.Infrastructure.ImageHandler
{
    public interface IImageHandlerService
    {
        Task<string> UploadImage(IFormFile file, string folder);
        Task RemoveFile(string path);
    }
}
