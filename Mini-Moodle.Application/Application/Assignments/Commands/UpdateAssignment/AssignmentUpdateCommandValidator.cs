using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace Mini_Moodle.Repositories.AssignmentsService.Commands.UpdateAssignment;

public class AssignmentUpdateCommandValidator : AbstractValidator<AssignmentUpdateCommand>
{
    public AssignmentUpdateCommandValidator()
    {
        RuleFor(x => x.userRequest.Title)
        .NotEmpty()
        .WithMessage("Title is required")
        .MaximumLength(1000)
        .WithMessage("Title can contain no more than 100 characters");

        RuleFor(x => x.userRequest.Description)
         .NotEmpty()
         .WithMessage("Description is required")
         .MaximumLength(1000)
         .WithMessage("Description can contain no more than 1000 characters");

        RuleFor(x => x.userRequest.AddMinutesToDeadLine)
            .NotEmpty()
            .WithMessage("AddMinutesToDeadLine is required");
    }
}

