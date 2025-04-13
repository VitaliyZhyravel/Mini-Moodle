using Mini_Moodle.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace Mini_Moodle.Models.Dto;

public class CourseRequestToCreateDto
{
    [MaxLength(40)]
    public string Title { get; set; }
    [MaxLength(200)]
    public string? Description { get; set; }
}
