using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Mini_Moodle.Repositories.SubmissionService.Commands.UploadVideoSubmisson
{
    public class SubmissionUploadFilesCommandValidator : AbstractValidator<SubmissionUploadFilesCommand>
    {
        public SubmissionUploadFilesCommandValidator()
        {
                RuleFor(x => x.id)
                .NotEmpty()
                .WithMessage("Submission id is required");

            RuleFor(x => x.userFile.Files)
                .NotEmpty()
                .WithMessage("File is required")
                .Must(ValidExtensionFile)
                .WithMessage("Invalid file format. Supported formats: mp4, avi, mkv")
                .Must(ValidSize)
                .WithMessage("File size is incorrect. Supported size is 100mb");
        }
        private static string[] ValidExtension = { ".mp4", ".avi", ".mkv" };

        private bool ValidExtensionFile(IFormFile file)
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
