
namespace Mini_Moodle.Models.Domain
{
    public class Grade
    {
        public Guid Id { get; set; }
        public double Score { get; set; }
        public string Feedback { get; set; }

        public Guid SubmissionId { get; set; }
        public Submission Submission { get; set; }
    }
}
