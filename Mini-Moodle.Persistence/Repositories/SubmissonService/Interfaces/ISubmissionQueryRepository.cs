using Mini_Moodle.Exceptions;
using Mini_Moodle.Models.Domain;

namespace Mini_Moodle.Repositories.SubmissonService.Interfaces
{
    public interface ISubmissionQueryRepository 
    {
        Task<OperationResult<Submission?>> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
