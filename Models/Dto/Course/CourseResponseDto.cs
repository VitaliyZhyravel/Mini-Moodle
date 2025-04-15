using Mini_Moodle.Models.Domain;

namespace Mini_Moodle.Models.Dto.Course;

public class CourseResponseDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; }

    public List<Lesson>? Lessons { get; set; }
}
