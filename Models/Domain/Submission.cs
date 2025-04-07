using Mini_Moodle.Models.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mini_Moodle.Models.Domain
{
    public class Submission
    {
        public Guid Id { get; set; }
        public DateTime DateSubmitted { get; set; }
        public string ProjectWorkPath { get; set; }
        [NotMapped]
        public IFormFile File { get; set; }

        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }

        public Guid AssignmentId { get; set; }
        public Assignment Assignment { get; set; }
    }
}
