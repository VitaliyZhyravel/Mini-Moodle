using FluentValidation;

namespace Mini_Moodle.Repositories.LessonService.Commands.UpdateLesson
{
    public class LessonUpdateCommandValidator : AbstractValidator<LessonUpdateCommand>
    {
        public LessonUpdateCommandValidator()
        {
            RuleFor(x => x.userRequest.Title)
              .NotEmpty()
              .WithMessage("Title is required")
              .MaximumLength(100)
              .WithMessage("Title can contain no more than 100 characters");

            RuleFor(x => x.userRequest.Description)
               .NotEmpty()
               .WithMessage("Description is required")
               .MaximumLength(100)
               .WithMessage("Description can contain no more than 200 characters");

            RuleFor(x => x.userRequest.CourseId)
              .NotEmpty()
              .WithMessage("Course Id is required");

            RuleFor(x => x.userRequest.CourseId)
              .NotEmpty()
              .WithMessage("Lesson Id is required");
        }
    }
}
