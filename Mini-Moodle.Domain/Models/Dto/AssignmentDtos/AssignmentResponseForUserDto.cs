using Mini_Moodle.Models.Domain;
using Mini_Moodle.Models.Dto.SubmissionDtos;
using System.ComponentModel.DataAnnotations;

namespace Mini_Moodle.Models.Dto.AssignmentDtos
{
    public class AssignmentResponseForUserDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DeadLine { get; set; }
    }
}
