
namespace Mini_Moodle.Models.Dto.Course;

public class CourseRequestToUpdateDto
{
    public string Title { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now.Date;
    public string CreatedBy { get; set; }
}
