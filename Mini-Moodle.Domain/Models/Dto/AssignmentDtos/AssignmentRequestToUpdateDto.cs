
namespace Mini_Moodle.Models.Dto.AssignmentDtos;

public class AssignmentRequestToUpdateDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public double AddMinutesToDeadLine { get; set; }
}
