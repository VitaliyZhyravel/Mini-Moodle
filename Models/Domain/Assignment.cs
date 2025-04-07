
namespace Mini_Moodle.Models.Domain
{
    public class Assignment
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime DeadLine{ get; set; }

        public Guid LessonId { get; set; }
        public Lesson Lesson { get; set; }
    }
}
