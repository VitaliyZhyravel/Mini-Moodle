using FluentValidation;

namespace Mini_Moodle.Repositories.SubmissionService.Commands.CreateSubmisson;

public class SubmissionCreateCommandValidator : AbstractValidator<SubmissionCreateCommand>
{
    public SubmissionCreateCommandValidator()
    {
        RuleFor(x => x.userRequest.AssignmentId)
            .NotEmpty()
            .WithMessage("Assignment id is required");

        RuleFor(x => x.userRequest.Comment)
            .MaximumLength(200)
            .WithMessage("Comment can be maximum of 200 characters");
    }
}