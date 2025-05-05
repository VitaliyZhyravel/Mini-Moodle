
namespace Mini_Moodle.Models.Dto.AssignmentDto;

public class AssignmentRequestDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public Guid DifficultyId { get; set; }
    public Guid LessonId { get; set; }
}
