using Mini_Moodle.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace Mini_Moodle.Models.Dto.GradeDto;

public class GradeResponseDto
{
    public double Score { get; set; }
    public string Feedback { get; set; }
}
