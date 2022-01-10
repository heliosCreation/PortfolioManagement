using Portfolio.Application.Models.Files;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Portfolio.Application.Contracts.FileManagement
{
    public interface IFileStorageService
    {
        Task<UrslDto> UploadAsync(ICollection<FileDto> files, string baseFolder, string folderName);
        Task<UrslDto> UploadSingleAsync(FileDto file, string baseFolder, string folderName);
        Task DeleteFileIfExistAsync(string containerName, string fileFullName);  
    }
}
