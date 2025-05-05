using FluentValidation;
using Mini_Moodle.Repositories.AccountService.Command.Login;
using System.ComponentModel.DataAnnotations;

namespace Mini_Moodle.Repositories.AssignmentsService.Commands.CreateAssignment
{
    public class AssignmentsCreateCommandValidator : AbstractValidator<AssignmentsCreateCommand>
    {
        public AssignmentsCreateCommandValidator()
        {
            RuleFor(x => x.userRequest.Title)
                .NotEmpty()
                .WithMessage("Title is required")
                .MaximumLength(100)
                .WithMessage("Name can contain no more than 100 characters"); 

            RuleFor(x => x.userRequest.Description)
                .NotEmpty()
                .WithMessage("Description is required")
                .MaximumLength(1000)
                .WithMessage("Description can contain no more than 1000 characters");

            RuleFor(x => x.userRequest.DifficultyId)
                .NotEmpty()
                 .WithMessage("Difficulty Id is required")
                .NotEqual(Guid.Empty)
                .WithMessage("Difficulty Id can't be empty");

            RuleFor(x => x.userRequest.LessonId)
                .NotEmpty()
                 .WithMessage("Lesson Id is required")
                .NotEqual(Guid.Empty)
                .WithMessage("Lesson Id can't be empty");
        }
    }
}
