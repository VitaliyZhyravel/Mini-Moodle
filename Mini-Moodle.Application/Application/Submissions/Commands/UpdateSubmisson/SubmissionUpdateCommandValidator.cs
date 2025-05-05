using FluentValidation;

namespace Mini_Moodle.Repositories.SubmissionService.Commands.UpdateSubmisson;

public class SubmissionUpdateCommandValidator : AbstractValidator<SubmissionUpdateCommand>
{
    public SubmissionUpdateCommandValidator()
    {
        RuleFor(x => x.userRequest.Comment)
        .NotEmpty()
        .WithMessage("Comment is required");
    }
}
