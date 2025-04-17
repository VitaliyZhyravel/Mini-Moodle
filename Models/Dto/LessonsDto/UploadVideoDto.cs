using Microsoft.AspNetCore.Http.Metadata;
using System.ComponentModel.DataAnnotations;

namespace Mini_Moodle.Models.Dto.Lessons
{
    public class UploadVideoDto
    {
        [Required]
        public IFormFile file { get; set; }
    }
}
