using Mini_Moodle.Models.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Mini_Moodle.Models.Dto.Lessons
{
    public class LessonResponseToCreateUpdateDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string VideoUrl { get; set; }
        public string? Description { get; set; }
    }
}
