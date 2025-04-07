using System.ComponentModel.DataAnnotations.Schema;

namespace Mini_Moodle.Models.Domain
{
    public class Lesson
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string VideoUrl { get; set; }
        [NotMapped]
        public IFormFile File { get; set; }
        public string? Description { get; set; }

        public Guid CourseId { get; set; }
        public Course Course { get; set; }

        public ICollection<Assignment> Assignments { get; set; }
    }
}
