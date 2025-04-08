
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mini_Moodle.Models.Domain
{
    public class Grade
    {
        public Guid Id { get; set; }
        public double Score { get; set; }
        [MaxLength(100)]
        public string Feedback { get; set; }

        public Guid SubmissionId { get; set; }
        public Submission Submission { get; set; }
    }
}
