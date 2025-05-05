using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mini_Moodle.Models.Domain
{
    public class Lesson
    {
        public Guid Id { get; set; }
        [MaxLength(100)]
        public string Title { get; set; }
        [MaxLength(100)]
        public string? VideoUrl { get; set; }
       
        [MaxLength(200)]
        public string Description { get; set; }

        public Guid CourseId { get; set; }
        public Course Course { get; set; }

        public List<Assignment> Assignments { get; set; }
    }
}
