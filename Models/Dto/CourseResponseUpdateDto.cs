namespace Mini_Moodle.Models.Dto
{
    public class CourseResponseUpdateDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
