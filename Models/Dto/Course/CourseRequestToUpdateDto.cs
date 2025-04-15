using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Mini_Moodle.Models.Dto.Course
{
    public class CourseRequestToUpdateDto
    {
        [Required(ErrorMessage = "{0} can't be empty")]
        [MaxLength(100, ErrorMessage = "{0} can contain no more than 100 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "{0} can't be empty")]
        [MaxLength(200, ErrorMessage = "{0} can contain no more than 200 characters")]
        public string? Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; } = DateTime.Now.Date;

        [Required(ErrorMessage = "{0} can't be empty")]
        [Display(Name = "Created by")]
        [MaxLength(40, ErrorMessage = "{0} can contain no more than 40 characters")]
        public string CreatedBy { get; set; }
    }
}
