using FluentValidation;

namespace Mini_Moodle.Repositories.GradeService.CreateGrade
{
    public class GradeCreateCommandValidator : AbstractValidator<GradeCreateCommand>
    {
        public GradeCreateCommandValidator()
        {
            RuleFor(x => x.userRequest.Score)
             .NotEmpty()
             .WithMessage("Grade is required")
             .InclusiveBetween(0, 100)
             .WithMessage("Grade must be between 0 and 100");

            RuleFor(x => x.userRequest.Feedback)
                .NotEmpty()
                .WithMessage("Feedback is required")
                .MaximumLength(100)
                .WithMessage("Feedback can contain no more than 100 characters");

            RuleFor(x => x.userRequest.SubmissionId)
               .NotEmpty()
               .WithMessage("SubmissionId is required");
        }
    }
}
