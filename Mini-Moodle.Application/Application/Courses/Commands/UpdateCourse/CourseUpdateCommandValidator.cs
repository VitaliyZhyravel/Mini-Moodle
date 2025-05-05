using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace Mini_Moodle.Repositories.CourseService.Commands.UpdateCourse
{
    public class CourseUpdateCommandValidator : AbstractValidator<CourseUpdateCommand>
    {
        public CourseUpdateCommandValidator()
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

            RuleFor(x => x.request.CreatedBy)
            .NotEmpty()
            .WithMessage("CreatedBy is required")
            .MaximumLength(40)
            .WithMessage("CreatedBy can contain no more than 40 characters");

        }
    }
}
