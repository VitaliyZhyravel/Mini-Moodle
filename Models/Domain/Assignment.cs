
using System.ComponentModel.DataAnnotations;

namespace Mini_Moodle.Models.Domain
{
    public class Assignment
    {
        public Guid Id { get; set; }
        [MaxLength(40)]
        public string Title { get; set; }
        [MaxLength(200)]
        public string? Description { get; set; }
        public DateTime DeadLine{ get; set; }

        public Guid LessonId { get; set; }
        public Lesson Lesson { get; set; }
    }
}
