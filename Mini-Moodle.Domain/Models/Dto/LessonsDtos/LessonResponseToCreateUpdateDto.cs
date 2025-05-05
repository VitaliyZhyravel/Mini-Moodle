
namespace Mini_Moodle.Models.Dto.Lessons;

public class LessonResponseToCreateUpdateDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string VideoUrl { get; set; }
    public string? Description { get; set; }
    public string CourseId { get; set; }
}
