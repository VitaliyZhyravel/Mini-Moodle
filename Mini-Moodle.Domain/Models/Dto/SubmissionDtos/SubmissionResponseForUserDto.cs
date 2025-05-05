using Mini_Moodle.Models.Domain;
using Mini_Moodle.Models.Dto.AssignmentDtos;
using Mini_Moodle.Models.Dto.GradeDto;

namespace Mini_Moodle.Models.Dto.SubmissionDtos
{
    public class SubmissionResponseForUserDto
    {
        public DateTime DateSubmitted { get; set; }
        public string? ProjectPath { get; set; }
        public string? Comment { get; set; }
        public bool IsLate { get; set; }

        public GradeResponseDto Grade { get; set; }
        public AssignmentResponseForUserDto Assignment { get; set; }

    }
}
