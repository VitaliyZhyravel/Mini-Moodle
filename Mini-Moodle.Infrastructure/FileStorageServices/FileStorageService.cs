
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Reflection.Metadata.Ecma335;

namespace Mini_Moodle.FileStorageServices
{
    public class FileStorageService  : IFileStorageService
    {
        private readonly IHttpContextAccessor httpContext;

        public FileStorageService(IHttpContextAccessor httpContext)
        {
            this.httpContext = httpContext;
        }

        public async Task<string?> SaveFileAsync(string directory, IFormFile file, CancellationToken cancellationToken)
        {
            string videosDir = Path.Combine(Directory.GetCurrentDirectory(), directory);
            if (!Directory.Exists(videosDir))
            {
                var info = Directory.CreateDirectory(videosDir);
            }

            var extension = Path.GetExtension(file.FileName);
            var fileName = $"{Guid.NewGuid()}";
            string path = Path.Combine(videosDir, $"{fileName}{extension}");
            try
            {
                using (FileStream fileStream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream, cancellationToken);
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException()
                {
                    Source = "FileStorageService",
                    Data = { { "Error", ex.Message } }
                };
            }
            return path;
        }

        public string ConvertToUrl(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("Path cannot be null or empty.", nameof(path));
            }

            if (!File.Exists(path))
            {
                throw new ArgumentException("Path cannot be null or empty.");
            }

            // Знаходимо, де закінчується "wwwroot" і починається потрібна частина
            var wwwrootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

            if (!path.StartsWith(wwwrootPath, StringComparison.InvariantCultureIgnoreCase))             
            {
                throw new InvalidOperationException("Path does not start with wwwroot directory.");
            }

            // Вирізаємо wwwroot
            var relativePath = path.Substring(wwwrootPath.Length).Replace('\\', '/');

            // Переконаємося, що починається з /
            if (!relativePath.StartsWith("/"))
            {
                relativePath = "/" + relativePath;
            }

            var requestHost = httpContext.HttpContext?.Request;

            return $"{relativePath}";
        }
    }
}
