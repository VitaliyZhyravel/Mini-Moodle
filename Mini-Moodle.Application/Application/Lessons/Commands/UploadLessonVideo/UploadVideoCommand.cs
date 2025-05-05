using MediatR;
using Microsoft.AspNetCore.Http;
using Mini_Moodle.Exceptions;
using Mini_Moodle.Models.Dto.Lessons;

namespace Mini_Moodle.Repositories.LessonService.Commands.UploadLessonVideo
{
    public record UploadVideoCommand(Guid id ,IFormFile file) : IRequest<OperationResult<UploadVideoResponseDto>> { }
}
