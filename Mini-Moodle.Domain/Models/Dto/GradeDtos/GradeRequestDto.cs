
namespace Mini_Moodle.Models.Dto.GradeDto;

public class GradeRequestDto
{
    public double Score { get; set; }
    public string Feedback { get; set; }
    public Guid SubmissionId { get; set; }
}
