
using Microsoft.AspNetCore.Http;

namespace Mini_Moodle.Models.Dto.Lessons
{
    public class UploadVideoDto
    {
        public IFormFile file { get; set; }
    }
}
