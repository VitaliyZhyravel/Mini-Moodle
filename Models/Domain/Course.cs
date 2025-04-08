
using System.ComponentModel.DataAnnotations;

namespace Mini_Moodle.Models.Domain
{
    public class Course
    {
        public Guid Id { get; set; }
        [MaxLength(40)]
        public string Title { get; set; }
        [MaxLength(200)]
        public string? Description { get; set; }

        public List<Lesson> Lessons { get; set; }
    }
}
