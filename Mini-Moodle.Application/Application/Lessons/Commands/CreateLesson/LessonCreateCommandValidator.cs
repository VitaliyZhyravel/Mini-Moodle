using FluentValidation;

namespace Mini_Moodle.Repositories.LessonService.Commands.CreateLesson;

public class LessonCreateCommandValidator : AbstractValidator<LessonCreateCommand>
{
    public LessonCreateCommandValidator()
    {
            RuleFor(x => x.request.Title)
            .NotEmpty()
            .WithMessage("Title is required")
            .MaximumLength(100)
            .WithMessage("Title can contain no more than 100 characters");

        RuleFor(x => x.request.Description)
           .NotEmpty()
           .WithMessage("Description is required")
           .MaximumLength(100)
           .WithMessage("Description can contain no more than 200 characters");

        RuleFor(x => x.request.CourseId)
          .NotEmpty()
          .WithMessage("Course Id is required");
    }
}

