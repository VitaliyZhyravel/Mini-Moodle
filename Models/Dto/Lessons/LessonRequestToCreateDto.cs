using Mini_Moodle.Models.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Mini_Moodle.Models.Dto.Lessons;

public class LessonRequestToCreateDto
{
    [Required(ErrorMessage = "{0} can't be empty")]
    [MaxLength(100, ErrorMessage = "{0} can contain no more than 100 characters")]
    public string Title { get; set; }

    [Required(ErrorMessage = "{0} can't be empty")]
    public IFormFile File { get; set; }

    [Required(ErrorMessage = "{0} can't be empty")]
    [MaxLength(200, ErrorMessage = "{0} can contain no more than 200 characters")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "{0} can't be empty")]
    [Display(Name = "Course id")]
    public Guid CourseId { get; set; }
}
