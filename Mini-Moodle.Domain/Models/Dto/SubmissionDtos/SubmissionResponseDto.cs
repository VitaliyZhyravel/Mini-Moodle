using Mini_Moodle.Models.Domain;
using Mini_Moodle.Models.Dto.GradeDto;
using Mini_Moodle.Models.Identity;

namespace Mini_Moodle.Models.Dto.SubmissionDtos
{
    public class SubmissionResponseDto
    {
        public Guid Id { get; set; }
        public DateTime DateSubmitted { get; set; }
        public string? ProjectPath { get; set; }
        public string? Comment { get; set; }
        public bool IsLate { get; set; }
        public GradeResponseDto Grade { get; set; }
    }
}
