
namespace Mini_Moodle.Models.Domain
{
    public class Course
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }

        public ICollection<Lesson> Lessons { get; set; }
    }
}
