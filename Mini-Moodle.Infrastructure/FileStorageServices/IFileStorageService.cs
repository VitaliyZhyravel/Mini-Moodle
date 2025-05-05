using Microsoft.AspNetCore.Http;

namespace Mini_Moodle.FileStorageServices
{
    public interface IFileStorageService
    {
        Task<string?> SaveFileAsync(string directory, IFormFile file, CancellationToken cancellationToken);
        string ConvertToUrl(string path);
    }
}
