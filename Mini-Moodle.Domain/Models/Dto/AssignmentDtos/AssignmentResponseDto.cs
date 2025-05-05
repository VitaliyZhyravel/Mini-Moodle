using Mini_Moodle.Models.Dto.DifficultyDtos;

namespace Mini_Moodle.Models.Dto.AssignmentDto;

public class AssignmentResponseDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DeadLine { get; set; }
    public DifficultyResponseDto Difficulty { get; set; }
}
