
using System.ComponentModel.DataAnnotations;

namespace Mini_Moodle.Models.Domain
{
    public class Assignment
    {
        public Guid Id { get; set; }
        [MaxLength(100)]
        public string Title { get; set; }
        [MaxLength(1000)]
        public string Description { get; set; }
        public DateTime DeadLine{ get; set; }

        public Guid LessonId { get; set; }
        public Lesson Lesson { get; set; }

        public Guid DifficultyId { get; set; }
        public Difficulty? Difficulty { get; set; }

        public List<Submission>? Submissions { get; set; }
    }
}
