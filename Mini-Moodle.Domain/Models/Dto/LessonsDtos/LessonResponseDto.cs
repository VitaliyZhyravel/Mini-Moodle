using Mini_Moodle.Models.Dto.AssignmentDto;

namespace Mini_Moodle.Models.Dto.Lessons
{
    public class LessonResponseDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string? VideoUrl { get; set; }
        public string? Description { get; set; }

        public List<AssignmentResponseDto> Assignments { get; set; }
    }
}

