using Microsoft.AspNetCore.Http;
using Mini_Moodle.Exceptions;
using Mini_Moodle.Models.Domain;

namespace Mini_Moodle.Repositories.LessonService.Interfaces;

public interface ILessonCommandRepository
{
    Task<OperationResult<Lesson>> CreateAsync(Lesson userRequest, CancellationToken cancellationToken);
    Task<OperationResult<Lesson>> UpdateAsync(Guid id, Lesson userRequest, CancellationToken cancellationToken);
    Task<OperationResult<Guid>> DeleteAsync(Guid id, CancellationToken cancellationToken);
    Task<OperationResult<string?>> UploadFileAsync(Guid id, IFormFile file, CancellationToken cancellationToken);
}
