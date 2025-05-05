
namespace Mini_Moodle.Models.Dto.Lessons;

public class LessonRequestToCreateDto
{
    public string Title { get; set; }
    public string? Description { get; set; }
    public Guid CourseId { get; set; }
}
