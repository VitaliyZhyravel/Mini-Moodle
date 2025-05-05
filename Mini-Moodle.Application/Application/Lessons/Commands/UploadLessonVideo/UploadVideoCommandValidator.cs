using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Mini_Moodle.Repositories.LessonService.Commands.UploadLessonVideo
{
    public class UploadVideoCommandValidator : AbstractValidator<UploadVideoCommand>
    {
        public UploadVideoCommandValidator()
        {
            RuleFor(x => x.file)
                .NotEmpty()
                .WithMessage("Video is required")
                .Must(ValidExtensionVideo)
                .WithMessage("Invalid video format. Supported formats: mp4, avi, mkv")
                .Must(ValidSize)
                .WithMessage("Video size is incorrect. Supported size is 100mb");
        }

        private static string[] ValidExtension = { ".mp4", ".avi", ".mkv" };

        private bool ValidExtensionVideo(IFormFile file)
        {
            var fileExtension = Path.GetExtension(file.FileName);

            return ValidExtension.Contains(fileExtension.ToLower());
        }
        private bool ValidSize(IFormFile file)
        {
            return file.Length <= 104857600;
        }
    }
}
