namespace Mini_Moodle.Models.Dto.Lessons
{
    public class UploadVideoResponseDto
    {
        public string Message { get; set; }
        public string VideoUrl { get; set; }
        public Guid LessonId { get; set; }
    }
}
