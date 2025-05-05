using Microsoft.AspNetCore.Http;
using Mini_Moodle.Exceptions;
using Mini_Moodle.Models.Domain;

namespace Mini_Moodle.Repositories.SubmissonService.Interfaces
{
    public interface ISubmissionCommandRepository
    {
        Task<OperationResult<Submission>> CreateAsync(Submission userRequest, CancellationToken cancellationToken);
        Task<OperationResult<Submission>> UpdateAsync(Guid id, Submission userRequest, CancellationToken cancellationToken);
        Task<OperationResult<Guid>> DeleteAsync(Guid id, CancellationToken cancellationToken);
        Task<OperationResult<string?>> UploadFileAsync(Guid id, IFormFile file, CancellationToken cancellationToken);
    }
}
