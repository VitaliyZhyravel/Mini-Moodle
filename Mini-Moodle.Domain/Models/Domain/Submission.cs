using Mini_Moodle.Models.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mini_Moodle.Models.Domain
{
    public class Submission
    {
        public Guid Id { get; set; }
        public DateTime DateSubmitted { get; set; }
        public string? ProjectPath { get; set; }
        public string? Comment { get; set; }
        public bool IsLate { get; set; }
        public double TotalGrade { get; set; } = 0.00;

        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }

        public Guid AssignmentId { get; set; }
        public Assignment Assignment { get; set; }

        public Grade? Grade { get; set; }
    }
}
