using Mini_Moodle.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace Mini_Moodle.Models.Dto
{
    public class CourseResponseDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }

        public List<Lesson>? Lessons { get; set; }
    }
}
